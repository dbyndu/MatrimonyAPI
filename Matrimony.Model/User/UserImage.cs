﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Matrimony.Model.User
{
    public class UserImage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public byte[] Image { get; set; }
        public string ImageString { get; set; }
        [JsonIgnore]
        public string UploadStatus { get; set; }
        [JsonIgnore]
        public string ImageTitle { get; set; }
        public string ContentType { get; set; }
        public IFormFile FormData { get; set; }
    }
}