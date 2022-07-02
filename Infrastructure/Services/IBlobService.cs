namespace Infrastructure.Services
{
    public interface IBlobService
    {
         string GetBlob(string name, string containerName);
         Task<IEnumerable<string>> AllBlobs(string containerName);
         Task<bool> UploadBlob(string name, IFormFile file, string containerName);
         Task<bool> DeleteBlob(string name, string containerName);
    }
}