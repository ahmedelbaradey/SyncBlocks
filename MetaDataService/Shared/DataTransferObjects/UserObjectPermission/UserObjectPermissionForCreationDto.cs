using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.UserObjectPermission
{
    public record UserObjectPermissionForCreationDto (
         [Required(ErrorMessage = "Permission is a required field.")]
        int PermissionId 
        )
    {
        //[Required(ErrorMessage = "Permission is a required field.")]
        //public required int PermissionId { get; init; }
    }

}
