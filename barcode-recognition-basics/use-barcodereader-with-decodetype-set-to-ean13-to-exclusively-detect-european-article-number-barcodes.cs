// Title: EAN13 Barcode Generation and Detection Example
// Description: Demonstrates generating an EAN13 barcode image and using BarCodeReader with DecodeType set to EAN13 to detect only European Article Number barcodes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create an EAN13 barcode and BarCodeReader with DecodeType.EAN13 to restrict scanning to this symbology. Developers working with product labeling, inventory systems, or retail applications often need to generate and read EAN13 codes, and this snippet illustrates the typical API classes and workflow.
/// Prompt: Use BarCodeReader with DecodeType set to EAN13 to exclusively detect European Article Number barcodes.
/// Tags: ean13, barcode, generation, recognition, decode, aspnet, aspose.barcode, symbology, one-dimensional

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a simple demonstration of generating an EAN13 barcode image
/// and reading it back using <see cref="BarCodeReader"/> with <see cref="DecodeType.EAN13"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a temporary EAN13 barcode image,
    /// reads it using a restricted decode type, outputs the results, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "EAN13Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode image file
        string imagePath = Path.Combine(tempFolder, "ean13.png");

        // Generate a sample EAN13 barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to generate the barcode image.");
            return;
        }

        // Read the barcode using DecodeType.EAN13 to restrict detection to EAN13 only
        using (var reader = new BarCodeReader(imagePath, DecodeType.EAN13))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            // Check whether any EAN13 barcodes were detected
            if (results.Length == 0)
            {
                Console.WriteLine("No EAN13 barcode detected.");
            }
            else
            {
                // Output details for each detected barcode
                foreach (var result in results)
                {
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"Value: {result.Extended.OneD.Value}");
                    Console.WriteLine($"CheckSum: {result.Extended.OneD.CheckSum}");
                }
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}