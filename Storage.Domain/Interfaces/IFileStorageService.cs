namespace Storage.Domain.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(string fileName, byte[] fileContent);
        Task<byte[]> DownloadFileAsync(string path);
        Task DeleteFileAsync(string path);
    }
}
