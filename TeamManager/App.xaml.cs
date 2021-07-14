using System.Data;
using System.Linq;
using System.Windows;
using TeamManager.Database;
using TeamManager.Database.Helpers;

namespace TeamManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using (var db = new TeamManagerContext())
            {
                db.Database.EnsureCreated();

                var departmentsCount = db.Department.Select(x => x).ToList().Count;

                if (departmentsCount == 3)
                {
                    // do nothing
                }
                else if (departmentsCount == 0)
                {
                    Informations.AddUsrAndDep();
                }
            }
        }
    }
}
