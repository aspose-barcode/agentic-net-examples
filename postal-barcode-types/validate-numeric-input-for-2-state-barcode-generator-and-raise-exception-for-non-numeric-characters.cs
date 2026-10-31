// Title: Validate numeric input for a 2‑state barcode generator
// Description: Demonstrates how to generate a Planet 2‑state barcode and enforce numeric-only code text, throwing an exception for invalid characters.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and generator parameters such as ThrowExceptionWhenCodeTextIncorrect. Typical use cases include validating input before creating barcodes for inventory, shipping, or tracking systems where only numeric data is allowed. Developers often need to ensure code text correctness to avoid runtime errors and maintain data integrity.
// Prompt: Validate numeric input for a 2‑state barcode generator and raise an exception for non‑numeric characters.
// Tags: barcode, planet, numeric validation, exception handling, generation, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that validates numeric input for a 2‑state (Planet) barcode
/// and generates the barcode image when the input is valid.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over sample inputs,
    /// attempts to generate a Planet barcode, and reports success or failure.
    /// </summary>
    static void Main()
    {
        // Sample inputs: one valid numeric string and one containing a non‑numeric character.
        string[] inputs = { "123456", "12A34" };

        // Process each input sequentially.
        for (int i = 0; i < inputs.Length; i++)
        {
            string codeText = inputs[i];
            try
            {
                // Initialize the barcode generator with Planet symbology and the current code text.
                using (var generator = new BarcodeGenerator(EncodeTypes.Planet, codeText))
                {
                    // Enable strict validation: an exception is thrown if the code text is not numeric.
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                    // Generate the barcode image.
                    using (Bitmap bitmap = generator.GenerateBarCodeImage())
                    {
                        // Build a temporary file path for the output PNG.
                        string outPath = Path.Combine(Path.GetTempPath(), $"Planet_{i}.png");

                        // Save the image to disk.
                        bitmap.Save(outPath, Aspose.Drawing.Imaging.ImageFormat.Png);

                        // Inform the user of successful generation.
                        Console.WriteLine($"Valid input \"{codeText}\" - barcode saved to {outPath}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Report invalid input and the associated exception message.
                Console.WriteLine($"Invalid input \"{codeText}\" - exception: {ex.Message}");
            }
        }
    }
}