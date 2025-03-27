using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessLayer.Common.Interface;
using BusinessLayer.Common.Response;
using BusinessLayer.Interfaces;
using DataAccessLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Connection;
using Newtonsoft.Json;
using Microsoft.Extensions.Primitives;

namespace BusinessLayer.Services
{
    public class LoginService : ILoginService
    {
        private readonly ParameterList _parameterList;
        private readonly DatabaseExecutions _databaseExecutions;
        private readonly IAuthService _authService; // Şifre hashleme servisi

        public LoginService(ParameterList parameterList, DatabaseExecutions databaseExecutions, IAuthService authService)
        {
            _parameterList = parameterList;
            _databaseExecutions = databaseExecutions;
            _authService = authService;
        }

        public IResponse<IEnumerable<string>> AuthenticateUser(LoginAuthDTO model)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@Email", model.Email);

                // Veritabanından kullanıcıyı çekiyoruz
                var jsonResult = _databaseExecutions.ExecuteQuery("SpAuthenticate_Customer", _parameterList);

                // JSON dizisi olabileceği için Liste olarak Deserialize et
                var users = JsonConvert.DeserializeObject<List<LoginDTO>>(jsonResult);

                // Eğer kullanıcı yoksa hata döndür
                var user = users?.FirstOrDefault();
                if (user == null)
                {
                    return new ErrorResponse<IEnumerable<string>>("Kullanıcı bulunamadı!");
                }

                // Kullanıcının girdiği şifreyi hashleyerek veritabanındaki hash ile karşılaştır
                string hashedInputPassword = _authService.GenerateHashedPassword(model.Password);
                if (hashedInputPassword != user.Password)
                {
                    return new ErrorResponse<IEnumerable<string>>("Şifre hatalı!");
                }


                /*Admin Bilgilerini Al*/
                bool isAdmin = user.IsAdmin;

                /* Token Üretme */
                string token = _authService.GenerateToken(user.Email, user.CustomerId, isAdmin);

                return new SuccessResponse<IEnumerable<string>>(token);

            }
            catch (Exception ex)
            {
                return new ErrorResponse<IEnumerable<string>>(ex.Message);
            }
        }
    }
}

