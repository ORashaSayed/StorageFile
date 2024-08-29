namespace Storage.Application.Dtos
{
    public class FileDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public DateTime UploadedAt { get; set; }
        public string Path { get; set; }
    }
}