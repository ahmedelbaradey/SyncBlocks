using Shared.DataTransferObjects.UserObjectPermission;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.UserObject
{
    public record  UserObjectForCreationDto(
        [Required(ErrorMessage = "User is a required field.")]
         int UserId ,
       ICollection<UserObjectPermissionForCreationDto>? UserObjectPermissions )
    {
        //[Required(ErrorMessage = "User is a required field.")]
        //public required int UserId { get; init; }
        //public ICollection<UserObjectPermissionForCreationDto>? UserObjectPermissions { get; init; }
    }

}
