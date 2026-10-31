// Title: Generate MaxiCode Mode 3 barcode with structured secondary message and save as JPEG
// Description: Demonstrates how to create a MaxiCode Mode 3 barcode, include a structured secondary message, and export the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode3, and MaxiCodeStructuredSecondMessage classes to build a barcode with primary and secondary data. Developers working with shipping, logistics, or inventory systems often need to generate MaxiCode symbols for package tracking, and this snippet illustrates the typical workflow.
// Prompt: Create a MaxiCode Mode 3 barcode using a structured secondary message and export the image as JPEG.
// Tags: maxicode, mode3, structured secondary message, jpeg, barcode generation, aspnet.barcode, complexbarcodegenerator, maxicode codetext

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates creating a MaxiCode Mode 3 barcode with a structured secondary message and saving it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, adds a secondary message, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory and file path
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeExample");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "MaxiCodeMode3Structured.jpg");

        // Create MaxiCode codetext for Mode 3 (primary data)
        var maxiCodeCodetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Build a structured secondary message (address information)
        var structuredMessage = new MaxiCodeStructuredSecondMessage();
        structuredMessage.Add("634 ALPHA DRIVE");
        structuredMessage.Add("PITTSBURGH");
        structuredMessage.Add("PA");
        structuredMessage.Year = 99;

        // Attach the secondary message to the MaxiCode codetext
        maxiCodeCodetext.SecondMessage = structuredMessage;

        // Generate the barcode and save it as a JPEG image
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            generator.Save(outputPath);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"MaxiCode Mode 3 barcode saved to: {outputPath}");
    }
}