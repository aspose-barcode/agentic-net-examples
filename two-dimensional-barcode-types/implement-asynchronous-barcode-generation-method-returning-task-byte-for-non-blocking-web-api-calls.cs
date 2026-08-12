// Title: Asynchronous barcode generation returning image bytes
// Description: Demonstrates how to generate a barcode image asynchronously and obtain the PNG byte array, suitable for non‑blocking web API scenarios.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, BaseEncodeType, and related classes to create barcode images. Typical use cases include web services that need to produce barcodes on‑the‑fly without blocking threads. Developers often require async methods that return raw image data for further processing or transmission.
// Prompt: Implement asynchronous barcode generation method returning Task<byte[]> for non‑blocking web API calls.
// Tags: barcode, symbology, async, generation, png, aspose.barcode, bytearray, webapi

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Console application that generates a barcode image asynchronously and saves it to a temporary file.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image on a background thread and returns the PNG bytes.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>A task that resolves to a byte array containing the PNG image.</returns>
    static async Task<byte[]> GenerateBarcodeAsync(BaseEncodeType encodeType, string codeText)
    {
        // Run the generation on a background thread to avoid blocking the caller.
        return await Task.Run(() =>
        {
            // Create and configure the barcode generator.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Save the barcode to a memory stream in PNG format.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // Return the raw image bytes.
                    return ms.ToArray();
                }
            }
        });
    }

    /// <summary>
    /// Application entry point. Parses optional command‑line arguments, generates the barcode, and writes it to a temporary PNG file.
    /// </summary>
    /// <param name="args">Optional arguments: [0] symbology name, [1] text to encode.</param>
    static async Task Main(string[] args)
    {
        // Sample parameters – use defaults if no command‑line arguments are provided.
        string symbologyName = args.Length > 0 ? args[0] : "Code128";
        string text = args.Length > 1 ? args[1] : "Sample123";

        // Resolve the symbology name to a BaseEncodeType using reflection.
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Generate the barcode asynchronously.
        byte[] imageBytes = await GenerateBarcodeAsync(encodeType, text);

        // Write the result to a temporary PNG file.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");
        await File.WriteAllBytesAsync(outputPath, imageBytes);
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}