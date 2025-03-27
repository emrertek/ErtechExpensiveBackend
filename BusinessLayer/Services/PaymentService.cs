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
using static DataAccessLayer.DTOs.OrderDetailsDTO;
using static DataAccessLayer.DTOs.PaymentDTO;

namespace BusinessLayer.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ParameterList _parameterList;
        private readonly DatabaseExecutions _databaseExecutions;

        public PaymentService(ParameterList parameterList, DatabaseExecutions databaseExecutions)
        {
            _parameterList = parameterList;
            _databaseExecutions = databaseExecutions;
        }

        public IResponse<string> Create(PaymentDTO.PaymentCreate model)
        {
            try
            {
                _parameterList.Reset();

                _parameterList.Add("@PaymentMethod", model.PaymentMethod);
                _parameterList.Add("@PaymentDate", model.PaymentDate);
                _parameterList.Add("@Amount", model.Amount);

                var requestResult = _databaseExecutions.ExecuteQuery("SpCreate_Payment", _parameterList);
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
                _parameterList.Add("@PaymentID", id);

                var requestResult = _databaseExecutions.ExecuteDeleteQuery("SpDelete_Payment", _parameterList);
                if (requestResult > 0)
                {
                    return new SuccessResponse<string>(Messages.Delete("Payment success"));
                }
                else
                {
                    return new ErrorResponse<string>(Messages.DeleteError("Payment error"));
                }

            }
            catch (Exception ex)
            {
                return new ErrorResponse<string>(ex.Message);
            }
                            
        }

        public IResponse<IEnumerable<PaymentDTO.PaymentQuery>> GetByID(int id)
        {
            try
            {
                _parameterList.Add("@PaymentID", id);

                var jsonResult = _databaseExecutions.ExecuteQuery("SpGetByID_Payment", _parameterList);
                var selectPayment = JsonConvert.DeserializeObject<List<PaymentQuery>>(jsonResult);

                if (selectPayment.IsNullOrEmpty())
                {
                    return new ErrorResponse<IEnumerable<PaymentQuery>>(Messages.NotFound("Payment bulunamadı"));
                }
                return new SuccessResponse<IEnumerable<PaymentQuery>>(selectPayment);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<IEnumerable<PaymentQuery>>(ex.Message);
            }
        }

        public IResponse<IEnumerable<PaymentQuery>> GetByMethod(string paymentMethod)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@PaymentMethod", paymentMethod);

                var jsonResult = _databaseExecutions.ExecuteQuery("SpGetByMethod_Payments", _parameterList);
                var selectPayment = JsonConvert.DeserializeObject<List<PaymentQuery>>(jsonResult);

                return new SuccessResponse<IEnumerable<PaymentQuery>>(selectPayment);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<IEnumerable<PaymentQuery>>(ex.Message);
            }
        }

        public IResponse<IEnumerable<PaymentDTO.PaymentQuery>> ListAll()
        {
            try
            {
                _parameterList.Reset();
                var jsonResult = _databaseExecutions.ExecuteQuery("SpGetAll_Payments", _parameterList);
                var selectPayment = JsonConvert.DeserializeObject<List<PaymentQuery>>(jsonResult);

                return new SuccessResponse<IEnumerable<PaymentQuery>>(selectPayment);
            }

            catch (Exception ex)
            {
                return new ErrorResponse<IEnumerable<PaymentQuery>>(ex.Message);
            }
        }

        public IResponse<string> Update(PaymentDTO.PaymentUpdate model)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@PaymentID", model.Id);
                _parameterList.Add("@PaymentMethod", model.PaymentMethod);
                _parameterList.Add("@PaymentDate", model.PaymentDate);
                _parameterList.Add("@Amount", model.Amount);

                var requestResult = _databaseExecutions.ExecuteQuery("SpUpdate_Payment", _parameterList);

                return new SuccessResponse<string>("Başarılı şekilde oluşturuldu");

            }
            catch (Exception ex)
            {
                return new ErrorResponse<string>(ex.Message);
            }
        }
    }
}
