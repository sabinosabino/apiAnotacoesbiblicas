using Microsoft.AspNetCore.Mvc;
using projetobibliiaapi.Context.Interface;
using projetobibliiaapi.InputModels;
using projetobibliiaapi.InputModels.Temas;
using projetobibliiaapi.Models;

namespace projetobibliiaapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemaController : ControllerBase
    {
        private readonly IRepositories<Temas> _temaRepositories;
        public TemaController(IRepositories<Temas> temaRepositories)
        {
            _temaRepositories=temaRepositories;
        }

        [HttpPost]
        public async Task<ActionResult<Temas>> Add([FromBody] TemaInsert m) {
            try
            {
                var model = new Temas();
                model.Tema=m.Tema;
                model.UsuarioId="usuario temporário";
                return Ok(await _temaRepositories.Add(model));
            }
            catch (System.Exception e)
            {
                return BadRequest(new {Erro=e.Message});
            }
        }

        [HttpPut]
        public async Task<ActionResult<Temas>> Update([FromBody] TemaUpdate m, string codigo) {
            try
            {
                var model = new Temas();
                model.Codigo=m.Codigo;
                model.Tema=m.Tema;
                model.UsuarioId="usuario temporário";
                model.AlteradoEm=DateTime.Now;
                return Ok(await _temaRepositories.Update(model,codigo));
            }
            catch (System.Exception e)
            {
                return BadRequest(new {Erro=e.Message});
            }
        }

        [HttpGet("Get/{codigo}")]
        public async Task<ActionResult<Temas>> Get(string codigo) {
            try
            {
                return Ok(await _temaRepositories.GetById(codigo));
            }
            catch (System.Exception e)
            {
                return BadRequest(new {Erro=e.Message});
            }
        }
       [HttpGet("GetWithWhere/{where?}")]
        public async Task<ActionResult<IEnumerable<Temas>>> GetWhere([FromRoute] string where="") {
            try
            {
                return Ok(await _temaRepositories.GetAll(where,"usuario temporário"));
            }
            catch (System.Exception e)
            {
                return BadRequest(new {Erro=e.Message});
            }
        }
        
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Temas>>> GetAll() {
            try
            {
                return Ok(await _temaRepositories.GetAll("","usuario temporário"));
            }
            catch (System.Exception e)
            {
                return BadRequest(new {Erro=e.Message});
            }
        }
        [HttpDelete]
        public async Task<ActionResult<Temas>> Delete([FromBody] Temas m) {
            try
            {
                var model = await _temaRepositories.GetById(m.Codigo);
                model.AlteradoEm=DateTime.Now;
                if(model is null){
                    return NotFound("Item não encontrado");
                }
                return Ok(await _temaRepositories.Delete(model));
            }
            catch (System.Exception e)
            {
                return BadRequest(new {Erro=e.Message});
            }
        }
    }
}