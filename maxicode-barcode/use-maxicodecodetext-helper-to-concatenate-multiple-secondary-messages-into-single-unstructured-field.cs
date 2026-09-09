// Title: Generate MaxiCode barcode with concatenated secondary messages
// Description: Demonstrates how to create a MaxiCode barcode and combine multiple secondary messages into a single unstructured field using the MaxiCodeCodetext helper.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of MaxiCodeCodetextMode3, MaxiCodeStandardSecondMessage, and ComplexBarcodeGenerator to produce MaxiCode symbols. Typical use cases include shipping labels and logistics where secondary data must be packed into a single unstructured field. Developers often need to concatenate messages, set postal information, and export the barcode as an image.
// Prompt: Use the MaxiCodeCodetext helper to concatenate multiple secondary messages into a single unstructured field.
// Tags: maxicode, barcode generation, png, complexbarcodegenerator, maxicodecodetextmode3, maxicodestandardsecondmessage

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a MaxiCode barcode with a concatenated secondary message.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds a MaxiCode barcode, concatenates secondary messages, and saves the image as PNG.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "MaxiCodeUnstructured.png");

        // Define multiple secondary messages to be combined
        string[] secondaryMessages = { "First part", "Second part", "Third part" };
        // Concatenate messages with a delimiter
        string concatenatedMessage = string.Join(" | ", secondaryMessages);

        // Create an unstructured second message containing the concatenated text
        var unstructuredSecond = new MaxiCodeStandardSecondMessage
        {
            Message = concatenatedMessage
        };

        // Build the MaxiCode codetext (using Mode 3 as an example) and assign the unstructured second message
        var maxiCodeCodetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = unstructuredSecond
        };

        // Generate the barcode and save it as a PNG file
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine("MaxiCode barcode saved to: " + outputPath);
    }
}