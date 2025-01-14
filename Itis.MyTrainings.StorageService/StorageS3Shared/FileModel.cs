namespace StorageS3Shared;

public class FileModel(string fileName, byte[] fileContent)
{
    public string FileName { get; set; } = fileName;
    public byte[] FileContent { get; set; } = fileContent;
    public string FileMetadata { get; set; }
}