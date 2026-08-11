// Title: BarCodeReader StripFNC Example
// Description: Demonstrates how to disable stripping of FNC characters when reading a GS1 Code128 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to create a GS1 Code128 barcode with possible FNC symbols using BarcodeGenerator, save it as an image, and then read it back with BarCodeReader while configuring BarcodeSettings.StripFNC. Developers working with GS1 barcodes often need to preserve FNC characters for accurate data extraction, making this pattern common in inventory, logistics, and retail applications.
// Prompt: Set BarCodeReader.StripFNC to false to remove FNC symbols from decoded results.
// Tags: barcode, gs1, code128, stripfnc, recognition, generation, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a GS1 Code128 barcode, saves it to disk,
/// and reads it back with StripFNC disabled to retain any FNC characters in the result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, saves the image,
    /// and performs recognition with StripFNC set to false.
    /// </summary>
    static void Main()
    {
        // Define temporary output directory and barcode image path
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        string barcodePath = Path.Combine(outputDir, "barcode.png");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate a GS1 Code128 barcode that may contain FNC characters
        using (BarcodeGenerator generator = new BarcodeGenerator(
            EncodeTypes.GS1Code128,
            "(02)04006664241007(37)1(400)7019590754"))
        {
            // Save the generated barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode image and configure the reader to keep FNC characters
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Disable stripping of FNC characters (set to false as requested)
            reader.BarcodeSettings.StripFNC = false;

            // Perform barcode recognition
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output recognition results
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text   : {result.CodeText}");
                }
            }
        }
    }
}