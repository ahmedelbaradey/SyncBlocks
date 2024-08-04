namespace Shared.DataTransferObjects.SharedObject
{
    [Serializable]
    public record SharedObjectDto(int Id, string Name,string Type, string RelativePath);

}
