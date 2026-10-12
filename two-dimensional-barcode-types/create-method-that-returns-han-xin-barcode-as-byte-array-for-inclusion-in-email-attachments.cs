// Title: Generate Han Xin Barcode as Byte Array
// Description: Demonstrates creating a Han Xin 2‑D barcode and returning it as a PNG byte array suitable for email attachments.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.HanXin. It covers configuring Han Xin specific parameters such as Unicode encoding mode and error correction level, then saving the barcode to a memory stream. Developers working with 2‑D barcodes for document automation, email embedding, or web services will find this pattern useful.
// Prompt: Create method that returns Han Xin barcode as byte array for inclusion in email attachments.
// Tags: hanxin, barcode, generation, png, bytearray, aspose.barcode, email

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides an example of generating a Han Xin barcode and obtaining its PNG representation as a byte array.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Han Xin barcode, displays its size, and optionally saves it to a temporary file.
    /// </summary>
    static void Main()
    {
        // Text to encode in the barcode
        string sampleText = "https://example.com";

        // Generate the barcode and receive the PNG data as a byte array
        byte[] barcodeBytes = GenerateHanXinBarcode(sampleText);
        Console.WriteLine($"Generated Han Xin barcode byte array length: {barcodeBytes.Length}");

        // Optional: write the byte array to a file for visual verification
        string outputPath = Path.Combine(Path.GetTempPath(), "HanXinBarcode.png");
        using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            fileStream.Write(barcodeBytes, 0, barcodeBytes.Length);
        }
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }

    /// <summary>
    /// Generates a Han Xin barcode for the specified text and returns the image as a PNG byte array.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>Byte array containing the PNG representation of the generated barcode.</returns>
    static byte[] GenerateHanXinBarcode(string codeText)
    {
        // Initialize the barcode generator with Han Xin symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Configure Unicode encoding mode and medium error correction level
            generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.Unicode;
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

            // Save the barcode image to a memory stream in PNG format
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                return memoryStream.ToArray();
            }
        }
    }
}