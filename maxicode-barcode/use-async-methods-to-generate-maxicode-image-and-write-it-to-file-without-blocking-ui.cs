// Title: Generate MaxiCode barcode image asynchronously
// Description: Demonstrates how to create a MaxiCode barcode using Aspose.BarCode and save it to a PNG file without blocking the UI thread.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode symbologies such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, and asynchronous file I/O to produce barcode images efficiently. Developers working with shipping, logistics, or inventory systems often need to generate MaxiCode symbols programmatically for labeling and tracking purposes.
// Prompt: Use async methods to generate a MaxiCode image and write it to a file without blocking the UI.
// Tags: maxicode, barcode, async, file-io, png, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates asynchronous generation of a MaxiCode barcode image and saving it to a file.
/// </summary>
class Program
{
    /// <summary>
    /// Asynchronous entry point that creates a MaxiCode barcode and writes it to a PNG file without blocking the UI thread.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    static async Task Main()
    {
        // Define the output file path for the generated PNG image.
        string outputPath = "maxicode.png";

        // Prepare MaxiCode data using Mode 2 (postal code, country code, service category).
        var maxiCodeData = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit postal code for Mode 2
            CountryCode = 56,           // Example country code
            ServiceCategory = 999       // Example service category
        };

        // Use a memory stream to hold the generated barcode image in memory.
        using (var memoryStream = new MemoryStream())
        {
            // Generate the barcode image on a background thread to avoid UI blocking.
            await Task.Run(() =>
            {
                using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
                {
                    // Save the barcode directly to the memory stream in PNG format.
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                }
            });

            // Reset the stream position to the beginning before reading its contents.
            memoryStream.Position = 0;

            // Asynchronously write the image bytes from the memory stream to the file system.
            await File.WriteAllBytesAsync(outputPath, memoryStream.ToArray());
        }

        // Inform the user where the image has been saved.
        Console.WriteLine($"MaxiCode image saved to '{Path.GetFullPath(outputPath)}'.");
    }
}