﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop_WebApp.Models 
{
    public class BufferedSingleFileUploadDb 
    {
        [Required]
       // [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
    }
}
