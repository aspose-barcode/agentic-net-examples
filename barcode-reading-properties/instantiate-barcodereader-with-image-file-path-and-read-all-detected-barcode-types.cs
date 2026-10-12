// Title: Read All Detected Barcodes from an Image Using BarCodeReader
// Description: Demonstrates how to generate a barcode image, then instantiate BarCodeReader to detect and list all barcode types present in the image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a barcode image and BarCodeReader to scan the image for any supported symbologies. Developers commonly use these APIs to automate barcode creation, batch‑process scanned documents, or integrate barcode scanning into .NET applications.
// Prompt: Instantiate BarCodeReader with an image file path and read all detected barcode types.
// Tags: barcode symbology, barcode generation, barcode recognition, read, aspose.barcode, csharp, console

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a barcode image, reads all barcodes from it,
/// and outputs the detected symbology names and values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // Generate a sample Code128 barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Barcode image file not found.");
            return;
        }

        // Instantiate BarCodeReader with the image path and read all detected barcodes
        using (var reader = new BarCodeReader(imagePath))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the detection results
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures are non‑critical for this demo
        }
    }
}