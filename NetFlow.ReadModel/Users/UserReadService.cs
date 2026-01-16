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

            if (!string.IsNullOrEmpty(pagedRequest.filter))
            {
                var (sql, p) = DevExtremeSqlBuilder.Compile(pagedRequest.filter);
                parameters.AddDynamicParams(p);
            }

            string orderBy = DevExtremeSqlBuilder.BuildOrderBy(pagedRequest.sort, "usr.Id DESC");

            string countSql = @"
                SELECT COUNT(1)
                FROM [User] usr WITH (NOLOCK)
            ";

            string dataSql = $@"
                SELECT
                    usr.Id,
                    usr.FirstName,
                    usr.LastName,
                    usr.Email,
                    usr.Phone,
                    usr.Password,
                    usr.Active,
                    STUFF((
                        SELECT ', ' + role.Name
                        FROM [UserInRole] userRole
                        INNER JOIN [Role] role ON role.Id = userRole.RoleId
                        WHERE userRole.UserId = usr.Id
                        FOR XML PATH(''), TYPE
                    ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS Roles
                FROM [User] usr WITH (NOLOCK)
                ORDER BY {orderBy}
                OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
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
                        SELECT ', ' + r.Name
                        FROM [UserInRole] ur
                        INNER JOIN [Role] r ON r.Id = ur.RoleId
                        WHERE ur.UserId = usr.Id
                        FOR XML PATH(''), TYPE
                    ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS Roles
                FROM [User] usr WITH (NOLOCK)
                WHERE usr.Id = @Id;

                SELECT
                    RoleId
                FROM [UserInRole] WITH (NOLOCK)
                WHERE UserId = @Id;
            ";

            using var cn = new SqlConnection(_opt.ConnectionString);
            using var multi = await cn.QueryMultipleAsync(sql, new { Id = id });

            var user = await multi.ReadFirstOrDefaultAsync<UserDto>();
            if (user == null)
                return null;

            user.RoleIds = (await multi.ReadAsync<int>()).ToList();
            return user;
        }


    }
}
