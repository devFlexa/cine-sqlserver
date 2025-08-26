using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEV_MANHA.Models
{
    public class Sessao
    {
        public int Id { get; set; }
        public int IdFilme { get; set; }
        public DateOnly Data { get; set; }
        public TimeOnly Hora { get; set; }

        public override string ToString() =>
            $"Sessão {Id} - Filme {IdFilme} - {Data:dd/MM/yyyy} {Hora:HH:mm}";
    }
}
