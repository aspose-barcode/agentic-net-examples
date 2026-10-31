// Title: Asynchronous Barcode Generation to Byte Array
// Description: Demonstrates generating a barcode image asynchronously and returning it as a byte array, suitable for non‑blocking web API scenarios.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class together with EncodeTypes to create barcode images in PNG format. Typical use cases include web services that need to produce barcode images on demand without blocking threads. Developers often need asynchronous methods that return byte[] for further processing or direct response streaming.
// Prompt: Implement asynchronous barcode generation method returning Task<byte[]> for non‑blocking web API calls.
// Tags: barcode, async, task, bytearray, png, code128, aspnet, aspose.barcode, generation

using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example of generating a barcode image asynchronously and saving it to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application. Generates a barcode asynchronously and writes the result to a PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Define sample input: symbology type and the text to encode.
        string symbology = "Code128";
        string text = "12345678";

        try
        {
            // Asynchronously generate the barcode image as a byte array.
            byte[] barcodeBytes = await GenerateBarcodeAsync(symbology, text);

            // Determine the output file path in the current working directory.
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

            // Write the byte array to a PNG file using an asynchronous file stream.
            using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                await fileStream.WriteAsync(barcodeBytes, 0, barcodeBytes.Length);
            }

            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation or file I/O.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a barcode image asynchronously based on the specified symbology name and text.
    /// </summary>
    /// <param name="symbologyName">The name of the barcode symbology (e.g., "Code128").</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the PNG image bytes.</returns>
    public static Task<byte[]> GenerateBarcodeAsync(string symbologyName, string codeText)
    {
        // Run the CPU‑bound barcode generation on a background thread to avoid blocking the caller.
        return Task.Run(() =>
        {
            // Resolve the symbology name to the corresponding BaseEncodeType enum value via reflection.
            FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
                throw new ArgumentException($"Unknown symbology: {symbologyName}");

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Create a BarcodeGenerator with the resolved type and the provided text.
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Save the generated barcode to a memory stream in PNG format.
                using (MemoryStream ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // Return the image bytes.
                    return ms.ToArray();
                }
            }
        });
    }
}