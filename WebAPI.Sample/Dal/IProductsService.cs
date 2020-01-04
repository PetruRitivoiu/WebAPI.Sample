using DellChallenge.D1.Api.Dto;
using System;
using System.Collections.Generic;

namespace DellChallenge.D1.Api.Dal
{
    public interface IProductsService
    {
        IEnumerable<ProductDto> GetAll();
        ProductDto GetById(Guid id);
        ProductDto Add(ProductDetailsDto productDetails);
        void Update(Guid id, ProductDetailsDto productDetails);
        void Delete(Guid id);
    }
}
