// Title: Disable Multithreaded Barcode Reading with ProcessorSettings
// Description: Demonstrates how to generate a Code128 barcode, save it as PNG, and configure Aspose.BarCode to use a single CPU core for barcode recognition.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with ProcessorSettings to control multithreading during decoding. Developers often need to limit CPU usage in environments with constrained resources or when deterministic performance is required.
// Prompt: Disable multithreaded barcode reading by setting ProcessorSettings.UseAllCores false and UseOnlyThisCoresCount to 1.
// Tags: code128, generation, recognition, png, barcodegenerator, barcodereader, processorsettings, multithreading

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Code128 barcode, saves it as a PNG file,
/// and reads it back using single‑core processing settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path where the generated barcode image will be stored
        string imagePath = "sample.png";

        // ------------------------------------------------------------
        // Generate a simple Code128 barcode and save it as PNG
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Save the barcode image to the specified file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Configure the barcode reader to use only one CPU core
        // ------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;

        // Verify that the barcode image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Barcode image not found at path: {Path.GetFullPath(imagePath)}");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode from the image using the configured settings
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
            }
        }
    }
}