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
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.DTOs.OrdersDTO;

namespace BusinessLayer.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ParameterList _parameterList;
        private readonly DatabaseExecutions _databaseExecutions;

        public OrdersService(ParameterList parameterList, DatabaseExecutions databaseExecutions)
        {
            _parameterList = parameterList;
            _databaseExecutions = databaseExecutions;
        }

        public IResponse<string> Create(OrdersDTO.OrdersCreate model)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@CustomerID", model.CustomerId);
                _parameterList.Add("@OrderDate", DateTime.Now);
                _parameterList.Add("@TotalAmount", model.TotalAmount);
                _parameterList.Add("@Status", model.Status);

                var requestResult = _databaseExecutions.ExecuteQuery("SpCreate_Order", _parameterList);
                return new SuccessResponse<string>("Başarılı order girişi");

            }
            catch (Exception ex)
            {
                return new ErrorResponse<string>($"Failed to create : {ex.Message}");
            }
        }

        public IResponse<string> Delete(int id)
        {
            try
            {
                _parameterList.Reset();

                _parameterList.Add("@OrderID", id);

                var requestResult = _databaseExecutions.ExecuteDeleteQuery("SpDelete_Order", _parameterList);

                if (requestResult > 0)
                {
                    return new SuccessResponse<string>(Messages.Delete("Order"));
                }
                else
                {
                    return new ErrorResponse<string>(Messages.DeleteError("Order"));
                }
            }
            catch (Exception ex)
            {
                return new ErrorResponse<string>(ex.Message);
            }
        }

        public IResponse<IEnumerable<OrdersDTO.OrdersQuery>> GetById(int id)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@OrderID", id);

                var jsonResult = _databaseExecutions.ExecuteQuery("SpGetByID_Order", _parameterList);
                var selectedOrder = JsonConvert.DeserializeObject<IEnumerable<OrdersQuery>>(jsonResult);

                if (selectedOrder.IsNullOrEmpty())
                {
                    return new ErrorResponse<IEnumerable<OrdersQuery>>(Messages.NotFound("Order"));
                }
                return new SuccessResponse<IEnumerable<OrdersQuery>>(selectedOrder);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<IEnumerable<OrdersQuery>>(ex.Message);
            }
        }

        public IResponse<IEnumerable<OrdersDTO.OrdersQuery>> ListAll()
        {
            try
            {
                _parameterList.Reset();

                var jsonResult = _databaseExecutions.ExecuteQuery("SpGetAll_Orders", _parameterList);
                var orders = JsonConvert.DeserializeObject<List<OrdersQuery>>(jsonResult);

                return new SuccessResponse<IEnumerable<OrdersQuery>>(orders);

            }
            catch (Exception ex)
            {
                return new ErrorResponse<IEnumerable<OrdersQuery>>(ex.Message);
            }
        }

        public IResponse<string> Update(OrdersDTO.OrdersUpdate model)
        {
            try
            {
                _parameterList.Reset();

                _parameterList.Add("@OrderID", model.Id);
                _parameterList.Add("@CustomerID", model.CustomerId ?? (object)DBNull.Value);
                _parameterList.Add("@OrderDate", model.OrderDate == default ? (object)DBNull.Value : model.OrderDate);
                _parameterList.Add("@TotalAmount", model.TotalAmount ?? (object)DBNull.Value);
                

                var requestResult = _databaseExecutions.ExecuteQuery("SpUpdate_Order", _parameterList);
                return new SuccessResponse<string>(Messages.Update("Orders"));
            }
            catch (Exception ex)
            {
                return new ErrorResponse<string>(ex.Message);
            }
        }

        public IResponse<string> UpdateStatus(OrdersDTO.OrdersUpdateStatus model)
        {
            try
            {   
                _parameterList.Reset();

                _parameterList.Add("@OrderId", model.Id);
                _parameterList.Add("@Status", model.Status ?? (object)DBNull.Value);

                var requestResult = _databaseExecutions.ExecuteQuery("SpUpdate_Order", _parameterList);
                return new SuccessResponse<string>(Messages.Update("Status"));
                
            }
            catch (Exception ex)
            {
                return new ErrorResponse<string>(ex.Message);
            }
        }
    }
}
