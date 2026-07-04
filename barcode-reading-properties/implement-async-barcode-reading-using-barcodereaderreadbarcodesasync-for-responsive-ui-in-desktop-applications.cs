// Title: Async barcode reading demo using Aspose.BarCode
// Description: Demonstrates generating a barcode image in memory and reading it asynchronously to keep UI responsive.
// Prompt: Implement async barcode reading using BarCodeReader.ReadBarCodesAsync for responsive UI in desktop applications.
// Tags: barcode symbology, async operation, console output, aspose.barcode, barcodereader

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing; // Required for ImageFormat enum

/// <summary>
/// Sample console application that shows how to generate a barcode,
/// then read it asynchronously to avoid blocking the UI thread in a desktop scenario.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image in memory, then reads it asynchronously.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Create a barcode generator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Store the generated barcode image in a memory stream.
            using (var ms = new MemoryStream())
            {
                // Save the barcode as PNG to the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Initialize the barcode reader with the image stream,
                // requesting detection of all supported barcode types.
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    // Perform the synchronous read on a background thread to keep UI responsive.
                    BarCodeResult[] results = await Task.Run(() => reader.ReadBarCodes());

                    // Output each detected barcode's type and text.
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }

                    // Inform the user if no barcodes were found.
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcodes detected.");
                    }
                }
            }
        }
    }
}