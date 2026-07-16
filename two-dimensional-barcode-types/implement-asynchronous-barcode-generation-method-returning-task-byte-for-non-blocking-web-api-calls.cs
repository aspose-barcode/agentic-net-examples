// Title: Asynchronous Barcode Generation Example
// Description: Demonstrates generating a barcode image asynchronously and returning its byte array, suitable for non‑blocking web API scenarios.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, BaseEncodeType, and BarCodeImageFormat to create barcode images. Typical use cases include web services that need to produce barcodes on‑the‑fly without blocking threads. Developers often require async patterns to improve scalability and responsiveness.
// Prompt: Implement asynchronous barcode generation method returning Task<byte[]> for non‑blocking web API calls.
// Tags: code128, generation, png, aspose.barcode, barcodegenerator, async

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides an example of asynchronous barcode generation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Asynchronously generates a barcode image and returns its byte array.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the PNG image bytes.</returns>
    static async Task<byte[]> GenerateBarcodeAsync(string codeText, BaseEncodeType encodeType)
    {
        // Offload the generation to a background thread to avoid blocking the calling thread.
        return await Task.Run(() =>
        {
            // Initialize the barcode generator with the specified symbology and data.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Write the generated barcode to a memory stream in PNG format.
                using (var memoryStream = new MemoryStream())
                {
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                    // Return the image bytes from the memory stream.
                    return memoryStream.ToArray();
                }
            }
        });
    }

    /// <summary>
    /// Entry point that demonstrates generating a barcode image asynchronously and saving it to disk.
    /// </summary>
    static async Task Main()
    {
        // Sample data to encode in the barcode.
        string sampleText = "123ABC";
        BaseEncodeType sampleType = EncodeTypes.Code128;

        // Generate the barcode image bytes asynchronously.
        byte[] barcodeBytes = await GenerateBarcodeAsync(sampleText, sampleType);

        // Output the size of the generated image.
        Console.WriteLine($"Generated barcode image size: {barcodeBytes.Length} bytes");

        // Save the image to a file for verification.
        string outputPath = "barcode.png";
        using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            fileStream.Write(barcodeBytes, 0, barcodeBytes.Length);
        }

        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}