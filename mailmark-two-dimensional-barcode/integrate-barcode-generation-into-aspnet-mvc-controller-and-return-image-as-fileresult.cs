using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation using Aspose.BarCode in a console application.
/// This logic can be reused in an ASP.NET MVC controller to return the image as a FileResult.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// Generates a Code128 barcode, converts it to a Base64 string, and writes it to the console.
    /// </summary>
    static void Main()
    {
        // Generate a Code128 barcode and obtain the image bytes.
        byte[] barcodeBytes = GenerateBarcodeBytes(EncodeTypes.Code128, "1234567890");

        // Convert the image bytes to a Base64 string for easy display.
        string base64 = Convert.ToBase64String(barcodeBytes);
        Console.WriteLine("Barcode PNG (Base64):");
        Console.WriteLine(base64);
    }

    /// <summary>
    /// Generates a barcode image in PNG format and returns it as a byte array.
    /// </summary>
    /// <param name="encodeType">The type of barcode to generate (e.g., Code128).</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>Byte array containing the PNG image of the generated barcode.</returns>
    static byte[] GenerateBarcodeBytes(BaseEncodeType encodeType, string codeText)
    {
        // Initialize the barcode generator with the specified type and text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Enable checksum calculation for the barcode (if applicable).
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Use a memory stream to hold the generated image.
            using (var ms = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
                // Return the image data as a byte array.
                return ms.ToArray();
            }
        }
    }
}