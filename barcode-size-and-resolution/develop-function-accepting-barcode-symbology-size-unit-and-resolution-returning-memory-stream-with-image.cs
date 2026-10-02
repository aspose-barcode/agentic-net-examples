// Title: Generate Barcode Image to MemoryStream
// Description: Demonstrates creating a barcode of a specified symbology, size unit, and resolution, and returning it as a PNG image in a MemoryStream.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and XDimension classes to produce barcode images. Typical use cases include dynamic barcode creation for invoices, shipping labels, or inventory systems where developers need to control image resolution and measurement units. The pattern is common for generating images on-the-fly and streaming them to web responses or further processing pipelines.
// Prompt: Develop function accepting barcode symbology, size unit, and resolution, returning memory stream with image.
// Tags: barcode, symbology, generation, png, memorystream, aspose.barcode, xdimension, resolution

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation using Aspose.BarCode and returning the image as a MemoryStream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a sample barcode, saves it to a file, and writes status to console.
    /// </summary>
    static void Main()
    {
        // Define sample input parameters
        string symbology = "Code128";
        string unit = "Pixels";
        float resolution = 300f;

        // Generate the barcode image as a memory stream
        MemoryStream barcodeStream = GenerateBarcode(symbology, unit, resolution);

        // Persist the generated image to a file for verification
        using (var file = new FileStream("barcode.png", FileMode.Create, FileAccess.Write))
        {
            barcodeStream.Position = 0;
            barcodeStream.CopyTo(file);
        }

        // Output information about the generated barcode
        Console.WriteLine($"Barcode generated with symbology '{symbology}', unit '{unit}', resolution {resolution} DPI.");
        Console.WriteLine($"Output file: {Path.GetFullPath("barcode.png")}");
    }

    /// <summary>
    /// Generates a barcode image based on the specified symbology, measurement unit, and resolution.
    /// </summary>
    /// <param name="symbologyName">Name of the barcode symbology (e.g., "Code128").</param>
    /// <param name="unitName">Measurement unit for XDimension ("Pixels", "Millimeters", "Points", or "Inches").</param>
    /// <param name="resolution">Image resolution in DPI.</param>
    /// <returns>A MemoryStream containing the barcode image in PNG format.</returns>
    static MemoryStream GenerateBarcode(string symbologyName, string unitName, float resolution)
    {
        // Resolve the symbology name to the corresponding BaseEncodeType using reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
            throw new ArgumentException($"Unknown symbology: {symbologyName}");

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Initialize the barcode generator with sample text
        var generator = new BarcodeGenerator(encodeType, "Sample");

        // Configure XDimension based on the requested measurement unit
        switch (unitName)
        {
            case "Pixels":
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                break;
            case "Millimeters":
                generator.Parameters.Barcode.XDimension.Millimeters = 2f;
                break;
            case "Points":
                generator.Parameters.Barcode.XDimension.Point = 2f;
                break;
            case "Inches":
                generator.Parameters.Barcode.XDimension.Inches = 0.05f;
                break;
            default:
                generator.Dispose();
                throw new ArgumentException($"Unsupported unit: {unitName}");
        }

        // Set the image resolution (DPI)
        generator.Parameters.Resolution = resolution;

        // Save the barcode to a memory stream in PNG format
        var ms = new MemoryStream();
        generator.Save(ms, BarCodeImageFormat.Png);
        generator.Dispose();

        // Reset stream position for downstream consumption
        ms.Position = 0;
        return ms;
    }
}