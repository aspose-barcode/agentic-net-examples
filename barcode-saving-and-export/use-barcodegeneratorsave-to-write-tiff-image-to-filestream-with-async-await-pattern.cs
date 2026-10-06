// Title: Save barcode as TIFF using async FileStream
// Description: Demonstrates generating a Code128 barcode and saving it as a TIFF image using Aspose.BarCode's BarcodeGenerator.Save method with async/await.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showing how to create barcodes and write them to a file stream asynchronously. It highlights the use of BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and the Save method, which developers commonly need when integrating barcode creation into web or desktop applications that require non‑blocking I/O.
// Prompt: Use BarcodeGenerator.Save to write a TIFF image to a FileStream with async/await pattern.
// Tags: barcode, code128, tiff, async, await, filestream, aspose.barcode, image-generation

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides the entry point for the barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode and saves it as a TIFF file asynchronously.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Define the temporary output file path for the TIFF image.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.tiff");

        // Create a FileStream for writing the barcode image.
        using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            // Initialize the BarcodeGenerator with Code128 symbology and the desired data.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
            {
                // Save the barcode image to the stream asynchronously using Task.Run to avoid blocking.
                await Task.Run(() => generator.Save(fileStream, BarCodeImageFormat.Tiff));
            }
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}