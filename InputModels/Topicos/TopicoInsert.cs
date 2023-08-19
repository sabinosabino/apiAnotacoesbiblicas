using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetobibliiaapi.InputModels.Topicos
{
    public class TopicoInsert
    {
        public string Topico { get; set; }=string.Empty;
        public string TemaId { get; set; }=string.Empty;
    }
}