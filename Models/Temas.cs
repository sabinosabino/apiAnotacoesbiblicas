using Dapper.Contrib.Extensions;

namespace projetobibliiaapi.Models
{
    [Dapper.Contrib.Extensions.Table("Temas")]
    public class Temas
    {
        [ExplicitKey]
        public string Codigo { get; set; } = Guid.NewGuid().ToString();
        public string Tema { get; set; } = string.Empty;
        public string UsuarioId { get; set; } = string.Empty;
        public DateTime? CriadoEm { get; set; } = DateTime.Now;
        public DateTime? AlteradoEm { get; set; }
        public bool? Excluido { get; set; }
        
        [Computed]
        public IEnumerable<Topicos>? Topicos{get;set;}
    }
}