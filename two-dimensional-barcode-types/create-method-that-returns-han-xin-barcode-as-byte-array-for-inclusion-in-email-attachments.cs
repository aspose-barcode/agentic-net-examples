// Title: Generate Han Xin barcode as byte array
// Description: Creates a Han Xin barcode from text and returns the image as a PNG byte array, suitable for embedding in email attachments.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to use the BarcodeGenerator class with EncodeTypes.HanXin to produce barcodes. Typical use cases include creating barcode images for documents, reports, or email attachments where a byte array is required. Developers often need to customize encoding mode and error correction level while retrieving the image in memory.
// Prompt: Create method that returns Han Xin barcode as byte array for inclusion in email attachments.
// Tags: hanxin, barcode, generation, png, byte array, aspose.barcode, email attachment

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Han Xin barcode and saving it as a PNG file,
/// while also providing the barcode image as a byte array for further use (e.g., email attachments).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, writes it to a temporary file,
    /// and outputs the file location to the console.
    /// </summary>
    static void Main()
    {
        // Text to encode in the Han Xin barcode
        string sampleText = "Hello Han Xin 123";

        // Generate the barcode image and obtain it as a byte array
        byte[] barcodeBytes = GenerateHanXinBarcode(sampleText);

        // Define a temporary file path for demonstration purposes
        string outputPath = Path.Combine(Path.GetTempPath(), "hanxin.png");

        // Write the byte array to the file system as a PNG image
        File.WriteAllBytes(outputPath, barcodeBytes);

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Han Xin barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Generates a Han Xin barcode for the specified text and returns the image as a PNG byte array.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>Byte array containing the PNG representation of the barcode.</returns>
    static byte[] GenerateHanXinBarcode(string codeText)
    {
        // Initialize the barcode generator with Han Xin symbology and the provided text
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Optional: configure encoding mode and error correction level
            generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.Auto;
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Return the image data as a byte array
                return ms.ToArray();
            }
        }
    }
}