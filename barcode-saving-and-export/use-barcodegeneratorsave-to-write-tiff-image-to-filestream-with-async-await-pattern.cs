// Title: Save Barcode as TIFF using async FileStream
// Description: Demonstrates generating a Code128 barcode and saving it as a TIFF image using Aspose.BarCode's BarcodeGenerator.Save method with an asynchronous FileStream.
// Category-Description: This example belongs to the Aspose.BarCode generation and image export category. It showcases the use of BarcodeGenerator (from Aspose.BarCode.Generation) to create barcodes, and the BarCodeImageFormat enumeration to specify output formats. Typical use cases include creating barcode images for reports, labels, or web services where asynchronous file I/O improves scalability. Developers often need to generate barcodes on the fly and write them to streams without blocking threads.
// Prompt: Use BarcodeGenerator.Save to write a TIFF image to a FileStream with async/await pattern.
// Tags: barcode, code128, generation, async, tiff, filestream, aspose.barcode

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode and saves it as a TIFF file using asynchronous I/O.
/// </summary>
class Program
{
    /// <summary>
    /// Asynchronously creates a temporary directory, generates a barcode, and writes it to a TIFF file via an async FileStream.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Create a unique temporary output folder.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the TIFF file.
        string filePath = Path.Combine(outputDir, "barcode.tiff");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Open an asynchronous FileStream for writing the image.
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
            {
                // Perform the save operation on a background thread to avoid blocking the async context.
                await Task.Run(() => generator.Save(fs, BarCodeImageFormat.Tiff));
            }
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to: {filePath}");
    }
}