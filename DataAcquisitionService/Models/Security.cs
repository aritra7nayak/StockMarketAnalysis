﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataAcquisitionService.Models
{
    public class Security:LogEntity
    {
        public int ID { get; set; }

        public int? SecurityRunID { get; set; }
        public string Name { get; set; }
        public string? Symbol { get; set; }
        public string? Series {  get; set; }
        public DateTime? ListingDate { get; set; }

        [DisplayName("Market Lot")]
        public int? MarketLot { get; set; }

        public int? BseCode { get; set; }

        public SecurityTypeEnum? SecurityType { get; set; }

        public virtual SecurityRun SecurityRun { get; set; }
    }

    

    public enum SecurityTypeEnum
    {
        Equity = 1,
        SME = 2,
        Index = 3
    }
}
