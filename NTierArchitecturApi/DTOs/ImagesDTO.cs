using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class ImagesDTO
    {
        public class ImagesCreate
        {
            public Guid ImgGuid { get; set; }
            public string Base64ImagesFormat { get; set; }
        }
     
        public class ImagesQuery
        {
            public int Id { get; set; }
            public Guid ImgGuid { get; set; }
            public string Base64ImagesFormat { get; set; }
            
        }


        public class ImageDataDto
        {
            public string Base64 { get; set; } // base64 string (image içeriği)
            public string ImgGuid { get; set; } // görsel ID'si
        }



        public class ImagesUpdate
        {
            public int Id { get; set; }
            public Guid ImgGuid { get; set; }
            public string Base64 { get; set; } = string.Empty;

            // public string Base64ImagesFormat { get; set; }
        }

        public class ImagesDelete
        {
            public int Id { get; set; }
        }


        public class ProductImageDto
        {
            public string ImgGuid { get; set; }
            public string Base64ImagesFormat { get; set; }
        }

    }
}
