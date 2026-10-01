// Title: Large XDimension Barcode Generation and Recognition Example
// Description: Demonstrates generating a Code128 barcode with an XDimension larger than 10 pixels and recognizing it using the Large XDimension mode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing how to configure XDimension for large barcodes, generate the image, and read it using the Large mode. It highlights key API classes such as BarcodeGenerator, BarCodeReader, and XDimensionMode, which developers use when handling high‑resolution or oversized barcodes in applications like inventory systems or industrial labeling.
// Prompt: Test recognition of very large barcodes (>10 pixels XDimension) using Large mode configuration.
// Tags: code128, large xdimension, barcode generation, barcode recognition, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with a large XDimension and recognizing it using Large mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a large‑XDimension barcode, saves it, reads it back, and outputs the result.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample
        string tempFolder = Path.Combine(Path.GetTempPath(), "LargeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define barcode parameters
        string codeText = "123456789012";
        string barcodePath = Path.Combine(tempFolder, "large_barcode.png");

        // Generate a barcode with a large XDimension (>10 pixels)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set XDimension to 12 pixels (greater than 10)
            generator.Parameters.Barcode.XDimension.Pixels = 12f;

            // Optional: increase bar height for better visibility
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode using Large mode configuration (QualitySettings.XDimension = Large)
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Configure recognition to use Large XDimension mode
            reader.QualitySettings.XDimension = XDimensionMode.Large;

            // Perform the read operation
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                // Output each detected barcode's details
                foreach (var result in results)
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                }
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}