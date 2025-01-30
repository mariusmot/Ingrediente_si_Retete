using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Ingrediente_si_Retete.Models
{
    public class RecipeIngredient
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [ForeignKey(typeof(Recipe))]
        public int RecipeID { get; set; }
        [ForeignKey(typeof(Ingredient))]
        public int IngredientID { get; set; }
        public string Quantity { get; set; }
    }
}