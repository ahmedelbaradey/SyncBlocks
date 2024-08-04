using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Block
{
    public   record  BlockForManipulationDto (
        [Required(ErrorMessage = "Block Number is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
          string BlockNumber ,

        [Required(ErrorMessage = "Block Hash is a required field.")]
         string BlockHash 
        )
    {
        //[Required(ErrorMessage = "Block Number is a required field.")]
        //[MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        //public required string BlockNumber { get; init; }

        //[Required(ErrorMessage = "Block Hash is a required field.")]
        //public required string BlockHash { get; init; }
    }
}
