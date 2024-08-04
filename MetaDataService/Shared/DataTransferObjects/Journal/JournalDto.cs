namespace Shared.DataTransferObjects.Journal
{
    [Serializable]
    public record JournalDto(int Id,DateTime Timestamp, string ChangeType);

}
