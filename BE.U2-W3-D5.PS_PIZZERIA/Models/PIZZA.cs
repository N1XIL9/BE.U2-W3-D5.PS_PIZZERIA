namespace BE.U2_W3_D5.PS_PIZZERIA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PIZZA")]
    public partial class PIZZA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PIZZA()
        {
            DETTAGLIO = new HashSet<DETTAGLIO>();
        }

        [Key]
        public int IdPizza { get; set; }

        [Required]
        [StringLength(50)]
        public string NomePizza { get; set; }

        [Required]
        [StringLength(50)]
        public string Ingredienti { get; set; }

        [Required]
        [StringLength(10)]
        public string Prezzo { get; set; }

        [Required]
        [StringLength(50)]
        public string Foto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETTAGLIO> DETTAGLIO { get; set; }
    }
}
