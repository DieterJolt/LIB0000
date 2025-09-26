using System.ComponentModel.DataAnnotations;

namespace LIB0000
{
    public partial class RecipeDetailModel : ObservableObject
    {
        [Key]
        [ObservableProperty]
        private int _recipeId;

        [ObservableProperty]
        private string _recipeDetail = "";

    }
}
