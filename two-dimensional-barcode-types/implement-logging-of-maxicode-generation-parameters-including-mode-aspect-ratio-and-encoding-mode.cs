// Title: Generate MaxiCode barcode with parameter logging
// Description: Demonstrates creating a MaxiCode barcode, logging its generation parameters (mode, aspect ratio, encoding mode), and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.MaxiCode. It shows typical tasks such as configuring MaxiCode-specific settings, retrieving parameter values for diagnostics, and exporting the barcode to common image formats. Developers working with shipping labels, logistics, or inventory systems often need these operations.
// Prompt: Implement logging of MaxiCode generation parameters, including mode, aspect ratio, and encoding mode.
// Tags: maxicode, barcode, generation, logging, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates MaxiCode barcode generation, parameter logging, and image saving using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a MaxiCode barcode, writes its configuration to the console, and saves the image.
    /// </summary>
    static void Main()
    {
        // Prepare the output file path in the temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "maxicode.png");

        // Sample code text (ASCII to avoid Binary mode issues).
        string codeText = "Sample MaxiCode";

        // Create and configure a MaxiCode generator.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Set the MaxiCode mode (e.g., Mode2 for standard shipping labels).
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode2;

            // Define the aspect ratio for the generated barcode.
            generator.Parameters.Barcode.MaxiCode.AspectRatio = 0.5f;

            // Choose the encoding mode (Binary in this example).
            generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.Binary;

            // Log the configured parameters to the console for diagnostic purposes.
            Console.WriteLine("MaxiCode Generation Parameters:");
            Console.WriteLine($"  Mode          : {generator.Parameters.Barcode.MaxiCode.Mode}");
            Console.WriteLine($"  Aspect Ratio  : {generator.Parameters.Barcode.MaxiCode.AspectRatio}");
            Console.WriteLine($"  Encode Mode   : {generator.Parameters.Barcode.MaxiCode.EncodeMode}");

            // Attempt to save the generated barcode as a PNG image.
            try
            {
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving barcode: {ex.Message}");
            }
        }
    }
}