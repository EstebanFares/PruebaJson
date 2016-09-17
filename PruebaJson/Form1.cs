using Datos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaJson
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            desdeDateTimePicker.Value = DateTime.Today;
            hastaDateTimePicker.Value = DateTime.Today;
        }

        private void jsonButton_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Globales.LlenarDataTableSinTry(dt, "VentasSelParaJSON",
                new SqlParameter("@Desde", desdeDateTimePicker.Value),
                new SqlParameter("@Hasta", hastaDateTimePicker.Value.AddDays(1)));

            List<Venta> ventas = new List<Venta>();
            MessageBox.Show(dt.Rows.Count.ToString()    );
            foreach (DataRow r in dt.Rows)
            {
                Venta v = new Venta();
                v.letraComprobante = r["LetraComprobante"].ToString();
                v.tipoComprobante = r["TipoComprobante"].ToString();
                v.puntoDeVenta = int.Parse(r["PuntoDeVenta"].ToString());
                v.numeroComprobante = int.Parse(r["NumeroComprobante"].ToString());
                v.cae = r["CAE"].ToString();
                v.fechaComprobante = DateTime.Parse(r["FechaComprobante"].ToString());

                Cliente c = new Cliente();
                c.cuit = r["CUIT"].ToString();
                c.razonSocial = r["RazonSocial"].ToString();
                c.domicilioComercial = r["DomicilioComercial"].ToString();
                c.condicionIva = r["CondicionIva"].ToString();
                c.segmento = r["Segmento"].ToString();
                c.condicionIngresosBrutos = r["CondicionIngresosBrutos"].ToString();
                v.Cliente = c;

                v.condicionVenta = r["CondicionVenta"].ToString();
                v.fechaVencimiento = DateTime.Parse(r["FechaVencimiento"].ToString());
                v.esCanjePorCereal = bool.Parse(r["EsCanjePorCereal"].ToString());
                v.esCuentaYOrden = bool.Parse(r["EsCuentaYOrden"].ToString());
                v.moneda =r["Moneda"].ToString();
                v.tipoDeCambio = decimal.Parse(r["TipoDeCambio"].ToString());
                v.netoTotal = decimal.Parse(r["NetoTotal"].ToString());
                v.netoExentoTotal = decimal.Parse(r["NetoExentoTotal"].ToString());
                v.ivaTotal = decimal.Parse(r["IvaTotal"].ToString());
                v.impuestoInterno1 = decimal.Parse(r["ImpuestoInterno1"].ToString());
                v.impuestoInterno2 = decimal.Parse(r["ImpuestoInterno2"].ToString());
                v.impuestoInterno3 = decimal.Parse(r["ImpuestoInterno3"].ToString());
                v.percepcionIngresosBrutos = decimal.Parse(r["PercepcionIngresosBrutos"].ToString());
                v.percepcionIva = decimal.Parse(r["PercepcionIva"].ToString());
                v.totalComprobante = decimal.Parse(r["TotalComprobante"].ToString());
                v.anulado = bool.Parse(r["Anulado"].ToString());
                v.VentaDetalle = new List<VentaDetalle>();

                DataTable detDT = new DataTable();
                Globales.LlenarDataTable(detDT, "VentaDetalleSelParaJSON",
                    new SqlParameter("@ModFor", r["ModFor"].ToString()),
                    new SqlParameter("@CodFor", r["CodFor"].ToString()),
                    new SqlParameter("@NroFor", r["NroFor"].ToString()));
                foreach (DataRow dr in detDT.Rows)
                {
               VentaDetalle vd = new VentaDetalle();
                vd.numeroItem = int.Parse(dr["NumeroItem"].ToString());
                vd.codigoArticulo = dr["CodigoArticulo"].ToString();
                vd.descripcion = dr["Descripcion"].ToString();
                vd.cantidad = decimal.Parse(dr["Cantidad"].ToString());
                vd.unidadDeMedida = dr["UnidadDeMedida"].ToString();
                vd.precioUnitario = decimal.Parse(dr["PrecioUnitario"].ToString());
                vd.netoUnitario = decimal.Parse(dr["NetoUnitario"].ToString());
                vd.netoExentoUnitario = decimal.Parse(dr["NetoExentoUnitario"].ToString());
                vd.ivaUnitario = decimal.Parse(dr["IvaUnitario"].ToString());
                vd.impuestoInternoUnitario1 = decimal.Parse(dr["ImpuestoInternoUnitario1"].ToString());
                vd.impuestoInternoUnitario2 = decimal.Parse(dr["ImpuestoInternoUnitario2"].ToString());
                vd.impuestoInternoUnitario3 = decimal.Parse(dr["ImpuestoInternoUnitario3"].ToString());
                vd.porcentajeBonificacion = decimal.Parse(dr["PorcentajeBonificacion"].ToString());
                vd.observaciones = dr["Observaciones"].ToString();
                v.VentaDetalle.Add(vd);
                }

                v.razonSocialConsignatado = r["RazonSocialConsignatado"].ToString();
                v.cuitConsignatado = r["CuitConsignatado"].ToString();
                
                v.Remito = new List<string>();
                DataTable rxDT = new DataTable();
                Globales.LlenarDataTable(detDT, "VentaRemitoSelParaJSON",
                    new SqlParameter("@ModFor", r["ModFor"].ToString()),
                    new SqlParameter("@CodFor", r["CodFor"].ToString()),
                    new SqlParameter("@NroFor", r["NroFor"].ToString()));
                foreach (DataRow rx in rxDT.Rows)
                {
                    v.Remito.Add(rx["Remito"].ToString());                    
                }
                ventas.Add(v);
            }

            jsonTextBox.Text = JsonConvert.SerializeObject(ventas);
            
        }

        private void grabarButton_Click(object sender, EventArgs e)
        {

        }

        private void salirButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
