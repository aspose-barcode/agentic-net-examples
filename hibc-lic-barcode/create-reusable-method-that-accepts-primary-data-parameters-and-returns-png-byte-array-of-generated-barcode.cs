// Title: Generate Barcode PNG as Byte Array
// Description: Demonstrates how to generate a barcode image using Aspose.BarCode and return it as a PNG byte array.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of the BarcodeGenerator class together with BaseEncodeType and BarCodeImageFormat to create barcode images. Typical scenarios include creating barcodes for labels, tickets, or inventory systems where the image needs to be transmitted or stored as a byte array. Developers often need reusable methods that accept encoding parameters and produce image data without writing to disk.
// Prompt: Create a reusable method that accepts primary data parameters and returns a PNG byte array of generated barcode.
// Tags: barcode, generation, png, byte-array, aspose.barcode, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation using Aspose.BarCode and returns the image as a PNG byte array.
/// </summary>
class Program
{
    // Generates a barcode image and returns it as a PNG byte array.
    // Parameters:
    //   encodeType - the barcode symbology (e.g., EncodeTypes.Code128)
    //   codeText   - the text to encode
    // Returns: PNG image bytes
    static byte[] GenerateBarcode(BaseEncodeType encodeType, string codeText)
    {
        // Validate input to avoid generating an empty barcode.
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("codeText cannot be null or empty.", nameof(codeText));

        // Create a generator instance with the specified symbology and data.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Optional: customize appearance here, e.g. colors, dimensions, etc.
            // generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            // generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Return the raw PNG bytes.
                return ms.ToArray();
            }
        }
    }

    /// <summary>
    /// Entry point that shows example usage of GenerateBarcode.
    /// </summary>
    static void Main()
    {
        // Example usage: generate a Code128 barcode.
        byte[] pngBytes = GenerateBarcode(EncodeTypes.Code128, "123ABC");

        // Output some info to verify execution.
        Console.WriteLine($"Generated PNG byte array length: {pngBytes.Length}");

        // Optionally, write the image to a file for visual verification.
        // File.WriteAllBytes("barcode.png", pngBytes);
    }
}