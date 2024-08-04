using Shared.DataTransferObjects.Journal;
using Shared.DataTransferObjects.UserObject;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.SharedObject
{
    public record SharedObjectForCreationDto : SharedObjectForManipulationDto
    {
        public SharedObjectForCreationDto(
            [MaxLength(256, ErrorMessage = "Maximum length for the Object Name is 256 characters."), Required(ErrorMessage = "Object Name is a required field.")] 
            string Name, 
            [MaxLength(20, ErrorMessage = "Maximum length for the Type is 20 characters."), Required(ErrorMessage = "Type is a required field.")] 
            string Type, 
            [MaxLength(256, ErrorMessage = "Maximum length for the Relative Path is 256 characters."), Required(ErrorMessage = "Relative Path is a required field.")] 
            string RelativePath,
             ICollection<JournalForCreationDto>? Journals,
             ICollection<UserObjectForCreationDto>? UserObjects) : base(Name, Type, RelativePath, Journals, UserObjects)
        {
        }
    }
}
