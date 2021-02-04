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
    public class CuentaModelo
    {
        private string nro_cuenta;
        private string tipo;
        private string moneda;
        private string nombre;
        private double saldo;

        private ICuentaRepositorio cuentaRepositorio;
        public EstadoEntidad estado { get; set; }
        public string Nro_cuenta { get => nro_cuenta; set => nro_cuenta = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string Moneda { get => moneda; set => moneda = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public double Saldo { get => saldo; set => saldo = value; }

        public CuentaModelo()
        {
            cuentaRepositorio = new CuentaRepositorio();
        }

        public string GuardarCambios()
        {
            string mensaje = null;
            try
            {
                var cuentaModeloDatos = new CUENTA();
                cuentaModeloDatos.nro_cuenta = nro_cuenta;
                cuentaModeloDatos.tipo = tipo;
                cuentaModeloDatos.moneda = moneda;
                cuentaModeloDatos.nombre = nombre;
                cuentaModeloDatos.saldo = saldo;

                switch(estado)
                {
                    case EstadoEntidad.agregado:
                        cuentaRepositorio.añadir(cuentaModeloDatos);
                        mensaje = "La cuenta se ha registrado Correctamente.";
                        break;
                    case EstadoEntidad.actualizado:
                        cuentaRepositorio.editar(cuentaModeloDatos);
                        mensaje = "La cuenta se ha actualizado correctamente.";
                        break;
                }
            }
            catch(Exception ex)
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

        public List<CuentaModelo> Devolver()
        {
            var cuentaDatos = new CUENTA();

            var cuentaModeloDatos = cuentaRepositorio.devolverTodo();
            
            var listCuenta = new List<CuentaModelo>();
            foreach(CUENTA item in cuentaModeloDatos)
            {
                listCuenta.Add(new CuentaModelo
                {
                    nro_cuenta = item.nro_cuenta,
                    tipo = item.tipo,
                    moneda = item.moneda,
                    nombre = item.nombre,
                    saldo = item.saldo,
                });
            }
            return listCuenta;
        }

        public List<CuentaModelo> DevolverConsulta()
        {
            var cuentaDatos = new CUENTA();
            cuentaDatos.nro_cuenta = nro_cuenta;

            var cuentaModeloDatos = cuentaRepositorio.devolverCuenta(cuentaDatos.nro_cuenta);
            var listCuenta = new List<CuentaModelo>();
            foreach (CUENTA item in cuentaModeloDatos)
            {
                listCuenta.Add(new CuentaModelo
                {
                    nro_cuenta = item.nro_cuenta,
                    tipo = item.tipo,
                    moneda = item.moneda,
                    nombre = item.nombre,
                    saldo = item.saldo,
                });
            }
            return listCuenta;
        }
    }
}
