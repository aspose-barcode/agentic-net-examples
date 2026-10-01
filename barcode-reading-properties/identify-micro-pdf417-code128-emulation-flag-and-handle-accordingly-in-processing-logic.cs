// Title: MicroPdf417 Barcode Generation with Code128 Emulation and Detection
// Description: Demonstrates how to generate a MicroPdf417 barcode with Code128 emulation enabled, save it as an image, and then read the barcode to verify the emulation flag.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and related parameter classes to work with MicroPdf417 symbology, including advanced features like Code128 emulation. Developers often need to create and validate MicroPdf417 barcodes for compact data encoding in logistics and inventory systems.
// Prompt: Identify Micro PDF417 Code128 emulation flag and handle accordingly in processing logic.
// Tags: barcode, micropdf417, code128, emulation, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating and reading a MicroPdf417 barcode with Code128 emulation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it, reads it back, and checks the emulation flag.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "MicroPdf417Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "micropdf417.png");

        // Generate a MicroPdf417 barcode with Code128 emulation enabled
        using (var generator = new BarcodeGenerator(EncodeTypes.MicroPdf417, "123456789012345678"))
        {
            generator.Parameters.Barcode.Pdf417.IsCode128Emulation = true;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Ensure the barcode image was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the barcode and inspect the IsCode128Emulation flag
        using (var reader = new BarCodeReader(barcodePath, DecodeType.MicroPdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                bool isEmulation = result.Extended.Pdf417.IsCode128Emulation;
                Console.WriteLine($"Detected MicroPdf417 barcode. Code128 Emulation: {isEmulation}");
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored: cleanup failures should not affect demo execution
        }
    }
}