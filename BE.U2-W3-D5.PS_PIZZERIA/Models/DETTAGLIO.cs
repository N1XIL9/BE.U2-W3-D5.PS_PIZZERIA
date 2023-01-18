namespace BE.U2_W3_D5.PS_PIZZERIA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DETTAGLIO")]
    public partial class DETTAGLIO
    {
        [Key]
        public int IdDettaglio { get; set; }

        public int Quantita { get; set; }

        public int IdPizza { get; set; }

        public int IdOrdine { get; set; }

        public virtual ORDINE ORDINE { get; set; }

        public virtual PIZZA PIZZA { get; set; }
    }
}
