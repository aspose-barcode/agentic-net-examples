// Title: Asynchronous barcode reading demo
// Description: Demonstrates using Aspose.BarCode's asynchronous pattern to read a barcode image without blocking the UI thread.
// Prompt: Use asynchronous BarCodeReader methods to read uploaded files while preserving UI responsiveness.
// Tags: barcode symbology, asynchronous operation, console output, aspose.barcode, barcodereader

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample console application that reads a barcode image asynchronously.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode if needed and reads it asynchronously.
    /// </summary>
    static async Task Main(string[] args)
    {
        // Path to the barcode image file
        const string imagePath = "sample.png";

        // Ensure the barcode image exists; generate a sample if missing
        if (!File.Exists(imagePath))
        {
            // Create a simple Code128 barcode and save it
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "AsyncDemo"))
            {
                generator.Save(imagePath);
            }
        }

        // Asynchronously read barcodes from the image to keep the UI thread responsive
        BarCodeResult[] results = await Task.Run(() =>
        {
            // Initialize the reader for all supported symbologies
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Perform the synchronous read operation (wrapped in Task.Run for asynchrony)
                return reader.ReadBarCodes();
            }
        });

        // Process and display the detection results
        foreach (var result in results)
        {
            Console.WriteLine($"Detected Type: {result.CodeTypeName}");
            Console.WriteLine($"Decoded Text: {result.CodeText}");
            Console.WriteLine($"Confidence: {result.Confidence}");
            Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
            Console.WriteLine();
        }
    }
}