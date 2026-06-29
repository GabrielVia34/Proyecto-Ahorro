using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace AppAhorro.Backend.Data
{
    public static class ConexionBD
    {
        private static readonly string NombreBaseDatos = "ahorro_data2.db";
        private static readonly string RutaBaseDatos = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), NombreBaseDatos);
    
        private static readonly string CadenaConexion = $"Data Source={RutaBaseDatos}";

        public static SqliteConnection ObtenerConexion()
        {
            return new SqliteConnection(CadenaConexion);
        }
        
        public static void InicializarBasedeDatos()
        {
            Console.WriteLine($"[BD] Inicializando Base de Datos en: {RutaBaseDatos}");
            if (!File.Exists(RutaBaseDatos))
            {
                using (var conexion = ObtenerConexion())
                {
                    conexion.Open();

                    string crearTablaUsuarios = @"
                    CREATE TABLE IF NOT EXISTS Usuarios (
                        UsuarioID INTEGER PRIMARY KEY AUTOINCREMENT,
                        NombreUsuario TEXT UNIQUE NOT NULL,
                        ContraseñaHash TEXT NOT NULL,
                        FechaCreacion DATETIME NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS RegistrosFinancieros (
                        RegistroID INTEGER PRIMARY KEY AUTOINCREMENT,
                        UsuarioID INTEGER,
                        Mes INTEGER NOT NULL,
                        Año INTEGER NOT NULL,
                        MontoIngreso REAL NOT NULL,
                        MontoAhorro REAL DEFAULT 0.0,
                        EsVariable INTEGER DEFAULT 0,
                        FOREIGN KEY(UsuarioID) REFERENCES Usuarios(UsuarioID)
                    );
                    
                    CREATE TABLE IF NOT EXISTS MetasAhorro (
                        MetaID INTEGER PRIMARY KEY AUTOINCREMENT,
                        UsuarioID INTEGER,
                        Descripcion TEXT NOT NULL,
                        MontoObjetivo REAL NOT NULL,
                        MontoActual REAL DEFAULT 0.0,
                        FechaLimite DATETIME,
                        FOREIGN KEY(UsuarioID) REFERENCES Usuarios(UsuarioID)
                    );
                    ";

                    using (var comando = new SqliteCommand(crearTablaUsuarios, conexion))
                    {
                        comando.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Base de datos creada e inicializada correctamente.");
            }
        }
    }
}