using MongoDB.Bson.Serialization.Attributes;

namespace StockAnalysis.Web.Models
{
    public class BankDeposit :GenericDocument
    {
        public string? Name { get; set; }

        public string? Owner { get; set; }

        public List<InvestmentDetail>? InvestmentDetails { get; set; } = new List<InvestmentDetail>();

        // Automatically computed total principal value
        public decimal? TotalPrincipalValue { get; set; }

        // Automatically computed total maturity amount
        public decimal? TotalMaturityAmount { get; set; }
    }
    public class InvestmentDetail
    {
        public int? FDId { get; set; }

        // Enum type for BankName
        public BankNameEnum? BankName { get; set; }

        public string? AccountNumber { get; set; }

        public decimal PrincipalAmount { get; set; }

        public double InterestRate { get; set; }

        // Automatically computed maturity date based on start date and duration
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? MaturityDate { get; set; }

        // Number of days, months, or years for which the FD is made
        public int Duration { get; set; }

        // Enum to select if duration is in days, months, or years
        public DurationType DurationType { get; set; }

        // Enum to specify if the FD is cumulative or non-cumulative
        public FDType FDType { get; set; }

        public decimal? InterestEarned { get; set; }

        // Automatically computed maturity amount
        public decimal? MaturityAmount { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StartDate { get; set; }
    }
    public enum DurationType
    {
        Days,
        Months,
        Years
    }

    // Enum for FD type
    public enum FDType
    {
        Cumulative,
        NonCumulative
    }
}