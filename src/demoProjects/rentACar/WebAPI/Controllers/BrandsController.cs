using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using Application.Features.Brands.Models;
using Application.Features.Brands.Queries.GetByIdBrand;
using Application.Features.Brands.Queries.GetListBrand;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : BaseController
    {
        /// <summary>
        /// body içinde ekleme işlemi yapılacak bir sorgu alınmakta 
        /// ve Mediator araclığıyla ilgili sorgunun handlerına yollanmakta.
        /// Handler ise Application/Features/ İlgili entity klasörü/createEntityCommand içinde
        /// </summary>
       
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateBrandCommand createBrandCommand)
        {
            CreatedBrandDto result = await Mediator.Send(createBrandCommand);
            return Created("", result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            // queryi aldık
            GetListBrandQuery getListBrandQuery = new() {PageRequest = pageRequest};
            // bir brandlistmodel döndürecek, mediator ile query'i ilgili handler'a yolluyorum
            BrandListModel result = await Mediator.Send(getListBrandQuery);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdBrandQuery getByIdIdBrandQuery)
        {
           BrandGetByIdDto brandGetByIdDto = await Mediator.Send(getByIdIdBrandQuery);
           return Ok(brandGetByIdDto);
        }
    }
}
