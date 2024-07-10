namespace DataAcquisitionService.Models
{
    public class CorporateActionType:LogEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int? CorporateActionTypeRunID { get; set; }

        public virtual CorporateActionTypeRun CorporateActionTypeRun { get; set; }

    }
}
