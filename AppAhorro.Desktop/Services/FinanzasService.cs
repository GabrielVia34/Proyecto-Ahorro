using System;
using AppAhorro.Backend.Dao;

namespace AppAhorro.Backend.Services
{
    
    public class FinanzasService
    {
        
        private readonly RegistroFinancieroDAO _registroFinancieroDao;
        public FinanzasService()
        {
            _registroFinancieroDao= new RegistroFinancieroDAO();
        }

        public string RegistrarMovimiento(int usuarioId, int mes, int año, double montoIngreso, double montoAhorro, int esVariable)
        {
        
            if (mes < 1 || mes > 12)
            {
                return "Error: El mes debe estar entre 1 y 12.";
            }

            if (montoIngreso < 0 || montoAhorro < 0)
            {
                return "Error: Los montos no pueden ser negativos.";
            }

            if (montoAhorro > montoIngreso)
            {
                return "Error: El monto de ahorro no puede ser mayor que el ingreso.";
            }
            bool exito = _registroFinancieroDao.InsertarRegistro(usuarioId, mes, año, montoIngreso, montoAhorro, esVariable);

            if (exito)
            {
                return "Registro financiero insertado con éxito.";
            }
            else
            {
                return "Error al insertar el registro financiero.";
            }
        }

    }
}
