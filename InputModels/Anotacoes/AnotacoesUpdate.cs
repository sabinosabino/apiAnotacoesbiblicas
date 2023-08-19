using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetobibliiaapi.InputModels.Anotacoes
{
    public class AnotacoesUpdate
    {
        public string codigo { get; set; }=string.Empty;  
        public string TopicoId { get; set; }=string.Empty;        
        public string Versao { get; set; } = string.Empty;
        public string Livro { get; set; }= string.Empty;
        public int Capitulo { get; set; }
        public string Intervalo { get; set; }= string.Empty;
        public string Comentario { get; set; }= string.Empty;
        public string Texto { get; set; }= string.Empty;
    }
}