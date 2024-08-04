using Shared.DataTransferObjects.Block;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Journal
{
    public record JournalForCreationDto : JournalForManipulationDto
    {
        public JournalForCreationDto(
            [Required(ErrorMessage = "Timestamp is a required field.")] 
        DateTime Timestamp, 
            [MaxLength(20, ErrorMessage = "Maximum length for the ChangeType is 20 characters."), Required(ErrorMessage = "Change Type is a required field.")] 
        string ChangeType, 
        int UserId, 
        int DeviceId, 
        ICollection<BlockForCreationDto>? Blocks) : base(Timestamp, ChangeType, UserId, DeviceId, Blocks)
        {
        }
    }
}
