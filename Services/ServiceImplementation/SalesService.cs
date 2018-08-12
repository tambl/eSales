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
                        Consultant = new ConsultantDto {FirstName= item.Consultants.FirstName ,LastName= item.Consultants.LastName , ID=item.Consultants.ID, PersonalNumber = item.Consultants.PersonalNumber},
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
        public SalesDto GetSaleById(int? id)
        {
            using (var dbContext = new SalesBogEntities())
            {
                var dbSale = dbContext.Sales.Find(id);
                SalesDto saleDto = new SalesDto
                {
                    ID = dbSale.ID,
                    Consultant = new ConsultantDto { ID = dbSale.ID, FirstName = dbSale.Consultants.FirstName, LastName = dbSale.Consultants.LastName },
                    SaleDate = dbSale.SaleDate,
                    SaleDescription = dbSale.SaleDescription,
                    Products = dbSale.ProductSales.Select(s => new ProductDto
                    {
                        ID = s.Products.ID,
                        Price = (decimal)s.Products.Price,
                        ProductCode = s.Products.ProductCode,
                        ProductName = s.Products.ProductName,
                        ProductCount = (int)s.ProductCount
                    }).ToList()
                };
                return saleDto;
            }
        }
        public bool EditSales(SalesDto sale)
        {
            using (var dbContext = new SalesBogEntities())
            {
                var dbSale = dbContext.Sales.Where(w => w.ID == sale.ID).FirstOrDefault();

                dbSale.SaleDescription = sale.SaleDescription;
                dbSale.ConsultantID = sale.ConsultantID;

                //Existing Products Edit
                var existingDBProducts = dbContext.ProductSales.Where(w => w.SaleID == sale.ID).ToList();

                var modelProducts = sale.Products.Where(w => w.IsDeleted == false && w.ID != 0).ToList();

                var equalProducts = (from e in existingDBProducts
                                     join m in modelProducts on e.ProductID equals m.ID
                                     where e.SaleID == sale.ID
                                     select e).ToList();

                if (equalProducts.Count > 0)
                {
                    foreach (var item in equalProducts)
                    {
                        var correspondingProduct = modelProducts.Where(w => w.ID == item.ProductID).FirstOrDefault();
                        item.ProductID = correspondingProduct.ID;
                        item.ProductCount = correspondingProduct.ProductCount;
                    }
                }

                //New Products Add
                var newProducts = modelProducts.Where(s => !existingDBProducts.Where(w => w.SaleID == sale.ID).Select(a => a.ProductID).Contains(s.ID)).ToList();
                if (newProducts.Count() > 0)
                {
                    var prodSales = new List<ProductSales>();
                    foreach (var item in newProducts)
                    {
                        prodSales.Add(new ProductSales { ProductID = item.ID, Sales = dbSale, ProductCount = item.ProductCount });
                    }
                    dbContext.ProductSales.AddRange(prodSales);
                }
                // Deleted Product

                var deletedProducts = sale.Products.Where(w => w.IsDeleted == true).ToList();
                if (deletedProducts.Count() > 0)
                {
                    foreach (var item in deletedProducts)
                    {
                        var deletedItem = dbContext.ProductSales.Where(w => w.ProductID == item.ID && w.SaleID == dbSale.ID).FirstOrDefault();

                        dbContext.ProductSales.Remove(deletedItem);
                    }
                }

                return dbContext.SaveChanges() > 0 ? true : false;
            }

        }
        public bool DeleteSale(int id){
            using (var dbContext = new SalesBogEntities())
            {
                Sales sale = dbContext.Sales.Find(id);


                dbContext.Sales.Remove(sale);
                return dbContext.SaveChanges() > 0 ? true : false;
            }
        }         
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

                Dictionary<int, int> consultantHierarchySums = new Dictionary<int, int>();

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
                                                select new { g.Key.ID, Count = g.Distinct().Count(p => p.ID != null) }).ToDictionary(d => d.ID, d => d.Count);


                foreach (var consultant in consultants)
                {
                    IEnumerable<Consultants> allNodes = TraverseHierarchy(consultant, node => node.Consultants1);

                    var ids = allNodes.Select(s => s.ID);

                    var res = (from c in dbContext.Consultants
                               join s in dbContext.Sales on c.ID equals s.ConsultantID into sf
                               from sl in sf.DefaultIfEmpty()
                               join sp in dbContext.ProductSales on sl.ID equals sp.SaleID into spf
                               from spl in spf.DefaultIfEmpty()
                               where
                                    startDate == null && endDate != null ? sl.SaleDate < endDate :
                                   endDate == null && startDate != null ? sl.SaleDate >= startDate :
                                   startDate != null && endDate != null ? sl.SaleDate >= startDate && sl.SaleDate < endDate : 1 == 1
                                   && ids.Contains(c.ID)
                               group sl by new { c.ID } into g
                               select new { g.Key.ID, Count = g.Distinct().Count(p => p.ID != null) }).Sum(q => q.Count);

                    consultantHierarchySums.Add(consultant.ID, res);
                }


                var result = (from c in consultants
                              join k in consultantHierarchySums on c.ID equals k.Key
                              join s in allConsultantOwnSaleSums on c.ID equals s.Key
                              group new { k, s } by new { c.ID, c.BirthDate, c.PersonalNumber, FullName = c.FirstName + " " + c.LastName } into g2
                              select new SaleConsultantProductsDto
                              {
                                  ConsultantBirthDate = (DateTime)g2.Key.BirthDate,
                                  ConsultantID = (int)g2.Key.ID,
                                  PersonalNumber = g2.Key.PersonalNumber,
                                  FullName = g2.Key.FullName,
                                  Quantity = g2.Sum(q => q.s.Value),
                                  QuantityOverHierarchy = g2.Sum(q => q.k.Value)
                              }).OrderByDescending(o => o.QuantityOverHierarchy).ToList();
                return result;
            }
        }
        public List<SaleConsultantProductsDto> GetConsultantsByTopSoldProducts(DateTime? startDate, DateTime? endDate)
        {
            using (var dbContext = new SalesBogEntities())
            {
                var consultants = dbContext.Consultants.ToList();
                var products = dbContext.Products.ToList();

                List<ConsultantSale> allConsultantsTopSales = new List<ConsultantSale>();
                List<ConsultantSale> allConsultantsTopProfitableSales = new List<ConsultantSale>();


                foreach (var consultant in consultants)
                {
                    var consultantTopSale = (from c in dbContext.Consultants
                                             join s in dbContext.Sales on c.ID equals s.ConsultantID into sf
                                             from sl in sf.DefaultIfEmpty()
                                             join sp in dbContext.ProductSales on sl.ID equals sp.SaleID into spf
                                             from spl in spf.DefaultIfEmpty()
                                             where
                                                  startDate == null && endDate != null ? sl.SaleDate < endDate :
                                                 endDate == null && startDate != null ? sl.SaleDate >= startDate :
                                                 startDate != null && endDate != null ? sl.SaleDate >= startDate && sl.SaleDate < endDate : 1 == 1
                                                 && c.ID == consultant.ID
                                             group sl by new { c.ID, spl.ProductID } into g
                                             select new ConsultantSale { ConsultantID = g.Key.ID, ProductID = g.Key.ProductID, MaxValue = g.Distinct().Count(p => p.ID != null) }).OrderByDescending(d => d.MaxValue).FirstOrDefault();

                    var consultantTopProfitableSale = (from c in dbContext.Consultants
                                                       join s in dbContext.Sales on c.ID equals s.ConsultantID into sf
                                                       from sl in sf.DefaultIfEmpty()
                                                       join sp in dbContext.ProductSales on sl.ID equals sp.SaleID into spf
                                                       from spl in spf.DefaultIfEmpty()
                                                       where
                                                            startDate == null && endDate != null ? sl.SaleDate < endDate :
                                                           endDate == null && startDate != null ? sl.SaleDate >= startDate :
                                                           startDate != null && endDate != null ? sl.SaleDate >= startDate && sl.SaleDate < endDate : 1 == 1
                                                           && c.ID == consultant.ID
                                                       group spl by new { c.ID, spl.ProductID } into g
                                                       select new ConsultantSale { ConsultantID = g.Key.ID, ProductID = g.Key.ProductID, MaxValue = g.Sum(s => s.ProductCount * s.Products.Price) }).OrderByDescending(d => d.MaxValue).FirstOrDefault();


                    allConsultantsTopSales.Add(consultantTopSale);
                    allConsultantsTopProfitableSales.Add(consultantTopProfitableSale);
                }


                var result = (from c in consultants
                              join k in allConsultantsTopSales on c.ID equals k.ConsultantID
                              join s in allConsultantsTopProfitableSales on c.ID equals s.ConsultantID
                              join p1 in products on k.ProductID equals p1.ID
                              join p2 in products on s.ProductID equals p2.ID
                              group new { k, s } by new
                              {
                                  c.ID,
                                  c.BirthDate,
                                  c.PersonalNumber,
                                  FullName = c.FirstName + " " + c.LastName,
                                  TopCode = p1.ProductCode,
                                  TopName = p1.ProductName,
                                  ProfitableCode = p2.ProductCode,
                                  ProfitableName = p2.ProductName
                              } into g2
                              select new SaleConsultantProductsDto
                              {
                                  ConsultantBirthDate = (DateTime)g2.Key.BirthDate,
                                  ConsultantID = (int)g2.Key.ID,
                                  PersonalNumber = g2.Key.PersonalNumber,
                                  FullName = g2.Key.FullName,
                                  TopSoldProductCode = g2.Key.TopCode,
                                  TopSoldProductName = g2.Key.TopName,
                                  TopSoldProductTotalQuantity = (int)g2.Sum(s => s.k.MaxValue),
                                  TopProfitableProductCode = g2.Key.ProfitableCode,
                                  TopProfitableProductName = g2.Key.ProfitableName,
                                  TopProfitableProductTotalAmount = (decimal)g2.Sum(s => s.s.MaxValue)
                              }).OrderByDescending(o => o.TopProfitableProductTotalAmount).ToList();
                return result;
            }
        }
        public static IEnumerable<T> TraverseHierarchy<T>(T item, Func<T, IEnumerable<T>> childSelector)
        {
            var stack = new Stack<T>();
            stack.Push(item);
            while (stack.Any())
            {
                var next = stack.Pop();
                yield return next;
                foreach (var child in childSelector(next))
                    stack.Push(child);
            }
        }

        #endregion
    }

    public class ConsultantSale
    {
        public int ConsultantID { get; set; }
        public int? ProductID { get; set; }
        public decimal? MaxValue { get; set; }
    }
}
