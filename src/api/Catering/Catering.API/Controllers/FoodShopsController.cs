﻿using AutoMapper;
using Catering.API.Dtos;
using Catering.BLL.Interfaces;
using Catering.DAL.Entities.FoodShops;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catering.API.Controllers
{
    public class FoodShopsController : BaseApiController
    {
        private readonly IFoodShopService _service;
        private readonly IMapper _mapper;

        public FoodShopsController(IFoodShopService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<FoodShopDto>>> GetFoodShops()
        {
            var shopsFromRepo = await _service.GetFoodShops();

            return Ok(_mapper.Map<IEnumerable<FoodShopDto>>(shopsFromRepo));
        }

        [HttpGet("{id}", Name = "GetFoodShop")]
        public async Task<IActionResult> GetFoodShop(int id)
        {
            var shopFromRepo = await _service.GetFoodShop(id);

            if(shopFromRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<FoodShopDto>(shopFromRepo));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFoodShop(int id, FoodShopUpdateDto updateDto)
        {
            var foodShop = await _service.GetFoodShop(id);
            _mapper.Map(updateDto, foodShop);

            await _service.UpdateFoodShop(foodShop);
            return Ok();
        }

        [HttpPost("create")]
        public async Task<ActionResult<FoodShopDto>> CreateFoodShop(FoodShopCreateDto createDto)
        {
            var shopEntity = _mapper.Map<FoodShop>(createDto);
            await _service.AddFoodShop(shopEntity);
            
            var toReturn = _mapper.Map<FoodShopDto>(shopEntity);
            return CreatedAtRoute("GetFoodShop", new { id = toReturn.Id }, toReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodShop(int id)
        {
            var shopFromRepo = await _service.GetFoodShop(id);
           
            if(shopFromRepo == null)
            {
                return NotFound();
            }

            await _service.DeleteFoodShop(shopFromRepo);

            return NoContent();
        }

    }
}
