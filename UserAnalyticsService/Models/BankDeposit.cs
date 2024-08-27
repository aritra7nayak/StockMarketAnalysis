using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace UserAnalyticsService.Models
{
    public class BankDeposit : GenericDocument
    {
        public string? Name { get; set; }
        public string? Owner { get; set; }

        public List<InvestmentDetail>? InvestmentDetails { get; set; } = new List<InvestmentDetail>();

        // Automatically computed total principal value
        public decimal? TotalPrincipalValue { get; set; }

        // Automatically computed total maturity amount
        public decimal? TotalMaturityAmount { get; set; }

        // Method to update total values
        public void UpdateValues()
        {
            TotalPrincipalValue = CalculateTotalPrincipalValue();
            TotalMaturityAmount = CalculateTotalMaturityAmount();
        }

        private decimal? CalculateTotalPrincipalValue()
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

        private decimal? CalculateTotalMaturityAmount()
        {
            decimal? totalMaturity = 0;
            if (InvestmentDetails != null)
            {
                foreach (var detail in InvestmentDetails)
                {
                    totalMaturity += detail.MaturityAmount;
                }
            }
            return totalMaturity;
        }
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
        public DateTime? MaturityDate { get; private set; }

        // Number of days, months, or years for which the FD is made
        public int Duration { get; set; }

        // Enum to select if duration is in days, months, or years
        public DurationType DurationType { get; set; }

        // Enum to specify if the FD is cumulative or non-cumulative
        public FDType FDType { get; set; }

        public decimal? InterestEarned { get; private set; }

        // Automatically computed maturity amount
        public decimal? MaturityAmount { get; private set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StartDate { get; set; }

        // Method to calculate maturity details
        public void UpdateMaturityDetails()
        {
            CalculateMaturityDate();
            CalculateMaturityAmount();
        }

        private void CalculateMaturityDate()
        {
            MaturityDate = StartDate;

            switch (DurationType)
            {
                case DurationType.Days:
                    MaturityDate = StartDate.AddDays(Duration);
                    break;
                case DurationType.Months:
                    MaturityDate = StartDate.AddMonths(Duration);
                    break;
                case DurationType.Years:
                    MaturityDate = StartDate.AddYears(Duration);
                    break;
            }
        }

        private void CalculateMaturityAmount()
        {
            if (FDType == FDType.Cumulative && DurationType == DurationType.Years)
            {
                // Formula for compound interest (cumulative FD)
                MaturityAmount = PrincipalAmount * (decimal)Math.Pow(1 + InterestRate / 100, Duration);
            }
            else
            {
                // Simple interest (non-cumulative FD)
                InterestEarned = PrincipalAmount * (decimal)(InterestRate / 100) * Duration;
                MaturityAmount = PrincipalAmount + InterestEarned;
            }
        }
    }

    // Enum for duration type
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
