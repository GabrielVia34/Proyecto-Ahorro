using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using AppAhorro.Backend.Data;

namespace AppAhorro.Backend.Dao
{
    public class RegistroFinancieroDAO
    {
        public bool InsertarRegistro(int usuarioId, int mes, int año, double montoIngreso, double montoAhorro, int esVariable)
        {
            try
            {
                using (var conexion = ConexionBD.ObtenerConexion())
                {
                    conexion.Open();

                 
                    string query = @"INSERT INTO RegistrosFinancieros (UsuarioID, Mes, Año, MontoIngreso, MontoAhorro, EsVariable) 
                                    VALUES (@UsuarioID, @Mes, @Año, @MontoIngreso, @MontoAhorro, @EsVariable)";

                    using (var command = new SqliteCommand(query, conexion))
                    {
                        command.Parameters.AddWithValue("@UsuarioID", usuarioId);
                        command.Parameters.AddWithValue("@Mes", mes);
                        command.Parameters.AddWithValue("@Año", año); 
                        command.Parameters.AddWithValue("@MontoIngreso", montoIngreso);
                        command.Parameters.AddWithValue("@MontoAhorro", montoAhorro);
                        command.Parameters.AddWithValue("@EsVariable", esVariable);

                        int filasAfectadas = command.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar registro financiero: {ex.Message}");
                return false;
            }
        }

        public List<string> ObtenerHistorial(int usuarioId)
        {
            var historial = new List<string>();
            try
            {
                using (var conexion = ConexionBD.ObtenerConexion())
                {
                    conexion.Open();
                    string query = "SELECT Mes, Año, MontoIngreso, MontoAhorro FROM RegistrosFinancieros WHERE UsuarioID = @UsuarioID ORDER BY Año DESC, Mes DESC";

                    using (var command = new SqliteCommand(query, conexion))
                    {
                        command.Parameters.AddWithValue("@UsuarioID", usuarioId);

                        using (var lector = command.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                int mes = lector.GetInt32(0);
                                int año = lector.GetInt32(1);
                                double ingreso = lector.GetDouble(2);
                                double ahorro = lector.GetDouble(3);

                                historial.Add($"Periodo: {mes}/{año} | Ingreso: S/. {ingreso:F2} | Ahorrado: S/. {ahorro:F2}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener historial: {ex.Message}");
            }
            return historial;
        }
    }
}