// Title: Restrict barcode recognition to a specific number of CPU cores
// Description: Demonstrates how to limit Aspose.BarCode barcode recognition to exactly four CPU cores using ProcessorSettings.
// Category-Description: This example belongs to the Aspose.BarCode performance tuning category. It showcases the use of BarCodeReader and its ProcessorSettings to control multithreading during barcode recognition. Developers often need to balance CPU usage and recognition speed, especially in server or CI environments, and this pattern provides a straightforward way to cap core utilization.
// Prompt: Set ProcessorSettings.UseOnlyThisCoresCount to 4 to restrict barcode recognition to four CPU cores.
// Tags: barcode, recognition, performance, multithreading, aspose.barcode, code128, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a Code128 barcode, restricts recognition to four CPU cores,
/// reads the barcode, and cleans up temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Restrict barcode recognition to exactly 4 CPU cores
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 4;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;

        // Verify the image exists before attempting to read
        if (File.Exists(imagePath))
        {
            // Initialize the reader for Code128 symbology
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Perform recognition and iterate over results
                var results = reader.ReadBarCodes();
                foreach (var result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }
        else
        {
            Console.WriteLine("Barcode image not found: " + imagePath);
        }

        // Clean up temporary files and directories
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not crash the program
        }
    }
}