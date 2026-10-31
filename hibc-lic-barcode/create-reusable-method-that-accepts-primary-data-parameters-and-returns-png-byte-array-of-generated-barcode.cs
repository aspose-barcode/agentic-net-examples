// Title: Generate Barcode PNG as Byte Array
// Description: Demonstrates creating a barcode image using Aspose.BarCode and returning it as a PNG byte array.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use EncodeTypes, BarcodeGenerator, and BarCodeImageFormat to produce barcode images. Typical use cases include generating barcodes for labels, receipts, or inventory systems where a PNG representation is required. Developers often need reusable methods that accept data and symbology parameters and output image bytes for further processing or storage.
// Prompt: Create a reusable method that accepts primary data parameters and returns a PNG byte array of generated barcode.
// Tags: barcode, symbology, png, byte-array, aspose.barcode, generation

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode image and saves it to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image in PNG format and returns the image bytes.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="symbologyName">The name of the barcode symbology (e.g., "Code128").</param>
    /// <returns>Byte array containing the PNG image.</returns>
    static byte[] GenerateBarcode(string codeText, string symbologyName)
    {
        // Resolve the EncodeTypes field that matches the provided symbology name.
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
            throw new ArgumentException($"Unknown symbology: {symbologyName}");

        // Cast the field value to BaseEncodeType for use with BarcodeGenerator.
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Create a BarcodeGenerator with the selected symbology and data.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set the X-dimension (module width) to 4 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Return the stream contents as a byte array.
                return ms.ToArray();
            }
        }
    }

    /// <summary>
    /// Entry point of the program. Generates a barcode, writes it to a file, and outputs status information.
    /// </summary>
    static void Main()
    {
        try
        {
            // Generate a Code128 barcode for the sample data.
            byte[] pngBytes = GenerateBarcode("12345678", "Code128");
            Console.WriteLine($"Generated barcode PNG bytes: {pngBytes.Length}");

            // Determine the output file path in the current working directory.
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

            // Write the PNG byte array to the file system.
            File.WriteAllBytes(outputPath, pngBytes);
            Console.WriteLine($"Saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation or file I/O.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}