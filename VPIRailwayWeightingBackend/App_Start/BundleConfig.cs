using System.Web.Optimization;

namespace RecycleBitBackEnd {

    /// <summary>
    /// Class for bundling
    /// </summary>
    public static class BundleConfig {

        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        /// <summary>
        /// Class to register bundles
        /// </summary>
        /// <param name="bundles">Bundle collection to be registered in</param>
        public static void RegisterBundles(BundleCollection bundles) {
            try {
                bundles.Add(new Bundle("~/RecycleBitBackEnd/bundles/jquery").Include(
                "~/RecycleBitBackEnd/Scripts/jquery-{version}.js"));

                // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender com ela. Após isso, quando você estiver
                // pronto para a produção, utilize a ferramenta de build em https://modernizr.com para escolher somente os testes que precisa.
                bundles.Add(new Bundle("~/RecycleBitBackEnd/bundles/modernizr").Include(
                            "~/RecycleBitBackEnd/Scripts/modernizr-*"));

                bundles.Add(new Bundle("~/RecycleBitBackEnd/bundles/bootstrap").Include(
                          "~/Scripts/bootstrap.js"));

                bundles.Add(new Bundle("~/RecycleBitBackEnd/Content/css").Include(
                          "~/RecycleBitBackEnd/Content/bootstrap.css",
                          "~/RecycleBitBackEnd/Content/site.css"));
            } catch {
                // Block to verify if bundle regristration was successful
            } finally {
                bundles.Add(new Bundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

                // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender com ela. Após isso, quando você estiver
                // pronto para a produção, utilize a ferramenta de build em https://modernizr.com para escolher somente os testes que precisa.
                bundles.Add(new Bundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));

                bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                          "~/Scripts/bootstrap.js"));

                bundles.Add(new Bundle("~/Content/css").Include(
                          "~/Content/bootstrap.css",
                          "~/Content/site.css"));
            }
        }
    }
}