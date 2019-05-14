using System.Configuration;
using System.IO;
using System.Web.Hosting;

namespace GreatPizza.Dal
{
    internal static class Constants
    {
        internal static readonly string DB_FOLDER_PATH = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, ConfigurationManager.AppSettings["DbFolderPath"]);
        internal static readonly string DB_PIZZA_PATH = Path.Combine(DB_FOLDER_PATH, ConfigurationManager.AppSettings["DbPizzasPath"]);
        internal static readonly string DB_TOPPING_PATH = Path.Combine(DB_FOLDER_PATH, ConfigurationManager.AppSettings["DbToppingsPath"]);
    }
}
