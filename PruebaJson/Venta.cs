using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaJson
{
    class Venta
    {        
        public string letraComprobante { get; set; }
        public string tipoComprobante { get; set; }
        public int puntoDeVenta { get; set; }
        public int numeroComprobante { get; set; }
        public string cae { get; set; }
        public DateTime fechaComprobante { get; set; }        
        public Cliente Cliente { get; set; }
        public string condicionVenta { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public bool esCanjePorCereal { get; set; }
        public bool esCuentaYOrden { get; set; }
        public string moneda { get; set; }
        public decimal tipoDeCambio { get; set; }
        public decimal netoTotal { get; set; }
        public decimal netoExentoTotal { get; set; }
        public decimal ivaTotal { get; set; }
        public decimal impuestoInterno1 { get; set; }
        public decimal impuestoInterno2 { get; set; }
        public decimal impuestoInterno3 { get; set; }
        public decimal percepcionIngresosBrutos { get; set; }
        public decimal percepcionIva { get; set; }
        public decimal totalComprobante { get; set; }
        public bool anulado { get; set; }
        public List<VentaDetalle> VentaDetalle { get; set; }
        public List<String> Remito { get; set; }
        public string razonSocialConsignatado { get; set; }
        public string cuitConsignatado { get; set; }        

    }

    class Cliente
    {
        public string cuit { get; set; }
        public string razonSocial { get; set; }
        public string domicilioComercial { get; set; }
        public string condicionIva { get; set; }
        public string segmento { get; set; }
        public string condicionIngresosBrutos { get; set; }               

    }

    class VentaDetalle
    {
        public int numeroItem { get; set; }
        public string codigoArticulo { get; set; }
        public string descripcion { get; set; }
        public decimal cantidad { get; set; }
        public string unidadDeMedida { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal netoUnitario { get; set; }
        public decimal netoExentoUnitario { get; set; }
        public decimal ivaUnitario { get; set; }
        public decimal impuestoInternoUnitario1 { get; set; }
        public decimal impuestoInternoUnitario2 { get; set; }
        public decimal impuestoInternoUnitario3 { get; set; }
        public decimal porcentajeBonificacion { get; set; }
        public string observaciones { get; set; }                       

    }
}
