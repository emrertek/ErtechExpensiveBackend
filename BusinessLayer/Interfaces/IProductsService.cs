using BusinessLayer.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.DTOs.CustomersDTO;
using static DataAccessLayer.DTOs.ProductsDTO;

namespace BusinessLayer.Interfaces
{
    public interface IProductsService
        //create,delete,getall,getbyid,update
    {
        IResponse<IEnumerable<ProductsQuery>> ListAll();
        IResponse<IEnumerable<ProductsQuery>> FindById(int id);
        IResponse<string> Update(ProductsUpdate model);
        IResponse<string> Create(ProductsCreate model);
        IResponse<string> Delete(int id);
    }
}
