// Title: Multi-Decode Barcode Recognition for Code128 and DataMatrix
// Description: Demonstrates generating Code128 and DataMatrix barcodes, combining them into a single image, and configuring MultiDecodeType to recognize both symbologies in one scan.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, Bitmap manipulation for image composition, and BarCodeReader with MultiDecodeType for detecting multiple barcode types in a single image. Developers often need to scan mixed symbology documents, and this pattern illustrates the typical workflow and key API classes (BarcodeGenerator, BarCodeReader, MultiDecodeType) required for such scenarios.
// Prompt: Configure MultyDecodeType with Code128 and DataMatrix, then recognize both types in a single image.
// Tags: barcode, multidecode, code128, datamatrix, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates Code128 and DataMatrix barcodes, merges them into one image,
/// and reads both types using MultiDecodeType.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that performs barcode generation, image composition, and multi‑type recognition.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeMultiDecode_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the individual barcodes and the combined image
        string code128Path = Path.Combine(tempFolder, "code128.png");
        string dataMatrixPath = Path.Combine(tempFolder, "datamatrix.png");
        string combinedPath = Path.Combine(tempFolder, "combined.png");

        // Generate a Code128 barcode and save it as PNG
        using (var generator128 = new BarcodeGenerator(EncodeTypes.Code128, "CODE128TEST"))
        {
            generator128.Save(code128Path, BarCodeImageFormat.Png);
        }

        // Generate a DataMatrix barcode and save it as PNG
        using (var generatorDM = new BarcodeGenerator(EncodeTypes.DataMatrix, "DATAMATRIXTEST"))
        {
            generatorDM.Save(dataMatrixPath, BarCodeImageFormat.Png);
        }

        // Combine the two barcode images side by side into a single bitmap
        using (var bmp1 = new Bitmap(code128Path))
        using (var bmp2 = new Bitmap(dataMatrixPath))
        {
            int combinedWidth = bmp1.Width + bmp2.Width;
            int combinedHeight = Math.Max(bmp1.Height, bmp2.Height);
            using (var combinedBmp = new Bitmap(combinedWidth, combinedHeight))
            {
                using (var graphics = Graphics.FromImage(combinedBmp))
                {
                    graphics.Clear(Aspose.Drawing.Color.White);
                    graphics.DrawImage(bmp1, 0, 0);
                    graphics.DrawImage(bmp2, bmp1.Width, 0);
                }
                combinedBmp.Save(combinedPath, ImageFormat.Png);
            }
        }

        // Ensure the combined image was created successfully before proceeding
        if (!File.Exists(combinedPath))
        {
            Console.WriteLine("Failed to create combined barcode image.");
            return;
        }

        // Configure MultiDecodeType to look for Code128 and DataMatrix symbologies
        var multiDecode = new MultiDecodeType(DecodeType.Code128, DecodeType.DataMatrix);

        // Read and display all recognized barcodes from the combined image
        using (var reader = new BarCodeReader(combinedPath, multiDecode))
        {
            Console.WriteLine("Recognized barcodes:");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Optional cleanup of temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any errors during cleanup to avoid interrupting the program flow
        }
    }
}