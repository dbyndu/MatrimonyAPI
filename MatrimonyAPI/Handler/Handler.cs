using Matrimony.Model.User;
using Matrimony.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatrimonyAPI.Handler
{

    public interface IImageHandler
    {
        Task<IActionResult> UploadImage(IFormFile file);
        Task<Object> UploadUserImage(IFormFile file);
    }

    public class ImageHandler : IImageHandler
    {
        private readonly IImageWriterService _imageWriter;
        public ImageHandler(IImageWriterService imageWriter)
        {
            _imageWriter = imageWriter;
        }

        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var result = await _imageWriter.UploadImage(file);
            return new ObjectResult(result);
        }
        public async Task<Object> UploadUserImage(IFormFile file)
        {
            var result = await _imageWriter.UploadImage(file);
            return result;
        }
    }

}
