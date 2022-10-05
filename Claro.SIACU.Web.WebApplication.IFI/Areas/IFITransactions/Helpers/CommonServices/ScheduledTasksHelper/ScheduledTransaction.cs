using Claro.Helpers;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Helpers.CommonServices.ScheduledTasksHelper
{
    public class ScheduledTransaction : IExcel
    {

        [Header(Title = "Contrato", Order = Claro.Constants.NumberZero)]
        public string CoId { get; set; }
        [Header(Title = "Id Cliente", Order = Claro.Constants.NumberOne)]
        public string CustomerId { get; set; }
        [Header(Title = "Fecha Programación", Order = Claro.Constants.NumberTwo)]
        public string ServdDateProg { get; set; }
        [Header(Title = "Fecha Registro", Order = Claro.Constants.NumberThree)]
        public string ServdDateReg { get; set; }
        [Header(Title = "Fecha Ejecución", Order = Claro.Constants.NumberFour)]
        public string ServdDateEjec { get; set; }
        public string ServcState { get; set; }
        [Header(Title = "Estado", Order = Claro.Constants.NumberFive)]
        public string DescState { get; set; }
        public string ServcIsBatch { get; set; }
        public string ServvMenError { get; set; }
        public string ServvCodeError { get; set; }
        public string ServUserSystem { get; set; }
        public string ServvIdEaiSw { get; set; }
        public string ServiCode { get; set; }
        [Header(Title = "Descripción Servicio", Order = Claro.Constants.NumberSix)]
        public string DescServi { get; set; }
        public string ServvMsisdn { get; set; }
        public string ServvIdBatch { get; set; }
        public string ServvUserAplication { get; set; }
        public string ServvEmailUserApp { get; set; }
        public string ServvXmlEntry { get; set; }
        [Header(Title = "Cuenta", Order = Claro.Constants.NumberSeven)]
        public string ServcNumberAccount { get; set; }
        public string ServcCodeInteraction { get; set; }
        public string ServcSellingPoint { get; set; }
        [Header(Title = "Tipo Servicio", Order = Claro.Constants.NumberEight)]
        public string ServcTypeServ { get; set; }
        public string ServcCoSer { get; set; }
        public string ServcTypeReg { get; set; }
        public string ServcDesCoSer { get; set; }
    }
}