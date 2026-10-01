// Title: Generate a Code128 barcode and save as TIFF using async FileStream
// Description: Demonstrates creating a barcode with Aspose.BarCode, writing it to a TIFF file via an asynchronous FileStream using async/await.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator and BarCodeImageFormat to produce barcode images. Typical use cases include generating shipping labels, inventory tags, or QR codes for mobile apps. Developers often need to save barcodes to various image formats asynchronously for web or cloud services.
// Prompt: Use BarcodeGenerator.Save to write a TIFF image to a FileStream with async/await pattern.
// Tags: barcode generation, code128, tiff, async, filestream, aspose.barcode

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode and saving it as a TIFF image using an asynchronous file stream.
/// </summary>
class Program
{
    /// <summary>
    /// Asynchronously creates a barcode image and writes it to a temporary TIFF file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    static async Task Main(string[] args)
    {
        // Define the barcode text and the output file location.
        string codeText = "1234567890";
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.tiff");

        // Initialize the barcode generator for Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Optional: configure additional barcode parameters here.
            // e.g., generator.Parameters.Barcode.XDimension.Point = 2f;

            // Open a FileStream configured for asynchronous operations.
            using (var fileStream = new FileStream(
                outputPath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 4096,
                useAsync: true))
            {
                // The Save method is synchronous; wrap it in Task.Run to avoid blocking the async flow.
                await Task.Run(() => generator.Save(fileStream, BarCodeImageFormat.Tiff));
            }
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}