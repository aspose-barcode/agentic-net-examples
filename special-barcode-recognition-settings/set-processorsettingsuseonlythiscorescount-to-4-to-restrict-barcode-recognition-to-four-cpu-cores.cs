// Title: Restrict barcode recognition to a specific number of CPU cores
// Description: Demonstrates how to generate a Code128 barcode, save it as PNG, and limit the barcode recognition process to use only four CPU cores.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows usage of BarcodeGenerator for creating barcodes and BarCodeReader with ProcessorSettings to control multi‑core processing. Developers often need to tune performance or resource usage when recognizing barcodes in high‑throughput or limited‑resource environments; this snippet illustrates the key classes and typical configuration steps.
// Prompt: Set ProcessorSettings.UseOnlyThisCoresCount to 4 to restrict barcode recognition to four CPU cores.
// Tags: barcode generation, barcode recognition, code128, multithreading, processor settings, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that creates a Code128 barcode image, configures the recognition engine
/// to use a limited number of CPU cores, reads the barcode back, and cleans up temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary folder for the sample barcode image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // --------------------------------------------------------------------
        // Generate a simple Code128 barcode and save it as PNG
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Parameters.Barcode.BarColor = Color.Black;   // Set barcode foreground color
            generator.Parameters.BackColor = Color.White;          // Set image background color
            generator.Save(imagePath, BarCodeImageFormat.Png);     // Save the image to the temporary path
        }

        // --------------------------------------------------------------------
        // Restrict barcode recognition to use only 4 CPU cores
        // --------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 4;

        // --------------------------------------------------------------------
        // Read the barcode from the generated image
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            var results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and directories
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);

            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}