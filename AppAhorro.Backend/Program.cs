using System;
using AppAhorro.Backend.Data;
using AppAhorro.Backend.Models;
using AppAhorro.Backend.Dao;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== BIENVENIDO A APPAHORRO ===");
        ConexionBD.InicializarBasedeDatos();
        UsuarioDAO usuarioDao = new UsuarioDAO();

        Console.WriteLine("\n--- Probando Registro de Usuario ---");
        string nuevoUsuario = "Gabriel";
        string miContraseña = "Yakovia3434";
        bool registroExitoso = usuarioDao.RegistrarUsuario(nuevoUsuario, miContraseña);

        if (registroExitoso)
        {
            Console.WriteLine($"Usuario '{nuevoUsuario}' registrado exitosamente.");
        }
        else
        {
            Console.WriteLine($"Error al registrar el usuario '{nuevoUsuario}'.");
        }

        Console.WriteLine("\n--- Probando Login de Usuario ---");
        Usuario? usuarioLogueado = usuarioDao.Login(nuevoUsuario, miContraseña);
        
        if (usuarioLogueado != null)
        {
            Console.WriteLine($"Login exitoso para el usuario: {usuarioLogueado.NombreUsuario}");
          
            Console.WriteLine($"ID de Usuario: {usuarioLogueado.UsuarioID} | Registrado el: {usuarioLogueado.FechaCreacion}");
        }
        else
        {
            Console.WriteLine("Login fallido. Verifica tus credenciales.");
        }

        Console.WriteLine("\nAplicación lista para operar");
        Console.ReadKey();
    }
}
        

      


