
namespace projetobibliiaapi.Context.Interface
{
    public interface IRepositories<T>
    {
        Task<T> GetById(string id);
        Task<IEnumerable<T>> GetAll(string where="",string usuario="");
        Task<IEnumerable<T>> GetFull(string where="",string usuario="");
        Task<T> Add(T m);
        Task<T> Update(T m,string codigo);
        Task<bool> Delete(T m);
        void Validate(T m);
    }
}