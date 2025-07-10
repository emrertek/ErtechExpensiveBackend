using BusinessLayer.Common.Interface;
using BusinessLayer.Common.Response;
using BusinessLayer.Interfaces;
using DataAccessLayer.Connection;
using DataAccessLayer.DTOs;
using System.Transactions;

public class OrderTransactionService : IOrderTransactionService
{
    private readonly DatabaseExecutions _databaseExecutions;

    public OrderTransactionService(DatabaseExecutions databaseExecutions)
    {
        _databaseExecutions = databaseExecutions;
    }
    public IResponse<string> CreateCompleteOrder(
        OrdersDTO.OrdersCreate orderDto,
        List<OrderDetailsDTO.OrderDetailsCreate> orderDetails,
        PaymentDTO.PaymentCreate paymentDto,
        CustomerAddressesDTO.CustomerAddressCreate addressDto)
    {
        try
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(2)))
            {
                // 1. Adres Kaydı
                var addressParams = new ParameterList
                {
                    { "@CustomerID", addressDto.CustomerId },
                    { "@AddressLine", addressDto.AddressLine },
                    { "@City", addressDto.City },
                    { "@PostalCode", addressDto.PostalCode },
                    { "@Country", addressDto.Country }
                };

                _databaseExecutions.ExecuteDeleteQuery("SpCreate_CustomerAddress", addressParams);

                // 2. Sipariş Oluştur
                var orderParams = new ParameterList
                {
                    { "@CustomerID", orderDto.CustomerId },
                    { "@OrderDate", orderDto.OrderDate },
                    { "@TotalAmount", orderDto.TotalAmount },
                    { "@Status", orderDto.Status }
                };

                int orderId = _databaseExecutions.ExecuteQueryWithOutput("SpCreate_Order", orderParams, "@OrderID");
                string orderNo = $"ORD-{orderId.ToString("D6")}";

                // 3. Sipariş Detayları Kaydet
                foreach (var detail in orderDetails)
                {
                    var detailParams = new ParameterList
                    {
                        { "@OrderNo", orderNo },
                        { "@ProductID", detail.ProductId },
                        { "@Quantity", detail.Quantity },
                        { "@SubTotal", detail.SubTotal }
                    };

                    _databaseExecutions.ExecuteDeleteQuery("SpCreate_OrderDetail", detailParams);
                }

                // 4. Ödeme Bilgisi Kaydet
                var paymentParams = new ParameterList
                {
                    { "@OrderNo", orderNo },
                    { "@PaymentMethod", paymentDto.PaymentMethod },
                    { "@PaymentDate", paymentDto.PaymentDate },
                    { "@Amount", paymentDto.Amount }
                };

                _databaseExecutions.ExecuteDeleteQuery("SpCreate_Payment", paymentParams);

                scope.Complete();
                return new SuccessResponse<string>($"Sipariş başarıyla oluşturuldu. Sipariş No: {orderNo}");
            }
        }
        catch (Exception ex)
        {
            return new ErrorResponse<string>($"Sipariş oluşturulamadı: {ex.Message}");
        }
    }
}
