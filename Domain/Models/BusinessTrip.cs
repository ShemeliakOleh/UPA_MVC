namespace Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BusinessTrip")]
    public partial class BusinessTrip
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int UserEventId { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Required]
        [StringLength(50)]
        public string Occasion { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string Institution { get; set; }

        [Required]
        [StringLength(50)]
        public string PayingParty { get; set; }

        public bool DoesSalaryRemain { get; set; }

        public virtual UserEvent UserEvent { get; set; }
    }
}
