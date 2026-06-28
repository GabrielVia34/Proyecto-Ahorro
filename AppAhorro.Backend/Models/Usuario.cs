using System;
namespace AppAhorro.Backend.Models
{
        public class Usuario
    {
        public int UsuarioID { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string ContraseñaHash { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }

    }


}