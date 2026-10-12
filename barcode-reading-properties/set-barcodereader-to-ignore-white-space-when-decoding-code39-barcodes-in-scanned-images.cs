// Title: Decode Code39 barcode while ignoring whitespace
// Description: Demonstrates how to generate a Code39 barcode containing spaces and configure BarCodeReader to strip whitespace (FNC) during decoding.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader and BarcodeGenerator classes to handle Code39 symbology. Developers often need to read barcodes from scanned images where extra whitespace or FNC characters may be present; setting StripFNC to true enables reliable decoding in such scenarios.
// Prompt: Set BarCodeReader to ignore white space when decoding Code39 barcodes in scanned images.
// Tags: code39, whitespace, stripfnc, barcode recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code39 barcode with whitespace and reading it while ignoring whitespace using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it with StripFNC enabled, and outputs the result.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code39Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "code39.png");

        // Generate a Code39 barcode that includes a space character
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "ABC 123"))
        {
            // Set the X-dimension (module width) to improve image clarity
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            // Save the generated barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode while ignoring whitespace (StripFNC)
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code39))
        {
            // Enable stripping of FNC characters, which removes whitespace for Code39
            reader.BarcodeSettings.StripFNC = true;

            // Perform the decoding operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the decoding results
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                }
            }
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}