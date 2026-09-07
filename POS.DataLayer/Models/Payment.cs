
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
        [MaxLength(30)]
        public string PaymentType { get; set; } = string.Empty;

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

        // Audit - who created the payment
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CreatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public User CreatedByUser { get; set; } = null!;

        // Audit - who last updated the payment
        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public User? UpdatedByUser { get; set; }

        public bool IsActive { get; set; } = true;
    }
}