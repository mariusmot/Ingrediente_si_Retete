using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Ingrediente_si_Retete.Models
{
    public class Recipe
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Instructions { get; set; }
        public string PrepTime { get; set; }
        [OneToMany]
        public List<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
