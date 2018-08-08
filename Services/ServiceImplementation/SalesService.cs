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
    }
}
