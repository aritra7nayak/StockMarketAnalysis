namespace DataAcquisitionService.Dtos
{
    public class CorporateActionDTO
    {
        public int? ID { get; set; }

        #region Candidate Key
        public DateTime? Date { get; set; }
        public int? SecurityID { get; set; }
        public string? Purpose { get; set; }

        public int? CorporateActionTypeID { get; set; }
        #endregion

        public int? CorporateActionRunID { get; set; }
        public string? SecurityName { get; set; }

        public decimal? FaceValue { get; set; }

        public DateTime? Record_Date { get; set; }
        public DateTime? Book_Closure_Start_Date { get; set; }
        public DateTime? Book_Closure_End_Date { get; set; }
    }
}
