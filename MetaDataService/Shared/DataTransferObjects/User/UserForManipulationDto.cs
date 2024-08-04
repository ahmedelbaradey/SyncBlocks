using Shared.DataTransferObjects.UserDevice;
using Shared.DataTransferObjects.UserObject;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.User
{
    public abstract record UserForManipulationDto
    {
        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public required string Name { get; set; }
        public IEnumerable<UserDeviceForCreationDto>? Devices { get; set; }
        public ICollection<UserObjectForCreationDto>? UserObjects { get; set; }
    }
}
