using Dapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NetFlow.Application.Common.Utils
{
    public static class DevExtremeSqlBuilder
    {
        public static (string Sql, DynamicParameters Params) Compile(string filterJson)
        {
            var p = new DynamicParameters();
            int i = 0;

            var token = JToken.Parse(filterJson);   // 🔥 string → JSON

            string Build(JToken f)
            {
                if (f is JArray arr && arr.Count == 3 && arr[0].Type == JTokenType.String)
                {
                    string field = arr[0].ToString();
                    string op = arr[1].ToString();
                    var val = arr[2];

                    string param = "@p" + i++;
                    p.Add(param, val.ToObject<object>());

                    return op switch
                    {
                        "contains" => $"{field} LIKE '%' + {param} + '%'",
                        "=" => $"{field} = {param}",
                        ">" => $"{field} > {param}",
                        "<" => $"{field} < {param}",
                        ">=" => $"{field} >= {param}",
                        "<=" => $"{field} <= {param}",
                        _ => throw new NotSupportedException(op)
                    };
                }
                if (f is JArray logic)
                {
                    var parts = new List<string>();

                    for (int i = 0; i < logic.Count; i++)
                    {
                        if (logic[i].Type == JTokenType.String)
                        {
                            parts.Add(logic[i].ToString().ToUpper());
                        }
                        else
                        {
                            parts.Add(Build(logic[i]));
                        }
                    }

                    return "(" + string.Join(" ", parts) + ")";
                }

                throw new NotSupportedException("Invalid DevExtreme filter");
            }

            string sql = Build(token);
            return (sql, p);
        }

        public static string BuildOrderBy(string? sortJson, string defaultOrder)
        {
            if (string.IsNullOrEmpty(sortJson))
                return defaultOrder;

            var arr = JArray.Parse(sortJson);
            var orders = new List<string>();

            foreach (var s in arr)
            {
                string field = s["selector"]!.ToString();
                bool desc = s["desc"]?.ToObject<bool>() ?? false;
                orders.Add($"{field} {(desc ? "DESC" : "ASC")}");
            }

            return string.Join(", ", orders);
        }
    }
}
}
