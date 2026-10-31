// Title: Disable multithreaded barcode reading in Aspose.BarCode
// Description: Demonstrates how to generate a Code128 barcode, save it as PNG, and read it using single‑threaded settings by configuring ProcessorSettings.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and ProcessorSettings for controlling multithreading. Developers often need to limit CPU usage during bulk or resource‑constrained scans, making this pattern useful for performance tuning and CI environments.
// Prompt: Disable multithreaded barcode reading by setting ProcessorSettings.UseAllCores false and UseOnlyThisCoresCount to 1.
// Tags: code128, single-threaded, png, barcodegenerator, barcodereader, processorsettings

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates a Code128 barcode, saves it as a PNG image,
/// and reads it back using single‑threaded processing settings.
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
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Parameters.Barcode.BarColor = Color.Black;   // Set barcode foreground color
            generator.Parameters.BackColor = Color.White;         // Set image background color
            generator.Save(barcodePath, BarCodeImageFormat.Png);   // Save image to file
        }

        // Configure ProcessorSettings to disable multithreaded barcode reading
        BarCodeReader.ProcessorSettings.UseAllCores = false;          // Do not use all CPU cores
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;    // Restrict to a single core
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0; // No extra threads

        // Read the barcode using the single‑threaded settings
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            var results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}