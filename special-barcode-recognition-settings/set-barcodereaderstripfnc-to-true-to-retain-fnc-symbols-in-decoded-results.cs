// Title: Demonstrate retaining FNC symbols in Code128 barcode decoding
// Description: This example generates a Code128 barcode, then reads it with StripFNC enabled so that any FNC symbols are preserved in the decoded text.
// Category-Description: Shows how to use Aspose.BarCode's BarCodeReader to control FNC symbol handling during barcode recognition. The example covers barcode generation with BarcodeGenerator, setting the StripFNC property via BarcodeSettings, and reading results with BarCodeReader. Developers working with Code128 or other symbologies often need to retain FNC characters for accurate data processing, making this a common scenario in inventory and logistics applications.
// Prompt: Set BarCodeReader.StripFNC to true to retain FNC symbols in decoded results.
// Tags: code128, fnc, stripfnc, barcode generation, barcode recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode and reads it while preserving FNC symbols.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, then reads it with StripFNC enabled and prints results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "FNC_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "code128.png");

        // Generate a Code128 barcode image and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify the image was created before attempting to read it
        if (File.Exists(imagePath))
        {
            // Initialize the reader for Code128 and enable StripFNC to retain FNC symbols
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                reader.BarcodeSettings.StripFNC = true;

                // Iterate through all detected barcodes and output their type and text
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                }
            }
        }
        else
        {
            Console.WriteLine("Barcode image not found.");
        }
    }
}