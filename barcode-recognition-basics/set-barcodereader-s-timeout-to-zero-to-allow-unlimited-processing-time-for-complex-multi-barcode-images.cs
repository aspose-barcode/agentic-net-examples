// Title: Unlimited barcode reading timeout example
// Description: Demonstrates setting BarCodeReader.Timeout to zero for unlimited processing time when reading complex multi‑barcode images.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to configure the BarCodeReader for extended processing. It uses key classes such as BarCodeReader, BarCodeResult, and BarcodeGenerator. Typical use cases include scanning images with many or dense barcodes where default timeouts may abort recognition. Developers often need to adjust the timeout to ensure full detection without premature termination.
/// Prompt: Set BarCodeReader's TimeOut to zero to allow unlimited processing time for complex multi‑barcode images.
/// Tags: barcode, timeout, barcodereader, recognition, aspose.barcode, code128, image, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates setting BarCodeReader.Timeout to zero for unlimited processing time when reading barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it with unlimited timeout, and outputs results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode image and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode with unlimited timeout (Timeout = 0)
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.Timeout = 0; // zero means unlimited processing time

            try
            {
                // Perform barcode recognition
                BarCodeResult[] results = reader.ReadBarCodes();
                Console.WriteLine($"Barcodes found: {results.Length}");

                // Output each recognized barcode's type and text
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
            catch (RecognitionAbortedException ex)
            {
                // Handle cases where recognition is aborted (e.g., due to internal errors)
                Console.WriteLine($"Recognition aborted: {ex.Message}");
            }
        }

        // Cleanup temporary files and folder (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}