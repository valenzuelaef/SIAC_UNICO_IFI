using System.Web.Mvc;
using System.Web.Optimization;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions
{
    public class IFITransactionsAreaRegistration : AreaRegistration
    {
        /// <summary>Método que ejecuta la transaccion</summary>
        /// <returns>string</returns>
        /// <remarks>AreaName</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019</FecCrea></item></list>
        public override string AreaName
        {
            get
            {
                return "IFITransactions";
            }
        }

        /// <summary>Método que ejecuta la transaccion de registro</summary>
        /// <param name="context"></param>
        /// <returns>string</returns>
        /// <remarks>RegisterArea</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019</FecCrea></item></list>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "IFITransactions_default",
                "IFITransactions/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>Método que ejecuta los bundles</summary>
        /// <param name="bundles"></param>
        /// <returns>string</returns>
        /// <remarks>RegisterBundles</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2019</FecCrea></item></list>
        private void RegisterBundles(BundleCollection bundles)
        {
            Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Admin.BundleConfig.RegisterBundles(BundleTable.Bundles);


            bundles.Add(new ScriptBundle("~/bundles/IFITransactions/SuspensionReconnectionCustomer")
               .Include("~/Areas/IFITransactions/Content/Scripts/SuspensionReconnectionCustomer.js"));
            bundles.Add(new ScriptBundle("~/bundles/IFITransactions/RetentionCancelServices")
               .Include("~/Areas/IFITransactions/Content/Scripts/RetentionCancelServices.js"));
            bundles.Add(new ScriptBundle("~/bundles/IFITransactions/ReadRecordPDF/ReadRecord")
               .Include("~/Areas/IFITransactions/Content/Scripts/ReadRecordPdf/ReadRecord.js"));
            bundles.Add(new ScriptBundle("~/bundles/IFITransactions/Auth/AuthUser")
               .Include("~/Areas/IFITransactions/Content/Scripts/AuthUser/AuthUser.js"));
            bundles.Add(new ScriptBundle("~/bundles/IFITransactions/UnlockService")
            .Include("~/Areas/IFITransactions/Scripts/UnlockService/UnlockService.js",
                       "~/Areas/IFITransactions/Scripts/UnlockService/ValidateUnlockService.js"));
            bundles.Add(new ScriptBundle("~/bundles/IFITransactions/ServiceLock")
            .Include("~/Areas/IFITransactions/Scripts/ServiceLock/ServiceLock.js",
            "~/Areas/IFITransactions/Scripts/ServiceLock/ValidateServiceLock.js"));


             bundles.Add(new ScriptBundle("~/bundles/IFITransactions/EditSuspensionReconnectionCustomer")
               .Include("~/Areas/IFITransactions/Content/Scripts/EditSuspensionReconnectionCustomer.js"));
             bundles.Add(new ScriptBundle("~/bundles/IFITransactions/ChangeMinor")
                  .Include("~/Areas/IFITransactions/Scripts/ChangeMinor/ChangeMinor.js"));
             bundles.Add(new ScriptBundle("~/bundles/IFITransactions/DuplicateReceipts")
                .Include("~/Areas/IFITransactions/Scripts/DuplicateReceipts/DuplicateReceipts.js"));
             bundles.Add(new ScriptBundle("~/bundles/IFITransactions/ChangePostalAddress")
      .Include("~/Areas/IFITransactions/Scripts/ChangePostalAddress/ChangePostalAddress.js"));
             bundles.Add(new ScriptBundle("~/bundles/IFITransactions/ChangeServiceAddress").Include("~/Areas/IFITransactions/Scripts/ChangeServiceAddress/ChangeServiceAddress.js"));
             bundles.Add(new ScriptBundle("~/bundles/IFITransactions/QuestionSecurity")
      .Include("~/Areas/IFITransactions/Scripts/TestSecurity/TestSecurity.js"));
             bundles.Add(new ScriptBundle("~/bundles/IFITransactions/PlanMigration")
      .Include("~/Areas/IFITransactions/Scripts/PlanMigration/PlanMigration.js"));
             bundles.Add(new ScriptBundle("~/bundles/IFITransactions/ChangeBillingCycle")
       .Include("~/Areas/IFITransactions/Scripts/ChangeBillingCycle/ChangeBillingCycle.js"));

             bundles.Add(new ScriptBundle("~/bundles/IFITransactions/PackageHistory-siacu")
       .Include("~/Areas/IFITransactions/Scripts/PackageHistory/PackageHistory.js"));

  bundles.Add(new ScriptBundle("~/bundles/IFITransactions/IFIPackagePurchaseServices")
          .Include("~/Areas/IFITransactions/Scripts/IFIPackagePurchaseServices/IFIPackagePurchaseServices.js"));
        }
    }
}