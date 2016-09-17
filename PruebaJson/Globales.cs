using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Drawing;
using System.Globalization;
using System.Security.Cryptography;
using System.Windows.Forms;
using DataLayer;

namespace Datos
{
    public static class Globales
    {
        #region BASE DE DATOS        

        //SERVER       
        //public static SqlServerDatabase BD = new SqlServerDatabase(@"Persist Security Info=True;Initial Catalog=SISTEMA;Data Source=192.168.0.10\AZARHNOS;User ID=usuarioSQL;Password=entrensipueden;"); 
        //SERVER BACK      
        //public static SqlServerDatabase BD = new SqlServerDatabase(@"Persist Security Info=True;Initial Catalog=SISTEMA;Data Source=192.168.0.10;User ID=usuarioSQL;Password=entrensipueden;");
        //public static string ServidorImpresion = "192.168.0.10";
        //public static bool versionCompletaSistema = true;
        //MAQUINA ESTEBAN        
        static string cs = System.IO.File.ReadAllText(@"C:\Sistema\Config.txt");
        static string server = cs.Split(';')[0];
        public static string dbase = cs.Split(';')[1];
        public static SqlServerDatabase BD = new SqlServerDatabase(@"Persist Security Info=False; Initial Catalog=" + dbase + ";Data Source=" + server + ";Failover Partner=192.168.0.127;User ID=usuario;Password=entrensipueden;Network=dbmssocn;");
        public static string ServidorImpresion = server;//Environment.MachineName;//@"Esteban\Esteban";//
        public static bool versionCompletaSistema = true;
        //IDEAL
        //public static SqlServerDatabase BD = new SqlServerDatabase(@"Persist Security Info=True;Initial Catalog=IDEAL;Data Source=Esteban\Esteban;User ID=usuario;Password=entrensipueden;");
        //public static string ServidorImpresion = @"Esteban\Esteban";

        public static string BaseDatosImpresion = dbase;//"SISTEMA"; //
        public static string UsuarioImpresion = "usuarioSQL";
        public static string ContraseñaImpresion = "entrensipueden";

        public static void EjecutarProcedimientoAlmacenado(string procedimiento, params SqlParameter[] parametros)
        {
            BD.OpenConnection();
            BD.ExecuteNonQueryThroughStoredProcedure(procedimiento, parametros);
            BD.CloseConnection();
        }

        public static void ejecutarInstruccion(string instruccion)
        {
            Globales.BD.OpenConnection();
            Globales.BD.ExecuteNonQueryThroughText(instruccion);
            Globales.BD.CloseConnection();
        }

        public static void EjecutarProcedimientoAlmacenadoLargo(string procedimiento, params SqlParameter[] parametros)
        {
            //BD.OpenConnection();
            //BD.ExecuteNonQueryThroughStoredProcedureLargo(procedimiento, parametros);
            //BD.CloseConnection();
        }

        public static void EjecutarProcedimientoAlmacenadoEnOtraBD(SqlServerDatabase cs, string procedimiento, params SqlParameter[] parametros)
        {
            cs.OpenConnection();
            cs.ExecuteNonQueryThroughStoredProcedure(procedimiento, parametros);
            cs.CloseConnection();
        }

        public static void EjecutarProcedimientoAlmacenadoEnContexto(ISqlServerExecutionContext t, string procedimiento, params SqlParameter[] parametros)
        {
            t.ExecuteNonQueryThroughStoredProcedure(procedimiento, parametros);            
        }

        public static void LlenarDataTableEnContexto(ISqlServerExecutionContext t, DataTable dataTable, string procedimiento, params SqlParameter[] parametros)
        {
            //BD.OpenConnection();
            dataTable.Clear();
            t.FillDataTableWithStoredProcedure(dataTable, procedimiento, parametros);
            //BD.CloseConnection();
        }

        public static void LlenarDataTable(DataTable dataTable, string procedimiento, params SqlParameter[] parametros)
        {
            try
            {                
                BD.OpenConnection();                
                dataTable.Clear();
                BD.FillDataTableWithStoredProcedure(dataTable, procedimiento, parametros);
                BD.CloseConnection();
            }
            catch (Exception)
            {
                if (BD.DatabaseState == DatabaseState.Opened)
                {
                    BD.CloseConnection();
                }
            }
            
        }

        public static void LlenarDataTableLargo(DataTable dataTable, string procedimiento, params SqlParameter[] parametros)
        {
            try
            {
                BD.OpenConnection();
                dataTable.Clear();
                BD.FillDataTableWithStoredProcedureLargo(dataTable, procedimiento, parametros);
                BD.CloseConnection();
            }
            catch (Exception)
            {
                if (BD.DatabaseState == DatabaseState.Opened)
                {
                    BD.CloseConnection();
                }
            }

        }

        public static void LlenarDataTableEnOtraBD(SqlServerDatabase cs,DataTable dataTable, string procedimiento, params SqlParameter[] parametros)
        {
            //try
            //{
                cs.OpenConnection();
                dataTable.Clear();
                cs.FillDataTableWithStoredProcedure(dataTable, procedimiento, parametros);
                cs.CloseConnection();
            //}
            //catch (Exception)
            //{
            //    if (BD.DatabaseState == DatabaseState.Opened)
            //    {
            //        BD.CloseConnection();
            //    }
            //}

        }

        public static void LlenarDataTableSinTry(DataTable dataTable, string procedimiento, params SqlParameter[] parametros)
        {            
            BD.OpenConnection();
            dataTable.Clear();
            BD.FillDataTableWithStoredProcedure(dataTable, procedimiento, parametros);
            BD.CloseConnection();
        }

        #endregion

        #region Valores Globales
        #region Ceci?
        public static string[] Busqueda;
        public static string UsuarioLogueo; 
        #endregion

        #region EMPRESA
        public static int EmpresaID;
        public static int SucursalID;
        public static int PuntoVenta = 1;
        public static int PuntoVentaManual;
        public static bool conectadoAImpresorFiscal = false;
        public static string modeloImpresorFiscal = "TMU220";

        public static byte[] LogoEmpresaArregloBytes;
        public static Image LogoEmpresa;

        public static void SetearLogoEmpresa()
        {
            System.Drawing.Image newImage;
            using (MemoryStream ms = new MemoryStream(Globales.LogoEmpresaArregloBytes, 0, Globales.LogoEmpresaArregloBytes.Length))
            {
                ms.Write(Globales.LogoEmpresaArregloBytes, 0, Globales.LogoEmpresaArregloBytes.Length);
                newImage = Image.FromStream(ms, true);
                LogoEmpresa = newImage;
            }
        }

        public static string Sucursal;
        public static string cuit;
        public static string condicionIVA;
        public static string ib;
        public static string telefono;
        public static string fax;
        public static string email;
        public static string direccion;
        public static DateTime inicioActividad;

        public static DataTable empresasDT = new DataTable();

        public static int cantidadDecimales = 3;
        public static int CantidadDecimalesCalculoPrecio = 0;
        public static double SegundosDemoraItemImpresionFiscal = 0;
        #endregion

        #region Generales, PC, Usuario, Rol        
        public static DateTime fechaHoraServidor()
        {
            DateTime d = DateTime.Now;
            DataTable dt = new DataTable();
            LlenarDataTable(dt, "ServidorSelFechaHora");
            if (dt.Rows.Count > 0)
            {
                d = DateTime.Parse(dt.Rows[0][0].ToString());
            }
            return d;
        }
        public static int UsuarioID;
        public static string Usuario;
        public static int DepositoID;
        public static string Deposito;
        public static string Maquina;        

        public static DateTime fechaServidor()
        {
            DateTime d = DateTime.Today;

            //System.Net.Sockets.TcpClient t = new System.Net.Sockets.TcpClient("192.168.0.10", 8080);
            //t.GetStream();
            //System.IO.StreamReader rd = new System.IO.StreamReader(t.GetStream());
            //rd.ReadToEnd();
            //rd.Close();
            //t.Close();

            return d;
        }

        public static bool PantallaCompleta = true;
        public static bool EsAdministrador = false;
        public static bool EsEncargado = false;
        public static bool EsCajero = false;
        public static bool EsVendedor = false;
        public static decimal UnCentavo = decimal.Parse("0.01");

        public static decimal PorcentajeModificacionPrecio = decimal.Parse("0.05");
        public static decimal PorcentajeMaximoDescuento = 0;
        public static decimal PorcentajeMaximoDescuentoUsuarioLogueado = 0;
        //public static DataLayer.DataSet.vRolPermisoActivoDataTable permisos;

        public static bool calculaPorcentajeEnModificacionPrecios = true; //PARA VER SI CALCULA PRECIO VENTA SOLO O NO EN ARTICULO
        public static bool calculaMarkUpSegunPrecioDeContado = true;

        public static bool esMaquinaDeSalon = false;
        public static bool imprimeProformas = true;
        public static bool imprimePapelReducido = false;        
        public static bool mensajeSiNoRespuestaPorDefecto = false;
        public static bool muestraArqueoEnCierreCaja = true;
        public static bool muestraEstadisticasEnCierreCaja = true;

        public static int diasParaConsiderarProximoAVencer = 2;

        public static int unidadesDeMedida = 1;

        public static bool cargaNumeroVendedor = true;
        public static bool muestraStockEnVenta = false;
        public static bool usaCantidadRegalo = false;
        #endregion

        #region Compras
        public static decimal RetencionIVA = decimal.Parse("0.1");
        public static decimal RetencionIB = decimal.Parse("0.1");
        public static decimal RetencionGANANCIAS = decimal.Parse("0.1");
        public static decimal PercepcionIVA = decimal.Parse("0.03");

        public static decimal MontoLimiteParaRetencionesIBEnPago = decimal.Parse("668");
        public static decimal PorcentajeRetencionesIBConvenioEnPago = decimal.Parse("1.0");
        public static decimal PorcentajeRetencionesIBSinConvenioEnPago = decimal.Parse("3.0");

        public static bool imprimeCartaPorteCero = false;
        #endregion

        #region Ventas
        public static int puertoCOM = 1;
        public static bool imprimeMediaHoja = true; 
        public static bool imprimeComoTicket = false;
        public static int cantidadCopiasImpresionVenta = 1;
        public static int ticketLineaCliente = 9;
        public static int ticketLineaClienteDomicilio = 10;
        public static int ticketLineaVendedor = 11;
        public static int facturaLineaVendedor = 11;
        public static bool ventaCheques = false;
        public static int diasClearing = 2;

        public static bool muestraCierreCajaEnCajaGeneral = false;
        public static bool cuentaCorrienteClienteUnica = true;
        public static bool pideAutorizacionParaNC = false;
        public static bool ConsultaComprobantesConHora = false;
        public static bool cierraPantallaVentas = true;        
        public static bool muestraTodosLosCamposTarjeta = true;
        public static bool validaCupones = false;
        public static bool imprimeCodigoBarraPropio = true;
        public static bool imprimeCodigoBarraProeedorYPropio = false;
        public static DataTable composicionCodigoBarraPropio;
        public static bool usaCodigoPropio = false;
        public static bool usaCodigoBarra = true;
        public static string tipoCodigoImprimeTicket = "PR";
        public static bool usaCurvaArticulos = true;
        public static bool focoEnCantidadPorDefecto = true;
        public static bool calculaPorcentajeEnCombinacion = true;
        public static int lineasEnComprobante = 8;
        public static decimal ivaTasaGeneral = decimal.Parse("21");
        public static decimal ivaTasaReducida = decimal.Parse("10.5");
        public static decimal ivaTasaEspecial = decimal.Parse("27");

        public static string contraseñaDescuento = "billy";
        public static decimal GastosAdministrativos = decimal.Parse("6.25");

        public static DataTable liquidacionVentas = new DataTable();

        public static bool ImprimePrecioSinBonificacionTicketFiscal = false;
        public static decimal MontoLimiteTicketFiscal = decimal.Parse("0");

        public static bool imprimeVentaSinPrevisualizar = true;
        public static bool imprimeCambioSinPrevisualizar = true;
        public static bool imprimeCobranzaSinPrevisualizar = true;
        public static bool imprimeRemitoSinPrevisualizar = true;

        public static bool ValidaCodigoCargadoEnVentas = false;
        public static bool ValidaUltimoComprobanteImpresoCorrectamente = false;
        public static bool cargaClienteSiempre = false;
        public static bool ValidaPocoPapel = false;
        public static string impresoraOE = "";
        #endregion

        #region Tarjetas
        public static decimal ArancelDeLeyTarjetas = decimal.Parse("0.03");
        public static decimal MontoLimiteParaCobroRetencionesIVATarjetas = decimal.Parse("400");
        public static decimal PorcentajeRetencionesIVATarjetas = decimal.Parse("0.03");
        public static decimal MontoLimiteParaCobroRetencionesGananciasTarjetas = decimal.Parse("728");        
        public static decimal PorcentajeRetencionesGananciasTarjetas = decimal.Parse("0.01");
        public static decimal MontoLimiteParaCobroRetencionesIBTarjetas = decimal.Parse("168");
        public static decimal PorcentajeRetencionesIBTarjetas = decimal.Parse("0.02");        
        #endregion
              
        #region Creditos
        public static string palabraCancelaCredito = "N";
        public static string palabraSuspendeCredito = "S";
        public static int cantidadCuotasEnPrecioListaEquivalenteAContado = int.Parse("6"); 
        #endregion  

        #region Presupuesto
        public static int diasVencimientoPresupuesto = 7;
        public static bool puedeCambiarFechaEmisionPresupuesto = true;
        public static bool puedeCambiarFechaVencimientoPresupuesto = true; 
        #endregion

        #region Clientes
        public static bool buscaClientePorDescripcion = true;
        public static string SexoPorDefecto = "M" ;
        public static string EstadoCivilPorDefecto = "SO";
        public static int ProvinciaPorDefecto = 21;
        public static int LocalidadPorDefecto = 60419;
        public static int condicionIVAIDporDefecto = 1;
        public static decimal limiteCreditoCliente = 500;
        #endregion

        #region Bar
        public static bool muestraIconosArticulos = true;
        public static bool pideYEntregaAMesa = true;
        public static bool imprimeComandaEnCocina = false;
        #endregion
        #endregion

        #region Funciones
        public static string devolverNumeroComercioEnFinanciera(int financieraID,int conEntrega,int empresaID , int sucursalID)
        {
            DataTable dt = new DataTable();
            LlenarDataTable(dt, "TarjetaCreditoSucursalSel",
                new SqlParameter("@TarjetaID", financieraID),
                new SqlParameter("@conEntrega", conEntrega),
                new SqlParameter("@EmpresaID", empresaID),
                new SqlParameter("@SucursalID", sucursalID));
            if (dt.Rows.Count > 0)
            {
                string codigo = "";
                try
                {
                    codigo = dt.Rows[0]["NumeroComercio"].ToString().Trim() + " - " + dt.Rows[0]["Abreviacion"].ToString().Trim() + " (" + Globales.UsuarioID + ")";
                }
                catch (Exception)
                {
                }
                

                return codigo;
            }
            else //SOLO AZAR HNOS
            {
                if (empresaID == 1)
                {
                    return "136 - CL" + " (" + Globales.UsuarioID + ")";
                }
                else
                {
                    if (sucursalID == 1)
                    {
                        return "135 - LH" + " (" + Globales.UsuarioID + ")"; ;
                    }
                    else
                    {
                        return "209 - SC" + " (" + Globales.UsuarioID + ")"; ;
                    }
                }
            }
        }

        public static string quitarCaracteresInvalidos(string s)
        {
            s = s.ToUpper();
            char[] c = s.ToCharArray();

            for (int i = 0; i < c.Length; i++)
            {
                if (c[i].ToString() == @"[")
                {

                }
                int n = Encoding.ASCII.GetBytes(c[i].ToString())[0];
                if ((n > 47 && n < 58) | (n > 64 && n < 91) | (n == 32))
                {

                }
                else
                {
                    s = s.Replace(c[i].ToString(), "");
                }
            }
            return s;
        }

        public static bool validarCuit(string cuit)
        {
            try
            {
                string d1 = cuit.Substring(0, 1);
                string d2 = cuit.Substring(1, 1);
                string d3 = cuit.Substring(2, 1);
                string d4 = cuit.Substring(3, 1);
                string d5 = cuit.Substring(4, 1);
                string d6 = cuit.Substring(5, 1);
                string d7 = cuit.Substring(6, 1);
                string d8 = cuit.Substring(7, 1);
                string d9 = cuit.Substring(8, 1);
                string d10 = cuit.Substring(9, 1);
                string d11 = cuit.Substring(10, 1);

                decimal subtotal;
                subtotal = decimal.Parse(d1) * 5;
                subtotal = (decimal.Parse(d2) * 4) + subtotal;
                subtotal = (decimal.Parse(d3) * 3) + subtotal;
                subtotal = (decimal.Parse(d4) * 2) + subtotal;
                subtotal = (decimal.Parse(d5) * 7) + subtotal;
                subtotal = (decimal.Parse(d6) * 6) + subtotal;
                subtotal = (decimal.Parse(d7) * 5) + subtotal;
                subtotal = (decimal.Parse(d8) * 4) + subtotal;
                subtotal = (decimal.Parse(d9) * 3) + subtotal;
                subtotal = (decimal.Parse(d10) * 2) + subtotal;

                decimal resto = (subtotal % 11);
                decimal digito = 0;
                if (resto != 0)
                {
                    digito = (11 - resto);
                }
                else
                {
                    digito = 0;
                }

                if (digito != decimal.Parse(d11))
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string formatoMoneda(string monto)
        {
            
            string sd = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            
            if (sd == ".")
            {
                monto = monto.Replace("$", "");
                double montoRedondeado = double.Parse(monto);
                montoRedondeado = redondearAMiles(montoRedondeado);//redondearAMiles(montoRedondeado);

                string resultado = montoRedondeado.ToString();
                string[] s = montoRedondeado.ToString().Split('.');
                if (s.Length == 1)
                {
                    //resultado = s[0] + ".000";
                    resultado = s[0] + ".00";
                }
                else
                {
                    //if (s[1].Length < 3)
                    if (s[1].Length < 2)
                    {
                        for (int i = s[1].Length; i < 2; i++) { s[1] += "0"; }
                    }

                    s[1] = s[1].Substring(0, 2);
                    resultado = s[0] + "." + s[1];
                }

                return resultado;
            }
            if (sd == ",")
            {
                monto = monto.Replace("$", "");
                double montoRedondeado = double.Parse(monto);
                montoRedondeado = redondearAMiles(montoRedondeado);//redondearAMiles(montoRedondeado);

                string resultado = montoRedondeado.ToString();
                string[] s = montoRedondeado.ToString().Split(',');
                if (s.Length == 1)
                {
                    //resultado = s[0] + ".000";
                    resultado = s[0] + ",00";
                }
                else
                {
                    //if (s[1].Length < 3)
                    if (s[1].Length < 2)
                    {
                        for (int i = s[1].Length; i < 2; i++) { s[1] += "0"; }
                    }

                    s[1] = s[1].Substring(0, 2);
                    resultado = s[0] + "," + s[1];
                }

                return resultado;
            }
            return monto;
        }
        
        private static double redondearAMiles(double montoRedondeado)
        {
            //double m = Math.Floor(montoRedondeado * 100 + .5);
            return Math.Floor(montoRedondeado *100 +.5)/100;            
        }

        private static double redondearACentesimos(double montoRedondeado)
        {
            //double m = Math.Floor(montoRedondeado * 100 + .5);
            return Math.Floor(montoRedondeado * 10 + .5) / 10;
        }

        public static double redondearAEnteros(double montoRedondeado)
        {
            //double m = Math.Floor(montoRedondeado * 100 + .5);
            return Math.Floor(montoRedondeado + .5);
        }

        public static string FormatoANumeroComprobante(string num)
        {
            num = num.Trim();
            int longitud = num.Length;
            if (longitud <= 8)
            {
                for (int i = longitud; i < 8; i++)
                {
                    num = "0" + num;
                }
                num = "0000-" + num;
            }
            else
            {
                for (int i = longitud; i < 12; i++)
                {
                    num = "0" + num;
                }
                string s1 = num.Substring(0, 4);
                string s2 = num.Substring(4, 8);
                num = s1 + "-" + s2;
            }

            return num;
        }

        public static string autoCompletarNumeroComprobante(string numero)
        {
            if (numero.Length <= 4)
            {
                string ceros = string.Empty;
                for (int i = numero.Length; i < 4; i++)
                {
                    ceros += "0";
                }
                numero = ceros + numero;
            }
            else
            {
                string primeraParte = numero.Substring(0, 4);
                string segundaParte = numero.Substring(5);
                string ceros = string.Empty;
                for (int i = segundaParte.Length; i < 8; i++)
                {
                    ceros += "0";
                }
                numero = primeraParte + "-" + ceros + segundaParte;
            }
            return numero;
        }

        public static string completarDigitosDeCodigoDecimal(string codigo)
        {
            try
            {
                decimal d = decimal.Parse(codigo);

                for (int i = codigo.Length; i < 3; i++)
                {
                    codigo = "0" + codigo;
                }
            }
            catch (Exception)
            {

            }

            return codigo;
        }

        public static string completarConCerosCodigoAticuloBuscadoSiEsNumerico(string codigo)
        {
            try
            {
                decimal d = decimal.Parse(codigo);
                for (int i = codigo.Length; i < 8; i++)
                {
                    codigo = "0" + codigo;
                }
            }
            catch (Exception)
            {

            }
            return codigo;
        }

        public static string devolverCodigoCorto(string codigo)
        {
            if (Globales.esDecimal(codigo))
            {
                if (codigo.Contains("-"))
                {
                    return codigo;
                }
                if (codigo.Length == Globales.longitudCodigoArticulo)
                {
                    string devolver = "";
                    int longitudCubierta = 0;

                    for (int i = 0; i < codigoComposicion.Count; i++)
                    {
                        string parte = codigo.Substring(longitudCubierta, codigoComposicion[i]);
                        int parteSinCeros = int.Parse(parte);
                        devolver = devolver + parteSinCeros.ToString() + "-";
                        longitudCubierta += codigoComposicion[i];
                    }
                    codigo = devolver.Substring(0, devolver.Length - 1);

                }
            }
            return codigo;                        
        }
     
        public static decimal importeEdicionPermitido(decimal importeOriginal, decimal importeEditado)
        {
            decimal importe = importeEditado;
            if (importeEditado > (importeOriginal+(importeOriginal*PorcentajeModificacionPrecio)))
            {
                importe = importeOriginal + (importeOriginal * PorcentajeModificacionPrecio);
            }
            if (importeEditado < (importeOriginal - (importeOriginal * PorcentajeModificacionPrecio)))
            {
                importe = importeOriginal - (importeOriginal * PorcentajeModificacionPrecio);
            }
            return importe;
        }

        public static DateTime primerDiaDelAño()
        {
            DateTime fecha;
            fecha = DateTime.Parse("01/01/"+DateTime.Now.Year);
            return fecha;
        }
        
        public static DateTime primerDiaDelMes()
        {
            DateTime fecha;
            fecha = DateTime.Today;
            fecha = fecha.AddDays((fecha.Day*-1)+1);
            return fecha;
        }

        public static List<int> codigoComposicion = new List<int>();
        public static int longitudCodigoArticulo = 0;
        public static bool buscaPorCodigoPropio = false;
        public static bool cargaConProveedorID = true;
        public static bool usaCodigoBarraProveedor = false;

        public static string codigoArticulo(string codigo, ref bool buscarEnBD)
        {
            buscarEnBD = false;
            string[] parteEnteraYDecimalCodigo = codigo.Split('.');
            codigo = parteEnteraYDecimalCodigo[0];

            int longitudCodigo = codigo.Length;

            string parteDecimal = string.Empty;
            if (parteEnteraYDecimalCodigo.Length == 2)
            {
                parteDecimal = "." + parteEnteraYDecimalCodigo[1];
            }

            if (codigo.ToString().Length > (longitudCodigoArticulo - 1) | codigo.Contains("&"))
            {
                buscarEnBD = true;
            }
            else
            {

                if (codigo.Length < longitudCodigoArticulo)
                {
                    int parteCodigo = 0;
                    int longitudCubierta = 0;
                    string devolver = "";
                    parteCodigo = parteDelCodigoEnLaQueEsta(longitudCodigo);
                    for (int i = 0; i < parteCodigo; i++)
                    {
                        string parte = codigo.Substring(longitudCubierta, codigoComposicion[i]);
                        longitudCubierta += codigoComposicion[i];
                        devolver += parte;
                    }
                    string ultimaParte = codigo.Substring(longitudCubierta, codigo.Length - longitudCubierta);
                    int hasta = (codigoComposicion[parteCodigo] - ultimaParte.Length);
                    for (int i = 0; i < hasta; i++)
                    {
                        //try
                        //{
                        //decimal.Parse(ultimaParte);
                            ultimaParte = "0" + ultimaParte;
                        //}
                        //catch (Exception)
                        //{
                        //    buscarEnBD = true;
                        //}
                        
                    }
                    codigo = devolver.Replace(".", "") + ultimaParte;
                    if (codigo.Length >= longitudCodigoArticulo)
                    {
                        buscarEnBD = true;
                    }
                }
            }

            return codigo + parteDecimal;
        }
        public static string devolverCodigoCompleto(string codigo)
        {
            if (codigo.Replace("-", "").Length < Globales.longitudCodigoArticulo)
            {
                string devolver = "";
                string[] parteEnteraYDecimalCodigo = codigo.Split('.');
                codigo = parteEnteraYDecimalCodigo[0];

                int longitudCodigo = codigo.Length;

                string parteDecimal = string.Empty;
                if (parteEnteraYDecimalCodigo.Length == 2)
                {
                    parteDecimal = "." + parteEnteraYDecimalCodigo[1];
                }

                int longitudCubierta = 0;

                for (int i = 0; i < codigoComposicion.Count; i++)
                {
                    string parte = "";
                    try
                    {
                        parte = codigo.Split('-')[i];
                        for (int j = parte.Length; j < codigoComposicion[i]; j++)
                        {
                            parte = "0" + parte;
                        }
                    }
                    catch (Exception)
                    {

                        
                    }
                    
                    devolver = devolver + parte;
                    longitudCubierta += codigoComposicion[i];
                }
                codigo = devolver.Substring(0, devolver.Length);
                codigo = codigo + parteDecimal;
            }

            return codigo;
        }

        private static int parteDelCodigoEnLaQueEsta(int longitudCodigo)
        {
            int longitud = 0;
            for (int i = 0; i < codigoComposicion.Count; i++)
            {
                longitud += codigoComposicion[i];
                if (longitudCodigo < longitud)
                {
                    return i;
                }
            }
            return 0;
        }
        public static string codigoArticuloAnda(string codigo, ref bool buscarEnBD)
        {
            buscarEnBD = false;

            if (codigo.Length > 16)
            {
                buscarEnBD = true;
            }
            else
            {
                if (codigo.Length < 17)
                {
                    #region Completar Codigo
                    if (codigo.Length <= 3)
                    {
                        for (int i = codigo.Length; i < 3; i++)
                        {
                            codigo = "0" + codigo;
                        }
                    }
                    else
                    {
                        if (codigo.Length < 11)
                        {
                            string codprov = codigo.Substring(0, 3);
                            string codArt = codigo.Substring(3, codigo.Length - 3);
                            for (int i = codigo.Length; i < 11; i++)
                            {
                                codArt = "0" + codArt;
                            }
                            codigo = codprov + codArt;
                        }
                        else
                        {
                            if (codigo.Length == 11)
                            {
                                codigo += "000";
                            }
                            else
                            {
                                if (codigo.Length < 14)
                                {
                                    string codprov = codigo.Substring(0, 11);
                                    string codArt = codigo.Substring(11, codigo.Length - 11);
                                    for (int i = codigo.Length; i < 14; i++)
                                    {
                                        codArt = "0" + codArt;
                                    }
                                    codigo = codprov + codArt;

                                }
                                else
                                {
                                    if (codigo.Length == 14)
                                    {
                                        codigo += "000";
                                        buscarEnBD = true;
                                    }
                                    else
                                    {
                                        string codprov = codigo.Substring(0, 14);
                                        string codArt = codigo.Substring(14, codigo.Length - 14);
                                        try
                                        {
                                            decimal dec = decimal.Parse(codArt);

                                            for (int i = codigo.Length; i < 17; i++)
                                            {
                                                codArt = "0" + codArt;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            for (int i = codigo.Length; i < 17; i++)
                                            {
                                                codArt = codArt + " ";
                                            }
                                        }
                                        codigo = codprov + codArt;
                                        buscarEnBD = true;
                                    }

                                }
                            }
                        }
                    }
                    #endregion
                    
                }
            } 

            return codigo;
        }
        #endregion

        public static string token = "";
        public static string firma = "";

        public static void abrirVentana(Form f)
        {
            if (Globales.PantallaCompleta)
            {
                //f.FormBorderStyle = FormBorderStyle.None;
                f.FormBorderStyle = FormBorderStyle.FixedSingle;
                f.MaximizeBox = false;
                f.MinimizeBox = false;
                f.ControlBox = true;
                f.ShowDialog();
            }
            else
            {
                f.FormBorderStyle = FormBorderStyle.FixedSingle;
                f.MaximizeBox = true;
                f.MinimizeBox = true;
                f.ControlBox = true;
                f.Show();
            }
        }

        public static bool contraseñaValidaParaDescuentos(string contraseña)
        {            
            DataTable dt = new DataTable();
            int usuarioID = Globales.UsuarioIDporClave(contraseña);
            if (usuarioID != -1)
            {
                Globales.LlenarDataTable(dt, "UsuarioConPermisoDescuentoSelPorUsuarioID",
                    new SqlParameter("@UsuarioID", usuarioID));
            }
            //Globales.LlenarDataTable(dt, "UsuarioSelUsuariosConPermisoPorEmpresaIDSucursalIDClave",
            //    new SqlParameter("@EmpresaID", EmpresaID),
            //    new SqlParameter("@SucursalID", SucursalID),
            //    new SqlParameter("@Clave", contraseña));
            if (dt.Rows.Count > 0)
            {
                PorcentajeMaximoDescuento = decimal.Parse(dt.Rows[0]["PorcentajeMaximoDescuento"].ToString());
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool EsMaquinaDeSalon()
        {
            return esMaquinaDeSalon;
        }

        public static bool EsMaquinaDeSalonLucho()
        {
            
            if (//(Environment.MachineName == "ESTEBAN") |
                (Environment.MachineName == "CL-51") | (Environment.MachineName == "CL-52") | (Environment.MachineName == "CL-53") | (Environment.MachineName == "CL-55"))// | (Environment.MachineName == "CL-54") 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool EsMaquinaDeSalonSC()
        {
            if (//(Environment.MachineName == "ESTEBAN") |
                (Environment.MachineName == "SC-31") | (Environment.MachineName == "SC-32") | (Environment.MachineName == "SC-33") | (Environment.MachineName == "SC-35"))//| (Environment.MachineName == "SC-34")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool EsMaquinaDeSalonLH()
        {
            if (//(Environment.MachineName == "ESTEBAN") |
                (Environment.MachineName == "LH-11") | (Environment.MachineName == "LH-12") | (Environment.MachineName == "LH-13") | (Environment.MachineName == "LH-15"))//| (Environment.MachineName == "LH-14")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool EsMaquinaDeCaja()
        {
            if (//(Environment.MachineName == "ESTEBAN") |
                (Environment.MachineName == "LH-14") | (Environment.MachineName == "SC-134") | (Environment.MachineName == "SC-34") | (Environment.MachineName == "CL-54"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool YaEstaAbiertaUnaInstanciaDelSistema()
        {            
            return ((System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length>1));
        }

        public static decimal saldoClienteALaFecha(int clienteID)
        {
            decimal saldo = 0;
            DataTable dt = new DataTable();
            Globales.LlenarDataTable(dt,"ClienteSelSaldoALaFecha",
                new SqlParameter("@EmpresaID",EmpresaID),
                new SqlParameter("@SucursalID",SucursalID),
                new SqlParameter("@ClienteID",clienteID),
                new SqlParameter("@Fecha",DateTime.Now),
                new SqlParameter("@TodasLasSucursales", true),
                new SqlParameter("@TodasLasEmpresas", true),
                new SqlParameter("@SumaRemitos", true));
            if (dt.Rows.Count > 0)
            {
                saldo = decimal.Parse(dt.Rows[0][0].ToString());
            }
            return saldo;
        }
        public static decimal saldoCtaCteClienteALaFecha(int clienteID)
        {
            decimal saldo = 0;
            DataTable dt = new DataTable();
            Globales.LlenarDataTable(dt, "ClienteSelSaldoALaFecha",
                new SqlParameter("@EmpresaID", EmpresaID),
                new SqlParameter("@SucursalID", SucursalID),
                new SqlParameter("@ClienteID", clienteID),
                new SqlParameter("@Fecha", DateTime.Now),
                new SqlParameter("@TodasLasSucursales", true),
                new SqlParameter("@TodasLasEmpresas", true),
                new SqlParameter("@SumaRemitos", false));
            if (dt.Rows.Count > 0)
            {
                saldo = decimal.Parse(dt.Rows[0][0].ToString());
            }
            return saldo;
        }
        public static decimal saldoCtaCteClienteALaFecha(int clienteID, int empresaID)
        {
            decimal saldo = 0;
            DataTable dt = new DataTable();
            Globales.LlenarDataTable(dt, "ClienteSelSaldoALaFecha",
                new SqlParameter("@EmpresaID", empresaID),
                new SqlParameter("@SucursalID", SucursalID),
                new SqlParameter("@ClienteID", clienteID),
                new SqlParameter("@Fecha", DateTime.Now),
                new SqlParameter("@TodasLasSucursales", true),
                new SqlParameter("@TodasLasEmpresas", false),
                new SqlParameter("@SumaRemitos", false));
            if (dt.Rows.Count > 0)
            {
                saldo = decimal.Parse(dt.Rows[0][0].ToString());
            }
            return saldo;
        }
        public static decimal saldoRemitosClienteALaFecha(int clienteID)
        {
            decimal saldo = 0;
            DataTable dt = new DataTable();
            Globales.LlenarDataTable(dt, "ClienteSelSaldoRemitosALaFecha",
                new SqlParameter("@EmpresaID", EmpresaID),
                new SqlParameter("@SucursalID", SucursalID),
                new SqlParameter("@ClienteID", clienteID),
                new SqlParameter("@Fecha", DateTime.Now),
                new SqlParameter("@TodasLasSucursales", true),
                new SqlParameter("@TodasLasEmpresas", true));
            if (dt.Rows.Count > 0)
            {
                saldo = decimal.Parse(dt.Rows[0][0].ToString());
            }
            return saldo;
        }
        public static decimal saldoRemitosClienteALaFecha(int clienteID, int empresaID)
        {
            decimal saldo = 0;
            DataTable dt = new DataTable();
            Globales.LlenarDataTable(dt, "ClienteSelSaldoRemitosALaFecha",
                new SqlParameter("@EmpresaID", empresaID),
                new SqlParameter("@SucursalID", SucursalID),
                new SqlParameter("@ClienteID", clienteID),
                new SqlParameter("@Fecha", DateTime.Now),
                new SqlParameter("@TodasLasSucursales", true),
                new SqlParameter("@TodasLasEmpresas", false));
            if (dt.Rows.Count > 0)
            {
                saldo = decimal.Parse(dt.Rows[0][0].ToString());
            }
            return saldo;
        }
        public static DataTable saldoCreditoClienteALaFecha(int clienteID, int? empresaID)
        {            
            DataTable dt = new DataTable();
            if (empresaID != null)
            {
                Globales.LlenarDataTable(dt, "CreditoChequeraSelPorClienteIDEmpresaID",
                    new SqlParameter("@EmpresaID", empresaID),
                    new SqlParameter("@ClienteID", clienteID),
                    new SqlParameter("@Fecha", DateTime.Now));
            }
            else
            {
                Globales.LlenarDataTable(dt, "CreditoChequeraSelPorClienteIDEmpresaID",                    
                    new SqlParameter("@ClienteID", clienteID),
                    new SqlParameter("@Fecha", DateTime.Now));
            }
            return dt;
        }

        public static string FormatoNumeroDocumento(string documento)
        {
            if (documento.Length == 11)
            {
                documento = documento.Substring(0, 2) + "-" + documento.Substring(2, 8) + "-" + documento.Substring(10, 1);
            }
            if (documento.Length == 8)
            {
                documento = documento.Substring(0, 2) + "." + documento.Substring(2, 3) + "." + documento.Substring(5, 3);
            }
            return documento;
        }

        public static bool esDecimal(string texto)
        {
            bool esDecimal;
            try
            {
                decimal.Parse(texto);
                esDecimal = true;
            }
            catch (Exception)
            {
                esDecimal = false;
            }
            return esDecimal;
        }


        public static byte[] EncriptarClave(ref string clave, ref byte[] nsalt)
        {
            //UnicodeEncoding utf16 = new UnicodeEncoding();

            //if (utf16 != null)
            //{
            //    // Create a random number object seeded from the value
            //    // of the last random seed value. This is done
            //    // interlocked because it is a static value and we want
            //    // it to roll forward safely.

            //    Random random = new Random(unchecked((int)DateTime.Now.Ticks));

            //    if (random != null)
            //    {
            //        // Create an array of random values.

            //        byte[] saltValue = new byte[16];

            //        random.NextBytes(saltValue);

            //        // Convert the salt value to a string. Note that the resulting string
            //        // will still be an array of binary values and not a printable string. 
            //        // Also it does not convert each byte to a double byte.

            //        string saltValueString = utf16.GetString(saltValue);

            //        // Return the salt value as a string.

            //        return saltValueString;
            //    }
            //}

            //return null;

            //var deriveBytes = new Rfc2898DeriveBytes(clave, 20);
            //byte[] nsalt = deriveBytes.Salt;
            //byte[] nclave = deriveBytes.GetBytes(20);

            nsalt = GetSalt();
            return GetSecureHash(clave,nsalt);
        }

        public static byte[] GetSalt()
        {
            var p = new RNGCryptoServiceProvider();
            var salt = new byte[16];
            p.GetBytes(salt);
            return salt;
        }

        public static byte[] GetSecureHash(string password, byte[] salt)
        {
            Rfc2898DeriveBytes PBKDF2 = new Rfc2898DeriveBytes(password, salt);
            return PBKDF2.GetBytes(64);
        }

        static string key = "alGun@CosaParaL0grarEncr!ptaR";

        public static string Encriptar(string cadena)
        {
            //arreglo de bytes donde guardaremos la llave
            byte[] keyArray;
            //arreglo de bytes donde guardaremos el texto que vamos a encriptar
            byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(cadena);
            //se utilizan las clases de encriptación provistas por el Framework Algoritmo MD5
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            //se guarda la llave para que se le realice hashing
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();

            //Algoritmo 3DAS
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            //se empieza con la transformación de la cadena
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //arreglo de bytes donde se guarda la cadena cifrada
            byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);
            tdes.Clear();
            //se regresa el resultado en forma de una cadena
            return Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
        }

        public static string Desencriptar(string clave)
        {
            byte[] keyArray;
            //convierte el texto en una secuencia de bytes
            byte[] Array_a_Descifrar =
            Convert.FromBase64String(clave);

            //se llama a las clases que tienen los algoritmos
            //de encriptación se le aplica hashing
            //algoritmo MD5
            MD5CryptoServiceProvider hashmd5 =
            new MD5CryptoServiceProvider();

            keyArray = hashmd5.ComputeHash(
            UTF8Encoding.UTF8.GetBytes(key));

            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes =
            new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform =
             tdes.CreateDecryptor();

            byte[] resultArray =
            cTransform.TransformFinalBlock(Array_a_Descifrar,
            0, Array_a_Descifrar.Length);

            tdes.Clear();
            //se regresa en forma de cadena
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public static int UsuarioIDporUsuarioYClave(string usuario, string contraseña)
        {
            DataTable dt = new DataTable();
            Globales.LlenarDataTable(dt, "UsuarioSelSalt",
                new SqlParameter("@Usuario", usuario));            
            int usuarioID = -1;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    usuarioID = int.Parse(r["UsuarioID"].ToString());

                    if (r["Clave"].ToString() != "")
                    {
                        string esalt = r["clave"].ToString();
                        string eclave = Desencriptar(esalt);

                        if (eclave == contraseña)
                        {
                            return usuarioID;
                        }
                        //else
                        //{
                        //    return -1;
                        //}
                    }
                    else
                    {
                        //if (dt.Rows[0]["Clave"].ToString() == contraseña)
                        //{
                        return usuarioID;
                        //}
                        //else
                        //{
                        //    return -1;
                        //}
                    }
                }
                return -1;
            }
            else
            {
                DataTable udt = new DataTable();
                Globales.LlenarDataTableSinTry(udt, "UsuarioRetrieveByUsuarioClave",
                    new SqlParameter("@Usuario", usuario),
                    new SqlParameter("@Clave", contraseña));

                if (udt.Rows.Count > 0)
                {
                    return int.Parse(udt.Rows[0]["UsuarioID"].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        public static int UsuarioIDporClave(string contraseña)
        {
            DataTable dt = new DataTable();
            Globales.LlenarDataTable(dt, "UsuarioSelSaltTodos");
            int usuarioID = -1;
            foreach (DataRow r in dt.Rows)
            {
                usuarioID = int.Parse(r["UsuarioID"].ToString());

                if (r["Clave"].ToString() != string.Empty)
                {

                    if (contraseña == Desencriptar(r["Clave"].ToString()))
                    {
                        return usuarioID;
                    }                    
                }
                //else
                //{
                //    DataTable udt = new DataTable();
                //    Globales.LlenarDataTableSinTry(udt, "UsuarioRetrieveByClave",
                //        new SqlParameter("@EmpresaID", EmpresaID),
                //        new SqlParameter("@SucursalID", SucursalID),
                //        new SqlParameter("@Clave", contraseña));

                //    if (udt.Rows.Count > 0)
                //    {
                //        return int.Parse(udt.Rows[0]["UsuarioID"].ToString());
                //    }                    
                //}
            }
            return -1;
        }

        
    }
}
