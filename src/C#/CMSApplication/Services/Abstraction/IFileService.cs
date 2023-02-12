namespace CMSApplication.Services.Abstraction
{
    public interface IFileService
    {
        Task<byte[]> DownloadFile(string name);
        Task<string> PostFile(IFormFile file, string empId);
    }
}
