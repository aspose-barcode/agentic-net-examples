// Title: Generate MaxiCode Mode 2 barcode with unstructured secondary message
// Description: Demonstrates creating a MaxiCode Mode 2 barcode that includes an unstructured secondary message and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, and related parameter settings to produce high‑density 2‑D barcodes. Developers often need to generate MaxiCode for shipping and logistics, customizing postal codes, country codes, and optional secondary messages.
// Prompt: Generate a MaxiCode Mode 2 barcode with an unstructured secondary message and save it as PNG.
// Tags: maxicode, barcode generation, png, complexbarcodegenerator, maxicodecodetextmode2

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a MaxiCode Mode 2 barcode with an unstructured secondary message and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures parameters, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeMode2Unstructured.png");

        // Build the MaxiCode codetext for Mode 2, including an unstructured secondary message.
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",          // 9 numeric digits required for Mode 2
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = new MaxiCodeStandardSecondMessage
            {
                Message = "Unstructured secondary message"
            }
        };

        // Generate the barcode using the complex barcode generator.
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Set the MaxiCode structural mode to Mode 2.
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode2;

            // Optional: adjust the module (dot) size for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}