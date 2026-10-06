// Title: Restrict Barcode Recognition to Four CPU Cores
// Description: Demonstrates how to limit Aspose.BarCode barcode recognition to a specific number of CPU cores while generating and reading a Code128 barcode image.
// Category-Description: This example belongs to the barcode generation and recognition category of Aspose.BarCode. It showcases the use of BarcodeGenerator for creating a barcode image and BarCodeReader with ProcessorSettings to control recognition performance. Developers often need to balance speed and resource usage when processing large batches of images, making core count restriction a common requirement.
// Prompt: Set ProcessorSettings.UseOnlyThisCoresCount to 4 to restrict barcode recognition to four CPU cores.
// Tags: code128, barcode recognition, png, barcodereader, barcodegenerator, processorsettings

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode, restricts the recognition
/// process to four CPU cores, reads the barcode, and cleans up temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Restrict barcode recognition to use only 4 CPU cores
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 4;

        // Prepare a temporary directory to store the generated barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "sample.png");

        // Generate a Code128 barcode and save it as a PNG file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode using the configured processor settings
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            Stopwatch watch = Stopwatch.StartNew(); // Start timing the recognition
            var results = reader.ReadBarCodes();    // Perform barcode recognition
            watch.Stop();                           // Stop timing

            Console.WriteLine($"Barcodes read: {results.Length}, Recognition time: {watch.ElapsedMilliseconds} ms");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}