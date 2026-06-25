using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a barcode image using Aspose.BarCode and returning it as a MemoryStream.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image and returns it as a MemoryStream.
    /// The caller is responsible for disposing the returned stream.
    /// </summary>
    /// <param name="encodeType">The type of barcode to generate.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>A MemoryStream containing the PNG image of the generated barcode.</returns>
    static MemoryStream GenerateBarcodeStream(BaseEncodeType encodeType, string codeText)
    {
        // Create a MemoryStream to hold the image data.
        var memoryStream = new MemoryStream();

        // Use BarcodeGenerator inside a using block to ensure proper disposal.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Save the barcode image directly into the MemoryStream in PNG format.
            generator.Save(memoryStream, BarCodeImageFormat.Png);
        }

        // Reset the stream position so it can be read from the beginning.
        memoryStream.Position = 0;
        return memoryStream;
    }

    /// <summary>
    /// Entry point of the application. Generates a sample Code128 barcode and outputs its size and Base64 representation.
    /// </summary>
    static void Main()
    {
        // Generate a Code128 barcode with the text "123ABC".
        using (var barcodeStream = GenerateBarcodeStream(EncodeTypes.Code128, "123ABC"))
        {
            // Output the length of the generated image stream.
            Console.WriteLine($"Generated barcode stream length: {barcodeStream.Length} bytes");

            // Convert the image to a Base64 string for display or API payloads.
            byte[] imageBytes = barcodeStream.ToArray();
            string base64 = Convert.ToBase64String(imageBytes);
            Console.WriteLine("Base64 PNG:");
            Console.WriteLine(base64);
        }
    }
}