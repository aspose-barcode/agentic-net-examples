// Title: Generate barcode image with customizable symbology, size unit, and resolution
// Description: Demonstrates creating a barcode using Aspose.BarCode, allowing callers to specify the symbology, measurement unit, and DPI, and returns the image as a MemoryStream.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and image parameter settings (AutoSizeMode, ImageWidth/Height, Resolution) to produce PNG barcodes. Developers often need to dynamically create barcodes with specific dimensions and resolutions for printing or web display, and this snippet illustrates the typical workflow.
// Prompt: Develop function accepting barcode symbology, size unit, and resolution, returning memory stream with image.
// Tags: barcode, symbology, generation, png, memorystream, aspnet, aspnetcore, aspose.barcode, encode types, resolution, size unit

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program demonstrating barcode generation with customizable parameters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that calls <see cref="GenerateBarcode"/> and displays the resulting stream size.
    /// </summary>
    static void Main()
    {
        // Sample call to the barcode generation function
        using (MemoryStream stream = GenerateBarcode("Code128", "Point", 300f))
        {
            // Output the size of the generated PNG image
            Console.WriteLine($"Generated barcode image size: {stream.Length} bytes");
            // The stream contains the PNG image; in a real scenario you could write it to a file:
            // File.WriteAllBytes("barcode.png", stream.ToArray());
        }
    }

    /// <summary>
    /// Generates a barcode image and returns it as a <see cref="MemoryStream"/>.
    /// </summary>
    /// <param name="symbologyName">Name of the barcode symbology (e.g., "Code128").</param>
    /// <param name="sizeUnit">Unit for image dimensions: "Point", "Pixels", "Inches", or "Millimeters".</param>
    /// <param name="resolution">Resolution (dpi) for the generated image.</param>
    /// <returns>MemoryStream containing the PNG image.</returns>
    static MemoryStream GenerateBarcode(string symbologyName, string sizeUnit, float resolution)
    {
        // Validate symbology name input
        if (string.IsNullOrWhiteSpace(symbologyName))
            throw new ArgumentException("Symbology name must be provided.", nameof(symbologyName));

        // Resolve the symbology name to a BaseEncodeType using reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
            throw new ArgumentException($"Unknown symbology: {symbologyName}", nameof(symbologyName));

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Create the barcode generator with the resolved encode type
        using (var generator = new BarcodeGenerator(encodeType))
        {
            // Set sample codetext; adjust as needed for specific symbologies
            generator.CodeText = "Sample123";

            // Use interpolation mode to control image size via ImageWidth/ImageHeight
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define a constant dimension value (example size)
            const float dimensionValue = 200f;

            // Set image dimensions based on the requested unit
            switch (sizeUnit?.Trim().ToLowerInvariant())
            {
                case "point":
                    generator.Parameters.ImageWidth.Point = dimensionValue;
                    generator.Parameters.ImageHeight.Point = dimensionValue;
                    break;
                case "pixels":
                    generator.Parameters.ImageWidth.Pixels = dimensionValue;
                    generator.Parameters.ImageHeight.Pixels = dimensionValue;
                    break;
                case "inches":
                    generator.Parameters.ImageWidth.Inches = dimensionValue;
                    generator.Parameters.ImageHeight.Inches = dimensionValue;
                    break;
                case "millimeters":
                    generator.Parameters.ImageWidth.Millimeters = dimensionValue;
                    generator.Parameters.ImageHeight.Millimeters = dimensionValue;
                    break;
                default:
                    throw new ArgumentException($"Unsupported size unit: {sizeUnit}", nameof(sizeUnit));
            }

            // Apply the requested resolution (dpi)
            generator.Parameters.Resolution = resolution;

            // Save the barcode to a memory stream in PNG format
            var ms = new MemoryStream();
            generator.Save(ms, BarCodeImageFormat.Png);
            ms.Position = 0; // Reset stream position for reading
            return ms;
        }
    }
}