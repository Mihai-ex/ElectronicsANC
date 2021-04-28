using ElectronicsANC.Models;
using ElectronicsANC.Models.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicsANC.Repository
{
    public class ProductRepository
    {
        public ElectronicsDbDataContext dbContext;

        public ProductRepository()
        {
            dbContext = new ElectronicsDbDataContext();
        }

        public ProductRepository(ElectronicsDbDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ProductModel> GetAllProducts()
        {
            List<ProductModel> productList = new List<ProductModel>();

            foreach (Product dbProduct in dbContext.Products)
                AddDbObjectToModelCollection(productList, dbProduct);

            return productList;
        }

        public List<ProductModel> GetAllProductsByCategoryId(Guid id)
        {
            List<ProductModel> productList = new List<ProductModel>();

            foreach (Product dbProduct in dbContext.Products)
                if (dbProduct.IdCategory == id)
                    AddDbObjectToModelCollection(productList, dbProduct);

            return productList;
        }

        public ProductModel GetProdctById(Guid id)
        {
            var product = dbContext.Products.FirstOrDefault(x => x.IdProduct == id);

            return MapDbObjectToModel(product);
        }

        public ProductModel GetProductByCategoryId(Guid id)
        {
            var product = dbContext.Products.FirstOrDefault(x => x.IdCategory == id);

            return MapDbObjectToModel(product);
        }

        //public List<ProductModel> GetProductsByName(string name)
        //{
        //    List<ProductModel> productList = new List<ProductModel>();

        //    foreach (Product dbProduct in dbContext.Products)
        //        if(dbProduct.ProductName.Equals(name))
        //            AddDbObjectToModelCollection(productList, dbProduct);

        //    return productList;
        //}

        public List<ProductModel> GetProductsByManufacturer(string manufacturer)
        {
            List<ProductModel> produstList = new List<ProductModel>();

            foreach (Product dbProduct in dbContext.Products)
                if (dbProduct.Manufacturer.Equals(manufacturer))
                    AddDbObjectToModelCollection(produstList, dbProduct);

            return produstList;
        }

        public List<ProductModel> GetProductsByPrice(decimal price)
        {
            List<ProductModel> productList = new List<ProductModel>();

            foreach (Product dbProduct in dbContext.Products)
                if (dbProduct.Price == price)
                    AddDbObjectToModelCollection(productList, dbProduct);

            return productList;
        }

        public List<ProductModel> GetProductsByRating(int rating)
        {
            List<ProductModel> productList = new List<ProductModel>();

            foreach (Product dbProduct in dbContext.Products)
                if (dbProduct.Rating == rating)
                    AddDbObjectToModelCollection(productList, dbProduct);

            return productList;
        }

        public List<ProductModel> OrderByDescendingParameterWithID(List<ProductModel> models, string parameter, Guid id)
        {
            if(parameter == "Name")            
                return models.OrderByDescending(x => x.ProductName).ToList();

            if (parameter == "Manufacturer")
                return models.OrderByDescending(x => x.Manufacturer).ToList();

            if (parameter == "Price")
                return models.OrderByDescending(x => x.Price).ToList();

            if (parameter == "Warranty")
                return models.OrderByDescending(x => x.Warranty).ToList();

            if (parameter == "Rating")
                return models.OrderByDescending(x => x.Rating).ToList();

            return models;
        }

        public List<ProductModel> OrderByAscendingParameterWithID(List<ProductModel> models, string parameter, Guid id)
        {
            if (parameter == "Name")
                return models.OrderBy(x => x.ProductName).ToList();

            if (parameter == "Manufacturer")
                return models.OrderBy(x => x.Manufacturer).ToList();

            if (parameter == "Price")
                return models.OrderBy(x => x.Price).ToList();

            if (parameter == "Warranty")
                return models.OrderBy(x => x.Warranty).ToList();

            if (parameter == "Rating")
                return models.OrderBy(x => x.Rating).ToList();

            return models;
        }

        public void InsertProduct(ProductModel product)
        {
            product.IdProduct = Guid.NewGuid();

            dbContext.Products.InsertOnSubmit(MapModelToDbObject(product));
            dbContext.SubmitChanges();
        }

        public void UpdateProduct(ProductModel product)
        {
            Product dbProduct = dbContext.Products.FirstOrDefault(x => x.IdProduct == product.IdProduct);

            if(dbProduct != null)
            {
                dbProduct.IdProduct = product.IdProduct;
                dbProduct.IdCategory = product.IdCategory;
                dbProduct.ProductName = product.ProductName;
                dbProduct.Description = product.Description;
                dbProduct.Manufacturer = product.Manufacturer;
                dbProduct.Warranty = product.Warranty;
                dbProduct.Price = product.Price;
                dbProduct.Rating = product.Rating;
                dbContext.SubmitChanges();
            }
        }

        public void DeleteProduct(Guid id)
        {
            Product dbProduct = dbContext.Products.FirstOrDefault(x => x.IdProduct == id);

            if(dbProduct != null)
            {
                dbContext.Products.DeleteOnSubmit(dbProduct);
                dbContext.SubmitChanges();
            }
        }

        private void AddDbObjectToModelCollection(List<ProductModel> productList, Product dbProduct)
        {
            productList.Add(MapDbObjectToModel(dbProduct));
        }

        private ProductModel MapDbObjectToModel(Product dbProduct)
        {
            ProductModel product = new ProductModel();

            if(dbProduct != null)
            {
                product.IdProduct = dbProduct.IdProduct;
                product.IdCategory = dbProduct.IdCategory;
                product.Description = dbProduct.Description;
                product.Manufacturer = dbProduct.Manufacturer;
                product.Price = dbProduct.Price;
                product.ProductName = dbProduct.ProductName;
                product.Rating = dbProduct.Rating;
                product.Warranty = dbProduct.Warranty;

                return product;
            }

            return null;
        }

        private Product MapModelToDbObject(ProductModel product)
        {
            Product dbProduct = new Product();

            if(product != null)
            {
                dbProduct.IdProduct = product.IdProduct;
                dbProduct.IdCategory = product.IdCategory;
                dbProduct.ProductName = product.ProductName;
                dbProduct.Description = product.Description;
                dbProduct.Manufacturer = product.Manufacturer;
                dbProduct.Warranty = product.Warranty;
                dbProduct.Price = product.Price;
                dbProduct.Rating = product.Rating;

                return dbProduct;
            }

            return null;
        }
    }
}