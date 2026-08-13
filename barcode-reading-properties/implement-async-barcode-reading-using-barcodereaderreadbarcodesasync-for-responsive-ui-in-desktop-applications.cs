// Title: Asynchronous barcode reading example
// Description: Demonstrates how to read barcodes asynchronously using BarCodeReader.ReadBarCodesAsync to keep UI responsive.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing asynchronous operations with BarCodeReader and BarcodeGenerator. Developers often need to process images without blocking the UI thread, especially in desktop applications, and this pattern illustrates using Task.Run to off‑load the synchronous ReadBarCodes call while preserving async flow.
// Prompt: Implement async barcode reading using BarCodeReader.ReadBarCodesAsync for responsive UI in desktop applications.
// Tags: barcode symbology, async, read, code128, aspose.barcode, desktop ui

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates asynchronous barcode reading using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode if missing and reads it asynchronously.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        const string imagePath = "barcode.png";

        // Generate a sample barcode image if it does not exist.
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // Asynchronously read barcodes from the image.
        await ReadBarcodesAsync(imagePath);
    }

    /// <summary>
    /// Reads barcodes from the specified image file on a background thread and returns the detected texts.
    /// </summary>
    /// <param name="imagePath">Path to the image containing barcodes.</param>
    private static async Task ReadBarcodesAsync(string imagePath)
    {
        // Run the blocking read operation on a background thread.
        List<string> detectedTexts = await Task.Run(() =>
        {
            var texts = new List<string>();
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Iterate through all detected barcodes.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Collect non‑empty barcode texts.
                    if (!string.IsNullOrEmpty(result.CodeText))
                    {
                        texts.Add(result.CodeText);
                    }
                }
            }
            return texts;
        });

        // Output the results to the console.
        foreach (var text in detectedTexts)
        {
            Console.WriteLine($"Detected barcode text: {text}");
        }
    }
}