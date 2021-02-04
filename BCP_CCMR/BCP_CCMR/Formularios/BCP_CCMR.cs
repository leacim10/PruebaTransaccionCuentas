using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio.Modelo;
using Dominio.ObjetosValor;

namespace BCP_CCMR
{
    public partial class Form1 : Form
    {
        private CuentaModelo cuenta = new CuentaModelo();
        private CuentaModelo cuenta2 = new CuentaModelo();
        private MovimientoModelo movimiento = new MovimientoModelo();
        string mensaje;

        public Form1()
        {
            InitializeComponent();
            paneles();
            mostrarOpciones();
            cmb00moneda.Items.Add("Bolivianos");
            cmb00moneda.Items.Add("Dólares");

            cmb00Tipo.Items.Add("Cuenta Corriente");
            cmb00Tipo.Items.Add("Cuenta de Ahorro");
        }

        private void paneles()
        {
            pnl00Cont.Size = new Size(pnl00Cont.Width, 0);
            pnl01Cont.Size = new Size(pnl01Cont.Width, 0);
            pnl02Cont.Size = new Size(pnl02Cont.Width, 0);
            pnl03Cont.Size = new Size(pnl03Cont.Width, 0);
            pnl04Cont.Size = new Size(pnl04Cont.Width, 0);
        }
        private void ocultarOpciones()
        {
            pnl00.Size = new Size(pnl00.Width, 0);
            pnl01.Size = new Size(pnl01.Width, 0);
            pnl02.Size = new Size(pnl02.Width, 0);
            pnl03.Size = new Size(pnl03.Width, 0);
            pnl04.Size = new Size(pnl04.Width, 0);
        }
        private void mostrarOpciones()
        {
            pnl00.Size = new Size(pnl00.Width, 80);
            pnl01.Size = new Size(pnl01.Width, 80);
            pnl02.Size = new Size(pnl02.Width, 80);
            pnl03.Size = new Size(pnl03.Width, 80);
            pnl04.Size = new Size(pnl04.Width, 80);
        }
            
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            paneles();
            ocultarOpciones();
            pnl00Cont.Size = new Size(pnl00Cont.Width, 450);
            limpiar00Adicionar();
        }

        private void btnDepositosRetiros_Click(object sender, EventArgs e)
        {
            paneles();
            ocultarOpciones();
            pnl01Cont.Size = new Size(pnl01Cont.Width, 320);
            limpiar01Depositos();
            listarCuentas();
        }

        private void btnTransferencia_Click(object sender, EventArgs e)
        {
            paneles();
            ocultarOpciones();
            pnl02Cont.Size = new Size(pnl02Cont.Width, 510);
            limpiar02Transferencia();
            listarCuentas();
        }

        private void btnSaldo_Click(object sender, EventArgs e)
        {
            paneles();
            ocultarOpciones();
            pnl03Cont.Size = new Size(pnl03Cont.Width, 380);
            devolverSaldosCuentas();
        }

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            paneles();
            ocultarOpciones();
            pnl04Cont.Size = new Size(pnl04Cont.Width, 700);
            limpiar04Movimientos();
            listarCuentas();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            paneles();
            mostrarOpciones();
        }

        ///*
        /// 
        ///
        public void limpiar00Adicionar()
        {
            txt00numero.Text = "";
            cmb00moneda.SelectedIndex = 0;
            cmb00Tipo.SelectedIndex = 0;
            txt00numero.Text = "";
        }

        public void limpiar01Depositos()
        {
            //cmb01Cuenta.SelectedIndex = 0;
            txt01Monto.Text = "";
        }

        public void limpiar02Transferencia()
        {
            cmb02Cuenta.Text = "";
            txt02Saldo.Text = "";
            cmb02CuentaDestino.Text = "";
            txt02Monto.Text = "";
        }

        public void limpiar04Movimientos()
        {
            lbl04Nombre.Text = "";
            lbl04Saldo.Text = "";
            lbl04Moneda.Text = "";
        }

        private void btn00Guardar_Click(object sender, EventArgs e)
        {
            if (txt00numero.Text!="" && txt00nombre.Text!="")
            {
                cuenta.Nro_cuenta = txt00numero.Text;
               
                if (cmb00Tipo.SelectedIndex == 0)
                    cuenta.Tipo = "CTE";
                else if (cmb00Tipo.SelectedIndex == 1)
                    cuenta.Tipo = "AHO";

                if (cmb00moneda.SelectedIndex == 0)
                    cuenta.Moneda = "Bs";
                else if (cmb00moneda.SelectedIndex == 1)
                    cuenta.Moneda = "$us";

                cuenta.Nombre = txt00nombre.Text;
                cuenta.Saldo = 0;

                bool valid = new Soporte.ValidacionDatos(cuenta).validacion();
                if (valid)
                {
                    mensaje = cuenta.GuardarCambios();
                    MessageBox.Show(mensaje);
                }

                importeCuenta("A", 0);
                paneles();
                mostrarOpciones();
            }
        }

        private void btn00Cancelar_Click(object sender, EventArgs e)
        {
            paneles();
            mostrarOpciones();
        }

        private void btn01Retiro_Click(object sender, EventArgs e)
        {
            if (cmb01Cuenta.Text!="" && txt01Monto.Text!="")
            {
                if (Convert.ToDouble(txt01Monto.Text)>cuenta.Saldo)
                {
                    MessageBox.Show("El monto es mayor a su saldo.");
                }
                else
                {
                    transaccion("D", Convert.ToDouble(txt01Monto.Text));
                    paneles();
                    mostrarOpciones();
                }
            }
            
        }

        private void btn01Deposito_Click(object sender, EventArgs e)
        {
            if (cmb01Cuenta.Text != "" && txt01Monto.Text != "")
            {
                transaccion("A", Convert.ToDouble(txt01Monto.Text));
                paneles();
                mostrarOpciones();
            }
        }

        private void btn02Aceptar_Click(object sender, EventArgs e)
        {
            if (cmb02Cuenta.Text !="" && cmb02CuentaDestino.Text!="" && txt02Monto.Text!="")
            {
                if (Convert.ToDouble(txt02Monto.Text) > cuenta.Saldo)
                { 
                    MessageBox.Show("El monto es superior a su saldo disponible.");
                }
                else
                {
                    // se retira el dinero de la cuenta 1 y se realiza el importe
                    transaccion("D", Convert.ToDouble(txt02Monto.Text));

                    // se abona a la segunda cuenta y se realiza el importe
                    cuenta.Nro_cuenta = cuenta2.Nro_cuenta;
                    cuenta.Tipo = cuenta2.Tipo;
                    cuenta.Moneda = cuenta2.Moneda;
                    cuenta.Nombre = cuenta2.Nombre;
                    cuenta.Saldo = cuenta2.Saldo;
                    transaccion("A", Convert.ToDouble(txt02Monto.Text));

                    paneles();
                    mostrarOpciones();
                }
                
            }
            else
            {
                MessageBox.Show("Debe llenar todo los campos.");
            }
        }

        private void btn02Cancelar_Click(object sender, EventArgs e)
        {
            paneles();
            mostrarOpciones();
        }

        private void cmb00Tipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb00Tipo.SelectedIndex == 0)
            { 
                txt00numero.MaxLength = 13;
                txt00numero.Text = "";
            }
            else if (cmb00Tipo.SelectedIndex == 1)
            { 
                txt00numero.MaxLength = 14;
                txt00numero.Text = "";
            }
        }

        private void listarCuentas()
        {
            try 
            {
                cmb01Cuenta.Items.Clear();
                cmb02Cuenta.Items.Clear();
                cmb02CuentaDestino.Items.Clear();
                cmb04Cuenta.Items.Clear();

                cuenta.estado = EstadoEntidad.devolviendo;
                var listCuentas = cuenta.Devolver();
                foreach(var item in listCuentas)
                {
                    cmb01Cuenta.Items.Add(item.Nro_cuenta);
                    cmb02Cuenta.Items.Add(item.Nro_cuenta);
                    cmb02CuentaDestino.Items.Add(item.Nro_cuenta);
                    cmb04Cuenta.Items.Add(item.Nro_cuenta);
                    cmb01Cuenta.SelectedIndex = 0;
                    cmb04Cuenta.SelectedIndex = 0;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void devolverCuenta(string nro_cuenta)
        {
            try
            {
                cuenta.estado = EstadoEntidad.devolviendo;
                cuenta.Nro_cuenta = nro_cuenta;
                var listCuentas = cuenta.DevolverConsulta();
                foreach (var item in listCuentas)
                {
                    cuenta.Nro_cuenta = item.Nro_cuenta;
                    cuenta.Tipo = item.Tipo;
                    cuenta.Moneda = item.Moneda;
                    cuenta.Nombre = item.Nombre;
                    cuenta.Saldo = item.Saldo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
         private void devolverSaldosCuentas()
        {
            try
            {
                data03Saldo.Rows.Clear();
                var listCuentas = cuenta.Devolver();
                foreach (var item in listCuentas)
                {
                    data03Saldo.Rows.Add(item.Tipo, item.Moneda, item.Nro_cuenta, item.Nombre, item.Saldo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void transaccion(string tipo, double importe)
        {
            
            cuenta.Nombre = cmb01Cuenta.Text;

            if (tipo == "A")
                cuenta.Saldo = cuenta.Saldo + importe;
            else if (tipo == "D")
                cuenta.Saldo = cuenta.Saldo - importe;

            cuenta.estado = EstadoEntidad.actualizado;
            bool valid = new Soporte.ValidacionDatos(cuenta).validacion();
            if (valid)
            {
                mensaje = cuenta.GuardarCambios();
                MessageBox.Show(mensaje);
            }

            importeCuenta(tipo, importe);
            
        }
        private void importeCuenta(string tipo, double importe)
        {
            movimiento.Fecha = DateTime.Now;
            movimiento.Nro_cuenta = cuenta.Nro_cuenta;
            movimiento.Tipo = tipo;

            if (tipo == "A")
                movimiento.Importe = importe;
            else if (tipo == "D")
                movimiento.Importe = importe * (-1);

            
            movimiento.estado = EstadoEntidad.agregado;
            bool valid = new Soporte.ValidacionDatos(movimiento).validacion();
            if (valid)
            {
                mensaje = movimiento.GuardarCambios();
               // MessageBox.Show(mensaje);
            }
        }

        private void devolverImporte()
        {
            try
            {
                data04Movimiento.Rows.Clear();
                movimiento.Nro_cuenta = cuenta.Nro_cuenta;
                movimiento.estado = EstadoEntidad.devolviendoConsulta;
                var listmovimientos = movimiento.Devolver();
                foreach (var item in listmovimientos)
                {
                    data04Movimiento.Rows.Add(item.Fecha, item.Importe);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cmb01Cuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            devolverCuenta(cmb01Cuenta.Text);
        }

        private void cmb02Cuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            devolverCuenta(cmb02Cuenta.Text);
            txt02Saldo.Text = cuenta.Saldo.ToString();
        }

        private void cmb02CuentaDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cuenta2.estado = EstadoEntidad.devolviendo;
                cuenta2.Nro_cuenta = cmb02CuentaDestino.Text;
                var listCuentas = cuenta2.DevolverConsulta();
                foreach (var item in listCuentas)
                {
                    cuenta2.Nro_cuenta = item.Nro_cuenta;
                    cuenta2.Tipo = item.Tipo;
                    cuenta2.Moneda = item.Moneda;
                    cuenta2.Nombre = item.Nombre;
                    cuenta2.Saldo = item.Saldo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cmb04Cuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            devolverCuenta(cmb04Cuenta.Text);
            lbl04Nombre.Text = cuenta.Nombre;
            lbl04Saldo.Text = cuenta.Saldo.ToString();
            lbl04Moneda.Text = cuenta.Moneda;

            devolverImporte();
        }
    }
}
