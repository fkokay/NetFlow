using Azure.Core;
using Dapper;
using NetFlow.Application.Common.DevExtreme;
using NetFlow.Application.Netsis.AccountingVouchers;
using NetFlow.Domain.Common.Pagination;
using NetFlow.Domain.Netsis.AccountingVouchers;
using NetFlow.Domain.Netsis.Orders;
using NetFlow.Infrastructure.Common;
using NetFlow.Netsis.Connection;
using NetFlow.Netsis.Dto;
using NetFlow.Netsis.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Netsis.Repositories
{
    public class NetsisAccountingVoucherReadRepository : IAccountingVoucherReadRepository
    {
        private readonly ISqlProvider _sql;
        private readonly NetsisConnectionFactory _factory;
        Dictionary<string, string> fieldMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["YearCode"] = "YIL_KODU",
            ["MonthCode"] = "AY_KODU",
            ["VoucherNo"] = "FISNO",
            ["LineNo"] = "SIRA",
            ["AccountCode"] = "HES_KOD",
            ["AccountName"] = "HESAPISMI",
            ["TransactionDate"] = "TARIH",
            ["DocumentDate"] = "EVRAKTARIHI",
            ["DebitCredit"] = "BA",
            ["Amount"] = "TUTAR",
            ["Quantity"] = "MIKTAR",
            ["Description1"] = "ACIKLAMA",
            ["Description2"] = "ACIKLAMA2",
            ["ReferenceCode"] = "REF_KOD",
            ["IntegrationReference"] = "ENTEGREFKEY",
            ["CurrencyType"] = "DOVIZTIP",
            ["CurrencyAmount"] = "DOVIZTUT",
            ["CompanyCurrencyType"] = "FIRMADOVTIP",
            ["CompanyCurrencyAmount"] = "FIRMADOVTUT",
            ["ProjectCode"] = "PROJE_KODU",
            ["BranchCode"] = "SUBE_KODU",
            ["IsBranchBased"] = "SUBELI",
            ["RowGuid"] = "GUID",
            ["TransactionSequence"] = "ISLEMSIRANO",
            ["CreatedBy"] = "KAYITYAPANKUL",
            ["CreatedDate"] = "KAYITTARIHI",
            ["UpdatedBy"] = "DUZELTMEYAPANKUL",
            ["UpdatedDate"] = "DUZELTMETARIHI"
        };

        public NetsisAccountingVoucherReadRepository(ISqlProvider sql, NetsisConnectionFactory factory)
        {
            _sql = sql;
            _factory = factory;

        }
        public async Task<PagedResult> GetAccountingVouchers(string accountCode, PagedRequest request)
        {
            using var con = _factory.Create();

            var sql = _sql.Get("AccountingVouchers.sql");
            var sqlCount = _sql.Get("AccountingVouchersCount.sql");

            string whereSql = "WHERE MUHFIS.HES_KOD = @HES_KOD";
            var parameters = new DynamicParameters();
            parameters.Add("@HES_KOD", accountCode);

            if (!string.IsNullOrEmpty(request.Filter))
            {
                var (filteSql, p) = DevExtremeSqlBuilder.Compile(request.Filter, fieldMap);
                whereSql += " AND " + filteSql;
                parameters.AddDynamicParams(p);
            }

            string orderBy = DevExtremeSqlBuilder.BuildOrderBy(request.Sort, "ORDER BY MUHFIS.TARIH DESC", fieldMap);
            parameters.Add("@Skip", request.Skip ?? 0);
            parameters.Add("@Take", request.Take ?? 10);

            string dataSql = $@"
                {sql}
                {whereSql}
                {orderBy}
                OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
            ";

            string countSql = $@"
                {sqlCount}
                {whereSql}
            ";

            int totalCount = con.ExecuteScalar<int>(
                countSql, parameters
            );

            if (request.IsCountQuery != null && request.IsCountQuery.Value)
            {
                return new PagedResult
                {
                    totalCount = totalCount,
                    data = Array.Empty<AccountingVoucher>(),
                };
            }

            var dto = con.Query<MuhFisDto>(dataSql, parameters).ToList();

            return new PagedResult
            {
                totalCount = totalCount,
                data = dto.Select(NetsisUtils.FixAllStrings).Select(x =>
                      AccountingVoucher.Create(
                            x.YIL_KODU,
                            x.AY_KODU,
                            x.FISNO,
                            x.SIRA,
                            x.HES_KOD,
                            x.HESAPISMI,
                            x.TARIH,
                            x.EVRAKTARIHI,
                            (DebitCreditType)x.BA,
                            x.TUTAR,
                            x.MIKTAR,
                            x.ACIKLAMA,
                            x.ACIKLAMA2,
                            x.REF_KOD,
                            x.ENTEGREFKEY,
                            x.DOVIZTIP,
                            x.DOVIZTUT,
                            x.FIRMADOVTIP,
                            x.FIRMADOVTUT,
                            x.PROJE_KODU,
                            x.SUBE_KODU,
                            x.SUBELI,
                            x.GUID,
                            x.ISLEMSIRANO,
                            x.KAYITYAPANKUL,
                            x.KAYITTARIHI,
                            x.DUZELTMEYAPANKUL,
                            x.DUZELTMETARIHI

                      )).ToList()
            };
        }
    }
}
