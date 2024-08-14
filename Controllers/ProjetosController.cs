using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Exo.WebApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class ProjetosController : ControllerBase
    {
        private readonly ProjetoRepository _projetoRepository;

        public ProjetosController(ProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_projetoRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var projeto = _projetoRepository.BuscarPorId(id);
            if (projeto == null)
            {
                return NotFound("Projeto não encontrado!");
            }
            return Ok(projeto);
        }

        public IActionResult Cadastrar(Projeto projeto)
        {
            try
            {
                _projetoRepository.Cadastrar(projeto);
                return StatusCode(201, "Projeto cadastrado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Projeto projeto)
        {
            try
            {
                var projetoBuscado = _projetoRepository.BuscarPorId(id);
                if (projetoBuscado == null)
                {
                    return NotFound("Projeto não encontrado!");
                }
                _projetoRepository.Atualizar(id, projeto);
                return StatusCode(201, "Projeto atualizado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                var projetoBuscado = _projetoRepository.BuscarPorId(id);
                if (projetoBuscado == null)
                {
                    return NotFound("Projeto não encontrado!");
                }
                _projetoRepository.Deletar(id);
                return StatusCode(201, "Projeto deletado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}