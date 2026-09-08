using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.DataLayer.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }


        // Bill
        [Required]
        public int BillId { get; set; }

        [ForeignKey(nameof(BillId))]
        public Bill Bill { get; set; } = null!;


        // Payment Type
        [Required]
        public int PaymentTypeId { get; set; }

        [ForeignKey(nameof(PaymentTypeId))]
        public PaymentType PaymentType { get; set; } = null!;


        // Payment Information
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        public string? ReferenceNumber { get; set; }


        // Online Payment Tracking
        public string? Req { get; set; }

        public string? Res { get; set; }

        public string? Callback { get; set; }


        // Audit - Created
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CreatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public User CreatedByUser { get; set; } = null!;


        // Audit - Updated
        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public User? UpdatedByUser { get; set; }


        // Soft Delete
        public bool IsActive { get; set; } = true;

        public int? DeletedBy { get; set; }

        [ForeignKey(nameof(DeletedBy))]
        public User? DeletedByUser { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}