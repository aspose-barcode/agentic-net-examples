// Title: Recognize Multiple Barcode Types (Code128 & DataMatrix) in a Single Image
// Description: Demonstrates generating Code128 and DataMatrix barcodes, combining them into one image, and recognizing both types using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and DecodeType. Typical use cases include multi‑symbology scanning scenarios where a single image contains different barcode types that need to be detected in one pass.
// Prompt: Configure MultyDecodeType with Code128 and DataMatrix, then recognize both types in a single image.
// Tags: code128, datamatrix, multidecode, image-combination, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates Code128 and DataMatrix barcodes, merges them into a single image,
/// and reads both barcode types using a multi‑decode configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, image composition, and recognition steps.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for individual barcode images and the combined image
        string code128Path = Path.Combine(tempFolder, "code128.png");
        string dataMatrixPath = Path.Combine(tempFolder, "datamatrix.png");
        string combinedPath = Path.Combine(tempFolder, "combined.png");

        // Generate a Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123456"))
        {
            generator.Save(code128Path, BarCodeImageFormat.Png);
        }

        // Generate a DataMatrix barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "DM123456"))
        {
            generator.Save(dataMatrixPath, BarCodeImageFormat.Png);
        }

        // Verify that both barcode images were successfully created
        if (!File.Exists(code128Path) || !File.Exists(dataMatrixPath))
        {
            Console.WriteLine("Failed to generate one or more barcode images.");
            return;
        }

        // Load the two barcode images and combine them side by side into a single bitmap
        using (var bmpCode128 = new Bitmap(code128Path))
        using (var bmpDataMatrix = new Bitmap(dataMatrixPath))
        {
            int combinedWidth = bmpCode128.Width + bmpDataMatrix.Width;
            int combinedHeight = Math.Max(bmpCode128.Height, bmpDataMatrix.Height);

            using (var combinedBitmap = new Bitmap(combinedWidth, combinedHeight))
            using (var graphics = Graphics.FromImage(combinedBitmap))
            {
                // Fill background with white to avoid transparency issues
                graphics.Clear(Color.White);
                // Draw the Code128 image on the left
                graphics.DrawImage(bmpCode128, 0, 0);
                // Draw the DataMatrix image on the right
                graphics.DrawImage(bmpDataMatrix, bmpCode128.Width, 0);
                // Save the combined image as PNG
                combinedBitmap.Save(combinedPath, ImageFormat.Png);
            }
        }

        // Ensure the combined image was created before attempting recognition
        if (!File.Exists(combinedPath))
        {
            Console.WriteLine("Failed to create the combined barcode image.");
            return;
        }

        // Read barcodes from the combined image using all supported decode types (multi‑decode)
        using (var reader = new BarCodeReader(combinedPath, DecodeType.AllSupportedTypes))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // Optional cleanup: delete the temporary folder and its contents
        // Directory.Delete(tempFolder, true);
    }
}