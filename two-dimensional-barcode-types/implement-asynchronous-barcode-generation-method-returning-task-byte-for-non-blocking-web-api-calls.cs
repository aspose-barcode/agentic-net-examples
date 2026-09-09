// Title: Asynchronous Barcode Generation to Byte Array
// Description: Demonstrates generating a barcode image asynchronously and returning the image bytes, useful for non‑blocking web API endpoints.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcode images on demand. Typical scenarios include web services that need to produce barcodes without blocking threads, returning PNG byte arrays directly to clients. Developers often need fast, thread‑safe barcode creation for various symbologies in high‑throughput applications.
// Prompt: Implement asynchronous barcode generation method returning Task<byte[]> for non‑blocking web API calls.
// Tags: barcode, symbology, generation, async, bytearray, aspose.barcode, png

using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an example of generating barcode images asynchronously using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image asynchronously and returns the image as a byte array.
    /// </summary>
    /// <param name="symbologyName">The name of the barcode symbology (e.g., "Code128").</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>A task that resolves to a PNG byte array representing the generated barcode.</returns>
    static async Task<byte[]> GenerateBarcodeAsync(string symbologyName, string codeText)
    {
        // Use reflection to map the symbology name to the corresponding EncodeTypes field.
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            // Symbology not found – log and return an empty array.
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return Array.Empty<byte>();
        }

        // Retrieve the actual EncodeTypes value.
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Run the barcode generation on a background thread to avoid blocking the caller.
        return await Task.Run(() =>
        {
            // Create a generator for the specified symbology and text.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Write the generated image to a memory stream.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // Return the image bytes.
                    return ms.ToArray();
                }
            }
        });
    }

    /// <summary>
    /// Entry point of the program. Parses arguments, generates a barcode, and saves it to a temporary file.
    /// </summary>
    /// <param name="args">Command‑line arguments: optional symbology name and text.</param>
    static async Task Main(string[] args)
    {
        // Determine the symbology and text to encode, using defaults if not provided.
        string symbology = args.Length > 0 ? args[0] : "Code128";
        string text = args.Length > 1 ? args[1] : "12345678";

        // Generate the barcode bytes asynchronously.
        byte[] barcodeBytes = await GenerateBarcodeAsync(symbology, text);
        Console.WriteLine($"Generated barcode byte array length: {barcodeBytes.Length}");

        // Save the resulting PNG to a temporary location for verification.
        string outputPath = Path.Combine(Path.GetTempPath(), "generated_barcode.png");
        File.WriteAllBytes(outputPath, barcodeBytes);
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}