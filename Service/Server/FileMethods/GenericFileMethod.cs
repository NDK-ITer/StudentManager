using Microsoft.IdentityModel.Tokens;

namespace Server.FileMethods
{
    public class GenericFileMethod
    {
        protected IWebHostEnvironment environment;
        public GenericFileMethod(IWebHostEnvironment env)
        {
            this.environment = env;
        }
        protected byte[] ConvertIFormFileToByteArray(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
        public string? SaveFile(string folder, IFormFile file, string? newName)
        {
            if (folder.IsNullOrEmpty() || file == null) { return null; }
            var contentPath = this.environment.ContentRootPath;
            var path = Path.Combine(contentPath, folder);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            var newFileName = file.FileName;
            if (newName != null) newFileName = newName;
            var fileWithPath = Path.Combine(path, newFileName);
            var stream = new FileStream(fileWithPath, FileMode.Create);
            file.CopyTo(stream);
            stream.Close();
            return newFileName;
        }
        public void DeleteFile(string folder, string fileName)
        {
            if (folder.IsNullOrEmpty() || fileName.IsNullOrEmpty()) return;
            var contentPath = this.environment.ContentRootPath;
            var path = Path.Combine(contentPath, folder);
            var fileWithPath = Path.Combine(path, fileName);
            File.Delete(fileWithPath);
        }
    }
}
