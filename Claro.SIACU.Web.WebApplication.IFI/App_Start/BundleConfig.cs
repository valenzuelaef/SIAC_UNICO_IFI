using System.Web;
using System.Web.Optimization;

namespace Claro.SIACU.Web.WebApplication.IFI
{
    public static class BundleConfig
    {
      
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

        }
    }
}
