// Title: Generate barcode image with custom dimensions and unit using Aspose.BarCode
// Description: Demonstrates how to create a Code128 barcode, set its size in various measurement units, and obtain the PNG image bytes.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the BarcodeGenerator class for creating barcodes, configuring image dimensions via the Parameters.ImageWidth/Height properties, and exporting to common image formats. Developers often need to produce barcodes with specific size requirements for web APIs, reports, or label printing.
// Prompt: Create web API endpoint accepting width, height, and unit, generating barcode and returning image bytes.
// Tags: barcode, code128, generation, dimensions, unit, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with custom size settings using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that simulates a request, generates a barcode, and writes the image to a file.
    /// </summary>
    static void Main()
    {
        // Simulate a request with width, height, and unit
        int width = 300;
        int height = 150;
        string unit = "Pixels";

        try
        {
            // Generate the barcode image bytes based on the supplied dimensions
            byte[] barcodeBytes = GenerateBarcode(width, height, unit);

            // Save the generated image to a file for verification
            string outputPath = "barcode.png";
            File.WriteAllBytes(outputPath, barcodeBytes);
            Console.WriteLine($"Barcode generated ({barcodeBytes.Length} bytes) and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a Code128 barcode image with the specified width, height, and measurement unit.
    /// </summary>
    /// <param name="width">The desired image width.</param>
    /// <param name="height">The desired image height.</param>
    /// <param name="unit">The measurement unit (Pixels, Millimeters, Inches, or Point).</param>
    /// <returns>Byte array containing the PNG image data.</returns>
    static byte[] GenerateBarcode(int width, int height, string unit)
    {
        if (string.IsNullOrWhiteSpace(unit))
            throw new ArgumentException("Unit must be provided.", nameof(unit));

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Apply the requested measurement unit to the image dimensions
            switch (unit.Trim().ToLowerInvariant())
            {
                case "pixels":
                    generator.Parameters.ImageWidth.Pixels = width;
                    generator.Parameters.ImageHeight.Pixels = height;
                    break;
                case "millimeters":
                    generator.Parameters.ImageWidth.Millimeters = width;
                    generator.Parameters.ImageHeight.Millimeters = height;
                    break;
                case "inches":
                    generator.Parameters.ImageWidth.Inches = width;
                    generator.Parameters.ImageHeight.Inches = height;
                    break;
                case "point":
                    generator.Parameters.ImageWidth.Point = width;
                    generator.Parameters.ImageHeight.Point = height;
                    break;
                default:
                    throw new ArgumentException($"Unsupported unit '{unit}'. Supported units: Pixels, Millimeters, Inches, Point.", nameof(unit));
            }

            // Save the barcode to a memory stream in PNG format and return the byte array
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}