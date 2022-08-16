﻿using Microsoft.AspNetCore.Mvc;
using Pet.WebAPI.Domain.Entities;
using Pet.WebAPI.Domain.Model;
using Pet.WebAPI.Interfaces.Services;
using Pet.WebAPI.Services;

namespace Pet.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class PrestadoresController : Controller, IPrestadoresController
    {
        private readonly IPrestadoresService _service;

        public PrestadoresController(IPrestadoresService service)
        {
            _service = service;
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePrestador(int id)
        {
            try
            {
                _service.Delete(id);
            }
            catch (Exception)
            {
                return NoContent();
            }
            return Ok();
        }

        //public IActionResult GetAllPrestador()
        //{
        //    //return Ok(_service.Get)
        //}

        [HttpGet("{id}")]
        public IActionResult GetPrestador(int id)
        {
            return Ok(_service.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostPrestador([FromBody] NovoPrestador prestador)
        {
            var result = await _service.Add(prestador);
            return CreatedAtAction(nameof(GetPrestador), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrestador(int id, AlterarPrestador prestador)
        {
            try
            {
                await _service.Update(id, prestador);
            }
            catch (Exception)
            {
                return NoContent();
            }

            return Ok();
        }
    }

    public interface IPrestadoresController
    {
        IActionResult GetPrestador(int id);
        Task<IActionResult> PostPrestador([FromBody] NovoPrestador prestador);
        Task<IActionResult> PutPrestador(int id, AlterarPrestador prestador);
        IActionResult DeletePrestador(int id);
        //IActionResult GetAllPrestador();
    }

}