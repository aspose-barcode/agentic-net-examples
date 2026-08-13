// Title: Asynchronous Barcode Decoding with TPL
// Description: Demonstrates generating sample barcode images and decoding them asynchronously using the Task Parallel Library to improve throughput for large image collections.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to use BarCodeReader, BarcodeGenerator, and related classes for batch processing. Typical use cases include high‑volume scanning, automated inventory, and document processing where many images must be decoded efficiently. Developers often need to configure processor settings and run recognition in parallel to maximize performance.
// Prompt: Implement asynchronous barcode decoding for large image collections using Task Parallel Library.
// Tags: barcode, decoding, asynchronous, task parallel library, aspose.barcode, image processing, batch, recognition

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides methods to generate sample barcode images and decode them asynchronously.
/// </summary>
class Program
{
    // Generates a set of sample barcode images in the specified folder.
    private static void GenerateSampleBarcodes(string folderPath)
    {
        // Ensure the output folder exists.
        Directory.CreateDirectory(folderPath);

        // Sample data: each tuple contains the symbology and the text to encode.
        var samples = new (BaseEncodeType encodeType, string text)[]
        {
            (EncodeTypes.Code128, "Sample123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DM12345"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text"),
            (EncodeTypes.Aztec, "AztecCode")
        };

        int index = 0;
        foreach (var (encodeType, text) in samples)
        {
            string filePath = Path.Combine(folderPath, $"barcode_{index}.png");
            using (var generator = new BarcodeGenerator(encodeType, text))
            {
                // Save as PNG.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            index++;
        }
    }

    // Asynchronously decodes a single barcode image and returns the first detected code text.
    private static Task<string> DecodeBarcodeAsync(string imagePath)
    {
        return Task.Run(() =>
        {
            using (var reader = new BarCodeReader())
            {
                // Use all supported symbologies.
                reader.BarCodeReadType = DecodeType.AllSupportedTypes;
                // Assign the image file.
                reader.SetBarCodeImage(imagePath);
                // Perform recognition.
                var results = reader.ReadBarCodes();
                if (results != null && results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                {
                    return results[0].CodeText;
                }
                return null;
            }
        });
    }

    /// <summary>
    /// Entry point of the program. Generates sample barcodes (if needed), then decodes all PNG images in the folder asynchronously.
    /// </summary>
    static async Task Main(string[] args)
    {
        // Folder to hold sample barcode images.
        string barcodeFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Generate sample images if the folder is empty.
        if (!Directory.Exists(barcodeFolder) || Directory.GetFiles(barcodeFolder, "*.png").Length == 0)
        {
            GenerateSampleBarcodes(barcodeFolder);
            Console.WriteLine($"Generated sample barcodes in '{barcodeFolder}'.");
        }

        // Get all PNG files in the folder.
        string[] imageFiles = Directory.GetFiles(barcodeFolder, "*.png");
        if (imageFiles.Length == 0)
        {
            Console.WriteLine("No barcode images found to decode.");
            return;
        }

        // Configure the reader to use all available processor cores.
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Create a decoding task for each image.
        var decodeTasks = new Task<string>[imageFiles.Length];
        for (int i = 0; i < imageFiles.Length; i++)
        {
            decodeTasks[i] = DecodeBarcodeAsync(imageFiles[i]);
        }

        // Await all decoding operations.
        string[] decodedTexts = await Task.WhenAll(decodeTasks);

        // Output the results.
        Console.WriteLine("Decoding results:");
        for (int i = 0; i < imageFiles.Length; i++)
        {
            string fileName = Path.GetFileName(imageFiles[i]);
            string codeText = decodedTexts[i] ?? "(no code detected)";
            Console.WriteLine($"{fileName}: {codeText}");
        }
    }
}