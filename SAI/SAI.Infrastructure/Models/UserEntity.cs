using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.Infrastructure.Models
{
    [Table("contacts")]
    public class ContactEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(255)]
        public required string FullName { get; set; }

        [Required, MaxLength(16)]
        public required ulong PhoneNumber { get; set; }

        [Required, MaxLength(256)]
        public required string EmailAddress { get; set; }

        [MaxLength(64)]
        public string? TelegramNickName { get; set; }

        // Adderss
        [Required ,MaxLength(6)]
        public required int HouseNumber { get; set; }
        [Required, MaxLength(60)]
        public required string Street { get; set; }
        [Required, MaxLength(6)]
        public required string City { get; set; }
        [Required, MaxLength(6)]
        public required string District { get; set; }
        [Required, MaxLength(6)]
        public required int Region { get; set; }

        [MaxLength(9)]
        public int? PostalIndex { get; set; }
        [MaxLength(20)]
        public string? Country { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<NoteEntity> Notes { get; set; } = new List<NoteEntity>();
    }
}

