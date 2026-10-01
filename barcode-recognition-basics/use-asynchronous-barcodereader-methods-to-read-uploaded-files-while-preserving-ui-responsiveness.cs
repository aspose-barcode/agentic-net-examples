// Title: Asynchronous Barcode Reading with Aspose.BarCode
// Description: Demonstrates how to generate barcode images and read them asynchronously using Aspose.BarCode to keep the UI responsive.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing asynchronous reading of multiple barcode images. It uses BarcodeGenerator for encoding and BarCodeReader for decoding, illustrating typical scenarios where developers need to process uploaded files without blocking the UI thread, such as in desktop or web applications.
// Prompt: Use asynchronous BarCodeReader methods to read uploaded files while preserving UI responsiveness.
// Tags: barcode, asynchronous, reading, aspnet, aspose.barcode, qr, code128, aztec, ui responsiveness

using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates several barcode images and reads them asynchronously.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, reads them asynchronously,
    /// and cleans up temporary files.
    /// </summary>
    static async Task Main()
    {
        // Create a temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images and collect their file paths
        var generatedFiles = new List<string>();
        generatedFiles.Add(GenerateBarcode(tempFolder, "QR", EncodeTypes.QR, "Hello World"));
        generatedFiles.Add(GenerateBarcode(tempFolder, "Code128", EncodeTypes.Code128, "1234567890"));
        generatedFiles.Add(GenerateBarcode(tempFolder, "Aztec", EncodeTypes.Aztec, "AztecSample"));

        // Asynchronously read all barcodes using background tasks
        var readTasks = new List<Task>();
        foreach (string file in generatedFiles)
        {
            readTasks.Add(ReadBarcodeAsync(file));
        }

        // Wait for all read operations to complete
        await Task.WhenAll(readTasks);

        // Clean up temporary files and folder
        foreach (string file in generatedFiles)
        {
            try { File.Delete(file); } catch { /* ignore */ }
        }
        try { Directory.Delete(tempFolder); } catch { /* ignore */ }
    }

    // Generates a barcode image and returns the file path
    private static string GenerateBarcode(string folder, string name, BaseEncodeType encodeType, string codeText)
    {
        string filePath = Path.Combine(folder, $"{name}.png");
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Save as PNG
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
        return filePath;
    }

    // Asynchronously reads a barcode image and prints results
    private static async Task ReadBarcodeAsync(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform the synchronous read on a background thread to keep UI responsive
            BarCodeResult[] results = await Task.Run(() => reader.ReadBarCodes());

            if (results.Length == 0)
            {
                Console.WriteLine($"No barcode detected in {Path.GetFileName(imagePath)}");
                return;
            }

            foreach (var result in results)
            {
                Console.WriteLine($"File: {Path.GetFileName(imagePath)}");
                Console.WriteLine($"  Code Text : {result.CodeText}");
                Console.WriteLine($"  Code Type : {result.CodeTypeName}");
                Console.WriteLine($"  Quality   : {result.ReadingQuality}");
            }
        }
    }
}