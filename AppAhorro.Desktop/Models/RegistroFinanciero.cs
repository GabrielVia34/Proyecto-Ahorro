using System;
namespace AppAhorro.Backend.Models
{
        public class RegistroFinanciero
    {
       public int RegistroFinancieroID { get; set; }
        public int UsuarioID { get; set; }
         public int Mes { get; set; }
          public int Año { get; set; }
            public decimal MontoIngresado { get; set; }

             public decimal MontoAhorro { get; set; }
            public bool variable { get; set; }
       
    }
}