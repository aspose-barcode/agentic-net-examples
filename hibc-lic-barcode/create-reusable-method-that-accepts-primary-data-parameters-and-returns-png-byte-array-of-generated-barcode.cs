using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode image using Aspose.BarCode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image and returns it as a PNG byte array.
    /// </summary>
    /// <param name="type">The barcode symbology (BaseEncodeType).</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>Byte array containing PNG image data.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="type"/> or <paramref name="codeText"/> is invalid.</exception>
    static byte[] GenerateBarcode(BaseEncodeType type, string codeText)
    {
        // Validate input parameters
        if (type == null)
            throw new ArgumentException("Barcode type must be provided.", nameof(type));
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("Code text must be provided.", nameof(codeText));

        // Create a barcode generator with the specified type and text
        using (var generator = new BarcodeGenerator(type, codeText))
        {
            // Set a simple parameter (optional) – 300 DPI resolution
            generator.Parameters.Resolution = 300f;

            // Use a memory stream to hold the generated image
            using (var ms = new MemoryStream())
            {
                // Save the barcode to the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);
                // Return the image bytes from the memory stream
                return ms.ToArray();
            }
        }
    }

    /// <summary>
    /// Entry point of the program. Generates a sample Code128 barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define barcode type and text to encode
        BaseEncodeType barcodeType = EncodeTypes.Code128;
        string text = "123ABC";

        // Generate the barcode image as a PNG byte array
        byte[] pngBytes = GenerateBarcode(barcodeType, text);

        // Output information about the generated image
        Console.WriteLine($"Generated PNG byte array length: {pngBytes.Length}");

        // Write the image to a file for verification
        File.WriteAllBytes("sample_barcode.png", pngBytes);
        Console.WriteLine("Barcode image saved as 'sample_barcode.png'.");
    }
}