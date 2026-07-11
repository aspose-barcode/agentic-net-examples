// Title: Disable multithreaded barcode reading example
// Description: Demonstrates how to turn off multi‑core processing for barcode recognition using Aspose.BarCode, ensuring single‑threaded execution.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of ProcessorSettings to control threading. It shows how to configure UseAllCores and UseOnlyThisCoresCount for the BarCodeReader class, a common requirement when integrating barcode scanning into environments with limited resources or when deterministic performance is needed. Developers often need to adjust these settings to match their application’s concurrency model.
// Prompt: Disable multithreaded barcode reading by setting ProcessorSettings.UseAllCores false and UseOnlyThisCoresCount to 1.
// Tags: barcode, multithreading, processor settings, code128, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates disabling multithreaded barcode reading using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample Code128 barcode if missing, configures single‑threaded processing, and reads the barcode.
    /// </summary>
    static void Main()
    {
        // Define the path for the sample barcode image.
        string imagePath = "sample_barcode.png";

        // Generate a sample barcode if it does not already exist on disk.
        if (!File.Exists(imagePath))
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Save the generated barcode as a PNG file.
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // Configure the processor to use a single core (disable multithreading).
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;

        // Initialize the reader with the image and specify the expected barcode type.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Iterate through all detected barcodes and output their details.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
            }
        }
    }
}