using Microsoft.OpenApi.Any;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mf_backend.Models
{
    [Table("tr_bpkb")]
    public class TrBpkb
    {
        [Key]
        [Column("agreement_number")]
        public string AgreementNumber { get; set; }

        [Column("bpkb_no")]
        public string BpkbNo { get; set; } = null!;

        [Column("branch_id")]
        public string BranchId { get; set; } = null!;

        [Column("bpkb_date")]
        public DateTime Alias { get; set; }

        [Column("faktur_no")]
        public string FakturNo { get; set; } = null!;

        [Column("faktur_date")]
        public DateTime FakturNoDate { get; set; }

        [Column("location_id")]
        public string LocationId { get; set; } = null!;

        [Column("police_no")]
        public string PoliceNo { get; set; } = null!;

        [Column("bpkb_date_in")]
        public DateTime BpkbDateIn { get; set; }

        public virtual MsStorageLocation? MsStorageLocation { get; set; }
    }
}
