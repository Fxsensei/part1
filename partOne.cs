using System;
using System.Globalization;

namespace RecipeApp
{
    // Ingredient class represents an ingredient in a recipe
    class Ingredient
    {
        public string Name { get; set; } //  the name of the ingredient
        public decimal Quantity { get; set; } // the Quantity
        public string Unit { get; set; } // the Unit of measurement 
        public decimal OriginalQuantity { get; set; } // this is for the original amount of the ingredient before we scale it
    }

    // this class is for the steps the user will input for the recipe
    class Step
    {
        public string Description { get; set; } // writting the description
    }

    // this class will store the arrays of ingredients and steps for the recipe
    class Recipe
    {
        public Ingredient[] Ingredients { get; set; } // Array of ingredients in the recipe
        public Step[] Steps { get; set; } // Array of steps in the recipe
    }

    class Program
    {
        // Main method to run the code/ program
        static void Main(string[] args)
        {
            Recipe recipe = new Recipe(); // this create a new recipe 

            while (true) // a while loop that will run indefinitely until the user chooses to exit
            {
                // options menu
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. Enter recipe details");
                Console.WriteLine("2. Display recipe");
                Console.WriteLine("3. Scale recipe");
                Console.WriteLine("4. Reset quantities");
                Console.WriteLine("5. Clear all data");
                Console.WriteLine("6. Exit");

                // read user input and parse as integer
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    // perform action based on user's choice
                    switch (choice)
                    {
                        case 1:
                            EnterRecipeDetails(recipe); // call EnterRecipeDetails method to enter recipe details
                            break;
                        case 2:
                            DisplayRecipe(recipe); // call DisplayRecipe method to display recipe details
                            break;
                        case 3:
                            ScaleRecipe(recipe); // call ScaleRecipe method to scale the recipe
                            break;
                        case 4:
                            ResetQuantities(recipe); // call ResetQuantities method to reset ingredient quantities
                            break;
                        case 5:
                            ClearData(recipe); // call ClearData method to clear all recipe data
                            break;
                        case 6:
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


        static void EnterRecipeDetails(Recipe recipe)
        {
            Console.Write("Enter the number of ingredients: ");
            int numIngredients;
            if (int.TryParse(Console.ReadLine(), out numIngredients))
            {
                recipe.Ingredients = new Ingredient[numIngredients];

                // Loop through each ingredient and prompt the user to enter its details
                for (int i = 0; i < numIngredients; i++)
                {
                    Console.WriteLine($"Enter the details for ingredient {i + 1}:");
                    Console.Write("Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Quantity: ");
                    decimal quantity;
                    if (decimal.TryParse(Console.ReadLine(), out quantity))
                    {
                        Console.Write("Unit: ");
                        string unit = Console.ReadLine();

                        recipe.Ingredients[i] = new Ingredient { Name = name, Quantity = quantity, Unit = unit, OriginalQuantity = quantity };
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity. Please try again.");
                        i--;
                    }
                }

                EnterRecipeSteps(recipe); // Call the method to enter recipe steps

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
                recipe.Steps = new Step[numSteps];

                // Loop through each step and prompt the user to enter its details
                for (int i = 0; i < numSteps; i++)
                {
                    Console.WriteLine($"Enter the details for step {i + 1}:");
                    Console.Write("Description: ");
                    string description = Console.ReadLine();

                    recipe.Steps[i] = new Step { Description = description };
                }

                Console.WriteLine("Recipe steps entered successfully.");
            }
        }


        // Method to display recipe details
        static void DisplayRecipe(Recipe recipe)
        {
            // Display the list of ingredients
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in recipe.Ingredients)
            {
                Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} {ingredient.Name}");
            }

            // Display the list of steps
            Console.WriteLine("Steps:");
            for (int i = 0; i < recipe.Steps.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {recipe.Steps[i].Description}");
            }
        }

        // Method to scale the recipe by a given factor
        static void ScaleRecipe(Recipe recipe)
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
            }
            else
            {
                Console.WriteLine("Invalid scaling factor. Please try again.");
            }
        }

        // Method to reset ingredient quantities to their original values
        static void ResetQuantities(Recipe recipe)
        {
            // loop through each ingredient and set its quantity back to the original quantity
            foreach (var ingredient in recipe.Ingredients)
            {
                ingredient.Quantity = ingredient.OriginalQuantity;
            }
            Console.WriteLine("Ingredient quantities reset successfully.");
        }

        // This function clears all data (ingredients and steps) from the recipe
        static void ClearData(Recipe recipe)
        {
            recipe.Ingredients = new Ingredient[0];
            recipe.Steps = new Step[0];
            Console.WriteLine("All data cleared successfully.");
        }

    }
}
