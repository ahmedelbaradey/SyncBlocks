using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Entities.DataModels
{
    public class Journal
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("JournalId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Timestamp is a required field.")]
        public required DateTime Timestamp { get;set ; }

        [Required(ErrorMessage = "Change Type is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the ChangeType is 20 characters.")]
        public required string ChangeType { get; set; }

         [ForeignKey(nameof(SharedObject))]
        public required int SharedObjectId { get; set; }
        public required SharedObject SharedObject { get; set; }

        public required ICollection<Block>? Blocks { get; set; }

        [ForeignKey(nameof(JournalUser))]
        public required int UserId { get; set; }

        public required User JournalUser { get; set; }

        [ForeignKey(nameof(JournalDevice))]
        public required int DeviceId { get; set; }

        public required UserDevice JournalDevice { get; set; }

    }
}
