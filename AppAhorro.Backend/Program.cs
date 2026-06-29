using System;
using System.Collections.Generic;
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
            Console.WriteLine($"Usuario '{nuevoUsuario}' registrado exitosamente.");
        else
            Console.WriteLine($"El usuario '{nuevoUsuario}' ya existe o ya estaba registrado.");

        Console.WriteLine("\n--- Probando Login de Usuario ---");
        Usuario? usuarioLogueado = usuarioDao.Login(nuevoUsuario, miContraseña);
        
        if (usuarioLogueado != null)
        {
            Console.WriteLine($"Login exitoso para el usuario: {usuarioLogueado.NombreUsuario}");
            Console.WriteLine($"ID de Usuario: {usuarioLogueado.UsuarioID} | Registrado el: {usuarioLogueado.FechaCreacion}");

       
            int idActual = usuarioLogueado.UsuarioID;
            RegistroFinancieroDAO registroDao = new RegistroFinancieroDAO();
            MetaAhorroDAO metaDao = new MetaAhorroDAO();

            Console.WriteLine("\n--- Probando Inserción de Registros Financieros ---");
            bool registroDinero = registroDao.InsertarRegistro(idActual, 6, 2026, 2500.00, 450.00, 0);
            if (registroDinero) Console.WriteLine("¡Registro financiero de Junio 2026 insertado con éxito!");

            Console.WriteLine("\n--- Probando Historial Financiero ---");
            List<string> historial = registroDao.ObtenerHistorial(idActual);
            foreach (var linea in historial)
            {
                Console.WriteLine(linea);
            }

            Console.WriteLine("\n--- Probando Creación de Metas de Ahorro ---");
            bool metaCreada = metaDao.CrearMeta(idActual, "Comprar componentes PC", 1500.00, "2026-12-31");
            if (metaCreada) Console.WriteLine("¡Meta de ahorro agregada correctamente para fin de año!");
        }
        else
        {
            Console.WriteLine("Login fallido. Verifica tus credenciales.");
        }

        Console.WriteLine("\nAplicación lista para operar");
        Console.ReadKey();
    }
}
      


