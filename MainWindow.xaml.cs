using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RecipeApp
{
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes;

        public MainWindow()
        {
            InitializeComponent();
            recipes = new List<Recipe>();
        }

        private void btnEnterRecipeDetails_Click(object sender, RoutedEventArgs e)
        {
            Recipe recipe = new Recipe();
            recipe.Name = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of the recipe:", "Recipe Name");

            int numIngredients;
            if (int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Enter the number of ingredients:", "Number of Ingredients"), out numIngredients))
            {
                recipe.Ingredients = new List<Ingredient>();

                for (int i = 0; i < numIngredients; i++)
                {
                    string ingredientName = Microsoft.VisualBasic.Interaction.InputBox($"Enter the details for ingredient {i + 1}:\nName:", "Ingredient Name");

                    decimal quantity;
                    if (decimal.TryParse(Microsoft.VisualBasic.Interaction.InputBox($"Enter the details for ingredient {i + 1}:\nQuantity:", "Ingredient Quantity"), out quantity))
                    {
                        string unit = Microsoft.VisualBasic.Interaction.InputBox($"Enter the details for ingredient {i + 1}:\nUnit:", "Ingredient Unit");

                        int calories;
                        if (int.TryParse(Microsoft.VisualBasic.Interaction.InputBox($"Enter the details for ingredient {i + 1}:\nCalories:", "Ingredient Calories"), out calories))
                        {
                            string foodGroup = Microsoft.VisualBasic.Interaction.InputBox($"Enter the details for ingredient {i + 1}:\nFood Group:", "Ingredient Food Group");

                            recipe.Ingredients.Add(new Ingredient
                            {
                                Name = ingredientName,
                                Quantity = quantity,
                                Unit = unit,
                                OriginalQuantity = quantity,
                                Calories = calories,
                                FoodGroup = foodGroup
                            });
                        }
                        else
                        {
                            MessageBox.Show("Invalid calories value. Please enter a valid integer.");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid quantity value. Please enter a valid decimal number.");
                        return;
                    }
                }

                recipes.Add(recipe);
                MessageBox.Show("Recipe details entered successfully.");
            }
            else
            {
                MessageBox.Show("Invalid number of ingredients. Please enter a valid integer.");
                return;
            }
        }

        private void btnDisplayRecipeList_Click(object sender, RoutedEventArgs e)
        {
            string recipeList = "Recipe List:\n\n";

            if (recipes.Any())
            {
                foreach (var recipe in recipes)
                {
                    recipeList += $"{recipe.Name}\n";
                }
            }
            else
            {
                recipeList += "No recipes found.";
            }

            MessageBox.Show(recipeList);
        }

        private void btnDisplayRecipe_Click(object sender, RoutedEventArgs e)
        {
            string recipeName = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of the recipe:", "Recipe Name");

            var recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

            if (recipe != null)
            {
                string recipeDetails = $"Recipe Details for {recipe.Name}:\n\n";

                recipeDetails += "Ingredients:\n";

                foreach (var ingredient in recipe.Ingredients)
                {
                    recipeDetails += $"- {ingredient.Name}: {ingredient.Quantity} {ingredient.Unit}\n";
                }

                MessageBox.Show(recipeDetails);
            }
            else
            {
                MessageBox.Show("Recipe not found.");
            }
        }

        private void btnScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            string recipeName = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of the recipe:", "Recipe Name");

            var recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

            if (recipe != null)
            {
                decimal scaleFactor;
                if (decimal.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Enter the scaling factor:", "Scaling Factor"), out scaleFactor))
                {
                    foreach (var ingredient in recipe.Ingredients)
                    {
                        ingredient.Quantity = ingredient.OriginalQuantity * scaleFactor;
                    }

                    MessageBox.Show("Recipe scaled successfully.");
                }
                else
                {
                    MessageBox.Show("Invalid scaling factor. Please enter a valid decimal number.");
                }
            }
            else
            {
                MessageBox.Show("Recipe not found.");
            }
        }

        private void btnResetQuantities_Click(object sender, RoutedEventArgs e)
        {
            string recipeName = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of the recipe:", "Recipe Name");

            var recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

            if (recipe != null)
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    ingredient.Quantity = ingredient.OriginalQuantity;
                }

                MessageBox.Show("Recipe quantities reset successfully.");
            }
            else
            {
                MessageBox.Show("Recipe not found.");
            }
        }

        private void btnClearData_Click(object sender, RoutedEventArgs e)
        {
            recipes.Clear();
            MessageBox.Show("All data cleared successfully.");
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            string foodGroup = Microsoft.VisualBasic.Interaction.InputBox("Enter the food group:", "Food Group");
            int maxCalories;
            if (int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Enter the maximum calories:", "Maximum Calories"), out maxCalories))
            {
                string ingredientName = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of the ingredient:", "Ingredient Name");

                // Filter the recipes based on the provided criteria
                var filteredRecipes = recipes.Where(recipe =>
                    recipe.Ingredients.Any(ingredient =>
                        ingredient.FoodGroup.Equals(foodGroup, StringComparison.OrdinalIgnoreCase) &&
                        ingredient.Calories <= maxCalories &&
                        ingredient.Name.Equals(ingredientName, StringComparison.OrdinalIgnoreCase)
                    )
                ).ToList();

                if (filteredRecipes.Any())
                {
                    string recipeList = "Filtered Recipes:\n\n";
                    foreach (var recipe in filteredRecipes)
                    {
                        recipeList += $"{recipe.Name}\n";
                    }
                    MessageBox.Show(recipeList);
                }
                else
                {
                    MessageBox.Show("No recipes found that match the specified criteria.");
                }
            }
            else
            {
                MessageBox.Show("Invalid maximum calories value. Please enter a valid integer.");
            }
        }
    }

    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }

    public class Ingredient
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public decimal OriginalQuantity { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; set; }
    }
}
