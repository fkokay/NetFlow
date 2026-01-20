using Dapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NetFlow.Application.Common.DevExtreme
{
    public static class DevExtremeSqlBuilder
    {
        public static (string Sql, DynamicParameters Params) Compile(string filterJson, Dictionary<string, string>? fieldMap = null)
        {
            if (string.IsNullOrWhiteSpace(filterJson))
                return ("1=1", new DynamicParameters());

            var parameters = new DynamicParameters();
            int index = 0;

            var token = JToken.Parse(filterJson);

            string ResolveColumn(string field)
            {
                // 🔹 fieldMap varsa ve eşleşme varsa onu kullan
                if (fieldMap != null &&
                    fieldMap.Count > 0 &&
                    fieldMap.TryGetValue(field, out var mapped))
                {
                    return mapped;
                }

                // 🔹 Aksi halde alan adını olduğu gibi kullan
                return field;
            }

            string Build(JToken f)
            {
                // 🔹 Basit filtre: ["Field", "op", value]
                if (f is JArray arr && arr.Count == 3 && arr[0].Type == JTokenType.String)
                {
                    string field = arr[0]!.ToString();
                    string op = arr[1]!.ToString().ToLowerInvariant();
                    var valueToken = arr[2];

                    string dbColumn = ResolveColumn(field);

                    string param = "@p" + index++;
                    object? value = valueToken.Type == JTokenType.Null
                        ? null
                        : valueToken.ToObject<object>();

                    parameters.Add(param, value);

                    return op switch
                    {
                        "contains" => $"{dbColumn} LIKE '%' + {param} + '%'",
                        "startswith" => $"{dbColumn} LIKE {param} + '%'",
                        "endswith" => $"{dbColumn} LIKE '%' + {param}",

                        "=" => $"{dbColumn} = {param}",
                        "<>" => $"{dbColumn} <> {param}",
                        ">" => $"{dbColumn} > {param}",
                        "<" => $"{dbColumn} < {param}",
                        ">=" => $"{dbColumn} >= {param}",
                        "<=" => $"{dbColumn} <= {param}",

                        _ => throw new NotSupportedException($"Operator not supported: {op}")
                    };
                }

                // 🔹 Mantıksal grup: [cond, "and"/"or", cond, ...]
                if (f is JArray logic)
                {
                    var parts = new List<string>();

                    foreach (var item in logic)
                    {
                        if (item.Type == JTokenType.String)
                        {
                            var keyword = item.ToString().ToUpperInvariant();
                            if (keyword != "AND" && keyword != "OR")
                                throw new NotSupportedException($"Invalid logical operator: {keyword}");

                            parts.Add(keyword);
                        }
                        else
                        {
                            parts.Add(Build(item));
                        }
                    }

                    return "(" + string.Join(" ", parts) + ")";
                }

                throw new NotSupportedException("Invalid DevExtreme filter format");
            }

            return (Build(token), parameters);
        }

        public static string BuildOrderBy(string? sortJson, string defaultOrder, Dictionary<string, string>? fieldMap = null)
        {
            if (string.IsNullOrWhiteSpace(sortJson))
                return defaultOrder;

            JArray arr;
            try
            {
                arr = JArray.Parse(sortJson);
            }
            catch
            {
                return defaultOrder;
            }

            string ResolveColumn(string field)
            {
                if (fieldMap != null &&
                    fieldMap.Count > 0 &&
                    fieldMap.TryGetValue(field, out var mapped))
                {
                    return mapped;
                }
                return field;
            }

            var orders = new List<string>();

            foreach (var s in arr)
            {
                var field = s["selector"]?.ToString();
                if (string.IsNullOrWhiteSpace(field))
                    continue;

                bool desc = s["desc"]?.ToObject<bool>() ?? false;

                var column = ResolveColumn(field);
                orders.Add($"{column} {(desc ? "DESC" : "ASC")}");
            }

            return orders.Count > 0
                ? "ORDER BY " + string.Join(", ", orders)
                : defaultOrder;
        }

        public static (string Sql, string[] Aliases) BuildSummary(string summaryJson, Dictionary<string, string>? fieldMap = null)
        {
            if (string.IsNullOrWhiteSpace(summaryJson))
                throw new ArgumentNullException(nameof(summaryJson));

            var summaries =
                JsonSerializer.Deserialize<List<DevExtremeSummaryInfo>>(summaryJson);

            if (summaries == null || summaries.Count == 0)
                throw new InvalidOperationException("Summary definition is empty.");

            string ResolveColumn(string field)
            {
                if (fieldMap != null &&
                    fieldMap.Count > 0 &&
                    fieldMap.TryGetValue(field, out var mapped))
                {
                    return mapped;
                }

                return field;
            }

            var selectParts = new List<string>();
            var aliases = new List<string>();

            int i = 0;
            foreach (var s in summaries)
            {
                var column = ResolveColumn(s.Selector);
                var alias = $"S{i++}";

                string sqlPart = s.SummaryType.ToLowerInvariant() switch
                {
                    "sum" => $"SUM({column}) AS {alias}",
                    "count" => $"COUNT({column}) AS {alias}",
                    "min" => $"MIN({column}) AS {alias}",
                    "max" => $"MAX({column}) AS {alias}",
                    "avg" => $"AVG({column}) AS {alias}",
                    _ => throw new NotSupportedException(
                            $"SummaryType not supported: {s.SummaryType}")
                };

                selectParts.Add(sqlPart);
                aliases.Add(alias);
            }

            return (string.Join(", ", selectParts), aliases.ToArray());
        }
    }

}
