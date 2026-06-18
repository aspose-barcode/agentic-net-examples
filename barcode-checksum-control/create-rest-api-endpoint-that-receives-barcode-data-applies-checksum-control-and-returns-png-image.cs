using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code39FullASCII barcode with checksum enabled,
/// saving it to a PNG file, and outputting a Base64 representation.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image as a PNG byte array.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>Byte array containing the PNG image.</returns>
    static byte[] GenerateBarcodeImage(string codeText)
    {
        // Choose Code39FullASCII which supports optional checksum.
        BaseEncodeType encodeType = EncodeTypes.Code39FullASCII;

        // Create a barcode generator with the specified type and text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Enable checksum calculation and ensure the checksum digit is displayed.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Return the PNG data as a byte array.
                return ms.ToArray();
            }
        }
    }

    /// <summary>
    /// Entry point of the application. Generates a barcode, writes it to disk,
    /// and prints a Base64-encoded representation to the console.
    /// </summary>
    static void Main()
    {
        // Sample input that would typically come from a REST request.
        string inputCodeText = "ABC123";

        // Generate the barcode image bytes.
        byte[] pngBytes = GenerateBarcodeImage(inputCodeText);

        // Write the image to a file for verification.
        string outputPath = "barcode.png";
        File.WriteAllBytes(outputPath, pngBytes);
        Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(outputPath)}");

        // Simulate an HTTP response by outputting a Base64-encoded PNG.
        string base64Image = Convert.ToBase64String(pngBytes);
        Console.WriteLine("Base64 PNG response:");
        Console.WriteLine(base64Image);
    }
}