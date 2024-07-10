using System.ComponentModel.DataAnnotations.Schema;

namespace DataAcquisitionService.Models
{
    public class CorporateActionTypeRun : ProcessEntity
    {
        public int Id { get; set; }

        [NotMapped]
        public byte[]? FileStream { get; set; }
    }
}