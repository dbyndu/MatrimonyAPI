using Matrimony.Service.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Matrimony.Helper;
using Matrimony.Model.User;

namespace Matrimony.Service.Common
{
    public class ImageWriterService : IImageWriterService
    {
        public async Task<Object> UploadImage(IFormFile file)
        {
            UserImage userImg = new UserImage();
            userImg.ImageTitle = file.FileName;
            try
            {
                if (CheckIfImageFile(file))
                {
                    userImg.Image = await WriteFile(file);
                    userImg.UploadStatus = "Success";
                    userImg.ContentType = file.ContentType;
                }
                else
                {
                    userImg.UploadStatus = "Invalid image file";
                }
            }
            catch (Exception e)
            {
                userImg.UploadStatus = e.Message;
            }
            return userImg;
        }

        /// <summary>
        /// Method to check if file is image file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return ImageWriterHelper.GetImageFormat(fileBytes) != ImageWriterHelper.ImageFormat.unknown;
        }
        /// <summary>
        /// Method to write file onto the disk
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<byte[]> WriteFile(IFormFile file)
        {
            byte[] fileBinary;
            try
            {
                //var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                //fileName = Guid.NewGuid().ToString() + extension; //Create a new Name 
                //                                                  //for the file due to security reasons.
                //var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                //using (var bits = new FileStream(path, FileMode.Create))
                //{
                //    await file.CopyToAsync(bits);
                //}
                var stream = file.OpenReadStream();
                BinaryReader binaryReader = new BinaryReader(stream);
                Byte[] bytes = binaryReader.ReadBytes((int)stream.Length);
                using (MemoryStream ms = new MemoryStream())
                {
                   await file.CopyToAsync(ms);
                    fileBinary = ms.ToArray();
                }
                return fileBinary;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
