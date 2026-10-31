// Title: Generate Han Xin barcode as byte array
// Description: Creates a Han Xin 2D barcode from a text string and returns the PNG image as a byte array, ideal for embedding in email attachments.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to use the BarcodeGenerator class with EncodeTypes.HanXin. It shows setting error correction level and version, saving the barcode to a memory stream, and retrieving the image bytes. Developers working with 2D barcodes, especially Han Xin, often need to produce barcode images for reports, emails, or other non‑file outputs.
// Prompt: Create method that returns Han Xin barcode as byte array for inclusion in email attachments.
// Tags: hanxin, barcode, generation, png, bytearray, aspose.barcode, email

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Han Xin barcode and obtaining its PNG representation as a byte array.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, displays its size, and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode.
        string sampleText = "Sample123";

        // Generate the barcode and retrieve the image as a byte array.
        byte[] barcodeData = GenerateHanXinBarcode(sampleText);
        Console.WriteLine($"Generated Han Xin barcode byte array length: {barcodeData.Length}");

        // Save the byte array to a PNG file for verification.
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "HanXin.png");
        File.WriteAllBytes(outputFile, barcodeData);
        Console.WriteLine($"Barcode saved to {outputFile}");
    }

    /// <summary>
    /// Generates a Han Xin barcode for the specified text and returns the PNG image as a byte array.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>Byte array containing the PNG representation of the barcode.</returns>
    static byte[] GenerateHanXinBarcode(string codeText)
    {
        // Initialize the barcode generator with Han Xin symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Configure error correction level and let the version be chosen automatically.
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;
            generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;

            // Save the barcode image to a memory stream in PNG format.
            using (var stream = new MemoryStream())
            {
                generator.Save(stream, BarCodeImageFormat.Png);
                // Return the image data as a byte array.
                return stream.ToArray();
            }
        }
    }
}