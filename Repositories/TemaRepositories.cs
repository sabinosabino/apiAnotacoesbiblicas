using Dapper;
using Dapper.Contrib.Extensions;
using projetobibliiaapi.Context;
using projetobibliiaapi.Context.Interface;
using projetobibliiaapi.Models;

namespace projetobibliiaapi.Repositories
{
    public class TemaRepositories : IRepositories<Temas>
    {
        private readonly IConnection _db;
        public TemaRepositories(IConnection db)
        {
            _db = db;
        }
        public async Task<Temas> Add(Temas model)
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

        public async Task<bool> Delete(Temas model)
        {
            try
            {
                await _db.GetConnection().ExecuteAsync("update temas set excluido=1 where codigo='" + model.Codigo + "'");
                return true;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        public async Task<IEnumerable<Temas>> GetAll(string where = "",string usuario="")
        {
            try
            {
                return await _db.GetConnection()
                    .QueryAsync<Temas>("Select * from temas where tema like '%" + where 
                            + "%' and usuarioId='" + usuario + "' and excluido is null");
            }
            catch
            {
                throw;
            }
        }
        public async Task<Temas> GetById(string id)
        {
            try
            {
                try
                {
                    var m = await _db.GetConnection()
                        .QueryFirstOrDefaultAsync<Temas>("Select * from temas where codigo='" + id + "' and excluido is null");
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

        public Task<IEnumerable<Temas>> GetFull(string where = "", string usuario = "")
        {
            throw new NotImplementedException();
        }

        public async Task<Temas> Update(Temas model, string codigo)
        {
            try
            {
                Validate(model);
                var m = await GetById(codigo);
                if (m is null)
                {
                    throw new Exception("Tema não existe no banco de dados");
                }
                if (model.Codigo != m.Codigo)
                {
                    throw new Exception("Temas não são iguais para alterações");
                }
                await _db.GetConnection().UpdateAsync(model);
                return model;
            }
            catch
            {
                throw;
            }
        }

        public void Validate(Temas m)
        {
            if (string.IsNullOrEmpty(m.Tema))
            {
                throw new Exception("Nome do tema precisa ser informado.");
            }
            if (string.IsNullOrEmpty(m.UsuarioId))
            {
                throw new Exception("Usuário não foi encontrado.");
            }
        }
    }
}