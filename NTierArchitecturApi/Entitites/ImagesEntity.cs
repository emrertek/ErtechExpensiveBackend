using DataAccessLayer.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Images : BaseEntity
    {
        public Guid ImgGuid { get; set; }
        public string Base64 { get; set; } = string.Empty;
        public string Base64ImagesFormat { get; set; }
    }
}
