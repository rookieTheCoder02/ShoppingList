using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Model.Request;
using ShoppingList.Models.Domain;
using ShoppingList.Models.DTO;
using ShoppingList.Models.Repository;

namespace ShoppingList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GoodsController : Controller
    {
        private readonly IGoodsRepository goodsRepository;
        private readonly IMapper mapper;

        public GoodsController(IGoodsRepository goodsRepository, IMapper mapper)
        {
            this.goodsRepository = goodsRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllGoodsAsync()
        {
            // Create request to read all records in table
            var request = await goodsRepository.GetAllGoodsAsync();
            // If request == null, return NotFound();
            if(request == null)
            {
                return NotFound();
            }
            // If request != null, convert request into response DTO object
            var responseDTO = mapper.Map<IList<GoodsDTO>>(request);
            // Return Ok(response)
            return Ok(responseDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetGoodsAsyncById")]
        public async Task<IActionResult> GetGoodsAsyncById(Guid id)
        {
            // Create request to read single record in table
            var request = await goodsRepository.GetGoodsAsyncById(id);
            // If request == null, return NotFound();
            if(request == null)
            {
                return NotFound();
            }
            // If request != null, convert request into response DTO object
            var responseDTO = mapper.Map<GoodsDTO>(request);
            // Return Ok(response)
            return Ok(responseDTO);
        }
        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetGoodsAsyncByName(string name)
        {
            // Create request to read single record in table
            var request = await goodsRepository.GetGoodsAsyncByName(name);
            // If request == null, return NotFound();
            //if (request == null)
            //{
            //    return NotFound();
            //}
            // If request != null, convert request into response DTO object
            var responseDTO = mapper.Map<IList<GoodsDTO>>(request);
            // If responseDTO == null, return NotFound();
            if(responseDTO is null)
            {
                return NotFound();
            }
            // Return Ok(response)
            return Ok(responseDTO);

        }
        [HttpPost]
        public async Task<IActionResult> AddGoodsAsync([FromBody]AddGoodsRequest addGoodsRequest)
        {
            // Create request to add single record into table by converting request model object into domain model object
            var request = new Goods()
            {
                Name = addGoodsRequest.Name,
                Price = addGoodsRequest.Price
            };
            // If request == null, return NotFound()
            if (request == null) return NotFound();
            // If request != null, return response
            var response = await goodsRepository.AddGoodsAsync(request);
            // Convert response into responseDTO model object
            var responseDTO = mapper.Map<GoodsDTO>(response);
            // Return CreateAtAction success message
            return CreatedAtAction(nameof(GetGoodsAsyncById), new {id = responseDTO.Id}, responseDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteGoodsAsync(Guid id)
        {
            var request = await goodsRepository.DeleteGoodsAsync(id);
            if (request == null) return NotFound();
            var responseDTO = mapper.Map<GoodsDTO>(request);
            return Ok(responseDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateGoodsAsync([FromRoute]Guid id, 
            [FromBody]UpdateGoodsRequest updateGoodsRequest)
        {
            var request = new Goods()
            {
                Name = updateGoodsRequest.Name,
                Price = updateGoodsRequest.Price
            };
            if(request == null) return NotFound();
            var response = await goodsRepository.UpdateGoodsAsync(id, request);
            var responseDTO = mapper.Map<GoodsDTO>(response);
            return Ok(responseDTO);
        }
    }
}
