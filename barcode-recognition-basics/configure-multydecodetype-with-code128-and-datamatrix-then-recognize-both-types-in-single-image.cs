// Title: Multi-Decode Barcode Recognition for Code128 and DataMatrix in a Single Image
// Description: Generates Code128 and DataMatrix barcodes, merges them into one PNG image, and uses MultiDecodeType to recognize both symbologies in a single scan.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to create multiple barcode types, combine them into a composite image, and decode them simultaneously. It highlights key API classes such as BarcodeGenerator, MultiDecodeType, and BarCodeReader, which developers commonly use for batch scanning, inventory management, and mixed-symbology processing scenarios.
// Prompt: Configure MultyDecodeType with Code128 and DataMatrix, then recognize both types in a single image.
// Tags: code128, datamatrix, multidecode, barcode generation, barcode recognition, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating Code128 and DataMatrix barcodes, combining them into one image,
/// and recognizing both types using MultiDecodeType.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, image composition, and multi-symbology recognition.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for individual and combined barcode images
        string code128Path = Path.Combine(tempFolder, "code128.png");
        string dataMatrixPath = Path.Combine(tempFolder, "datamatrix.png");
        string combinedPath = Path.Combine(tempFolder, "combined.png");

        // Generate a Code128 barcode and save as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "CODE128TEXT"))
        {
            generator.Save(code128Path, BarCodeImageFormat.Png);
        }

        // Generate a DataMatrix barcode and save as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "DATAMATRIXTEXT"))
        {
            generator.Save(dataMatrixPath, BarCodeImageFormat.Png);
        }

        // Load the generated barcode images and combine them side by side
        using (var bmpCode128 = new Bitmap(code128Path))
        using (var bmpDataMatrix = new Bitmap(dataMatrixPath))
        {
            int combinedWidth = bmpCode128.Width + bmpDataMatrix.Width;
            int combinedHeight = Math.Max(bmpCode128.Height, bmpDataMatrix.Height);

            using (var combinedBitmap = new Bitmap(combinedWidth, combinedHeight))
            {
                using (var graphics = Graphics.FromImage(combinedBitmap))
                {
                    // Fill background with white
                    graphics.Clear(Color.White);
                    // Draw Code128 on the left
                    graphics.DrawImage(bmpCode128, 0, 0, bmpCode128.Width, bmpCode128.Height);
                    // Draw DataMatrix on the right
                    graphics.DrawImage(bmpDataMatrix, bmpCode128.Width, 0, bmpDataMatrix.Width, bmpDataMatrix.Height);
                }

                // Save the combined image
                combinedBitmap.Save(combinedPath, ImageFormat.Png);
            }
        }

        // Verify that the combined image was created successfully
        if (!File.Exists(combinedPath))
        {
            Console.WriteLine("Combined image was not created.");
            return;
        }

        // Configure MultiDecodeType to recognize both Code128 and DataMatrix symbologies
        var multiDecode = new MultiDecodeType(DecodeType.Code128, DecodeType.DataMatrix);

        // Read and decode barcodes from the combined image
        using (var reader = new BarCodeReader(combinedPath, multiDecode))
        {
            var results = reader.ReadBarCodes();
            Console.WriteLine("Recognition results:");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Cleanup temporary files (optional)
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}