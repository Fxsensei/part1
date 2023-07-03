
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApp
{
    // Ingredient class represents an ingredient in a recipe
    class Ingredient
    {
        public string Name { get; set; } // the name of the ingredient
        public decimal Quantity { get; set; } // the Quantity
        public string Unit { get; set; } // the Unit of measurement
        public decimal OriginalQuantity { get; set; } // this is for the original amount of the ingredient before we scale it
        public int Calories { get; set; } // the number of calories
        public string FoodGroup { get; set; } // the food group
    }

    // this class is for the steps the user will input for the recipe
    class Step
    {
        public string Description { get; set; } // writing the description
    }

    // this class will store the lists of ingredients and steps for the recipe
    class Recipe
    {
        public string Name { get; set; } // the name of the recipe
        public List<Ingredient> Ingredients { get; set; } // List of ingredients in the recipe
        public List<Step> Steps { get; set; } // List of steps in the recipe
    }

    class Program
    {
        // Delegate to notify the user when a recipe exceeds 300 calories
        delegate void RecipeExceedsCaloriesNotification(Recipe recipe);

        static void Main(string[] args)
        {
            List<Recipe> recipes = new List<Recipe>(); // List to store multiple recipes

            while (true) // a while loop that will run indefinitely until the user chooses to exit
            {
                // options menu
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. Enter recipe details");
                Console.WriteLine("2. Display recipe list");
                Console.WriteLine("3. Display recipe");
                Console.WriteLine("4. Scale recipe");
                Console.WriteLine("5. Reset quantities");
                Console.WriteLine("6. Clear all data");
                Console.WriteLine("7. Exit");

                // read user input and parse as integer
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    // perform action based on user's choice
                    switch (choice)
                    {
                        case 1:
                            EnterRecipeDetails(recipes); // call EnterRecipeDetails method to enter recipe details
                            break;
                        case 2:
                            DisplayRecipeList(recipes); // call DisplayRecipeList method to display recipe list
                            break;
                        case 3:
                            DisplayRecipe(recipes); // call DisplayRecipe method to display recipe details
                            break;
                        case 4:
                            ScaleRecipe(recipes); // call ScaleRecipe method to scale the recipe
                            break;
                        case 5:
                            ResetQuantities(recipes); // call ResetQuantities method to reset ingredient quantities
                            break;
                        case 6:
                            ClearData(recipes); // call ClearData method to clear all recipe data
                            break;
                        case 7:
                            Console.WriteLine("Thank you for using RecipeApp. Exiting..."); // Exit the program
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again."); // display error message for invalid choice
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again."); // display error message for invalid input
                }

                Console.WriteLine(); // add a new line for spacing after each user input
            }
        }

        static void EnterRecipeDetails(List<Recipe> recipes)
        {
            Recipe recipe = new Recipe(); //
            Console.Write("Enter the name of the recipe: ");
            string name = Console.ReadLine();
            recipe.Name = name;

            Console.Write("Enter the number of ingredients: ");
            int numIngredients;
            if (int.TryParse(Console.ReadLine(), out numIngredients))
            {
                recipe.Ingredients = new List<Ingredient>();

                // Loop through each ingredient and prompt the user to enter its details
                for (int i = 0; i < numIngredients; i++)
                {
                    Console.WriteLine($"Enter the details for ingredient {i + 1}:");
                    Console.Write("Name: ");
                    string ingredientName = Console.ReadLine();
                    Console.Write("Quantity: ");
                    decimal quantity;
                    if (decimal.TryParse(Console.ReadLine(), out quantity))
                    {
                        Console.Write("Unit: ");
                        string unit = Console.ReadLine();
                        Console.Write("Calories: ");
                        int calories;
                        if (int.TryParse(Console.ReadLine(), out calories))
                        {
                            Console.Write("Food Group: ");
                            string foodGroup = Console.ReadLine();

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



                            Console.WriteLine("Invalid calories. Please try again.");
                            i--;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity. Please try again.");
                        i--;
                    }
                }

                EnterRecipeSteps(recipe); // Call the method to enter recipe steps

                recipes.Add(recipe); // Add the recipe to the list of recipes

                Console.WriteLine("Recipe details entered successfully.");
            }
        }

        // Method to enter recipe steps
        static void EnterRecipeSteps(Recipe recipe)
        {
            Console.Write("Enter the number of steps: ");
            int numSteps;
            if (int.TryParse(Console.ReadLine(), out numSteps))
            {
                recipe.Steps = new List<Step>();

                // Loop through each step and prompt the user to enter its details
                for (int i = 0; i < numSteps; i++)
                {
                    Console.WriteLine($"Enter the details for step {i + 1}:");
                    Console.Write("Description: ");
                    string description = Console.ReadLine();

                    recipe.Steps.Add(new Step { Description = description });
                }

                Console.WriteLine("Recipe steps entered successfully.");
            }
        }

        // Method to display recipe list
        static void DisplayRecipeList(List<Recipe> recipes)
        {
            if (recipes.Count > 0)
            {
                Console.WriteLine("Recipe List:");
                recipes = recipes.OrderBy(r => r.Name).ToList(); // Sort the recipes by name

                foreach (var recipe in recipes)
                {
                    Console.WriteLine(recipe.Name);
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No recipes found.");
                Console.WriteLine();
            }
        }

        // Method to display recipe details
        static void DisplayRecipe(List<Recipe> recipes)
        {
            if (recipes.Count > 0)
            {
                Console.WriteLine("Enter the name of the recipe to display:");
                string recipeName = Console.ReadLine();

                Recipe recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

                if (recipe != null)
                {
                    Console.WriteLine($"Recipe: {recipe.Name}");

                    // Display the list of ingredients
                    Console.WriteLine("Ingredients:");
                    foreach (var ingredient in recipe.Ingredients)
                    {
                        Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} {ingredient.Name} ({ingredient.Calories} calories) - {ingredient.FoodGroup}");

                    }

                    // Display the list of steps
                    Console.WriteLine("Steps:");
                    for (int i = 0; i < recipe.Steps.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {recipe.Steps[i].Description}");
                    }
                }
                else
                {
                    Console.WriteLine("Recipe not found.");
                }
            }
            else
            {
                Console.WriteLine("No recipes found.");
            }

            Console.WriteLine();
        }

        // Method to scale the recipe by a given factor
        static void ScaleRecipe(List<Recipe> recipes)
        {
            if (recipes.Count > 0)
            {
                Console.WriteLine("Enter the name of the recipe to scale:");
                string recipeName = Console.ReadLine();

                Recipe recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

                if (recipe != null)
                {
                    Console.Write("Enter the scaling factor: ");
                    decimal factor;
                    if (decimal.TryParse(Console.ReadLine(), out factor))
                    {
                        // Scale the ingredient quantities
                        foreach (var ingredient in recipe.Ingredients)
                        {
                            ingredient.Quantity = ingredient.OriginalQuantity * factor;
                        }

                        Console.WriteLine("Recipe scaled successfully.");

                        // Check if the total calories exceed 300 and notify the user
                        int totalCalories = recipe.Ingredients.Sum(ingredient => ingredient.Calories);
                        if (totalCalories > 300)
                        {
                            Console.WriteLine("Warning: The total calories of the recipe exceed 300.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid scaling factor. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Recipe not found.");
                }
            }
            else
            {
                Console.WriteLine("No recipes found.");
            }

            Console.WriteLine();
        }

        // Method to reset ingredient quantities to their original values
        static void ResetQuantities(List<Recipe> recipes)
        {
            if (recipes.Count > 0)
            {
                Console.WriteLine("Enter the name of the recipe to reset quantities:");
                string recipeName = Console.ReadLine();

                Recipe recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

                if (recipe != null)
                {
                    // Loop through each ingredient and set its quantity back to the original quantity
                    foreach (var ingredient in recipe.Ingredients)
                    {
                        ingredient.Quantity = ingredient.OriginalQuantity;
                    }

                    Console.WriteLine("Ingredient quantities reset successfully.");
                }
                else
                {
                    Console.WriteLine("Recipe not found.");
                }
            }
            else
            {
                Console.WriteLine("No recipes found.");
            }

            Console.WriteLine();
        }

        // Method to clear all data (ingredients and steps) from the recipe
        static void ClearData(List<Recipe> recipes)
        {
            if (recipes.Count > 0)
            {
                Console.WriteLine("Enter the name of the recipe to clear data:");
                string recipeName = Console.ReadLine();

                Recipe recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

                if (recipe != null)
                {
                    recipes.Remove(recipe);
                    Console.WriteLine("Data cleared successfully.");
                }
                else
                {
                    Console.WriteLine("Recipe not found.");
                }
            }
            else
            {
                Console.WriteLine("No recipes found.");
            }

            Console.WriteLine();
        }
    }
}
