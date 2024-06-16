using DataAcquisitionService.Models;
using System.ComponentModel;

namespace DataAcquisitionService.Dtos
{
    public class SecurityDTO
    {
        public int ID { get; set; }

        public int? SecurityRunID { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Series { get; set; }
        public DateTime? ListingDate { get; set; }

        [DisplayName("Market Lot")]
        public int? MarketLot { get; set; }

        public SecurityTypeEnum? SecurityType { get; set; }
    }
}
