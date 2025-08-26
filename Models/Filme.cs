using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEV_MANHA.Models.Enums;

namespace DEV_MANHA.Models
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public Genero Genero { get; set; }
        public int Ano { get; set; }

        public override string ToString() => $"{Id} - {Titulo} ({Ano}) - {Genero}";
    }
}
