using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StockAnalysis.Web.Models
{
    public class Security:LogEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Series {  get; set; }
        public DateTime? ListingDate { get; set; }

        [DisplayName("Market Lot")]
        public int? MarketLot { get; set; }

        public SecurityTypeEnum? SecurityType { get; set; }

    }

    public enum SecurityTypeEnum
    {
        Equity = 1,
        SME = 2,
        Index = 3
    }
}
