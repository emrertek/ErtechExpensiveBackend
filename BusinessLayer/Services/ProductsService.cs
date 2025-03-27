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
using static DataAccessLayer.DTOs.ProductsDTO;

namespace BusinessLayer.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ParameterList _parameterList;
        private readonly DatabaseExecutions _databaseExecutions;

        public ProductsService(ParameterList parameterList, DatabaseExecutions databaseExecutions)
        {
            _parameterList = parameterList;
            _databaseExecutions = databaseExecutions;
        }

        public IResponse<string> Create(ProductsDTO.ProductsCreate model)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@ProductName", model.ProductName);
                _parameterList.Add("@Price", model.Price);
                _parameterList.Add("@Stock", model.Stock);
                _parameterList.Add("@Category", model.Category);

                _databaseExecutions.ExecuteQuery("SpCreate_Product", _parameterList);
                return new SuccessResponse<string>("Product successfully created.");
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
                _parameterList.Add("@ProductID", id);

                var requestResult = _databaseExecutions.ExecuteDeleteQuery("SpDelete_Product", _parameterList);
                if (requestResult > 0)
                {
                    return new SuccessResponse<string>(Messages.Delete("Product"));
                }
                else
                {
                    return new ErrorResponse<string>(Messages.DeleteError("Product"));
                }
            }
            catch (Exception ex)
            {
                return new ErrorResponse<string>(ex.Message);
            }
        }

        public IResponse<IEnumerable<ProductsDTO.ProductsQuery>> FindById(int id)
        {
            try
            {
                _parameterList.Add("@ProductID", id);

                var jsonResult = _databaseExecutions.ExecuteQuery("SpGetByID_Product", _parameterList);
                var selectedProduct = JsonConvert.DeserializeObject<IEnumerable<ProductsQuery>>(jsonResult);

                if (selectedProduct.IsNullOrEmpty())
                {
                    return new ErrorResponse<IEnumerable<ProductsQuery>>(Messages.NotFound("Product"));
                }
                return new SuccessResponse<IEnumerable<ProductsQuery>>(selectedProduct);
            }
            catch (Exception ex)
            {
                return new ErrorResponse<IEnumerable<ProductsQuery>>(ex.Message);
            }
        }

        public IResponse<IEnumerable<ProductsDTO.ProductsQuery>> ListAll()
        {
            try
            {
                _parameterList.Reset();
                var jsonResult = _databaseExecutions.ExecuteQuery("SpGetAll_Products", _parameterList);
                var listProduct = JsonConvert.DeserializeObject<List<ProductsQuery>>(jsonResult);

                return new SuccessResponse<IEnumerable<ProductsQuery>>(listProduct);

            }
            catch (Exception ex)
            {
                return new ErrorResponse<IEnumerable<ProductsQuery>>(ex.Message);
            }
        }

        public IResponse<string> Update(ProductsDTO.ProductsUpdate model)
        {
            try
            {
                _parameterList.Reset();
                _parameterList.Add("@ProductID", model.Id);
                _parameterList.Add("@ProductName", model.ProductName);
                _parameterList.Add("@Price", model.Price);
                _parameterList.Add("@Stock", model.Stock);
                _parameterList.Add("@Category", model.Category);

                _databaseExecutions.ExecuteQuery("SpUpdate_Product", _parameterList);


                return new SuccessResponse<string>(Messages.Update("Product"));
            }
            catch (Exception ex)
            {
                return new ErrorResponse<string>(ex.Message);
            }
        }
    }
}
