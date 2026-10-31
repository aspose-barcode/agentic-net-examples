// Title: Ignore whitespace when decoding Code39 barcodes with BarCodeReader
// Description: Demonstrates how to configure BarCodeReader to ignore whitespace-like characters while decoding a Code39 barcode that contains spaces, using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to create a Code39 barcode with spaces using BarcodeGenerator, then read it with BarCodeReader while setting the StripFNC property to ignore whitespace-like characters. Developers working with barcode scanning, especially those handling Code39 symbology in scanned images, often need to adjust reader settings to improve decoding accuracy.
// Prompt: Set BarCodeReader to ignore white space when decoding Code39 barcodes in scanned images.
// Tags: code39, barcode, whitespace, stripfnc, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code39 barcode containing spaces,
/// then reads it while ignoring whitespace-like characters using BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it with whitespace ignored,
    /// and outputs the decoded information to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code39Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "code39.png");

        // Generate a Code39 barcode that includes spaces in its data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "A B C"))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            // Save the barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image not found: " + barcodePath);
            return;
        }

        // Initialize BarCodeReader to decode Code39 barcodes from the image
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code39))
        {
            // Enable StripFNC to treat whitespace-like characters as non-data (ignore them)
            reader.BarcodeSettings.StripFNC = true;

            // Perform the decoding operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the decoding results
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                }
            }
        }

        // Attempt to clean up temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome
        }
    }
}