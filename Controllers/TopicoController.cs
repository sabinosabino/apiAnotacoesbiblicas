using Microsoft.AspNetCore.Mvc;
using projetobibliiaapi.Context.Interface;
using projetobibliiaapi.InputModels.Topicos;
using projetobibliiaapi.Models;

namespace projetobibliiaapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicoController : ControllerBase
    {
        private readonly IRepositories<Topicos> _TopicoRepositories;
        public TopicoController(IRepositories<Topicos> TopicoRepositories)
        {
            _TopicoRepositories = TopicoRepositories;
        }

        [HttpPost]
        public async Task<ActionResult<Topicos>> Add([FromBody] TopicoInsert m)
        {
            try
            {
                var model = new Topicos();
                model.Topico = m.Topico;
                model.TemaId = m.TemaId;
                return Ok(await _TopicoRepositories.Add(model));
            }
            catch (System.Exception e)
            {
                return BadRequest(new { Erro = e.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<Topicos>> Update([FromBody] TopicoUpdate m, string codigo)
        {
            try
            {
                var model = await _TopicoRepositories.GetById(codigo);
                model.Topico = m.Topico;
                model.AlteradoEm = DateTime.Now;
                return Ok(await _TopicoRepositories.Update(model, codigo));
            }
            catch (System.Exception e)
            {
                return BadRequest(new { Erro = e.Message });
            }
        }

        [HttpGet("Get/{codigo}")]
        public async Task<ActionResult<Topicos>> Get(string codigo)
        {
            try
            {
                return Ok(await _TopicoRepositories.GetById(codigo));
            }
            catch (System.Exception e)
            {
                return BadRequest(new { Erro = e.Message });
            }
        }
        [HttpGet("GetByTema/{where?}")]
        public async Task<ActionResult<IEnumerable<Topicos>>> GetWhere([FromRoute] string where = "")
        {
            try
            {
                return Ok(await _TopicoRepositories.GetAll(where, "usuario temporário"));
            }
            catch (System.Exception e)
            {
                return BadRequest(new { Erro = e.Message });
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Topicos>> Delete([FromBody] Topicos m)
        {
            try
            {
                var model = await _TopicoRepositories.GetById(m.Codigo);
                model.AlteradoEm = DateTime.Now;
                if (model is null)
                {
                    return NotFound("Item não encontrado");
                }
                return Ok(await _TopicoRepositories.Delete(model));
            }
            catch (System.Exception e)
            {
                return BadRequest(new { Erro = e.Message });
            }
        }
    }
}