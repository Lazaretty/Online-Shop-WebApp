using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace OnlineShop_WebApp.Models
{
    public class Product  
    {
        public int Id { get; set; }

        public string NameOfProduct { get; set; }
            
        public double Price { get; set; }

        public string Producer { get; set; }

        public int IdSubCategory { get; set; }

        public byte[] Image { get; set; } // используется для загрузки изображения на серврер. Не работает 

        [Microsoft.AspNetCore.Mvc.BindProperty]
        private BufferedSingleFileUploadDb FileUpload { get; set; }  // используется для загрузки изображения на серврер. Не работает 

        public string About { get; set; }
    }
}
