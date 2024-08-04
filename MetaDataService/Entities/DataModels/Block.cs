using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataModels
{
    public class Block
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("BlockId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Block Number is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public required string BlockNumber { get; set; }

        [Required(ErrorMessage = "Block Hash is a required field.")]
        public required string BlockHash { get; set; }

        [ForeignKey(nameof(Journal))]
        public required int JournalId { get; set; }
        public required Journal Journal { get; set; }
    }
}
