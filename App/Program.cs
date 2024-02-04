// Main entry point for the BubblePuzzle game application.
using System;

try
{
    // Using 'var' for implicit type declaration. 'using' ensures proper disposal of the game object.
    using var game = new BubblePuzzle.Main(); // Creates a new instance of the game.

    // Starts the game loop. This is where the game's updating and rendering happens.
    game.Run();
}
catch (Exception ex)
{
    // Handle any exceptions that occur during the game's initialization or execution.
    // Logging the exception details can be helpful for debugging.
    Console.WriteLine($"An error occurred: {ex.Message}");
    // Additional error handling logic can be added here.
}