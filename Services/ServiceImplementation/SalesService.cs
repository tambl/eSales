using DAL.Context;
using Services.DTOs;
using Services.ServiceAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServiceImplementation
{
    public class SalesService : ISalesService
    {
        #region Consultants
        public List<ConsultantDto> GetConsultants()
        {
            using (var dbContext = new SalesBogEntities())
            {
                var dbConsultants = dbContext.Consultants.ToList();
                List<ConsultantDto> consultants = new List<ConsultantDto>();
                foreach (var item in dbConsultants)
                {
                    consultants.Add(new ConsultantDto
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Gender = item.Gender,
                        BirthDate = item.BirthDate.Value.Date,
                        ID = item.ID,
                        PersonalNumber = item.PersonalNumber
                    });
                }
                return consultants;
            }
        }
        public bool CreateConsultant(ConsultantDto consultant)
        {
            using (var dbContext = new SalesBogEntities())
            {
                dbContext.Consultants.Add(new Consultants
                {
                    FirstName = consultant.FirstName,
                    LastName = consultant.LastName,
                    BirthDate = consultant.BirthDate,
                    PersonalNumber = consultant.PersonalNumber,
                    Gender = consultant.Gender,
                    RecommenderConsultantID = consultant.RecommenderConsultantID
                });
                return dbContext.SaveChanges() > 0 ? true : false;

            }
        }

        public ConsultantDto GetConsultantById(int id)
        {
            using (var dbContext = new SalesBogEntities())
            {
                var dbConsultant = dbContext.Consultants.Find(id);
                ConsultantDto consultantDto = new ConsultantDto
                {
                    ID = dbConsultant.ID,
                    FirstName = dbConsultant.FirstName,
                    LastName = dbConsultant.LastName,
                    PersonalNumber = dbConsultant.PersonalNumber,
                    BirthDate = dbConsultant.BirthDate,
                    Gender = dbConsultant.Gender,
                    RecommenderConsultantID = dbConsultant.RecommenderConsultantID
                };
                return consultantDto;
            }
        }

        public bool EditConultant(ConsultantDto consultant)
        {
            using (var dbContext = new SalesBogEntities())
            {
                var dbConsultant = dbContext.Consultants.Where(s => s.ID == consultant.ID).FirstOrDefault();
                dbConsultant.FirstName = consultant.FirstName;
                dbConsultant.LastName = consultant.LastName;
                dbConsultant.Gender = consultant.Gender;
                dbConsultant.BirthDate = consultant.BirthDate;
                dbConsultant.RecommenderConsultantID = consultant.RecommenderConsultantID;
                dbConsultant.PersonalNumber = consultant.PersonalNumber;
                return dbContext.SaveChanges() > 0 ? true : false;
            }
        }

        public bool DeleteConsultant(int id)
        {
            using (var dbContext = new SalesBogEntities())
            {
                Consultants consultant = dbContext.Consultants.Find(id);
                dbContext.Consultants.Remove(consultant);
                return dbContext.SaveChanges() > 0 ? true : false;
            }

        }
        public bool CheckIsConsultantRecommender(int? id, string personalNo)
        {
            using (var dbContext = new SalesBogEntities())
            {
                if (id != null)
                {
                    var currentRecommenderConsultant = dbContext.Consultants.Where(w => w.ID == id).FirstOrDefault();

                    while (currentRecommenderConsultant.PersonalNumber != personalNo)
                    {
                        return CheckIsConsultantRecommender(currentRecommenderConsultant.RecommenderConsultantID, personalNo);
                    }
                    return true;

                }
                return false;
            }
        }
        #endregion

        #region Products
        public List<ProductDto> GetProducts()
        {
            using (var dbContext = new SalesBogEntities())
            {
                var dbProducts = dbContext.Products.ToList();
                List<ProductDto> data = new List<ProductDto>();
                foreach (var item in dbProducts)
                {
                    data.Add(new ProductDto
                    {
                        ID = item.ID,
                        Price = (decimal)item.Price,
                        ProductCode = item.ProductCode,
                        ProductName = item.ProductName

                    });
                }
                return data;
            }
        }
        public bool CreateProduct(ProductDto product)
        {
            using (var dbContext = new SalesBogEntities())
            {
                dbContext.Products.Add(new Products
                {
                    Price = product.Price,
                    ProductCode = product.ProductCode,
                    ProductName = product.ProductName
                });
                return dbContext.SaveChanges() > 0 ? true : false;

            }
        }
        public ProductDto GetProductById(int id)
        {
            using (var dbContext = new SalesBogEntities())
            {
                var dbProduct = dbContext.Products.Find(id);
                ProductDto data = new ProductDto
                {
                    ID = dbProduct.ID,
                    Price = (decimal)dbProduct.Price,
                    ProductName = dbProduct.ProductName,
                    ProductCode = dbProduct.ProductCode
                };
                return data;
            }
        }
        public bool EditProduct(ProductDto product)
        {
            using (var dbContext = new SalesBogEntities())
            {
                var dbProduct = dbContext.Products.Where(s => s.ID == product.ID).FirstOrDefault();
                dbProduct.Price = product.Price;
                dbProduct.ProductCode = product.ProductCode;
                dbProduct.ProductName = product.ProductName;
                return dbContext.SaveChanges() > 0 ? true : false;
            }
        }
        public bool DeleteProduct(int id)
        {
            using (var dbContext = new SalesBogEntities())
            {
                Products prod = dbContext.Products.Find(id);
                dbContext.Products.Remove(prod);
                return dbContext.SaveChanges() > 0 ? true : false;
            }
        }
        #endregion

        #region Sales
        public List<SalesDto> GetSales()
        {
            using (var dbContext = new SalesBogEntities())
            {
                var dbSales = dbContext.Sales.ToList();
                List<SalesDto> sales = new List<SalesDto>();
                foreach (var item in dbSales)
                {
                    sales.Add(new SalesDto
                    {
                        ID = item.ID,
                        SaleDate = item.SaleDate,
                        SaleDescription = item.SaleDescription,
                        ConsultantID = (int)item.ConsultantID,
                        Products = item.ProductSales.Select(
                            s => new ProductDto
                            {
                                ID = s.Products.ID,
                                Price = (decimal)s.Products.Price,
                                ProductName = s.Products.ProductName
                            }).ToList()
                    });
                }
                return sales;
            }
        }
        public bool CreateSale(SalesDto sale)
        {
            using (var dbContext = new SalesBogEntities())
            {
                var dbSale = new Sales
                {
                    ConsultantID = sale.ConsultantID,
                    SaleDescription = sale.SaleDescription,
                    SaleDate = DateTime.Now
                };

                var prodSales = new List<ProductSales>();
                var products = new List<Products>();

                foreach (var item in sale.Products)
                {
                    prodSales.Add(new ProductSales { ProductID = item.ID, Sales = dbSale, ProductCount = item.ProductCount });
                }
                dbContext.ProductSales.AddRange(prodSales);

                return dbContext.SaveChanges() > 0 ? true : false;
            }

        }
        //public SalesDto GetSaleById(int id){}
        //public bool EditSales(SalesDto sale){}
        //public bool DeleteSale(int id){}         
        #endregion

        #region Analytics
        public List<SaleConsultantProductsDto> GetSalesByConsultants(DateTime startDate, DateTime endDate)
        {
            using (var dbContext = new SalesBogEntities())
            {

                var result = (from s in dbContext.Sales
                              join ps in dbContext.ProductSales on s.ID equals ps.SaleID
                              join p in dbContext.Products on ps.ProductID equals p.ID
                              join c in dbContext.Consultants on s.ConsultantID equals c.ID
                              where s.SaleDate >= startDate && s.SaleDate < endDate
                              group ps by new { SaleID = s.ID, s.ConsultantID, s.SaleDate, c.PersonalNumber, FullName = c.FirstName + " " + c.LastName } into g
                              select new SaleConsultantProductsDto
                              {
                                  SaleID = g.Key.SaleID,
                                  SaleDate = (DateTime)g.Key.SaleDate,
                                  ConsultantID = (int)g.Key.ConsultantID,
                                  PersonalNumber = g.Key.PersonalNumber,
                                  FullName = g.Key.FullName,
                                  Quantity = g.Sum(t => t.ProductCount),
                                  SumAmount = g.Sum(t => t.ProductCount * t.Products.Price)
                              }).ToList();


                return result;
            }

        }
        public List<SaleConsultantProductsDto> GetSalesByProductPrice(DateTime startDate, DateTime endDate, decimal minPrice, decimal maxPrice)
        {
            using (var dbContext = new SalesBogEntities())
            {

                var result = (from s in dbContext.Sales
                              join ps in dbContext.ProductSales on s.ID equals ps.SaleID
                              join p in dbContext.Products on ps.ProductID equals p.ID
                              join c in dbContext.Consultants on s.ConsultantID equals c.ID
                              where s.SaleDate >= startDate && s.SaleDate < endDate
                               && p.Price >= minPrice && p.Price < maxPrice
                              group ps by new { SaleID = s.ID, s.ConsultantID, s.SaleDate, c.PersonalNumber, FullName = c.FirstName + " " + c.LastName } into g
                              select new SaleConsultantProductsDto
                              {
                                  SaleID = g.Key.SaleID,
                                  SaleDate = (DateTime)g.Key.SaleDate,
                                  ConsultantID = (int)g.Key.ConsultantID,
                                  PersonalNumber = g.Key.PersonalNumber,
                                  FullName = g.Key.FullName,
                                  Quantity = g.Select(l => l.ProductID).Distinct().Count()
                              }).ToList();


                return result;
            }
        }
        public List<SaleConsultantProductsDto> GetConsultantsByProductQuantity(DateTime startDate, DateTime endDate, string productCode, decimal minQuantityOfProducts)
        {
            using (var dbContext = new SalesBogEntities())
            {

                var result = (from s in dbContext.Sales
                              join ps in dbContext.ProductSales on s.ID equals ps.SaleID
                              join p in dbContext.Products on ps.ProductID equals p.ID
                              join c in dbContext.Consultants on s.ConsultantID equals c.ID
                              where s.SaleDate >= startDate && s.SaleDate < endDate
                               && (string.IsNullOrEmpty(productCode) ? 1 == 1 : p.ProductCode == productCode)
                              group ps by new { s.ConsultantID, c.BirthDate, c.PersonalNumber, FullName = c.FirstName + " " + c.LastName, p.ProductCode } into g
                              where g.Count() > minQuantityOfProducts
                              select new SaleConsultantProductsDto
                              {
                                  ConsultantBirthDate = (DateTime)g.Key.BirthDate,
                                  ConsultantID = (int)g.Key.ConsultantID,
                                  PersonalNumber = g.Key.PersonalNumber,
                                  FullName = g.Key.FullName,
                                  ProductCode = g.Key.ProductCode,
                                  Quantity = g.Count()

                              }).ToList();
                return result;
            }
        }
        public List<SaleConsultantProductsDto> GetConsultantsBySummedSales(DateTime? startDate, DateTime? endDate)
        {
            using (var dbContext = new SalesBogEntities())
            {
                var consultants = dbContext.Consultants.ToList();


                GetConsultantsHierarchy(1, null, null);

                var allConsultantOwnSaleSums = (from c in dbContext.Consultants
                                                join s in dbContext.Sales on c.ID equals s.ConsultantID into sf
                                                from sl in sf.DefaultIfEmpty()
                                                join sp in dbContext.ProductSales on sl.ID equals sp.SaleID into spf
                                                from spl in spf.DefaultIfEmpty()
                                                where
                                                     startDate == null && endDate != null ? sl.SaleDate < endDate :
                                                    endDate == null && startDate != null ? sl.SaleDate >= startDate :
                                                    startDate != null && endDate != null ? sl.SaleDate >= startDate && sl.SaleDate < endDate : 1 == 1
                                                group sl by new { c.ID } into g
                                                select new { g.Key.ID, Count = g.Distinct().Count(p => p.ID != null) }).ToDictionary(kvp => kvp.ID, kvp => kvp.Count);

                Dictionary<int, int> consultantHierarchySums = new Dictionary<int, int>();
                consultantHierarchySums.Add(1, 9);


                foreach (var item in allConsultantOwnSaleSums)
                {
                    consultantHierarchySums.Add(GetConsultantsHierarchy(item.Key, startDate, endDate));
                }

                var all = allConsultantOwnSaleSums.Union(consultantHierarchySums).GroupBy(g => g.Key).Select(s => new { s.Key, Quantity = s.Sum(l => l.Value) }).ToDictionary(kvp => kvp.Key, kvp => kvp.Quantity); ;


                var result = (from g in consultants
                              join k in all on g.ID equals k.Key
                              join s in allConsultantOwnSaleSums on g.ID equals s.Key
                              group new { k, s } by new { g.ID } into g2
                              select new SaleConsultantProductsDto
                              {
                                  //ConsultantBirthDate = (DateTime)g.BirthDate,
                                  ConsultantID = (int)g2.Key.ID,
                                  //PersonalNumber = g.PersonalNumber,
                                  //FullName = g.FirstName,
                                  Quantity = g2.Sum(q => q.s.Value),
                                  QuantityOverHierarchy = g2.Sum(q => q.k.Value)
                              }).ToList();
                return result;
            }
        }

        public Dictionary<int, int> GetConsultantsHierarchy(int consultantID, DateTime? startDate, DateTime? endDate)
        {
            using (var dbContext = new SalesBogEntities())
            {
                var 
                var result = (from c in dbContext.Consultants
                              join s in dbContext.Sales on c.ID equals s.ConsultantID into sf
                              from sl in sf.DefaultIfEmpty()
                              join sp in dbContext.ProductSales on sl.ID equals sp.SaleID into spf
                              from spl in spf.DefaultIfEmpty()
                              where
                                   startDate == null && endDate != null ? sl.SaleDate < endDate :
                                       endDate == null && startDate != null ? sl.SaleDate >= startDate :
                                       startDate != null && endDate != null ? sl.SaleDate >= startDate && sl.SaleDate < endDate : 1 == 1
                                && c.RecommenderConsultantID == consultantID
                              group sl by new { c.ID } into g
                              select new { g.Key.ID, Count = g.Distinct().Count(p => p.ID != null) }).ToDictionary(kvp => kvp.ID, kvp => kvp.Count);
                return result;


            }
        }



        #endregion
    }
}
