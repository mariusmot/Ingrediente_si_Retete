using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Ingrediente_si_Retete.Models
{
    public class Ingredient
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        [OneToMany]
        public List<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
