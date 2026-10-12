// Title: Barcode Generation and Recognition Using All CPU Cores
// Description: Demonstrates generating a Code128 barcode, saving it as PNG, and recognizing it while configuring the processor to utilize all CPU cores.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with ProcessorSettings for high‑performance recognition. Developers often need to process large volumes of images quickly; enabling UseAllCores leverages all available CPU threads to accelerate decoding.
// Prompt: Configure ProcessorSettings.UseAllCores true to allocate all CPU cores automatically for barcode recognition.
// Tags: barcode symbology, generation, recognition, multithreading, aspose.barcode, code128, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that creates a Code128 barcode image, reads it back,
/// and demonstrates how to enable multi‑core processing for barcode recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder to store the generated barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // --------------------------------------------------------------------
        // Generate a simple Code128 barcode and save it as a PNG file.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Enable the processor to use all available CPU cores for faster decoding.
        // --------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        // --------------------------------------------------------------------
        // Read and decode the generated barcode if the file exists.
        // --------------------------------------------------------------------
        if (File.Exists(barcodePath))
        {
            using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                var results = reader.ReadBarCodes();
                foreach (var result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }
        else
        {
            Console.WriteLine("Barcode image not found.");
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and directories.
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignoring any cleanup errors to avoid interrupting the flow.
        }
    }
}