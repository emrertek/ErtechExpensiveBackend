using BusinessLayer.Common.Extensions;
using BusinessLayer.Common.Interface;
using BusinessLayer.Common.Response;
using BusinessLayer.Common.SharedLibrary;
using BusinessLayer.Interfaces;
using DataAccessLayer.Connection;
using DataAccessLayer.DTOs;
using DataAccessLayer.Entitites;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.DTOs.OrderDetailsDTO;
using static DataAccessLayer.DTOs.ProductsDTO;

namespace BusinessLayer.Services
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly ParameterList _parameterList;
        private readonly DatabaseExecutions _databaseExecutions;

        public OrderDetailsService(ParameterList parameterList, DatabaseExecutions databaseExecutions)
        {
            _parameterList = parameterList;
            _databaseExecutions = databaseExecutions;
        }

        public IResponse<string> Create(OrderDetailsDTO.OrderDetailsCreate model)
        {
            try
            {
                _parameterList.Reset();

                _parameterList.Add("@OrderNo", model.OrderNo);
                _parameterList.Add("@ProductID", model.ProductId);
                _parameterList.Add("@Quantity", model.Quantity);
                _parameterList.Add("@SubTotal", model.SubTotal);

                var requestResult = _databaseExecutions.ExecuteQuery("SpCreate_OrderDetail", _parameterList);

                return new SuccessResponse<string>("Başarılı şekilde oluşturuldu");
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

                _parameterList.Add("@OrderDetailID", id);

                var requestResult = _databaseExecutions.ExecuteDeleteQuery("SpDelete_OrderDetail", _parameterList);
                if (requestResult > 0)
                {
                    return new SuccessResponse<string>(Messages.Delete("Order Details"));
                }
                else
                {
                    return new ErrorResponse<string>(Messages.DeleteError("Order Details"));
                }

            }
            catch (Exception ex)
            {
                return new ErrorResponse<string>(ex.Message);
            }
        }

        public IResponse<IEnumerable<OrderDetailsDTO.OrderDetailsQuery>> GetById(int id)
        {
            try
            {
                _parameterList.Add("@OrderDetailID", id);

                var jsonResult = _databaseExecutions.ExecuteQuery("SpGetByID_OrderDetail", _parameterList);
                var selectedOrderDetails = JsonConvert.DeserializeObject<IEnumerable<OrderDetailsQuery>>(jsonResult);

                if (selectedOrderDetails.IsNullOrEmpty())
                {
                    return new ErrorResponse<IEnumerable<OrderDetailsQuery>>(Messages.NotFound("Kategori"));
                }
                return new SuccessResponse<IEnumerable<OrderDetailsQuery>>(selectedOrderDetails);

            }
            catch (Exception ex)
            {
                return new ErrorResponse<IEnumerable<OrderDetailsQuery>>(ex.Message);
            }
        }

        public IResponse<IEnumerable<OrderDetailsDTO.OrderDetailsQuery>> GetByOrderNo(string orderNo)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@OrderNo", orderNo);

                var jsonResult = _databaseExecutions.ExecuteQuery("SpGetByOrderNo_OrderDetails", _parameterList);
                var selectedOrderDetails = JsonConvert.DeserializeObject<List<OrderDetailsQuery>>(jsonResult);

                return new SuccessResponse<IEnumerable<OrderDetailsQuery>>(selectedOrderDetails);
            }

            catch (Exception ex)
            {
                return new ErrorResponse<IEnumerable<OrderDetailsQuery>>(ex.Message);
            }
        }

        public IResponse<IEnumerable<OrderDetailsDTO.OrderDetailsQuery>> ListAll()
        {
            try
            {
                _parameterList.Reset();
                var jsonResult = _databaseExecutions.ExecuteQuery("SpGetAll_OrderDetails", _parameterList);
                var selectedOrderDetails = JsonConvert.DeserializeObject<List<OrderDetailsQuery>>(jsonResult);

                return new SuccessResponse<IEnumerable<OrderDetailsQuery>>(selectedOrderDetails);
            }

            catch (Exception ex)
            {
                return new ErrorResponse<IEnumerable<OrderDetailsQuery>>(ex.Message);
            }
        }

        public IResponse<string> Update(OrderDetailsDTO.OrderDetailsUpdate model)
        {
            try
            {
                _parameterList.Reset();

                _parameterList.Add("@OrderDetailID", model.Id);
                _parameterList.Add("@OrderNo", model.OrderNo ?? (object)DBNull.Value);
                _parameterList.Add("@ProductID", model.ProductId ?? (object)DBNull.Value);
                _parameterList.Add("@Quantity", model.Quantity ?? (object)DBNull.Value);
                _parameterList.Add("@SubTotal", model.SubTotal ?? (object)DBNull.Value);

                var requestResult = _databaseExecutions.ExecuteQuery("SpUpdate_OrderDetail", _parameterList);
                return new SuccessResponse<string>("Güncelleme başarılı");
            }
            catch (Exception ex)
            {
                return new ErrorResponse<string>(ex.Message);
            }
        }
    }
}
