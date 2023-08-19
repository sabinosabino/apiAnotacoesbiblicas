using Microsoft.AspNetCore.Mvc;
using projetobibliiaapi.Context.Interface;
using projetobibliiaapi.InputModels.Anotacoes;
using projetobibliiaapi.Models;

namespace projetobibliiaapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnotacoesController : ControllerBase
    {
        private readonly IRepositories<Anotacoes> _AnotacoeRepositories;
        public AnotacoesController(IRepositories<Anotacoes> AnotacoeRepositories)
        {
            _AnotacoeRepositories = AnotacoeRepositories;
        }

        [HttpPost]
        public async Task<ActionResult<Anotacoes>> Add([FromBody] AnotacoesInsert m)
        {
            try
            {
                var model = new Anotacoes
                {
                    Versao = m.Versao,
                    Capitulo = m.Capitulo,
                    Intervalo = m.Intervalo,
                    Comentario = m.Comentario,
                    Livro = m.Livro,
                    Texto = m.Texto,
                    TopicoId = m.TopicoId
                };
                return Ok(await _AnotacoeRepositories.Add(model));
            }
            catch (System.Exception e)
            {
                return BadRequest(new { Erro = e.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<Anotacoes>> Update([FromBody] AnotacoesUpdate m, string codigo)
        {
            try
            {
                var model = await _AnotacoeRepositories.GetById(codigo);
                model.Versao = m.Versao;
                model.Capitulo = m.Capitulo;
                model.Intervalo = m.Intervalo;
                model.Comentario = m.Comentario;
                model.Livro = m.Livro;
                model.Texto = m.Texto;
                model.AlteradoEm = DateTime.Now;
                return Ok(await _AnotacoeRepositories.Update(model, codigo));
            }
            catch (System.Exception e)
            {
                return BadRequest(new { Erro = e.Message });
            }
        }

        [HttpGet("Get/{codigo}")]
        public async Task<ActionResult<Anotacoes>> Get(string codigo)
        {
            try
            {
                return Ok(await _AnotacoeRepositories.GetById(codigo));
            }
            catch (System.Exception e)
            {
                return BadRequest(new { Erro = e.Message });
            }
        }
        [HttpGet("GetByTopico/{where?}")]
        public async Task<ActionResult<IEnumerable<Anotacoes>>> GetWhere([FromRoute] string where = "")
        {
            try
            {
                return Ok(await _AnotacoeRepositories.GetAll(where, "usuario temporário"));
            }
            catch (System.Exception e)
            {
                return BadRequest(new { Erro = e.Message });
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Anotacoes>> Delete([FromBody] AnotacoesUpdate m)
        {
            try
            {
                var model = await _AnotacoeRepositories.GetById(m.codigo);
                model.AlteradoEm = DateTime.Now;
                if (model is null)
                {
                    return NotFound("Item não encontrado");
                }
                return Ok(await _AnotacoeRepositories.Delete(model));
            }
            catch (System.Exception e)
            {
                return BadRequest(new { Erro = e.Message });
            }
        }
    }
}