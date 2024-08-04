using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataModels
{
    public class UserObjectPermission
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserObjectPermissionId")]
        public int Id { get; set; }

        [ForeignKey(nameof(UserObjectId))]
        public required int UserObjectId { get; set; }
        public required UserObject UserObject { get; set; }

        [ForeignKey(nameof(Permission))]
        public required int PermissionId { get; set; }
        public required Permission Permission { get; set; }

    }
}
