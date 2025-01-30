using Ingrediente_si_Retete.Data;

namespace Ingrediente_si_Retete
{
    public partial class App : Application
    {
        static RecipeDatabase database;
        public static RecipeDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new RecipeDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RecipeDatabase.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}