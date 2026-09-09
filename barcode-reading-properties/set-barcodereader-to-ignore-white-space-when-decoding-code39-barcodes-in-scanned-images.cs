// Title: Ignore whitespace when decoding Code39 barcodes with BarCodeReader
// Description: Demonstrates how to configure BarCodeReader to strip whitespace-like characters while decoding Code39 barcodes generated with spaces.
// Category-Description: This example belongs to the Aspose.BarCode reading and decoding category. It showcases the use of BarCodeReader and BarcodeGenerator classes to create a Code39 barcode, then read it with and without whitespace stripping. Developers often need to handle barcodes that contain spaces or other non‑data characters; setting the StripFNC property enables ignoring such characters during recognition, a common requirement in inventory and logistics applications.
// Prompt: Set BarCodeReader to ignore white space when decoding Code39 barcodes in scanned images.
// Tags: code39, barcode, whitespace, stripfnc, decoding, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code39 barcode containing a space and
/// demonstrates how to read it with and without whitespace stripping using
/// Aspose.BarCode's <see cref="BarCodeReader"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it twice (default
    /// and with <c>StripFNC</c> enabled), and cleans up temporary files.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code39Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "code39.png");

        // Generate a Code39 barcode that includes a whitespace character in the text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "ABC 123"))
        {
            // Set the X‑dimension (module width) to 2 pixels for better visibility
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            // Save the barcode as a PNG image
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the barcode without stripping whitespace (default behavior)
        // --------------------------------------------------------------------
        Console.WriteLine("Reading without StripFNC (default):");
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code39))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // --------------------------------------------------------------------
        // Read the barcode with StripFNC = true to ignore whitespace-like characters
        // --------------------------------------------------------------------
        Console.WriteLine("\nReading with StripFNC = true (ignore whitespace):");
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code39))
        {
            // Enable stripping of FNC characters, which also removes spaces for Code39
            reader.BarcodeSettings.StripFNC = true;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // Cleanup temporary files (optional)
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}