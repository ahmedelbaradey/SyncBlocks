using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Block
{
    public record BlockForCreationDto : BlockForManipulationDto
    {
        public BlockForCreationDto(
            [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters."), Required(ErrorMessage = "Block Number is a required field.")] 
        string BlockNumber, 
            [Required(ErrorMessage = "Block Hash is a required field.")]
        string BlockHash) : base(BlockNumber, BlockHash)
        {
        }
    }
}
