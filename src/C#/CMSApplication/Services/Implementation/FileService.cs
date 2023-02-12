using CMSApplication.Services.Abstraction;

namespace CMSApplication.Services.Implementation
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;
        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string> PostFile(IFormFile file, string empId)
        {
            var fileType = Path.GetExtension(file.FileName);
            string fileName = $"{empId}_{Guid.NewGuid()}{fileType}";
            var filePath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}Profiles{Path.DirectorySeparatorChar}{fileName}";

            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }

        public async Task<byte[]> DownloadFile(string name)
        {
            var filePath = $"{_env.WebRootPath}{Path.DirectorySeparatorChar}Profiles{Path.DirectorySeparatorChar}{name}";
            return await File.ReadAllBytesAsync(filePath);
        }
    }
}
