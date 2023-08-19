using Dapper;
using Dapper.Contrib.Extensions;
using projetobibliiaapi.Context;
using projetobibliiaapi.Context.Interface;
using projetobibliiaapi.Models;

namespace projetobibliiaapi.Repositories
{
    public class AnotacoesRepositories : IRepositories<Anotacoes>
    {
        private readonly IConnection _db;
        public AnotacoesRepositories(IConnection db)
        {
            _db = db;
        }
        public async Task<Anotacoes> Add(Anotacoes model)
        {

            try
            {
                Validate(model);
                await _db.GetConnection().InsertAsync(model);
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(Anotacoes model)
        {
            try
            {
                await _db.GetConnection().ExecuteAsync("update Anotacoes set excluido=1 where codigo='" + model.Codigo + "'");
                return true;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        public async Task<IEnumerable<Anotacoes>> GetAll(string where = "",string usuario="")
        {
            try
            {
                return await _db.GetConnection()
                    .QueryAsync<Anotacoes>("Select * from Anotacoes where topicoid = '" + where 
                            + "' and excluido is null");
            }
            catch
            {
                throw;
            }
        }
        public async Task<Anotacoes> GetById(string id)
        {
            try
            {
                try
                {
                    var m = await _db.GetConnection()
                        .QueryFirstOrDefaultAsync<Anotacoes>("Select * from Anotacoes where codigo='" + id + "' and excluido is null");
                    if(m is null){
                        throw new Exception("Ítem não encontrado.");
                    }
                    return m;
                }
                catch
                {
                    throw;
                }
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<Anotacoes>> GetFull(string where = "", string usuario = "")
        {
            throw new NotImplementedException();
        }

        public async Task<Anotacoes> Update(Anotacoes model, string codigo)
        {
            try
            {
                Validate(model);
                if (model is null)
                {
                    throw new Exception("Tema não existe no banco de dados");
                }
                if (model.Codigo != codigo)
                {
                    throw new Exception("Anotacoes não são iguais para alterações");
                }
                await _db.GetConnection().UpdateAsync(model);
                return model;
            }
            catch
            {
                throw;
            }
        }

        public void Validate(Anotacoes m)
        {
            if (string.IsNullOrEmpty(m.Versao))
            {
                throw new Exception("Informe uma versão");
            }
            if (string.IsNullOrEmpty(m.Livro))
            {
                throw new Exception("Informe um Livro");
            }
            if (m.Capitulo==0)
            {
                throw new Exception("Informe um Capitulo");
            }
            if (string.IsNullOrEmpty(m.Intervalo))
            {
                throw new Exception("Informe um intervalo");
            }
            if (string.IsNullOrEmpty(m.Texto))
            {
                throw new Exception("Informe um texto");
            }
        }
    }
}