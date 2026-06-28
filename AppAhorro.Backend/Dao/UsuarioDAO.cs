using System;
using Microsoft.Data.Sqlite;
using AppAhorro.Backend.Models;
using AppAhorro.Backend.Data;

namespace AppAhorro.Backend.Dao
{
    public class UsuarioDAO
    {
        public bool RegistrarUsuario(string Nombre, string Contraseña)
        {
            try
            {
                using (var conexion = ConexionBD.ObtenerConexion())
                {
                    conexion.Open();

                    string contraseñaHash = Convert.ToBase64String(System.Security.Cryptography
                    .SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(Contraseña)));
                    
                    
                    string query = "INSERT INTO Usuarios (NombreUsuario, ContraseñaHash, FechaCreacion) VALUES (@NombreUsuario, @ContraseñaHash, @FechaCreacion)";
                    
                    using (var command = new SqliteCommand(query, conexion))
                    {
                        command.Parameters.AddWithValue("@NombreUsuario", Nombre);
                        command.Parameters.AddWithValue("@ContraseñaHash", contraseñaHash);
                        command.Parameters.AddWithValue("@FechaCreacion", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                        int filasAfectadas = command.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar usuario: {ex.Message}");
                return false;
            }
        }

        public Usuario? Login(string Nombre, string Contraseña)
        {
            try
            {
                using (var conexion = ConexionBD.ObtenerConexion())
                {
                    conexion.Open();
                    
                    string contraseñaHash = Convert.ToBase64String(System.Security.Cryptography
                    .SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(Contraseña)));

             
                    string query = "SELECT * FROM Usuarios WHERE NombreUsuario = @NombreUsuario AND ContraseñaHash = @ContraseñaHash";
                    
                    using (var command = new SqliteCommand(query, conexion))
                    {
                        command.Parameters.AddWithValue("@NombreUsuario", Nombre);
                        command.Parameters.AddWithValue("@ContraseñaHash", contraseñaHash);

                        using (var lector = command.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                return new Usuario
                                {
                                    UsuarioID = lector.GetInt32(0),
                                    NombreUsuario = lector.GetString(1),
                                    ContraseñaHash = lector.GetString(2),
                                    FechaCreacion = DateTime.Parse(lector.GetString(3))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al iniciar sesión: {ex.Message}");
            }
            return null;
        }
    }
}