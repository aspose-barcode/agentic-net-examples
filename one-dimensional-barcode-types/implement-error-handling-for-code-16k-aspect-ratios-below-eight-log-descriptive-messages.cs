// Title: Generate Code 16K barcode with aspect‑ratio validation and error handling
// Description: This example creates a Code 16K barcode, ensures the aspect ratio meets the minimum requirement, and saves the image as PNG.
// Category-Description: Demonstrates Aspose.BarCode barcode generation focusing on Code16K symbology. It covers using BarcodeGenerator, setting symbology‑specific parameters (AspectRatio), handling invalid input, and catching BarCodeException. Ideal for developers needing to produce high‑density linear barcodes with proper validation and logging.
// Prompt: Implement error handling for Code 16K aspect ratios below eight, log descriptive messages.
// Tags: barcode, code16k, aspectratio, errorhandling, generation, png, aspose.barcode, aspnet

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code 16K barcode with aspect‑ratio validation and error handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Validates aspect ratio, generates the barcode, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Desired aspect ratio (example value). Change this value to test different scenarios.
        float requestedAspectRatio = 5.5f;

        // Validate aspect ratio for Code16K (minimum allowed is 8). Adjust if below the threshold.
        if (requestedAspectRatio < 8f)
        {
            Console.WriteLine($"[Warning] Code16K aspect ratio {requestedAspectRatio} is below the minimum of 8. Adjusting to 8.");
            requestedAspectRatio = 8f;
        }

        try
        {
            // Create a Code16K barcode generator with sample code text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, "1234567890"))
            {
                // Apply the (validated) aspect ratio to the Code16K parameters.
                generator.Parameters.Barcode.Code16K.AspectRatio = requestedAspectRatio;

                // Generate the barcode image.
                using (Aspose.Drawing.Bitmap image = generator.GenerateBarCodeImage())
                {
                    // Define the output file path and save the image as PNG.
                    string outputPath = "code16k.png";
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Barcode image saved to '{outputPath}'.");
                }
            }
        }
        catch (BarCodeException ex)
        {
            // Handle barcode‑specific errors.
            Console.WriteLine($"[Error] Barcode generation failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors.
            Console.WriteLine($"[Error] Unexpected exception: {ex.Message}");
        }
    }
}