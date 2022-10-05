using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Claro.SIACU.Web.WebApplication.IFI.Areas.IFITransactions.Admin
{
    public static class BundleConfig
    {

        /// <summary>Método que permite usar los scripts y estilos agrupandolos por paquetes</summary>
        /// <param name="bundles"></param>
        /// <remarks>RegisterBundles</remarks>
        /// <list type="PageLoad">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>30/01/2018.</FecCrea></item></list>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/bootstrap-addon-css-siacu-ifi").Include(
                "~/Content/css/bootstrap.css",
                "~/Content/css/font-awesome.css",
                "~/Content/css/dataTables.bootstrap.min.css",
                "~/Content/css/jquery.dataTables.select.css",
                "~/Content/css/datepicker.css",
                "~/Content/css/jquery.smartmenus.bootstrap.css"));

            bundles.Add(new StyleBundle("~/bundles/jquery-addon-css-siacu-ifi")
                .Include("~/Content/css/jquery-ui.css",
                    "~/Content/css/jquery.bar.css"
                ));

            bundles.Add(new StyleBundle("~/bundles/Site-css-siacu-ifi").Include(
                "~/Content/css/Site.css",
                "~/Content/css/Header.css",
                "~/Content/css/Footer.css",
                "~/Content/css/MyContainer.css",
                "~/Content/css/TreeView.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jquery-siacu-ifi").Include(
                "~/Content/Lib/jquery-2.0.0.js",
                "~/Content/Lib/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval-siacu-ifi").Include(
                "~/Content/Lib/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-siacu-ifi").Include(
                "~/Content/Lib/bootstrap.js",
                "~/Content/Lib/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-addon-siacu-ifi").Include(
                "~/Content/Lib/jquery.dataTables.min.js",
                "~/Content/Lib/jquery.dataTables.select.js",
                "~/Content/Lib/jquery.blockUI.js",
                "~/Content/Lib/jquery.smartmenus.js",
                "~/Content/Lib/jquery.smartmenus.bootstrap.js",
                "~/Content/Lib/jquery.numeric.js",
                "~/Content/Lib/dataTables.bootstrap.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/moment-js-siacu-ifi")
                .Include("~/Content/Lib/moment.js",
                    "~/Content/Lib/moment-es.js"));

            bundles.Add(new ScriptBundle("~/bundles/Claro-siacu-ifi")
                .Include("~/Content/Scripts/ClaroSession.js",
                    "~/Areas/IFITransactions/Content/Scripts/ClaroAppCommon.js",
                    "~/Content/Scripts/ClaroRedirect.js",
                    "~/Content/Scripts/plupload.full.min.js",
                    "~/Content/Scripts/ClaroModalTemplate.js",
                    "~/Content/Scripts/ClaroModalLoad.js",
                   
                    "~/Content/Scripts/ClaroUtils.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker-siacu-ifi")
                .Include("~/Content/Lib/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/stepsIFI-ifi").Include(
                "~/Content/lib/jquery.steps.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/numeric-ifi").Include(
            "~/Content/Lib/jquery.numeric.js"));

            bundles.Add(new ScriptBundle("~/bundles/IFITransactions/Redirect/Redirect")
           .Include("~/Content/Scripts/Redirect/Redirect.js"));


            bundles.Add(new StyleBundle("~/bundles/Site-TransactionsIFI-ifi").Include(
               "~/Areas/IFITransactions/Content/css/Site-Transaction.css",
               "~/Areas/IFITransactions/Content/css/Site.css",
               "~/Content/css/jquery.steps.css"
               ));

            bundles.Add(new ScriptBundle("~/bundles/IFITransactions/PCRFConsultation")
           .Include("~/Areas/IFITransactions/Scripts/PCRFConsultation/PCRFConsultation.js"));


            bundles.Add(new ScriptBundle("~/bundles/IFITransactions/ScheduledTasks")
                .Include("~/Areas/IFITransactions/Scripts/ScheduledTasks/ScheduledTasks.js"));


            bundles.Add(new ScriptBundle("~/bundles/IFITransactions/MailReceipt")
           .Include("~/Areas/IFITransactions/Scripts/MailReceipt/MailReceipt.js"));

            bundles.Add(new ScriptBundle("~/bundles/Content/Lib/BloqueoF12")
                .Include("~/Content/Lib/BloqueoF12.js"));
        }
    }
}