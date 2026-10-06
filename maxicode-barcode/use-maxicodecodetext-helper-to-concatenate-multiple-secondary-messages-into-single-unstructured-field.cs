// Title: Generate MaxiCode barcode with concatenated secondary messages
// Description: Demonstrates creating a MaxiCode (Mode 3) barcode and merging several secondary messages into one unstructured field using the MaxiCodeCodetext helper.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on MaxiCode symbology. It showcases key API classes such as ComplexBarcodeGenerator, MaxiCodeCodetextMode3, and MaxiCodeStandardSecondMessage. Typical use cases include shipping labels and logistics where MaxiCode encodes address and service data. Developers often need to combine multiple text fragments into the secondary message field for compliance with industry standards.
// Prompt: Use the MaxiCodeCodetext helper to concatenate multiple secondary messages into a single unstructured field.
// Tags: maxicode, barcode generation, png, complexbarcodegenerator, maxicodecodetextmode3, maxicodestandardsecondmessage

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with concatenated secondary messages.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "MaxiCode_Unstructured.png");

        // Define multiple secondary messages to concatenate
        string[] secondaryMessages = new[] { "First part", "Second part", "Third part" };
        // Combine messages into a single string separated by spaces
        string concatenatedMessage = string.Join(" ", secondaryMessages);

        // Build MaxiCode codetext with an unstructured second message
        var codetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = new MaxiCodeStandardSecondMessage
            {
                Message = concatenatedMessage
            }
        };

        // Generate the barcode using the complex barcode generator
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            // Set MaxiCode mode to 3 (required for this codetext structure)
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode3;
            // Optional: adjust module size for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}