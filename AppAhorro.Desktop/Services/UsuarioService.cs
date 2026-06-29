using System;
using AppAhorro.Backend.Dao;
using AppAhorro.Backend.Models;

namespace AppAhorro.Backend.Services
{
    public class UsuarioService
    {
        private readonly UsuarioDAO _usuarioDao;

        public UsuarioService()
        {
            _usuarioDao = new UsuarioDAO();
        }
        public string RegistrarNuevoUsuario(string nombre, string password)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(password))
            {
                return "Error: El nombre de usuario y la contraseña son obligatorios.";
            }

            nombre = nombre.Trim();

            if (password.Length < 6)
            {
                return "Error: La contraseña debe tener al menos 6 caracteres por seguridad.";
            }
            bool exito = _usuarioDao.RegistrarUsuario(nombre, password);

            if (exito)
            {
                return $"¡Usuario '{nombre}' registrado con éxito!";
            }
            else
            {
                return "Error: El nombre de usuario ya se encuentra registrado.";
            }
        }
        public Usuario? IniciarSesion(string nombre, string password)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(password))
            {
             return null;
            }
            return _usuarioDao.Login(nombre.Trim(), password);
        }
    }
}