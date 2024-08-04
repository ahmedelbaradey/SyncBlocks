using Shared.DataTransferObjects.Block;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Journal
{
    public record JournalForManipulationDto (
         [Required(ErrorMessage = "Timestamp is a required field.")]
           DateTime Timestamp,
         [Required(ErrorMessage = "Change Type is a required field.")]
         [MaxLength(20, ErrorMessage = "Maximum length for the ChangeType is 20 characters.")]
          string ChangeType,
          int UserId ,
          int DeviceId ,
          ICollection<BlockForCreationDto>? Blocks 
        )

    {
        //[Required(ErrorMessage = "Timestamp is a required field.")]
        //public required DateTime Timestamp { get; init; }

        //[Required(ErrorMessage = "Change Type is a required field.")]
        //[MaxLength(20, ErrorMessage = "Maximum length for the ChangeType is 20 characters.")]
        //public required string ChangeType { get; init; }

        //public required int UserId { get; init; }
        //public required int DeviceId { get; init; }
        //public ICollection<BlockForCreationDto>? Blocks { get; init; }
    }
}
