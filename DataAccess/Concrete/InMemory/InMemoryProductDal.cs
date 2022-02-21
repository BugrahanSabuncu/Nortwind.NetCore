using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    class InMemoryProductDal : IProductDal
    {
        List<Product> _products;

        public InMemoryProductDal()
        {
            _products = new List<Product>() {
            new Product(){ProductId=1,CategoryId=2,ProductName="Bardak",UnitPrice=50,UnitsInStock=45},
            new Product(){ProductId=2,CategoryId=2,ProductName="Tabak",UnitPrice=50,UnitsInStock=15},
            new Product(){ProductId=3,CategoryId=2,ProductName="Çatal",UnitPrice=40,UnitsInStock=30},
            new Product(){ProductId=4,CategoryId=3,ProductName="Tava",UnitPrice=20,UnitsInStock=15},
            new Product(){ProductId=5,CategoryId=3,ProductName="Tencere",UnitPrice=15,UnitsInStock=60},
            };
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            Product deleteToProduct = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            _products.Remove(deleteToProduct);
        }

        public List<Product> GetAllByCategories(int categori)
        {
            return _products.Where(p => p.CategoryId == categori).ToList();
        }

       

        public void Update(Product product)
        {
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductId = product.ProductId;
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            return _products;
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<ProductDetailDto> GetProductsDetails()
        {
            throw new NotImplementedException();
        }
    }
}
