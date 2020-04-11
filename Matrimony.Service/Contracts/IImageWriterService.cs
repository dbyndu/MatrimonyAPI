using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Matrimony.Service.Contracts
{
    public interface IImageWriterService
    {
        Task<object> UploadImage(IFormFile file);
    }
}
