// Title: Generate and Read a Code128 Barcode with Proper Resource Disposal
// Description: This example generates a Code128 barcode image, saves it to a temporary file, then reads it back using BarCodeReader, demonstrating correct disposal of unmanaged resources.
// Category-Description: This sample belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Developers often need to generate barcodes for labeling, inventory, or ticketing systems and then validate or extract the encoded data. The example highlights proper resource management with using blocks, a common requirement when working with unmanaged resources in barcode processing.
// Prompt: Dispose BarCodeReader instance properly within a using block to release unmanaged resources.
// Tags: code128, barcode generation, barcode recognition, resource disposal, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it to a temporary file,
/// reading it back, and properly disposing of the BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a barcode, reads it, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the output path and the text to encode
        string barcodePath = Path.Combine(tempFolder, "sample.png");
        string codeText = "1234567890";

        // Generate a Code128 barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode using BarCodeReader inside a using block to ensure proper disposal
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }

        // Clean up temporary files and folder; ignore any errors during cleanup
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect demo execution
        }
    }
}