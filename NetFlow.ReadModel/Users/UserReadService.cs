using Dapper;
using Microsoft.Data.SqlClient;
using NetFlow.Application.Common.Pagination;
using NetFlow.Application.Common.Utils;
using NetFlow.ReadModel.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel.Users
{
    public class UserReadService
    {
        private readonly ReadModelOptions _opt;
        public UserReadService(ReadModelOptions opt) => _opt = opt;

        public async Task<PagedResult> ListAsync(PagedRequest pagedRequest)
        {
            using var cn = new SqlConnection(_opt.ConnectionString);
            var parameters = new DynamicParameters();

            string whereSql = "";

            // 🔹 Filter
            if (!string.IsNullOrEmpty(pagedRequest.filter))
            {
                var (sql, p) = DevExtremeSqlBuilder.Compile(pagedRequest.filter);
                whereSql = " WHERE " + sql;
                parameters.AddDynamicParams(p);
            }

   
            string orderBy = DevExtremeSqlBuilder.BuildOrderBy(
                pagedRequest.sort,
                "usr.Id DESC"
            );


            string countSql = $@"
                SELECT COUNT(1)
                FROM [User] usr WITH (NOLOCK)
                {whereSql}
            ";

            int totalCount = await cn.ExecuteScalarAsync<int>(countSql, parameters);

            if (pagedRequest.isCountQuery == true)
            {
                return new PagedResult
                {
                    data = Array.Empty<UserDto>(),
                    totalCount = totalCount
                };
            }

            parameters.Add("@Skip", pagedRequest.skip ?? 0);
            parameters.Add("@Take", pagedRequest.take ?? 10);

           
            string dataSql = $@"
                 SELECT
                     usr.Id,
                     usr.FirstName,
                     usr.LastName,
                     usr.Email,
                     usr.Phone,
                     usr.Active,

                     -- 🔹 Roles (firmadan bağımsız, kullanıcı bazlı)
                     STUFF((
                         SELECT DISTINCT ', ' + r.Name
                         FROM [UserInFirm] uf
                         INNER JOIN [Role] r ON r.Id = uf.RoleId
                         WHERE uf.UserId = usr.Id
                         FOR XML PATH(''), TYPE
                     ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS Roles,

                     -- 🔹 Firms
                     STUFF((
                         SELECT DISTINCT ', ' + f.FirmName
                         FROM [UserInFirm] uf
                         INNER JOIN [Firm] f ON f.Id = uf.FirmId
                         WHERE uf.UserId = usr.Id
                         FOR XML PATH(''), TYPE
                     ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS Firms

                 FROM [User] usr WITH (NOLOCK)
                 {whereSql}
                 ORDER BY {orderBy}
                 OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
             ";

            var data = (await cn.QueryAsync<UserDto>(dataSql, parameters)).ToList();

            return new PagedResult
            {
                data = data,
                totalCount = totalCount
            };
        }


        public async Task<UserDto?> GetAsync(int id)
        {
            const string sql = @"
        SELECT TOP (1)
            usr.Id,
            usr.FirstName,
            usr.LastName,
            usr.Email,
            usr.Phone,
            usr.Password,
            usr.Active,

            
            STUFF((
                SELECT DISTINCT ', ' + r.Name
                FROM [UserInFirm] uf
                INNER JOIN [Role] r ON r.Id = uf.RoleId
                WHERE uf.UserId = usr.Id
                FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS Roles,

           
            STUFF((
                SELECT DISTINCT ', ' + f.FirmName
                FROM [UserInFirm] uf
                INNER JOIN [Firm] f ON f.Id = uf.FirmId
                WHERE uf.UserId = usr.Id
                FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS Firms

        FROM [User] usr WITH (NOLOCK)
        WHERE usr.Id = @Id;

   
        SELECT DISTINCT
            uf.RoleId
        FROM [UserInFirm] uf WITH (NOLOCK)
        WHERE uf.UserId = @Id;


        SELECT DISTINCT
            uf.FirmId
        FROM [UserInFirm] uf WITH (NOLOCK)
        WHERE uf.UserId = @Id;
    ";

            using var cn = new SqlConnection(_opt.ConnectionString);
            using var multi = await cn.QueryMultipleAsync(sql, new { Id = id });

            var user = await multi.ReadFirstOrDefaultAsync<UserDto>();
            if (user == null)
                return null;

            user.RoleIds = (await multi.ReadAsync<int>()).ToList();
            user.FirmIds = (await multi.ReadAsync<int>()).ToList();

            return user;
        }


    }
}
