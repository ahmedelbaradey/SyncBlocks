using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Permisson
{
    public abstract record PermissionForManipulationDto
    {
        [Required(ErrorMessage = "Employee name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string? Name { get; init; }
      
    }
}
