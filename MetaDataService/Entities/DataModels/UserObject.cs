using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataModels
{
    public class UserObject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserObjectId")]
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public required int UserId { get; set; }
        public required User User { get; set; }
        [ForeignKey(nameof(SharedObject))]
        public required int SharedObjectId { get; set; }
        public required SharedObject SharedObject { get; set; }
        public required ICollection<UserObjectPermission> UserObjectPermissions { get; set; }
    }
}
