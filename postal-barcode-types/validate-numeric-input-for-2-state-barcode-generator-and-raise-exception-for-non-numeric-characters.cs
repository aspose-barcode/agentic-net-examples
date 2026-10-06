// Title: Numeric Validation for 2‑State Planet Barcode Generation
// Description: Demonstrates how to validate that the code text contains only numeric characters before generating a 2‑state Planet barcode, throwing an exception for invalid input.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on input validation for 2‑state symbologies such as Planet. It showcases the use of BarcodeGenerator, EncodeTypes, and the ThrowExceptionWhenCodeTextIncorrect parameter to enforce correct code text. Developers often need to ensure data integrity before creating barcodes for inventory, tracking, or packaging applications.
// Prompt: Validate numeric input for a 2‑state barcode generator and raise an exception for non‑numeric characters.
// Tags: barcode, planet, numeric validation, exception handling, c#, aspose.barcode, generation, 2-state

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that validates numeric input for a 2‑state Planet barcode
/// and generates the barcode image when the input is valid.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Iterates over sample code texts,
    /// validates each, and attempts to generate a barcode image.
    /// </summary>
    static void Main()
    {
        // Sample code texts: one valid numeric string and one containing a non‑numeric character.
        string[] samples = { "123456", "12A34" };

        // Process each sample text.
        foreach (var text in samples)
        {
            Console.WriteLine($"Processing code text: {text}");
            try
            {
                // Validate the text and generate the barcode if valid.
                ValidateAndGenerate(text);
                Console.WriteLine("Barcode generated successfully.");
            }
            catch (Exception ex)
            {
                // Output any validation or generation errors.
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Validates that the provided code text contains only numeric characters
    /// and generates a Planet barcode image. An exception is thrown automatically
    /// if the validation fails, thanks to the ThrowExceptionWhenCodeTextIncorrect setting.
    /// </summary>
    /// <param name="codeText">The code text to validate and encode.</param>
    static void ValidateAndGenerate(string codeText)
    {
        // Initialize the barcode generator for the Planet symbology with the given text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, codeText))
        {
            // Enable automatic exception throwing for invalid code text.
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

            // Generate the barcode image.
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Build a unique temporary file path for the PNG image.
                string tempPath = Path.Combine(Path.GetTempPath(), $"Planet_{codeText}_{Guid.NewGuid():N}.png");

                // Save the generated image to the temporary file.
                using (var fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
                {
                    bitmap.Save(fileStream, ImageFormat.Png);
                }
            }
        }
    }
}