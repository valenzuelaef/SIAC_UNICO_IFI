using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.IFI.Common
{
    [DataContract]
    public class Line
    {
        [DataMember]
        public string PhoneNumber{get;set;}
        [DataMember]
        public string LineStatus{get;set;}
        [DataMember]
        public string MainBalance{get;set;}
        [DataMember]
        public string ExpirationDateBalance { get; set; }
        [DataMember]
        public string TriosChanguesFree {get;set;}
        [DataMember]
        public string TariffChangesFree {get;set;}
        [DataMember]
        public string TariffPlan{get;set;}
        [DataMember]
        public string ActivationDate{get;set;}
        [DataMember]
        public string DolDate { get; set; }
        [DataMember]
        public string ExpDate_Line{get;set;}
        [DataMember]
        public string NumIMSI{get;set;}
        [DataMember]
        public string StatusIMSI{get;set;}
        [DataMember]
        public string NumICCID{get;set;}
        [DataMember]
        public string NumFamFriends{get;set;}
        [DataMember]
        public string TriacionType{get;set;}
        [DataMember]
        public string ProviderID{get;set;}
        [DataMember]

        public DateTime DateStatus{get;set;}
        [DataMember]
        public string Reason{get;set;}
        [DataMember]
        public string Plan{get;set;}
        [DataMember]
        public string TermContract { get; set; }
        [DataMember]
        public string Sale { get; set; }
        [DataMember]
        public string Bell { get; set; }
        [DataMember]
        public string FlagPlatform { get; set; }
        [DataMember]
        public string PIN1 { get; set; }
        [DataMember]
        public string  PIN2 { get; set; }
        [DataMember]
        public string PUK1 { get; set; }
        [DataMember]
        public string PUK2 { get; set; }
        [DataMember]
        public string ContractID { get; set; }
        [DataMember]
        public string CodPlanTariff { get; set; }

        [DataMember]
        public string CNTNumber { get; set; }
        [DataMember]
        public string IsCNTPossible { get; set; }
        [DataMember]
        public string SubscriberStatus { get; set; }
        [DataMember]
        public string FlagLoadDataLine{get;set;}
        [DataMember]
        public string IsTFI { get; set; }
        [DataMember]
        public string IsDTH { get; set; }
        [DataMember]
        public string OutstandingBalance { get; set; }
        [DataMember]
        public string MinuteBalance_Select { get; set; }
        [DataMember]
        public string ExpDate_Select { get; set; }
        [DataMember]
        public string IsSelect{get;set;}

        

        //CAMPOS PARA GetDataline

        
         public string NOMBRES { get; set; }
         
         public string APELLIDOS { get; set; }
      
         public string CUENTA { get; set; }
       
         public string SEXO { get; set; }
       
         public string NRO_DOC { get; set; }
        
         public string DOMICILIO { get; set; }
        
         public string ZIPCODE { get; set; }
        
         public string DEPARTAMENTO { get; set; }
       
         public string DISTRITO { get; set; }
        
         public string RAZON_SOCIAL { get; set; }
      
         public string PROVINCIA { get; set; }
       
         public string DNI_RUC { get; set; }
       
         public string ASESOR { get; set; }
      
         public string CICLO_FACTURACION { get; set; }
        
         public string CONSULTOR { get; set; }
        
         public string MODALIDAD { get; set; }
      
         public string SEGMENTO { get; set; }
      
         public string CREDIT_SCORE { get; set; }
      
         public string ESTADO_CUENTA { get; set; }
     
         public DateTime FECHA_ACT { get; set; }
   
         public string FECHA_NAC { get; set; }
         
         public string LIMITE_CREDITO { get; set; }
      
         public string TOTAL_CUENTAS { get; set; }
        
         public string TOTAL_LINEAS { get; set; }
      
         public string RESPONSABLE_PAGO { get; set; }
        
         public string TIPO_CLIENTE { get; set; }
       
         public string REPRESENTANTE_LEGAL { get; set; }
       
         public string EMAIL { get; set; }
      
         public string TELEF_REFERENCIA { get; set; }
      
         public string CONTACTO_CLIENTE { get; set; }
        
         public string TIPO_DOC { get; set; }
      
         public string NOMBRE_COMERCIAL { get; set; }
         
         public string FAX { get; set; }
        
         public string CONTRATO_ID { get; set; }
         
         public string CARGO { get; set; }
       
         public string CUSTOMER_ID { get; set; }
         
         public string TELEFONO_CONTACTO { get; set; }
        
         public string CALLE_FAC { get; set; }
         
         public string POSTAL_FAC { get; set; }
         
         public string URBANIZACION_FAC { get; set; }
        
         public string DEPARTEMENTO_FAC { get; set; }
        
         public string PROVINCIA_FAC { get; set; }
         
         public string DISTRITO_FAC { get; set; }
         
         public string CALLE_LEGAL { get; set; }
        
         public string POSTAL_LEGAL { get; set; }
       
         public string URBANIZACION_LEGAL { get; set; }
        
         public string DEPARTEMENTO_LEGAL { get; set; }
        
         public string PROVINCIA_LEGAL { get; set; }
         
         public string DISTRITO_LEGAL { get; set; }
         
         public string PAIS_FAC { get; set; }
         
         public string PAIS_LEGAL { get; set; }
         
         public string REFERENCIA { get; set; }
       
         public string LUGAR_NACIMIENTO_DES { get; set; }
        
         public string LUGAR_NACIMIENTO_ID { get; set; }
      
         public string ESTADO_CIVIL { get; set; }
        
         public string ESTADO_CIVIL_ID { get; set; }
      
         public string NICHO { get; set; }
        
         public string FORMA_PAGO { get; set; }
        
         public string COD_TIPO_CLIENTE { get; set; }
        
     }
}
