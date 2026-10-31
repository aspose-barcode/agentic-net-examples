// Title: Generate MaxiCode barcodes and save as GIF images
// Description: Demonstrates creating simple and complex MaxiCode barcodes using Aspose.BarCode and saving them in GIF format.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology. It showcases the use of BarcodeGenerator for basic barcodes and ComplexBarcodeGenerator for advanced configurations, including structured and standard second messages. Developers working with shipping, logistics, or inventory systems often need to generate MaxiCode images in various modes and output formats.
// Prompt: Set the generator's ImageFormat property to GIF and produce a series of MaxiCode images.
// Tags: maxi code, barcode generation, gif, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates several MaxiCode barcodes and saves them as GIF files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates simple and complex MaxiCode images in GIF format.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeSamples_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // 1. Simple MaxiCode using BarcodeGenerator
        using (var genSimple = new BarcodeGenerator(EncodeTypes.MaxiCode, "Simple MaxiCode"))
        {
            // Set the X-dimension (module size) in pixels
            genSimple.Parameters.Barcode.XDimension.Pixels = 15f;

            // Define the output file path and save as GIF
            string pathSimple = Path.Combine(outputDir, "MaxiCode_Simple.gif");
            genSimple.Save(pathSimple, BarCodeImageFormat.Gif);
            Console.WriteLine($"Saved: {pathSimple}");
        }

        // 2. Complex MaxiCode Mode 2 with a structured second message
        var codetextMode2 = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140", // 9 digits
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Build a structured second message (address lines, year, etc.)
        var structuredMsg = new MaxiCodeStructuredSecondMessage();
        structuredMsg.Add("634 ALPHA DRIVE");
        structuredMsg.Add("PITTSBURGH");
        structuredMsg.Add("PA");
        structuredMsg.Year = 99;
        codetextMode2.SecondMessage = structuredMsg;

        using (var genMode2 = new ComplexBarcodeGenerator(codetextMode2))
        {
            // Configure barcode appearance and mode
            genMode2.Parameters.Barcode.XDimension.Pixels = 15f;
            genMode2.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode2;

            // Save the generated image as GIF
            string pathMode2 = Path.Combine(outputDir, "MaxiCode_Mode2_Structured.gif");
            genMode2.Save(pathMode2, BarCodeImageFormat.Gif);
            Console.WriteLine($"Saved: {pathMode2}");
        }

        // 3. Complex MaxiCode Mode 3 with a standard second message
        var codetextMode3 = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Create a standard second message
        var standardMsg = new MaxiCodeStandardSecondMessage { Message = "Standard second message" };
        codetextMode3.SecondMessage = standardMsg;

        using (var genMode3 = new ComplexBarcodeGenerator(codetextMode3))
        {
            // Configure barcode appearance and mode
            genMode3.Parameters.Barcode.XDimension.Pixels = 15f;
            genMode3.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode3;

            // Save the generated image as GIF
            string pathMode3 = Path.Combine(outputDir, "MaxiCode_Mode3_Standard.gif");
            genMode3.Save(pathMode3, BarCodeImageFormat.Gif);
            Console.WriteLine($"Saved: {pathMode3}");
        }

        Console.WriteLine("All MaxiCode images generated.");
    }
}