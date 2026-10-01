// Title: Ignore whitespace when decoding Code39 barcodes with BarCodeReader
// Description: Demonstrates how to configure BarCodeReader to strip whitespace (FNC characters) while decoding a Code39 barcode generated with spaces.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator to create a Code39 barcode and BarCodeReader with BarcodeSettings to modify decoding behavior. Developers often need to read barcodes from scanned images where whitespace or FNC characters should be ignored, making StripFNC a common setting for clean data extraction.
// Prompt: Set BarCodeReader to ignore white space when decoding Code39 barcodes in scanned images.
// Tags: code39, whitespace, stripfnc, barcode, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code39 barcode containing spaces and
/// decodes it while ignoring whitespace using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, decodes it with whitespace
    /// stripping enabled, and outputs the result to the console.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "code39.png");

        // Create a Code39 barcode that contains whitespace characters
        string codeTextWithSpaces = "A B C";

        // Generate the barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeTextWithSpaces))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the barcode while ignoring whitespace (strip FNC characters)
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code39))
        {
            // Instruct the reader to strip FNC/whitespace characters during decoding
            reader.BarcodeSettings.StripFNC = true;

            // Perform the decoding
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                }
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program outcome
        }
    }
}