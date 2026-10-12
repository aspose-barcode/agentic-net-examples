// Title: Generate Planet Postal Barcode with XDimension Validation
// Description: Demonstrates creating a Planet postal barcode while validating the XDimension parameter to ensure it is positive.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.Planet. It covers setting barcode dimensions, handling invalid input, and saving the result as an image. Developers working with postal symbologies often need to validate parameters like XDimension to meet printing standards.
// Prompt: Implement error handling for invalid XDimension values when creating a postal barcode.
// Tags: barcode, postal, planet, xdimension, validation, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Planet postal barcode with XDimension validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses optional XDimension argument, validates it, and generates a barcode image.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may specify XDimension in points.</param>
    static void Main(string[] args)
    {
        // Default XDimension value (points)
        float xDimension = 2f;

        // Attempt to parse XDimension from the first command‑line argument, if provided
        if (args.Length > 0)
        {
            if (!float.TryParse(args[0], out xDimension))
            {
                Console.WriteLine("Invalid XDimension format. Using default value 2.");
                xDimension = 2f;
            }
        }

        // Validate XDimension: must be greater than zero
        if (xDimension <= 0f)
        {
            Console.WriteLine($"Error: XDimension must be greater than zero. Provided value: {xDimension}");
            return;
        }

        // Prepare output path for the generated PNG image
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "planet.png");

        try
        {
            // Initialize the barcode generator for the Planet postal symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "1234567"))
            {
                // Apply the validated XDimension (in points) to the barcode
                generator.Parameters.Barcode.XDimension.Point = xDimension;

                // Ensure that an incorrect code text would raise an exception (optional for this example)
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                // Save the generated barcode as a PNG file
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Barcode generated successfully at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during barcode generation or saving
            Console.WriteLine($"Failed to generate barcode: {ex.Message}");
        }
    }
}