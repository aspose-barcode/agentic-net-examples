using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Generates Postnet barcodes for a list of zip codes and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Processes command‑line arguments, retrieves zip codes,
    /// validates them, generates barcodes, and writes the images to the output folder.
    /// </summary>
    /// <param name="args">Optional first argument specifying the output directory.</param>
    static void Main(string[] args)
    {
        // Determine the output folder: use the first argument if provided, otherwise default to a subfolder.
        string outputFolder;
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            outputFolder = args[0];
        }
        else
        {
            outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "PostnetBarcodes");
        }

        // Ensure the output directory exists.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Retrieve the list of Postnet values (zip codes). Replace this stub with real DB access as needed.
        List<string> postnetValues = GetPostnetValuesFromDatabase();

        int index = 1; // Counter for naming output files sequentially.

        // Iterate over each zip code, validate, generate, and save the barcode.
        foreach (string code in postnetValues)
        {
            // Validate: Postnet barcodes require numeric zip codes only.
            if (string.IsNullOrWhiteSpace(code) || !IsDigitsOnly(code))
            {
                Console.WriteLine($"Skipping invalid code '{code}'.");
                continue;
            }

            // Build the output file name and full path.
            string fileName = $"Postnet_{index:D3}.png";
            string filePath = Path.Combine(outputFolder, fileName);

            // Create a barcode generator for the Postnet format and save the image.
            using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, code))
            {
                // Optional: set image resolution (dots per inch).
                generator.Parameters.Resolution = 300f;
                generator.Save(filePath);
            }

            Console.WriteLine($"Saved barcode for '{code}' to '{filePath}'.");
            index++;
        }

        Console.WriteLine("Barcode generation completed.");
    }

    // ------------------------------------------------------------------------
    // Helper methods
    // ------------------------------------------------------------------------

    /// <summary>
    /// Placeholder method that simulates retrieving zip codes from a database.
    /// Replace with actual data‑access code (e.g., ADO.NET, Entity Framework).
    /// </summary>
    /// <returns>A list of zip code strings.</returns>
    static List<string> GetPostnetValuesFromDatabase()
    {
        // Sample data for demonstration purposes.
        return new List<string>
        {
            "12345",
            "90210",
            "10001",
            "33109",
            "60614"
        };
    }

    /// <summary>
    /// Determines whether the supplied string consists solely of decimal digits.
    /// </summary>
    /// <param name="str">The string to evaluate.</param>
    /// <returns>True if the string contains only digits; otherwise, false.</returns>
    static bool IsDigitsOnly(string str)
    {
        foreach (char c in str)
        {
            if (c < '0' || c > '9')
                return false;
        }
        return true;
    }
}