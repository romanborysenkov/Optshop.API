namespace OptShopAPI.IServices
{
    public interface IFilesService
    {
         Task<string> SaveFile(IFormFile file);
        void DeleteFile(string fileName);
        
    }
}
