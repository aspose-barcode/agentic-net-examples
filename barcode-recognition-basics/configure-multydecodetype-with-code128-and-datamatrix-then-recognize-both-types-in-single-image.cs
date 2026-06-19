using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating Code128 and DataMatrix barcodes, combining them into a single image,
/// and then recognizing both barcodes from the combined image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates two barcodes, merges them, saves to a temporary file,
    /// reads and prints detected barcode types and texts, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Prepare sample texts for the two barcodes
        string code128Text = "ABC123";
        string dataMatrixText = "DM12345";

        // Generate Code128 barcode bitmap
        Bitmap code128Bitmap;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, code128Text))
        {
            // Generate the barcode image and assign to bitmap variable
            code128Bitmap = generator.GenerateBarCodeImage();
        }

        // Generate DataMatrix barcode bitmap
        Bitmap dataMatrixBitmap;
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, dataMatrixText))
        {
            // Generate the barcode image and assign to bitmap variable
            dataMatrixBitmap = generator.GenerateBarCodeImage();
        }

        // Combine both bitmaps into a single image with a small spacing between them
        int spacing = 10;
        int combinedWidth = code128Bitmap.Width + spacing + dataMatrixBitmap.Width;
        int combinedHeight = Math.Max(code128Bitmap.Height, dataMatrixBitmap.Height);
        Bitmap combinedBitmap = new Bitmap(combinedWidth, combinedHeight);
        using (Graphics g = Graphics.FromImage(combinedBitmap))
        {
            // Fill background with white
            g.Clear(Aspose.Drawing.Color.White);
            // Draw the first barcode at the left
            g.DrawImage(code128Bitmap, 0, 0);
            // Draw the second barcode to the right of the first, with spacing
            g.DrawImage(dataMatrixBitmap, code128Bitmap.Width + spacing, 0);
        }

        // Save the combined image to a temporary file in PNG format
        string combinedPath = Path.Combine(Path.GetTempPath(), "combined.png");
        combinedBitmap.Save(combinedPath, ImageFormat.Png);

        // Release individual bitmaps as they are no longer needed
        code128Bitmap.Dispose();
        dataMatrixBitmap.Dispose();

        // Recognize both Code128 and DataMatrix barcodes from the combined image
        using (Bitmap imageToRead = new Bitmap(combinedPath))
        {
            // Configure MultiDecodeType to look for Code128 and DataMatrix symbologies
            var multiDecode = new MultiDecodeType(DecodeType.Code128, DecodeType.DataMatrix);
            using (var reader = new BarCodeReader(imageToRead, multiDecode))
            {
                // Iterate through all detected barcodes and output their type and text
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                }
            }
        }

        // Delete the temporary combined image file
        if (File.Exists(combinedPath))
        {
            File.Delete(combinedPath);
        }
    }
}