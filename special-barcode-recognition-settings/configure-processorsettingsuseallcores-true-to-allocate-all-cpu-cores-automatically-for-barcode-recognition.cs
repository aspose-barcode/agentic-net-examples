// Title: Barcode Recognition Using All CPU Cores
// Description: Demonstrates how to enable multi‑core processing for barcode recognition with Aspose.BarCode and reads a generated Code128 barcode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader and its ProcessorSettings to leverage all available CPU cores for faster decoding. Typical use cases include high‑throughput scanning applications where performance is critical. Developers often need to configure ProcessorSettings, select DecodeType, and retrieve barcode metadata such as type, text, and region.
// Prompt: Configure ProcessorSettings.UseAllCores true to allocate all CPU cores automatically for barcode recognition.
// Tags: barcode, recognition, multithreading, useallcores, code128, aspnet, aspnetcore, aspose.barcode, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates configuring Aspose.BarCode to use all CPU cores for barcode recognition and reading a Code128 barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a sample barcode if missing, enables multi‑core processing, and reads the barcode information.
    /// </summary>
    static void Main()
    {
        // Enable utilization of all CPU cores for barcode recognition
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        // Path to the barcode image file
        string imagePath = "barcode.png";

        // Generate a sample Code128 barcode image if it does not already exist
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // Initialize the reader to decode all supported barcode types from the image
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through all detected barcodes and output their details
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Barcode Text: {result.CodeText}");

                var bounds = result.Region.Rectangle;
                Console.WriteLine($"Region - X:{bounds.X}, Y:{bounds.Y}, Width:{bounds.Width}, Height:{bounds.Height}");
            }
        }
    }
}