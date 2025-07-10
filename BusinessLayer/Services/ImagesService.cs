using BusinessLayer.Common.Interface;
using BusinessLayer.Common.Response;
using BusinessLayer.Common.SharedLibrary;
using BusinessLayer.Interfaces;
using DataAccessLayer.Connection;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static DataAccessLayer.DTOs.ImagesDTO;

namespace BusinessLayer.Services
{
    public class ImagesService : IImagesService
    {
        private readonly ParameterList _parameterList;
        private readonly DatabaseExecutions _databaseExecutions;
        private readonly IConfiguration _configuration;

        public ImagesService(ParameterList parameterList, DatabaseExecutions databaseExecutions, IConfiguration configuration)
        {
            _parameterList = parameterList;
            _databaseExecutions = databaseExecutions;
            _configuration = configuration;
        }

        public IResponse<string> Create(ImagesCreate model)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@ImgGuid", model.ImgGuid);
                _parameterList.Add("@Base64ImagesFormat", model.Base64ImagesFormat);

                _databaseExecutions.ExecuteQuery("SpUpload_CreateImage", _parameterList);
                return new SuccessResponse<string>("Image successfully created.");
            }
            catch (Exception ex)
            {
                return new ErrorResponse<string>(ex.Message);
            }
        }

        public IResponse<string> Delete(int id)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@Id", id);

                var result = _databaseExecutions.ExecuteDeleteQuery("SpDelete_Images", _parameterList);
                return result > 0
                    ? new SuccessResponse<string>("Silindi")
                    : new ErrorResponse<string>("Silinemedi");
            }
            catch (Exception ex)
            {
                return new ErrorResponse<string>(ex.Message);
            }
        }

        public IResponse<IEnumerable<ImagesQuery>> ListAll()
        {
            try
            {
                _parameterList.Reset();

                var jsonResult = _databaseExecutions.ExecuteQuery("SpGetAll_Images", _parameterList);
                var list = JsonConvert.DeserializeObject<List<ImagesQuery>>(jsonResult);

                return new SuccessResponse<IEnumerable<ImagesQuery>>(list);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<IEnumerable<ImagesQuery>>(ex.Message);
            }
        }

        public IResponse<ImagesQuery> FindByGuid(Guid imgGuid)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@ImgGuid", imgGuid);

                var jsonResult = _databaseExecutions.ExecuteQuery("SpGetByGuid_Images", _parameterList);
                var result = JsonConvert.DeserializeObject<List<ImagesQuery>>(jsonResult)?.FirstOrDefault();

                if (result == null)
                    return new ErrorResponse<ImagesQuery>("Image not found");

                return new SuccessResponse<ImagesQuery>(result);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<ImagesQuery>(ex.Message);
            }
        }
        
        public string UploadFile(IFormFile file, int productId)
        {
            if (file == null || file.Length == 0)
                return null;

            string fileName = Path.GetFileName(file.FileName);
            string fileExtension = Path.GetExtension(fileName).ToLower();
            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            string documentGuid = Guid.NewGuid().ToString();
            string newFileName = documentGuid + fileExtension;
            string filePath = Path.Combine(uploadPath, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var imageBase64Format = Convert.ToBase64String(System.IO.File.ReadAllBytes(filePath));

            _parameterList.Reset();
            _parameterList.Add("@ImgGuid", documentGuid);
            _parameterList.Add("@Base64ImagesFormat", imageBase64Format);
            _parameterList.Add("@ProductID", productId); // 🆕 BURASI!

            _databaseExecutions.ExecuteQuery("SpUpload_CreateImage", _parameterList);

            return documentGuid;
        }

        public IResponse<ImageDataDto> GetImageDataForFrontend(Guid imgGuid)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@ImgGuid", imgGuid);

                var jsonResult = _databaseExecutions.ExecuteQuery("SpGetByGuid_Images", _parameterList);
                var imageData = JsonConvert.DeserializeObject<List<ImagesQuery>>(jsonResult)?.FirstOrDefault();

                if (imageData == null)
                    return new ErrorResponse<ImageDataDto>("Image not found");

                var dto = new ImageDataDto
                {
                    ImgGuid = imageData.ImgGuid.ToString(),
                    Base64 = imageData.Base64ImagesFormat // burada format değil, base64 data var!
                };

                return new SuccessResponse<ImageDataDto>(dto);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<ImageDataDto>(ex.Message);
            }
        }

        public IResponse<List<ProductImageDto>> GetImagesByProductId(int productId)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@ProductID", productId);

                var json = _databaseExecutions.ExecuteQuery("SpGetImages_ByProductId", _parameterList);
                var list = JsonConvert.DeserializeObject<List<ProductImageDto>>(json);

                return new SuccessResponse<List<ProductImageDto>>(list);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<List<ProductImageDto>>(ex.Message);
            }
        }



        









        /* private string GetFileType(string fileName)
         {
             string extension = Path.GetExtension(fileName).ToLower();
             switch (extension)
             {
                 case ".jpg":
                 case ".jpeg":
                 case ".png":
                 case ".jfif":
                     return "Resim";
                 default:
                     return "Diger";
             }
         }*/






    }
}
