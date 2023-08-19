using Dapper;
using Dapper.Contrib.Extensions;
using projetobibliiaapi.Context;
using projetobibliiaapi.Context.Interface;
using projetobibliiaapi.Models;

namespace projetobibliiaapi.Repositories
{
    public class TopicosRepositories : IRepositories<Topicos>
    {
        private readonly IConnection _db;
        public TopicosRepositories(IConnection db)
        {
            _db = db;
        }
        public async Task<Topicos> Add(Topicos model)
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

        public async Task<bool> Delete(Topicos model)
        {
            try
            {
                await _db.GetConnection().ExecuteAsync("update Topicos set excluido=1 where codigo='" + model.Codigo + "'");
                return true;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        public async Task<IEnumerable<Topicos>> GetAll(string where = "",string usuario="")
        {
            try
            {
                return await _db.GetConnection()
                    .QueryAsync<Topicos>("Select * from Topicos where temaId = '" + where 
                            + "' and excluido is null");
            }
            catch
            {
                throw;
            }
        }
        public async Task<Topicos> GetById(string id)
        {
            try
            {
                try
                {
                    var m = await _db.GetConnection()
                        .QueryFirstOrDefaultAsync<Topicos>("Select * from Topicos where codigo='" + id + "' and excluido is null");
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

        public Task<IEnumerable<Topicos>> GetFull(string where = "", string usuario = "")
        {
            throw new NotImplementedException();
        }

        public async Task<Topicos> Update(Topicos model, string codigo)
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
                    throw new Exception("Topicos não são iguais para alterações");
                }
                await _db.GetConnection().UpdateAsync(model);
                return model;
            }
            catch
            {
                throw;
            }
        }

        public void Validate(Topicos m)
        {
            if (string.IsNullOrEmpty(m.Topico))
            {
                throw new Exception("Nome do tema precisa ser informado.");
            }
        }
    }
}