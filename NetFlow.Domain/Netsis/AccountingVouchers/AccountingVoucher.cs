using NetFlow.Domain.Netsis.Customers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace NetFlow.Domain.Netsis.AccountingVouchers
{
    public enum DebitCreditType
    {
        Debit = 0,   // Borç
        Credit = 1   // Alacak
    }

    public class AccountingVoucher(int yearCode, int monthCode, string voucherNo, int lineNo, string accountCode, string accountName, DateTime transactionDate, DateTime? documentDate, DebitCreditType debitCredit, decimal amount, decimal quantity, string description1, string description2, string referenceCode, string integrationReference, byte? currencyType, decimal currencyAmount, byte? companyCurrencyType, decimal companyCurrencyAmount, string projectCode, short branchCode, char? isBranchBased, string rowGuid, int? transactionSequence, string createdBy, DateTime createdDate, string updatedBy, DateTime? updatedDate)
    {
        public int YearCode { get; } = yearCode;
        public int MonthCode { get; } = monthCode;

        public string VoucherNo { get; } = voucherNo;
        public int LineNo { get; } = lineNo;

        public string AccountCode { get; } = accountCode;
        public string AccountName { get; } = accountName;

        public DateTime TransactionDate { get; } = transactionDate;
        public DateTime? DocumentDate { get; } = documentDate;

        public DebitCreditType DebitCredit { get; } = debitCredit;
        public decimal Amount { get; } = amount;
        public decimal Quantity { get; } = quantity;

        public string Description1 { get; } = description1;
        public string Description2 { get; } = description2;

        public string ReferenceCode { get; } = referenceCode;
        public string IntegrationReference { get; } = integrationReference;

        public byte? CurrencyType { get; } = currencyType;
        public decimal CurrencyAmount { get; } = currencyAmount;

        public byte? CompanyCurrencyType { get; } = companyCurrencyType;
        public decimal CompanyCurrencyAmount { get; } = companyCurrencyAmount;

        public string ProjectCode { get; } = projectCode;
        public short BranchCode { get; } = branchCode;
        public char? IsBranchBased { get; } = isBranchBased;

        public string RowGuid { get; } = rowGuid;
        public int? TransactionSequence { get; } = transactionSequence;

        public string CreatedBy { get; } = createdBy;
        public DateTime CreatedDate { get; } = createdDate;

        public string UpdatedBy { get; } = updatedBy;
        public DateTime? UpdatedDate { get; } = updatedDate;

        public static AccountingVoucher Create(
            int yearCode,
            int monthCode,
            string voucherNo,
            int lineNo,
            string accountCode,
            string accountName,
            DateTime transactionDate,
            DateTime? documentDate,
            DebitCreditType debitCredit,
            decimal amount,
            decimal quantity,
            string description1,
            string description2,
            string referenceCode,
            string integrationReference,
            byte? currencyType,
            decimal currencyAmount,
            byte? companyCurrencyType,
            decimal companyCurrencyAmount,
            string projectCode,
            short branchCode,
            char? isBranchBased,
            string rowGuid,
            int? transactionSequence,
            string createdBy,
            DateTime createdDate,
            string updatedBy,
            DateTime? updatedDate)
        {

            if (string.IsNullOrWhiteSpace(voucherNo))
                throw new InvalidVoucherNoException();
            return new AccountingVoucher(
                yearCode,
                monthCode,
                voucherNo,
                lineNo,
                accountCode,
                accountName,
                transactionDate,
                documentDate,
                debitCredit,
                amount,
                quantity,
                description1,
                description2,
                referenceCode,
                integrationReference,
                currencyType,
                currencyAmount,
                companyCurrencyType,
                companyCurrencyAmount,
                projectCode,
                branchCode,
                isBranchBased,
                rowGuid,
                transactionSequence,
                createdBy,
                createdDate,
                updatedBy,
                updatedDate);
        }


    }
}
