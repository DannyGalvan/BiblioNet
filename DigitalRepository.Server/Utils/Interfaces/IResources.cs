using DigitalRepository.Server.Entities.Response;

namespace DigitalRepository.Interfaces
{
    public interface IResources
    {
        public Response<string> SaveDocumentInServer(IFormFile? file, string previousValue);
    }
}
