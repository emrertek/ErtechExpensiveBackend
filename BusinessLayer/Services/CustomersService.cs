using BusinessLayer.Common.Extensions;
using BusinessLayer.Common.Interface;
using BusinessLayer.Common.Response;
using BusinessLayer.Common.SharedLibrary;
using BusinessLayer.Interfaces;
using DataAccessLayer.Connection;
using DataAccessLayer.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ParameterList _parameterList;
        private readonly DatabaseExecutions _databaseExecutions;
        private readonly IAuthService _authService;

        public CustomersService(ParameterList parameterList, DatabaseExecutions databaseExecutions, IAuthService authService)
        {
            _parameterList = parameterList;
            _databaseExecutions = databaseExecutions;
            this._authService = authService;
        }

        public IResponse<string> Create(CustomersDTO.CustomerCreate model)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@FirstName", model.FirstName);
                _parameterList.Add("@LastName", model.LastName);
                _parameterList.Add("@Email", model.Email);
                _parameterList.Add("@Phone", model.Phone);
                _parameterList.Add("@IsAdmin", model.IsAdmin);
                //_parameterList.Add("@Password", model.Password);


                string hashedPassword =  _authService.GenerateHashedPassword(model.Password);
                _parameterList.Add("@Password", hashedPassword);


                var requestResult = _databaseExecutions.ExecuteQuery("SpCreate_Customer", _parameterList);

                return new SuccessResponse<string>("Kategori başarılı bir şekilde oluşturuldu");
            }
            catch (Exception ex)
            {
                return new ErrorResponse<string>($"Failed to create {ex.Message}");
            }
        }

        public IResponse<string> Delete(int id)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@CustomerID", id);

                var requestResult = _databaseExecutions.ExecuteDeleteQuery("SpDelete_Customer", _parameterList);
                if (requestResult > 0)
                {
                    return new SuccessResponse<string>(Messages.Delete("Customer"));
                }
                else
                {
                    return new ErrorResponse<string>(Messages.DeleteError("Customer"));
                }
            }
            catch (Exception ex)
            {
                return new ErrorResponse<string>(ex.Message);
            }        
        }

        public IResponse<IEnumerable<CustomersDTO.CustomerQuery>> FindById(int id)
        {
            try
            {
                _parameterList.Add("@CustomerID", id);

                var jsonResult = _databaseExecutions.ExecuteQuery("SpGetById_Customer", _parameterList);
                var selectedCustomer = JsonConvert.DeserializeObject<IEnumerable<CustomersDTO.CustomerQuery>>(jsonResult);

                if (selectedCustomer.IsNullOrEmpty())
                {
                    return new ErrorResponse<IEnumerable<CustomersDTO.CustomerQuery>>(Messages.NotFound("Kategori"));
                }
                    return new SuccessResponse<IEnumerable<CustomersDTO.CustomerQuery>>(selectedCustomer);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<IEnumerable<CustomersDTO.CustomerQuery>>(ex.Message);
            }
        }

        public IResponse<IEnumerable<CustomersDTO.CustomerQuery>> ListAll()
        {
            try
            {
                _parameterList.Reset();
                var jsonResult = _databaseExecutions.ExecuteQuery("SpGetAll_Customers", _parameterList);
                var customers = JsonConvert.DeserializeObject<List<CustomersDTO.CustomerQuery>>(jsonResult);

                return new SuccessResponse<IEnumerable<CustomersDTO.CustomerQuery>>(customers);

            }
            catch (Exception ex)
            {
                return new ErrorResponse<IEnumerable<CustomersDTO.CustomerQuery>>(ex.Message);
            }
            
        }

        public IResponse<string> Update(CustomersDTO.CustomerUpdate model)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@CustomerID", model.Id);
                _parameterList.Add("@FirstName", model.FirstName);
                _parameterList.Add("@LastName", model.LastName);
                _parameterList.Add("@Email", model.Email);
                _parameterList.Add("@Phone", model.Phone);
                _parameterList.Add("IsAdmin", model.IsAdmin);
                

                var jsonResult = _databaseExecutions.ExecuteQuery("SpUpdate_Customer", _parameterList);

                return new SuccessResponse<string>(Messages.Update("Customers"));

            }
            catch (Exception ex)
            {
                return new ErrorResponse<string>(ex.Message);
            }
        }


        public IResponse<string> UpdatePassword(int customerId, string password)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@CustomerID", customerId);
                _parameterList.Add("@Password", password);



                var jsonResult = _databaseExecutions.ExecuteQuery("SpUpdate_CustomerPassword", _parameterList);

                return new SuccessResponse<string>(Messages.Update("Password"));
            }
            catch (Exception ex)
            {
                return new ErrorResponse<string>(ex.Message);
            }
        }
    }
}
