// Title: Create MaxiCode Mode 3 barcode with structured secondary message and save as JPEG
// Description: Demonstrates how to generate a MaxiCode Mode 3 barcode, embed a structured secondary message, and export the image as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of MaxiCodeCodetextMode3, MaxiCodeStructuredSecondMessage, and ComplexBarcodeGenerator classes to produce high‑density 2‑D barcodes. Typical scenarios include shipping labels, parcel tracking, and logistics where MaxiCode Mode 3 is required. Developers often need to add secondary address information and export the result in common image formats.
// Prompt: Create a MaxiCode Mode 3 barcode using a structured secondary message and export the image as JPEG.
// Tags: maxicode, mode3, structured secondary message, jpeg, aspose.barcode, complexbarcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a MaxiCode Mode 3 barcode with a structured secondary message and saves it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, adds a secondary message, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare the output directory where the generated image will be stored
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the full file path for the resulting JPEG image
        string outputPath = Path.Combine(outputDir, "MaxiCodeMode3StructuredSecondMessage.jpg");

        // --------------------------------------------------------------
        // Create the MaxiCode codetext for Mode 3 and set required fields
        // --------------------------------------------------------------
        MaxiCodeCodetextMode3 maxiCodeCodetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // --------------------------------------------------------------
        // Build a structured secondary message (address information)
        // --------------------------------------------------------------
        MaxiCodeStructuredSecondMessage structuredMessage = new MaxiCodeStructuredSecondMessage();
        structuredMessage.Add("634 ALPHA DRIVE");
        structuredMessage.Add("PITTSBURGH");
        structuredMessage.Add("PA");
        structuredMessage.Year = 99;

        // Attach the secondary message to the MaxiCode codetext
        maxiCodeCodetext.SecondMessage = structuredMessage;

        // --------------------------------------------------------------
        // Generate the barcode image and save it as a JPEG file
        // --------------------------------------------------------------
        using (ComplexBarcodeGenerator complexGenerator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            complexGenerator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the file has been saved
        Console.WriteLine($"MaxiCode Mode 3 barcode saved to: {outputPath}");
    }
}