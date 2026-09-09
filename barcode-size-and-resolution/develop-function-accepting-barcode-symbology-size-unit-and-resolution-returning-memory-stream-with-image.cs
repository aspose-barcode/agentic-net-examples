// Title: Generate Barcode Image with Custom Symbology, Size Unit, and Resolution
// Description: Demonstrates how to create a barcode using Aspose.BarCode by specifying the symbology, X-dimension size unit, and image resolution, returning the result as a PNG memory stream.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of EncodeTypes, BarcodeGenerator, and related parameter settings. Developers often need to produce barcodes with precise dimensions and DPI for printing or digital display, and this snippet illustrates typical API calls for such scenarios.
// Prompt: Develop function accepting barcode symbology, size unit, and resolution, returning memory stream with image.
// Tags: barcode, symbology, size unit, resolution, memorystream, aspose.barcode, generation, png

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with customizable parameters using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a sample barcode and saves it to a file.
    /// </summary>
    static void Main()
    {
        // Define sample input parameters
        string symbology = "Code128";
        string sizeUnit = "Pixels";
        float unitValue = 3f;
        float resolution = 300f;

        // Generate the barcode image as a memory stream
        MemoryStream barcodeStream = GenerateBarcode(symbology, sizeUnit, unitValue, resolution);

        // Determine output file path
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Write the memory stream to a physical PNG file
        using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            barcodeStream.CopyTo(file);
        }

        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }

    /// <summary>
    /// Generates a barcode image based on the specified symbology, size unit, unit value, and resolution.
    /// </summary>
    /// <param name="symbologyName">Name of the barcode symbology (e.g., "Code128").</param>
    /// <param name="sizeUnit">Unit for the X-dimension (Pixels, Millimeters, Points, Inches).</param>
    /// <param name="unitValue">Numeric value for the chosen size unit.</param>
    /// <param name="resolution">Image resolution in DPI.</param>
    /// <returns>A <see cref="MemoryStream"/> containing the generated PNG barcode image.</returns>
    static MemoryStream GenerateBarcode(string symbologyName, string sizeUnit, float unitValue, float resolution)
    {
        // Validate input arguments
        if (string.IsNullOrWhiteSpace(symbologyName))
            throw new ArgumentException("Symbology name must be provided.", nameof(symbologyName));

        if (string.IsNullOrWhiteSpace(sizeUnit))
            throw new ArgumentException("Size unit must be provided.", nameof(sizeUnit));

        // Resolve symbology name to the corresponding EncodeTypes enum value via reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
            throw new ArgumentException($"Unknown symbology: {symbologyName}", nameof(symbologyName));

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Create a BarcodeGenerator with sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, "Sample"))
        {
            // Apply the X-dimension based on the provided size unit
            switch (sizeUnit.Trim().ToLowerInvariant())
            {
                case "pixels":
                    generator.Parameters.Barcode.XDimension.Pixels = unitValue;
                    break;
                case "millimeters":
                    generator.Parameters.Barcode.XDimension.Millimeters = unitValue;
                    break;
                case "points":
                    generator.Parameters.Barcode.XDimension.Point = unitValue;
                    break;
                case "inches":
                    generator.Parameters.Barcode.XDimension.Inches = unitValue;
                    break;
                default:
                    throw new ArgumentException($"Unsupported size unit: {sizeUnit}", nameof(sizeUnit));
            }

            // Set the image resolution (dots per inch)
            generator.Parameters.Resolution = resolution;

            // Save the barcode to a memory stream in PNG format
            MemoryStream ms = new MemoryStream();
            generator.Save(ms, BarCodeImageFormat.Png);
            ms.Position = 0; // Reset stream position for reading
            return ms;
        }
    }
}