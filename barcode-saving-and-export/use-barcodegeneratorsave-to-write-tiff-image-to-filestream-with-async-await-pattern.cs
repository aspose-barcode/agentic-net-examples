// Title: Asynchronous TIFF barcode generation using Aspose.BarCode
// Description: Demonstrates how to generate a Code128 barcode and save it as a TIFF image asynchronously to a FileStream.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include creating barcode images for documents, labels, or web services where non‑blocking I/O is required. Developers often need to write barcode images to streams efficiently, especially in async workflows.
// Prompt: Use BarcodeGenerator.Save to write a TIFF image to a FileStream with async/await pattern.
// Tags: barcode, code128, tiff, async, await, filestream, aspose.barcode, generation

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates asynchronous generation and saving of a Code128 barcode as a TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and writes it to a temporary TIFF file using async/await.
    /// </summary>
    static async Task Main(string[] args)
    {
        // Define the output directory inside the system temporary folder.
        string outputDirectory = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");

        // Ensure the directory exists.
        Directory.CreateDirectory(outputDirectory);

        // Build the full path for the output TIFF file.
        string outputPath = Path.Combine(outputDirectory, "barcode.tiff");

        // Create an asynchronous FileStream for writing the image.
        using (FileStream fileStream = new FileStream(
            outputPath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            bufferSize: 4096,
            useAsync: true))
        {
            // Initialize the barcode generator with Code128 symbology and the data to encode.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
            {
                // Execute the synchronous Save method on a background thread to avoid blocking the async flow.
                await Task.Run(() => generator.Save(fileStream, BarCodeImageFormat.Tiff));
            }
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}