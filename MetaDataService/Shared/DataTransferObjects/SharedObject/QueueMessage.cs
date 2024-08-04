
namespace Shared.DataTransferObjects.SharedObject
{

    public class QueueMessage
    {
        public SharedObjectForCreationDto SharedObjectForCreationDto { get; set; }
        public string ParentObject { get; set; }
        public int UserId { get; set; }
    }
}
