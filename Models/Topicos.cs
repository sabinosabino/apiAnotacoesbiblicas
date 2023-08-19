using Dapper.Contrib.Extensions;

namespace projetobibliiaapi.Models
{
    [Dapper.Contrib.Extensions.Table("Topicos")]
    public class Topicos
    {
        [ExplicitKey]
        public string Codigo { get; set; } = Guid.NewGuid().ToString();
        public string Topico { get; set; } = string.Empty;
        public string TemaId { get; set; } = string.Empty;
        public DateTime? CriadoEm { get; set; } = DateTime.Now;
        public DateTime? AlteradoEm { get; set; }
        public bool? Excluido { get; set; }
        
        [Computed]
        public Temas? Tema {get;set;}=null;

        [Computed]
        public List<Anotacoes>? Anotacoes { get; set; }=null;
    }
}