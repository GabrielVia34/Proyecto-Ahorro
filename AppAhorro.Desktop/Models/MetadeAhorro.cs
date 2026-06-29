using System;

namespace AppAhorro.Backend.Models
{
    public class MetadeAhorro
    {
            public int MetaId { get; set; }
            public int UsuarioId { get; set; }
                public string Descripcion { get; set; } = string.Empty;
                    public decimal MontoObjetivo { get; set; }
                        public decimal MontoActual { get; set; }
                            public DateTime? FechaLimite { get; set; }
    }
}