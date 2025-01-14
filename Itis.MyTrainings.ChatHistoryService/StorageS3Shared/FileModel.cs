namespace StorageS3Shared;

public class FileModel
{
    public FileModel(string fileName, byte[] fileContent, string fileMetadata)
    {
        FileName = fileName;
        FileContent = fileContent;
        FileMetadata = fileMetadata;
    }

    public string FileName { get; set; }
    public byte[] FileContent { get; set; }
    public string FileMetadata { get; set; }
}