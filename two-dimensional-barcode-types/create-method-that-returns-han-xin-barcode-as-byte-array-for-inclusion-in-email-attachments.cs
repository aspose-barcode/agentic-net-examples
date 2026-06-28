using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Han Xin barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Han Xin barcode and writes it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Generate barcode bytes for the provided text.
        byte[] barcodeBytes = GenerateHanXinBarcode("1234567890ABCDEFG");

        // Write the PNG image bytes to a file named "HanXin.png".
        File.WriteAllBytes("HanXin.png", barcodeBytes);

        // Output the size of the generated barcode image.
        Console.WriteLine("Han Xin barcode generated (size: {0} bytes).", barcodeBytes.Length);
    }

    /// <summary>
    /// Generates a Han Xin barcode image and returns it as a byte array (PNG format).
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>Byte array containing the PNG image.</returns>
    static byte[] GenerateHanXinBarcode(string codeText)
    {
        // Validate input.
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("Code text must be a non-empty string.", nameof(codeText));

        // Use a memory stream to capture the generated image.
        using (var ms = new MemoryStream())
        {
            // Create a barcode generator for Han Xin symbology with the specified text.
            using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
            {
                // Configure error correction level.
                generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

                // Set encoding mode to automatic.
                generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.Auto;

                // Let the library choose the appropriate version.
                generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;

                // Save the barcode as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Return the PNG image as a byte array.
            return ms.ToArray();
        }
    }
}