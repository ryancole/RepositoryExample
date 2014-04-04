using System.Web;
using System.Web.Optimization;

namespace RepositoryExample.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/public/styles/bootstrap").Include("~/public/styles/bootstrap.css"));
            bundles.Add(new StyleBundle("~/public/styles/warcraft").Include("~/public/styles/warcraft.css"));
            
            bundles.Add(new ScriptBundle("~/public/scripts/boostrap").Include("~/public/scripts/bootstrap.js"));
        }
    }
}