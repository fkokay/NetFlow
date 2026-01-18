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

    public class AccountingVoucher
    {
        public int YearCode { get;  }              // YIL_KODU
        public int MonthCode { get;  }             // AY_KODU

        public string VoucherNo { get;  }           // FISNO
        public int LineNo { get;  }                 // SIRA

        public string AccountCode { get;  }         // HES_KOD
        public string AccountName { get;  }         // HESAPISMI

        public DateTime TransactionDate { get;  }  // TARIH
        public DateTime? DocumentDate { get;  }      // EVRAKTARIHI

        public DebitCreditType DebitCredit { get;  } // BA
        public decimal Amount { get;  }             // TUTAR
        public decimal Quantity { get;  }           // MIKTAR

        public string Description1 { get;  }        // ACIKLAMA
        public string Description2 { get;  }        // ACIKLAMA2

        public string ReferenceCode { get;  }       // REF_KOD
        public string IntegrationReference { get;  } // ENTEGREFKEY

        public byte? CurrencyType { get;  }           // DOVIZTIP
        public decimal CurrencyAmount { get;  }     // DOVIZTUT

        public byte? CompanyCurrencyType { get;  }    // FIRMADOVTIP
        public decimal CompanyCurrencyAmount { get;  } // FIRMADOVTUT

        public string ProjectCode { get;  }          // PROJE_KODU
        public short BranchCode { get;  }           // SUBE_KODU
        public char? IsBranchBased { get;  }          // SUBELI

        public string RowGuid { get;  }                // GUID
        public int? TransactionSequence { get;  }    // ISLEMSIRANO

        public string CreatedBy { get;  }            // KAYITYAPANKUL
        public DateTime CreatedDate { get;  }        // KAYITTARIHI

        public string UpdatedBy { get;  }            // DUZELTMEYAPANKUL
        public DateTime? UpdatedDate { get;  }       // DUZELTMETARIHI

        public AccountingVoucher(int yearCode, int monthCode, string voucherNo, int lineNo, string accountCode, string accountName, DateTime transactionDate, DateTime? documentDate, DebitCreditType debitCredit, decimal amount, decimal quantity, string description1, string description2, string referenceCode, string integrationReference, byte? currencyType, decimal currencyAmount, byte? companyCurrencyType, decimal companyCurrencyAmount, string projectCode, short branchCode, char? isBranchBased, string rowGuid, int? transactionSequence, string createdBy, DateTime createdDate, string updatedBy, DateTime? updatedDate)
        {
            YearCode = yearCode;
            MonthCode = monthCode;
            VoucherNo = voucherNo;
            LineNo = lineNo;
            AccountCode = accountCode;
            AccountName = accountName;
            TransactionDate = transactionDate;
            DocumentDate = documentDate;
            DebitCredit = debitCredit;
            Amount = amount;
            Quantity = quantity;
            Description1 = description1;
            Description2 = description2;
            ReferenceCode = referenceCode;
            IntegrationReference = integrationReference;
            CurrencyType = currencyType;
            CurrencyAmount = currencyAmount;
            CompanyCurrencyType = companyCurrencyType;
            CompanyCurrencyAmount = companyCurrencyAmount;
            ProjectCode = projectCode;
            BranchCode = branchCode;
            IsBranchBased = isBranchBased;
            RowGuid = rowGuid;
            TransactionSequence = transactionSequence;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            UpdatedBy = updatedBy;
            UpdatedDate = updatedDate;
        }

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
