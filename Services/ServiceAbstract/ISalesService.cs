using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiceAbstract
{
    public interface ISalesService
    {
        #region Consultants
        List<ConsultantDto> GetConsultants();
        bool CreateConsultant(ConsultantDto consultant);
        ConsultantDto GetConsultantById(int id);
        bool EditConultant(ConsultantDto consultant);
        bool DeleteConsultant(int id);
        bool CheckIsConsultantRecommender(int? id, string personalNo);

        #endregion

        #region Products
        List<ProductDto> GetProducts();
        bool CreateProduct(ProductDto product);
        ProductDto GetProductById(int id);
        bool EditProduct(ProductDto product);
        bool DeleteProduct(int id);
        #endregion

        #region Sales

        List<SalesDto> GetSales();
        //bool CreateSale(SalesDto sale);
        //SalesDto GetSaleById(int id);
        //bool EditSales(SalesDto sale);
        //bool DeleteSale(int id); 
        #endregion

        #region Analytics
        List<SaleConsultantProductsDto> GetSalesByConsultants(DateTime startDate, DateTime endDate);
        List<SaleConsultantProductsDto> GetSalesByProductPrice(DateTime startDate, DateTime endDate, decimal minPrice, decimal maxPrice);
        List<SaleConsultantProductsDto> GetConsultantsByProductQuantity(DateTime startDate, DateTime endDate, string productCode, decimal minQuantityOfProducts);
        List<SaleConsultantProductsDto> GetConsultantsBySummedSales(DateTime? startDate, DateTime? endDate);
        List<SaleConsultantProductsDto> GetConsultantsByTopSoldProducts(DateTime? startDate, DateTime? endDate);

        #endregion

    }
}
