// Title: Generate a barcode image and return PNG byte array
// Description: Demonstrates creating a barcode using Aspose.BarCode, returning the image as a PNG byte array, and saving it to disk.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes to produce barcode images. It covers key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, typical for developers needing to embed barcodes in applications, generate them on the fly, or export them for further processing.
// Prompt: Create a reusable method that accepts primary data parameters and returns a PNG byte array of generated barcode.
// Tags: barcode, code128, png, byte array, aspose.barcode, generation, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode image, returns it as a PNG byte array,
/// and saves the image to a file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, displays information,
    /// and writes the PNG image to disk.
    /// </summary>
    static void Main()
    {
        try
        {
            // Generate a PNG byte array for a Code128 barcode with the specified text.
            byte[] pngBytes = GenerateBarcode("Code128", "12345678");
            Console.WriteLine($"Generated PNG byte array length: {pngBytes.Length}");

            // Determine the output file path in the current directory.
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

            // Write the PNG byte array to the file system.
            File.WriteAllBytes(outputPath, pngBytes);
            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during barcode generation or file I/O.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a barcode image using the specified symbology and code text,
    /// and returns the image as a PNG byte array.
    /// </summary>
    /// <param name="symbologyName">The name of the barcode symbology (e.g., "Code128").</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>Byte array containing the PNG representation of the generated barcode.</returns>
    public static byte[] GenerateBarcode(string symbologyName, string codeText)
    {
        // Use reflection to map the symbology name to the corresponding EncodeTypes field.
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
            throw new ArgumentException($"Unknown symbology: {symbologyName}");

        // Retrieve the EncodeTypes value for the requested symbology.
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Initialize the barcode generator with the selected symbology and data.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Example setting: adjust the module (X) dimension in pixels.
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
}