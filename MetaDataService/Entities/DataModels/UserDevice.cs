using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataModels
{
    public class UserDevice
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("DeviceId")]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public required int UserId { get; set; }
        public required User User { get; set; }
    }
}
