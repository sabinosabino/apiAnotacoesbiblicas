using Dapper.Contrib.Extensions;

namespace projetobibliiaapi.Models
{
    [Dapper.Contrib.Extensions.Table("Anotacoes")]
    public class Anotacoes
    {
        [ExplicitKey]
        public string Codigo { get; set; } = Guid.NewGuid().ToString();
        public string TopicoId { get; set; } = string.Empty;
        public string Versao { get; set; } = string.Empty;
        public string Livro { get; set; }= string.Empty;
        public int Capitulo { get; set; }
        public string Intervalo { get; set; }= string.Empty;
        public string Comentario { get; set; }= string.Empty;
        public string Texto { get; set; }= string.Empty;
        public DateTime? CriadoEm { get; set; } = DateTime.Now;
        public DateTime? AlteradoEm { get; set; }
        public bool? Excluido { get; set; }
        [Computed]
        public Topicos? Topico {get;set;}=null;
    }
}