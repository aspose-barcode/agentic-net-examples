// Title: Generate barcode image and save to file using Aspose.BarCode
// Description: Demonstrates creating a barcode of a specified symbology, size unit, and resolution, then returning it as a MemoryStream.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and image format settings. Developers commonly need to generate barcodes dynamically for PDFs, labels, or web responses, adjusting dimensions and resolution to meet printing or display requirements.
// Prompt: Develop function accepting barcode symbology, size unit, and resolution, returning memory stream with image.
// Tags: barcode, generation, code128, pixel, resolution, memorystream, aspose.barcode, png

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, saves it to a temporary PNG file, and writes details to console.
    /// </summary>
    static void Main()
    {
        try
        {
            // Generate barcode with specified symbology, size unit, and resolution
            using (MemoryStream barcodeStream = GenerateBarcode("Code128", "Pixel", 300f))
            {
                // Define output path in the system's temporary folder
                string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

                // Write the memory stream contents to a file
                using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    barcodeStream.CopyTo(file);
                }

                // Inform the user where the file was saved and its size
                Console.WriteLine($"Barcode image saved to: {outputPath}");
                Console.WriteLine($"Stream length: {barcodeStream.Length} bytes");
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation or file operations
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a barcode image based on the provided symbology, size unit, and resolution.
    /// </summary>
    /// <param name="symbologyName">Name of the barcode symbology (e.g., "Code128").</param>
    /// <param name="sizeUnit">Unit for X-dimension size ("Pixel", "Millimeter", "Point", "Inch").</param>
    /// <param name="resolution">Image resolution in DPI.</param>
    /// <returns>A MemoryStream containing the generated PNG barcode image.</returns>
    static MemoryStream GenerateBarcode(string symbologyName, string sizeUnit, float resolution)
    {
        // Retrieve the EncodeTypes field that matches the symbology name
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
            throw new ArgumentException($"Unknown symbology: {symbologyName}");

        // Cast the field value to BaseEncodeType
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
        var memoryStream = new MemoryStream();

        // Initialize the barcode generator with the selected symbology and sample text
        using (var generator = new BarcodeGenerator(encodeType, "Sample"))
        {
            // Set X-dimension based on the requested size unit
            switch (sizeUnit.ToLowerInvariant())
            {
                case "pixel":
                case "pixels":
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;
                    break;
                case "millimeter":
                case "millimeters":
                    generator.Parameters.Barcode.XDimension.Millimeters = 2f;
                    break;
                case "point":
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    break;
                case "inch":
                case "inches":
                    generator.Parameters.Barcode.XDimension.Inches = 0.1f;
                    break;
                default:
                    throw new ArgumentException($"Unsupported size unit: {sizeUnit}");
            }

            // Apply the desired image resolution
            generator.Parameters.Resolution = resolution;

            // Save the barcode image to the memory stream in PNG format
            generator.Save(memoryStream, BarCodeImageFormat.Png);
        }

        // Reset stream position for reading by the caller
        memoryStream.Position = 0;
        return memoryStream;
    }
}