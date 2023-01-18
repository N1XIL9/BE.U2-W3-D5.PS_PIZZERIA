namespace BE.U2_W3_D5.PS_PIZZERIA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ORDINE")]
    public partial class ORDINE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ORDINE()
        {
            DETTAGLIO = new HashSet<DETTAGLIO>();
        }

        [Key]
        public int IdOrdne { get; set; }

        [Required]
        [StringLength(50)]
        public string Note { get; set; }

        [Required]
        [StringLength(50)]
        public string Confermato { get; set; }

        [Required]
        [StringLength(10)]
        public string TotaleImporto { get; set; }

        [Required]
        [StringLength(50)]
        public string Evaso { get; set; }

        public int IdUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETTAGLIO> DETTAGLIO { get; set; }

        public virtual USER USER { get; set; }
    }
}
