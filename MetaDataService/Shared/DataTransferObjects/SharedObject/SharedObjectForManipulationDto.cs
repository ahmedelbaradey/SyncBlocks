using Shared.DataTransferObjects.Journal;
using Shared.DataTransferObjects.UserDevice;
using Shared.DataTransferObjects.UserObject;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.SharedObject
{
    public   record  SharedObjectForManipulationDto(

        [Required(ErrorMessage = "Object Name is a required field."),MaxLength(256, ErrorMessage = "Maximum length for the Object Name is 256 characters.")]
         string Name ,

        [Required(ErrorMessage = "Type is a required field.") ,MaxLength(20, ErrorMessage = "Maximum length for the Type is 20 characters.")]
        string Type,

        [Required(ErrorMessage = "Relative Path is a required field."),MaxLength(256, ErrorMessage = "Maximum length for the Relative Path is 256 characters.")]
        string RelativePath,

        ICollection<JournalForCreationDto>? Journals,
        ICollection<UserObjectForCreationDto>? UserObjects
        )
    {
        //[Required(ErrorMessage = "Object Name is a required field.")]
        //[MaxLength(256, ErrorMessage = "Maximum length for the Object Name is 256 characters.")]
        //public required string Name { get; init; }

        //[Required(ErrorMessage = "Type is a required field.")]
        //[MaxLength(20, ErrorMessage = "Maximum length for the Type is 20 characters.")]
        //public required string Type { get; init; }

        //[Required(ErrorMessage = "Relative Path is a required field.")]
        //[MaxLength(256, ErrorMessage = "Maximum length for the Relative Path is 256 characters.")]
        //public required string RelativePath { get; init; }
        //public ICollection<JournalForCreationDto>? Journals { get; init; }
        //public ICollection<UserObjectForCreationDto>? UserObjects { get; init; }
    }
}
