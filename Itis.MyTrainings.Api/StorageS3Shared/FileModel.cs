namespace StorageS3Shared;

public class FileModel
{
    public FileModel(string fileName, byte[] fileContent)
    {
        FileName = fileName;
        FileContent = fileContent;
    }

    public string FileName { get; set; }
    public byte[] FileContent { get; set; }
}