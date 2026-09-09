// Title: Create MaxiCode Mode 3 barcode with structured secondary message and save as JPEG
// Description: Demonstrates how to generate a MaxiCode barcode in Mode 3, include a structured secondary message, and export the image as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode3, and MaxiCodeStructuredSecondMessage classes to build a barcode with both primary and secondary data. Developers working with shipping, logistics, or inventory systems often need to create MaxiCode symbols with structured messages for automated scanning.
// Prompt: Create a MaxiCode Mode 3 barcode using a structured secondary message and export the image as JPEG.
// Tags: maxicode, mode3, structured secondary message, jpeg, barcode generation, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a MaxiCode Mode 3 barcode with a structured secondary message
/// and saves it as a JPEG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Prepares the output folder, builds the barcode data,
    /// generates the MaxiCode symbol, and writes the result to a JPEG file.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory where the generated image will be stored
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the full path for the resulting JPEG file
        string outputPath = Path.Combine(outputDir, "MaxiCodeMode3StructuredSecondMessage.jpg");

        // Create and configure the primary MaxiCode data for Mode 3
        MaxiCodeCodetextMode3 maxiCodeCodetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Build the structured secondary message (address information)
        MaxiCodeStructuredSecondMessage structuredMessage = new MaxiCodeStructuredSecondMessage();
        structuredMessage.Add("634 ALPHA DRIVE");
        structuredMessage.Add("PITTSBURGH");
        structuredMessage.Add("PA");
        structuredMessage.Year = 99;

        // Attach the secondary message to the MaxiCode data
        maxiCodeCodetext.SecondMessage = structuredMessage;

        // Generate the barcode and save it as a JPEG image
        using (ComplexBarcodeGenerator complexGenerator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            complexGenerator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}