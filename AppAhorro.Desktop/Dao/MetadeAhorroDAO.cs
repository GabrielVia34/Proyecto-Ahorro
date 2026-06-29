using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using AppAhorro.Backend.Data;

namespace AppAhorro.Backend.Dao
{
    public class MetaAhorroDAO
    {
        public bool CrearMeta(int usuarioId, string descripcion, double montoObjetivo, string fechaLimite)
        {
            try
            {
                using (var conexion = ConexionBD.ObtenerConexion())
                {
                    conexion.Open();
                    string query = @"INSERT INTO MetasAhorro (UsuarioID, Descripcion, MontoObjetivo, MontoActual, FechaLimite) 
                                    VALUES (@UsuarioID, @Descripcion, @MontoObjetivo, 0.0, @FechaLimite)";

                    using (var command = new SqliteCommand(query, conexion))
                    {
                        command.Parameters.AddWithValue("@UsuarioID", usuarioId);
                        command.Parameters.AddWithValue("@Descripcion", descripcion);
                        command.Parameters.AddWithValue("@MontoObjetivo", montoObjetivo);
                        command.Parameters.AddWithValue("@FechaLimite", fechaLimite);

                        int filasAfectadas = command.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear meta de ahorro: {ex.Message}");
                return false;
            }
        }


        public bool AbonarAMeta(int metaId, double montoAAbonar)
        {
            try
            {
                using (var conexion = ConexionBD.ObtenerConexion())
                {
                    conexion.Open();
                    string query = "UPDATE MetasAhorro SET MontoActual = MontoActual + @MontoAbono WHERE MetaID = @MetaID";

                    using (var command = new SqliteCommand(query, conexion))
                    {
                        command.Parameters.AddWithValue("@MontoAbono", montoAAbonar);
                        command.Parameters.AddWithValue("@MetaID", metaId);

                        int filasAfectadas = command.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al abonar a la meta: {ex.Message}");
                return false;
            }
        }
    }
}