using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Contratos;
using AccesoDatos.Entidades;
using AccesoDatos.Repositorios;
using Dominio.ObjetosValor;

namespace Dominio.Modelo
{
    public class MovimientoModelo
    {
        private DateTime fecha;
        private string nro_cuenta;
        private string tipo;
        private double importe;

        private IMovimientoRepositorio movimientoRepositorio;
        public EstadoEntidad estado { get; set; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public string Nro_cuenta { get => nro_cuenta; set => nro_cuenta = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public double Importe { get => importe; set => importe = value; }

        public MovimientoModelo()
        {
            movimientoRepositorio = new MovimientoRepositorio();
        }

        public string GuardarCambios()
        {
            string mensaje = null;
            try
            {
                var movimientoModeloDatos = new MOVIMIENTO();
                movimientoModeloDatos.fecha = fecha;
                movimientoModeloDatos.nro_cuenta = nro_cuenta;
                movimientoModeloDatos.tipo = tipo;
                movimientoModeloDatos.importe = importe;

                switch (estado)
                {
                    case EstadoEntidad.agregado:
                        movimientoRepositorio.añadir(movimientoModeloDatos);
                        mensaje = "La transacción se ha registrado Correctamente.";
                        break;
                    case EstadoEntidad.actualizado:
                        movimientoRepositorio.editar(movimientoModeloDatos);
                        mensaje = "La transacción se ha actualizado correctamente.";
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Data.SqlClient.SqlException sqlEx = ex as System.Data.SqlClient.SqlException;
                //si el registro esta duplicado
                if (sqlEx != null && sqlEx.Number == 2627)
                    mensaje = "Registro duplicado";
                else
                    mensaje = ex.ToString();
            }
            return mensaje;
        }

        public List<MovimientoModelo> Devolver()
        {
            var movimientoDatos = new CUENTA();
            movimientoDatos.nro_cuenta = nro_cuenta;

            var movimientoModeloDatos = movimientoRepositorio.devolverMovimiento(movimientoDatos.nro_cuenta);
            switch (estado)
            {
                case EstadoEntidad.devolviendo:
                    movimientoModeloDatos = movimientoRepositorio.devolverTodo();
                    break;
                case EstadoEntidad.devolviendoConsulta:
                    movimientoModeloDatos = movimientoRepositorio.devolverMovimiento(movimientoDatos.nro_cuenta);
                    break;
            }
            var listmovimiento = new List<MovimientoModelo>();
            foreach (MOVIMIENTO item in movimientoModeloDatos)
            {
                listmovimiento.Add(new MovimientoModelo
                {
                    fecha = item.fecha,
                    nro_cuenta = item.nro_cuenta,
                    tipo = item.tipo,
                    importe = item.importe,
                });
            }
            return listmovimiento;
        }
    }
}
