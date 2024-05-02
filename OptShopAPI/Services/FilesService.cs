using OptShopAPI.IServices;
using System.Reflection;

namespace OptShopAPI.Services
{
    public class FilesService : IFilesService
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        public FilesService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        

        public async Task<string> SaveFile(IFormFile file)
        {
                    var fileName = file.FileName;
                   string  filePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                  
            return filePath;


        }

        public void DeleteFile(string fileName)
        {
            var filePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", fileName);
            if(System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
