using DellChallenge.D1.Api.Dal;
using DellChallenge.D1.Api.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DellChallenge.D1.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet, EnableCors("AllowReactCors")]
        public ActionResult<IEnumerable<ProductDto>> Get()
        {
            return Ok(_productsService.GetAll());
        }

        [HttpGet("{id}"), EnableCors("AllowReactCors"), ProducesResponseType(200), ProducesResponseType(404)]
        public ActionResult<ProductDto> Get(Guid id)
        {
            ProductDto product = null;

            try
            {
                product = _productsService.GetById(id);
            }
            catch (NotFoundException)
            {
                NotFound();
            }

            return Ok(product);
        }


        [HttpPost, EnableCors("AllowReactCors"), ProducesResponseType(201)]
        public ActionResult<ProductDto> Post([FromBody] ProductDetailsDto productDetails)
        {
            var product = _productsService.Add(productDetails);

            return Created(
                Url.Action(nameof(Get), ControllerContext.ActionDescriptor.ControllerName, new { product.Id }),
                product);
        }

        [HttpPut("{id}"), EnableCors("AllowReactCors"), ProducesResponseType(204), ProducesResponseType(404)]
        public ActionResult<ProductDto> Put(Guid id, [FromBody] ProductDetailsDto productDetails)
        {
            try
            {
                _productsService.Update(id, productDetails);
            }
            catch (NotFoundException)
            {
                NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}"), EnableCors("AllowReactCors"), ProducesResponseType(204), ProducesResponseType(404)]
        public ActionResult<ProductDto> Delete(Guid id)
        {
            try
            {
                _productsService.Delete(id);
            }
            catch (NotFoundException)
            {
                NotFound();
            }

            return NoContent();
        }
    }
}
