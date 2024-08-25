using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace UserAnalyticsService.Models
{
    public class BankDeposit : GenericDocument
    {
        public string OwnerId { get; set; }

        public List<InvestmentDetail>? InvestmentDetails { get; set; } = new List<InvestmentDetail>();

        public decimal? TotalPrincipalValue
        {
            get
            {
                decimal totalPrincipal = 0;
                if (InvestmentDetails != null)
                {
                    foreach (var detail in InvestmentDetails)
                    {
                        totalPrincipal += detail.PrincipalAmount;
                    }
                }
                return totalPrincipal;
            }
        }

        public decimal? CurrentPrincipalValue { get; set; }
    }

    public class InvestmentDetail
    {
        public int FDId { get; set; }

        public string BankName { get; set; }

        public string AccountNumber { get; set; }

        public decimal PrincipalAmount { get; set; }

        public double InterestRate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime MaturityDate { get; set; }

        public decimal InterestEarned { get; set; }

        public decimal MaturityAmount { get; set; }

        public string FDType { get; set; } // e.g., cumulative, non-cumulative

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StartDate { get; set; }
    }
}
