using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mf_backend.Models
{
    [Table("tr_bpkb")]
    public class MsStorageLocation
    {
        [Key]
        [Column("location_id")]
        public string LocationId { get; set; }

        [Column("location_name")]
        public string LocationName { get; set; } = null!;
    }
}
