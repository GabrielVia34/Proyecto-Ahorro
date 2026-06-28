using System;
using AppAhorro.Backend.Data;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Iniciando la aplicación...");
        ConexionBD.InicializarBasedeDatos();

        Console.WriteLine("Aplicación lista para operar");
        Console.ReadKey();
    }
}
