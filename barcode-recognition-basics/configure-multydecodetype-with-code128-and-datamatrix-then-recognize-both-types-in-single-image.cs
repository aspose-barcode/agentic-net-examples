// Title: Multi-format barcode generation and recognition example
// Description: Demonstrates generating Code128 and DataMatrix barcodes, combining them into a single image, and recognizing both symbologies in one scan.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, Bitmap manipulation for image composition, and BarCodeReader with MultiDecodeType to detect multiple symbologies in a single image. Developers often need to process mixed barcode types in batch workflows, inventory systems, or document scanning pipelines.
// Prompt: Configure MultyDecodeType with Code128 and DataMatrix, then recognize both types in a single image.
// Tags: barcode, code128, datamatrix, multidecode, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating Code128 and DataMatrix barcodes, combining them, and recognizing both using MultiDecodeType.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcodes, merges them, and reads them back.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for individual and combined barcode images
        string code128Path = Path.Combine(tempDir, "code128.png");
        string dataMatrixPath = Path.Combine(tempDir, "datamatrix.png");
        string combinedPath = Path.Combine(tempDir, "combined.png");

        // Generate a Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "CODE128TEXT"))
        {
            generator.Save(code128Path, BarCodeImageFormat.Png);
        }

        // Generate a DataMatrix barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "DM123456"))
        {
            generator.Save(dataMatrixPath, BarCodeImageFormat.Png);
        }

        // Combine the two barcode images side by side into a single image
        using (var bmp1 = new Bitmap(code128Path))
        using (var bmp2 = new Bitmap(dataMatrixPath))
        {
            int width = bmp1.Width + bmp2.Width;
            int height = Math.Max(bmp1.Height, bmp2.Height);
            using (var combined = new Bitmap(width, height))
            {
                using (var graphics = Graphics.FromImage(combined))
                {
                    graphics.DrawImage(bmp1, 0, 0);                 // Draw Code128 on the left
                    graphics.DrawImage(bmp2, bmp1.Width, 0);       // Draw DataMatrix on the right
                }
                combined.Save(combinedPath, ImageFormat.Png);
            }
        }

        // Verify that the combined image was successfully created
        if (!File.Exists(combinedPath))
        {
            Console.WriteLine("Combined image was not created.");
            return;
        }

        // Configure MultiDecodeType to recognize both Code128 and DataMatrix symbologies
        var multiDecode = new MultiDecodeType(DecodeType.Code128, DecodeType.DataMatrix);

        // Read all barcodes from the combined image using the configured MultiDecodeType
        using (var reader = new BarCodeReader(combinedPath, multiDecode))
        {
            var results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}:{result.CodeText}");
            }
        }

        // Cleanup temporary files and directory (optional)
        try
        {
            File.Delete(code128Path);
            File.Delete(dataMatrixPath);
            File.Delete(combinedPath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}