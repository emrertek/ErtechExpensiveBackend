using BusinessLayer.Common.Interface;
using BusinessLayer.Common.Response;
using BusinessLayer.Interfaces;
using DataAccessLayer.Connection;
using DataAccessLayer.DTOs;
using Newtonsoft.Json;
using static DataAccessLayer.DTOs.CustomerAddressesDTO;

namespace BusinessLayer.Services
{
    public class CustomerAddressesService : ICustomerAddressesService
    {
        private readonly ParameterList _parameterList;
        private readonly DatabaseExecutions _databaseExecutions;

        public CustomerAddressesService(ParameterList parameterList, DatabaseExecutions databaseExecutions)
        {
            _parameterList = parameterList;
            _databaseExecutions = databaseExecutions;
        }

        public IResponse<string> Create(AddressCreate model)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@CustomerID", model.CustomerID);
                _parameterList.Add("@AddressLine", model.AddressLine);
                _parameterList.Add("@City", model.City);
               // _parameterList.Add("@State", model.State);
                _parameterList.Add("@Country", model.Country);
                _parameterList.Add("@PostalCode", model.PostalCode);

                _databaseExecutions.ExecuteQuery("SpCreate_CustomerAddress", _parameterList);

                return new SuccessResponse<string>("Adres eklendi.");
            }
            catch (Exception ex)
            {
                return new ErrorResponse<string>($"Hata: {ex.Message}");
            }
        }

        public IResponse<string> Update(AddressUpdate model)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@AddressId", model.AddressId);
                _parameterList.Add("@AddressLine", model.AddressLine);
                _parameterList.Add("@City", model.City);
                _parameterList.Add("@State", model.State);
                _parameterList.Add("@Country", model.Country);
                _parameterList.Add("@PostalCode", model.PostalCode);

                _databaseExecutions.ExecuteQuery("SpUpdate_CustomerAddress", _parameterList);
                return new SuccessResponse<string>("Adres güncellendi.");
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
                _parameterList.Add("@AddressID", id);
                _databaseExecutions.ExecuteQuery("SpDelete_CustomerAddress", _parameterList);
                return new SuccessResponse<string>("Adres silindi.");
            }
            catch (Exception ex)
            {
                return new ErrorResponse<string>(ex.Message);
            }
        }

        public IResponse<IEnumerable<AddressQuery>> ListAll()
        {
            try
            {
                _parameterList.Reset();
                var jsonResult = _databaseExecutions.ExecuteQuery("SpGetAll_CustomerAddresses", _parameterList);
                var addresses = JsonConvert.DeserializeObject<IEnumerable<AddressQuery>>(jsonResult);
                return new SuccessResponse<IEnumerable<AddressQuery>>(addresses);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<IEnumerable<AddressQuery>>(ex.Message);
            }
        }

        public IResponse<IEnumerable<AddressQuery>> FindByCustomerId(int customerId)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@CustomerID", customerId);
                var jsonResult = _databaseExecutions.ExecuteQuery("SpGetById_CustomerAddress", _parameterList);
                var addresses = JsonConvert.DeserializeObject<IEnumerable<AddressQuery>>(jsonResult);
                return new SuccessResponse<IEnumerable<AddressQuery>>(addresses);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<IEnumerable<AddressQuery>>(ex.Message);
            }
        }
    }
}
