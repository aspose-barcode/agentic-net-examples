// Title: Load PNG into Bitmap and Recognize Barcodes with BarCodeReader
// Description: Demonstrates loading a PNG image into an Aspose.Drawing.Bitmap and using BarCodeReader to detect all supported barcode types.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases key API classes such as BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and BarCodeResult for accessing decoded data. Typical use cases include scanning barcode images from files or streams, extracting product codes, and integrating barcode processing into .NET applications. Developers often need quick samples that cover image handling, temporary file management, and cleanup.
// Prompt: Load a PNG image into a Bitmap object and recognize barcodes via BarCodeReader constructor.
// Tags: barcode, png, bitmap, recognition, generation, aspose.barcode, decode, encode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates a barcode image, loads it into a Bitmap, and reads barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary barcode PNG, loads it as a Bitmap, and extracts barcode information.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a sample barcode image if it does not exist
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Save the generated barcode as a PNG file
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // Load the PNG image into a Bitmap object
        using (Aspose.Drawing.Bitmap bitmap = new Aspose.Drawing.Bitmap(imagePath))
        {
            // The bitmap is now loaded; it can be used for further processing if needed
        }

        // Recognize barcodes from the image using BarCodeReader
        if (File.Exists(imagePath))
        {
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Read all detected barcodes
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected.");
                }
                else
                {
                    // Output details for each detected barcode
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"Code Text: {result.CodeText}");
                        Console.WriteLine($"Symbology: {result.CodeTypeName}");
                        Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                        Console.WriteLine($"Region Angle: {result.Region.Angle}");
                        Console.WriteLine();
                    }
                }
            }
        }
        else
        {
            Console.WriteLine($"Image file not found: {imagePath}");
        }

        // Clean up temporary files
        try
        {
            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}