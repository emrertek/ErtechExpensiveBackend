using BusinessLayer.Common.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DataAccessLayer.DTOs.ImagesDTO;

namespace BusinessLayer.Interfaces
{
    public interface IImagesService
    // create, delete, getall, getbyguid
    {
        IResponse<IEnumerable<ImagesQuery>> ListAll();
        IResponse<ImagesQuery> FindByGuid(Guid imgGuid);
        IResponse<string> Create(ImagesCreate model);
        IResponse<string> Delete(int id);
        string UploadFile(IFormFile file,int productId);
        IResponse<ImageDataDto> GetImageDataForFrontend(Guid imgGuid);

        IResponse<List<ProductImageDto>> GetImagesByProductId(int productId);



    }
}
