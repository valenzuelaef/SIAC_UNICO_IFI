﻿using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.IFI.Common
{
    [DataContract]
    public class Customer : Claro.Entity.Request
    {
        public Customer()
        {
            objPostDataAccount = new DataAccount();
        }

        private DateTime _FECHA_ACT;
        private string _NOMBRES = "";
        private string _APELLIDOS = "";
        private string _NOMBRE_COMPLETO = "";
        private DataAccount _objPostDataAccount;

        [DataMember(Name = "ContactCode")]
        public string OBJID_CONTACTO { get; set; }

        [DataMember(Name = "SiteCode")]
        public string OBJID_SITE { get; set; }

        [DataMember(Name = "Telephone")]
        public string TELEFONO { get; set; }

        [DataMember(Name = "Account")]
        public string CUENTA { get; set; }

        [DataMember(Name = "Modality")]
        public string MODALIDAD { get; set; }
        [DataMember(Name = "Segment2")]
        public string SEGMENTO { get; set; }
        public string ROL_CONTACTO { get; set; }
        public string ESTADO_CONTACTO { get; set; }
        public string ESTADO_CONTRATO { get; set; }
        public string ESTADO_SITE { get; set; }
        public string S_NOMBRES { get; set; }
        public string S_APELLIDOS { get; set; }

        [DataMember(Name = "Name")]
        public string NOMBRES { get; set; }

        [DataMember(Name = "LastName")]
        public string APELLIDOS { get; set; }

        [DataMember(Name = "Address")]
        public string DOMICILIO { get; set; }

        [DataMember(Name = "Urbanization")]
        public string URBANIZACION { get; set; }

        [DataMember(Name = "Reference")]
        public string REFERENCIA { get; set; }

        public string CIUDAD { get; set; }

        [DataMember(Name = "District")]
        public string DISTRITO { get; set; }

        [DataMember(Name = "Departament")]
        public string DEPARTAMENTO { get; set; }

        public string ZIPCODE { get; set; }

        [DataMember(Name = "Email")]
        public string EMAIL { get; set; }

        [DataMember(Name = "PhoneReference")]
        public string TELEF_REFERENCIA { get; set; }

        [DataMember(Name = "Fax")]
        public string FAX { get; set; }

        [DataMember(Name = "BirthDate")]
        public string FECHA_NAC { get; set; }

        [DataMember(Name = "Sex")]
        public string SEXO { get; set; }

        [DataMember(Name = "CivilStatus")]
        public string ESTADO_CIVIL { get; set; }

        [DataMember(Name = "CivilStatusID")]
        public string ESTADO_CIVIL_ID { get; set; }

        [DataMember(Name = "DocumentType")]
        public string TIPO_DOC { get; set; }

        [DataMember(Name = "DocumentNumber")]
        public string NRO_DOC { get; set; }

        [DataMember(Name = "Date_Present")]
        public DateTime FECHA_ACT { get { return _FECHA_ACT; } set { _FECHA_ACT = value; } }
        public string PUNTO_VENTA { get; set; }
        [DataMember]
        public int FLAG_REGISTRADO { get; set; }
        public string OCUPACION { get; set; }
        public string CANT_REG { get; set; }
        public string FLAG_EMAIL { get; set; }
        public string MOTIVO_REGISTRO { get; set; }
        public string FUNCION { get; set; }

        [DataMember(Name = "Position")]
        public string CARGO { get; set; }

        [DataMember(Name = "BirthPlaceID")]
        public string LUGAR_NACIMIENTO_ID { get; set; }

        [DataMember(Name = "BirthPlace")]
        public string LUGAR_NACIMIENTO_DES { get; set; }

        public string P_FLAG_CONSULTA { get; set; }
        public string P_MSG_TEXT { get; set; }

        [DataMember(Name = "User")]
        public string USUARIO { get; set; }

        [DataMember(Name = "BusinessName")]
        public string RAZON_SOCIAL { get; set; }

        [DataMember(Name = "DNIRUC")]
        public string DNI_RUC { get; set; }

        [DataMember(Name = "Province")]
        public string PROVINCIA { get; set; }

        [DataMember(Name = "Assessor")]
        public string ASESOR { get; set; }
        public string CONSULTOR { get; set; }

        [DataMember(Name = "BillingCycle")]
        public string CICLO_FACTURACION { get; set; }
        public string CREDIT_SCORE { get; set; }
        public string TOTAL_CUENTAS { get; set; }
        public string DEPOSITO { get; set; }

        public string ESTADO_CUENTA { get; set; }
        public string RENTA { get; set; }

        [DataMember(Name = "CustomerType")]
        public string TIPO_CLIENTE { get; set; }
        public string TOTAL_LINEAS { get; set; }
        public string LIMITE_CREDITO { get; set; }
        public string RESPONSABLE_PAGO { get; set; }

        [DataMember(Name = "LegalAgent")]
        public string REPRESENTANTE_LEGAL { get; set; }

        [DataMember(Name = "CustomerContact")]
        public string CONTACTO_CLIENTE { get; set; }

        [DataMember(Name = "PhoneContact")]
        public string TELEFONO_CONTACTO { get; set; }

        [DataMember(Name = "InvoiceAddress")]
        public string CALLE_FAC { get; set; }

        [DataMember(Name = "InvoicePostal")]
        public string POSTAL_FAC { get; set; }

        [DataMember(Name = "InvoiceUrbanization")]
        public string URBANIZACION_FAC { get; set; }

        [DataMember(Name = "InvoiceDepartament")]
        public string DEPARTEMENTO_FAC { get; set; }

        [DataMember(Name = "InvoiceProvince")]
        public string PROVINCIA_FAC { get; set; }

        [DataMember(Name = "InvoiceDistrict")]
        public string DISTRITO_FAC { get; set; }

        [DataMember(Name = "Tradename")]
        public string NOMBRE_COMERCIAL { get; set; }

        [DataMember(Name = "LegalAddress")]
        public string CALLE_LEGAL { get; set; }

        [DataMember(Name = "LegalPostal")]
        public string POSTAL_LEGAL { get; set; }

        [DataMember(Name = "LegalUrbanization")]
        public string URBANIZACION_LEGAL { get; set; }

        [DataMember(Name = "LegalDepartament")]
        public string DEPARTEMENTO_LEGAL { get; set; }

        [DataMember(Name = "LegalProvince")]
        public string PROVINCIA_LEGAL { get; set; }

        [DataMember(Name = "LegalDistrict")]
        public string DISTRITO_LEGAL { get; set; }

        [DataMember(Name = "LegalCountry")]
        public string PAIS_LEGAL { get; set; }

        [DataMember(Name = "InvoiceCountry")]
        public string PAIS_FAC { get; set; }

        [DataMember(Name = "CustomerID")]
        public string CUSTOMER_ID { get; set; }

        [DataMember(Name = "ContractID")]
        public string CONTRATO_ID { get; set; }
        public string NICHO { get; set; }

        [DataMember(Name = "OfficeAddress")]
        public string DIRECCION_DESPACHO { get; set; }

        [DataMember(Name = "PaymentMethod")]
        public string FORMA_PAGO { get; set; }

        [DataMember(Name = "CodCustomerType")]
        public string COD_TIPO_CLIENTE { get; set; }

        [DataMember(Name = "PlaneCodeBilling")]
        public string CODIGO_PLANO_FACT { get; set; }

        [DataMember(Name = "PlaneCodeInstallation")]
        public string CODIGO_PLANO_INST { get; set; }

        public string TEXTO_NOTAS { get; set; }

        [DataMember(Name = "InvoiceUbigeo")]
        public string UBIGEO_FACT { get; set; }

        [DataMember(Name = "InstallUbigeo")]
        public string UBIGEO_INST { get; set; }

        [DataMember(Name = "ProductType")]
        public string TIPO_PRODUCTO { get; set; }


        [DataMember(Name = "FullName")]
        public string NOMBRE_COMPLETO
        {
            set { _NOMBRE_COMPLETO = value; }
            get
            {
                if (string.IsNullOrEmpty(_NOMBRE_COMPLETO))
                    _NOMBRE_COMPLETO = _NOMBRES + " " + _APELLIDOS;
                return _NOMBRE_COMPLETO.Trim();
            }
        }
        [DataMember(Name = "TypeServices")]
        public string TIPO_SERVICIO { get; set; }

        [DataMember(Name = "CodeCenterPopulate")]
        public string COD_CENTRO_POBLADO { get; set; }

        public string DES_CENTRO_POBLADO { get; set; }

        [DataMember(Name = "ActivationDate")]
        public string FECHA_ACTSTRING
        {
            set
            {
                DateTime.TryParse(value, out _FECHA_ACT);
            }
            get { return _FECHA_ACT.ToString(); }
        }

        [DataMember(Name = "objPostDataAccount")]
        public DataAccount objPostDataAccount
        {
            set { _objPostDataAccount = value; }
            get { return _objPostDataAccount; }
        }

        [DataMember(Name = "Des_CAC")]
        public string Des_CAC { get; set; }

        [DataMember(Name = "Reason")]
        public string Reason { get; set; }

        [DataMember(Name = "Cod_Motive")]
        public string COD_MOTIVE { get; set; }

        [DataMember(Name = "FlagNdPcs")]
        public string FLAG_ND_PCS { get; set; }

        [DataMember(Name = "FlagOccApadece")]
        public string FLAG_OCC_APADECE { get; set; }

        [DataMember(Name = "MontoFidelizacion")]
        public string MONTO_FIDELIZACION { get; set; }

        [DataMember(Name = "MontoPCs")]
        public string MONTO_PCS { get; set; }

        [DataMember(Name = "FechaProgramacion")]
        public string FECHA_PROGRAMACION { get; set; }

        [DataMember(Name = "SubMotivePCS")]
        public string SUB_MOTIVO_PCS { get; set; }

        [DataMember(Name = "Observation")]
        public string OBSERVACIONES { get; set; }

        [DataMember(Name = "MotivePCS")]
        public string MOTIVO_PCS { get; set; }

        [DataMember(Name = "AmountPenalty")]
        public string MONTO_PENALIDAD{ get; set; }

        [DataMember(Name = "MailUserAplication")]
        public string MAIL_USUARIO_APLICACION { get; set; }

        [DataMember(Name = "AreaPCs")]
        public string AREA_PCS { get; set; }

        [DataMember(Name = "CodigoInteraction")]
        public string CODIGO_INTERACCION { get; set; }

        [DataMember(Name = "CodigoService")]
        public string CODIGO_SERVICIO { get; set; }

        [DataMember(Name = "DateProgrammingSot")]
        public string FECHA_PROGRAMACION_SOT { get; set; }

        [DataMember(Name = "FringeHorary")]
        public string FRANJA_HORARIO { get; set; }

        [DataMember(Name = "Trace")]
        public string TRACE { get; set; }

        [DataMember(Name = "TypeWork")]
        public string TIPO_TRABAJO { get; set; }

        [DataMember(Name = "UserAdvisor")]
        public string TIPO_ASESOR { get; set; }
        
        [DataMember(Name = "Msisdn")]
        public String Msisdn { get; set; }

        [DataMember(Name = "FechaCompromiso")]
        public string FECHA_COMPROMISO { get; set; }

        [DataMember(Name = "ApplicationName")]
        public string ApplicationName { get; set; }
        [DataMember(Name = "UserApplication")]
        public string UserApplication { get; set; }

    }
}
