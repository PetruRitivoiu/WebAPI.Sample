using System;
using System.Collections.Generic;
using System.Linq;
using DellChallenge.D1.Api.Dto;
using Microsoft.EntityFrameworkCore;

namespace DellChallenge.D1.Api.Dal
{
    public class ProductsService : IProductsService
    {
        private readonly ProductsContext _context;

        public ProductsService(ProductsContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductDto> GetAll()
            => _context.Products.Select(p => MapToDto(p));

        public ProductDto GetById(Guid id)
        {
            var product = _context.Products.AsNoTracking().SingleOrDefault(p => p.Id == id);

            if (product == null)
                throw new NotFoundException();

            return MapToDto(product);
        }

        public ProductDto Add(ProductDetailsDto productDetails)
        {
            var product = MapToData(productDetails);

            _context.Products.Add(product);
            _context.SaveChanges();

            return MapToDto(product);
        }

        public void Update(Guid id, ProductDetailsDto productDetails)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);

            if (product == null)
                throw new NotFoundException();

            MapToData(product, productDetails);

            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var product = _context.Products.AsNoTracking().SingleOrDefault(p => p.Id == id);

            if (product == null)
                throw new NotFoundException();

            _context.Products.Remove(product);

            _context.SaveChanges();
        }

        private Product MapToData(ProductDetailsDto productDetails)
            => MapToData(new Product(), productDetails);

        private Product MapToData(Product product, ProductDetailsDto productDetails)
        {
            product.Name = productDetails.Name;
            product.Category = productDetails.Category;

            return product;
        }

        private ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category
            };
        }
    }
}
