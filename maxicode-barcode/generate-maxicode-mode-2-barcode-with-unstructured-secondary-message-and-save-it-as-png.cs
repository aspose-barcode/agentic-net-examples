// Title: Generate MaxiCode Mode 2 barcode with unstructured secondary message
// Description: Demonstrates creating a MaxiCode Mode 2 barcode that includes an unstructured secondary message and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, and related parameter settings to customize barcode appearance. Developers working with shipping, logistics, or inventory systems often need to generate MaxiCode symbols with specific data fields and visual options.
// Prompt: Generate a MaxiCode Mode 2 barcode with an unstructured secondary message and save it as PNG.
// Tags: maxicode, mode2, secondary message, png, barcode generation, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a MaxiCode Mode 2 barcode with an unstructured secondary message and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures visual parameters, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeMode2Unstructured.png");

        // Build the MaxiCode codetext for Mode 2, including postal code, country code, service category, and an unstructured secondary message
        MaxiCodeCodetextMode2 codetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = new MaxiCodeStandardSecondMessage
            {
                Message = "Unstructured secondary message"
            }
        };

        // Initialize the complex barcode generator with the prepared codetext
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(codetext))
        {
            // Set the MaxiCode mode to Mode 2
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode2;

            // Optional visual customizations
            generator.Parameters.Barcode.XDimension.Pixels = 15f;               // Size of a single module in pixels
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black; // Barcode bar color
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;       // Background color

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"MaxiCode Mode 2 barcode saved to: {outputPath}");
    }
}