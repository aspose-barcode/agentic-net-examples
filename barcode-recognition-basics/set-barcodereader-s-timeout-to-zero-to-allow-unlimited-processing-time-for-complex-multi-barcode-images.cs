// Title: Unlimited barcode reading timeout example
// Description: Demonstrates setting BarCodeReader.Timeout to zero for unlimited processing time when reading complex multi‑barcode images.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating how to configure the BarCodeReader for unlimited timeout. It uses key classes such as BarCodeReader, DecodeType, and BarCodeResult to read barcodes from images. Typical use cases include processing high‑resolution or densely packed barcode images where default timeouts may be insufficient. Developers often need to adjust the timeout to ensure complete recognition without premature aborts.
// Prompt: Set BarCodeReader's TimeOut to zero to allow unlimited processing time for complex multi‑barcode images.
// Tags: barcode, timeout, unlimited, barcodereader, recognition, decode, aspnet, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates setting BarCodeReader.Timeout to zero for unlimited processing time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a sample barcode, reads it with unlimited timeout, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a sample Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Ensure the barcode image was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the barcode with unlimited timeout (Timeout = 0)
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            reader.Timeout = 0; // Zero means no time limit

            try
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
                        Console.WriteLine($"Code Text: {result.CodeText}");
                        Console.WriteLine($"Symbology: {result.CodeTypeName}");
                    }
                }
            }
            catch (RecognitionAbortedException ex)
            {
                // This block is required when Timeout is set, even though zero timeout should not abort
                Console.WriteLine($"Recognition aborted: {ex.Message}");
            }
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}