﻿using Bussines.Abstract;
using Bussines.BussinesAspects.Autofac;
using Bussines.CCS;
using Bussines.Constants;
using Bussines.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Bussines;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bussines.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;


        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }
        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidatior))]
        public IResult Add(Product product)
        {
            //ValidationTool.Validate(new ProductValidatior(), product);

            var result = BussinesRules.Run(
                CheckIfProductCountofCategoryCorrect(product.CategoryID),
                CheckIfProductNameExists(product.ProductName),CheckIfCategoryLimitExceded());
            if (result!=null)
            {                
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult();
        }

        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryID == id), Messages.ProductListed);
        }

        public IDataResult<Product> GetById(int id)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == id));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice < max && p.UnitPrice > min), Messages.ProductListed);
        }

        public IDataResult<List<ProductDetailDto>> productDetails()
        {
            if (DateTime.Now.Hour == 12)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(_productDal.GetProductsDetails(), Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductsDetails());
        }

        public IResult Update(Product product)
        {
            if (CheckIfProductCountofCategoryCorrect(product.CategoryID).Success)
            {
                _productDal.Update(product);

                return new SuccessResult(Messages.ProductAddedMessage);
            }
            return new SuccessResult(Messages.ProductAddedMessage);
        }
        private IResult CheckIfProductCountofCategoryCorrect(int CategoryID)
        {
            var result = _productDal.GetAll(p => p.CategoryID == CategoryID).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductNameExists(string ProductName)
        {
            var result = _productDal.GetAll(p => p.ProductName == ProductName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCategoryLimitExceded()
        {
            
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

    }
}
