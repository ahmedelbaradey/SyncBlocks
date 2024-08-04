using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataModels
{
    public class SharedObject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ObjectId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Object Name is a required field.")]
        [MaxLength(256, ErrorMessage = "Maximum length for the Object Name is 256 characters.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Type is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Type is 20 characters.")]
        public required string Type { get; set; }

        [Required(ErrorMessage = "Relative Path is a required field.")]
        [MaxLength(256, ErrorMessage = "Maximum length for the Relative Path is 256 characters.")]
        public required string RelativePath { get; set; }

        [ForeignKey(nameof(ParentObject))]
        public int? ParentObjectId { get; set; }
        public SharedObject? ParentObject { get; set; }
        public ICollection<SharedObject>? SiblingObjects { get; set; }
        public ICollection<Journal>? Journals { get; set; }
        public ICollection<UserObject>? UserObjects { get; set; }
    }
}
