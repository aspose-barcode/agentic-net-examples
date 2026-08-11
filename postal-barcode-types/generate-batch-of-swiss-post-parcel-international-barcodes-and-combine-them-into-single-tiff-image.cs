// Title: Generate Swiss Post Parcel barcodes and combine into a TIFF
// Description: Demonstrates creating multiple Swiss Post Parcel international barcodes and merging them into a single TIFF image for batch processing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.SwissPostParcel, manipulate bitmap images, and produce multi-page TIFF output. Developers working with postal barcode standards often need to generate batches of barcodes and combine them for printing or archival; this snippet illustrates the typical workflow using Aspose.BarCode and Aspose.Drawing APIs.
// Prompt: Generate a batch of Swiss Post Parcel international barcodes and combine them into a single TIFF image.
// Tags: swisspostparcel, barcode generation, tiff, aspose.barcode, aspose.drawing, batch processing

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a batch of Swiss Post Parcel barcodes and combining them into a single TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for sample texts and saves the combined TIFF.
    /// </summary>
    static void Main()
    {
        // Sample Swiss Post Parcel international code texts
        var codeTexts = new List<string>
        {
            "12345678901234567890",
            "09876543210987654321",
            "11223344556677889900",
            "00112233445566778899",
            "99988877766655544433"
        };

        // Path for the combined TIFF image
        string outputPath = "SwissPostParcelBatch.tiff";

        // Generate the combined TIFF from the list of barcode texts
        GenerateCombinedTiff(codeTexts, outputPath);

        // Inform the user where the file was saved
        Console.WriteLine($"Combined TIFF saved to: {Path.GetFullPath(outputPath)}");
    }

    /// <summary>
    /// Generates individual barcode images from the provided texts and merges them vertically into a single TIFF file.
    /// </summary>
    /// <param name="codeTexts">Collection of barcode data strings.</param>
    /// <param name="outputFile">File path for the resulting TIFF image.</param>
    static void GenerateCombinedTiff(List<string> codeTexts, string outputFile)
    {
        if (codeTexts == null || codeTexts.Count == 0)
            throw new ArgumentException("codeTexts collection must contain at least one element.");

        // Store generated barcode bitmaps
        var barcodeImages = new List<Bitmap>();

        // Generate each barcode image
        foreach (var text in codeTexts)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, text))
            {
                // Optional: adjust module size if needed
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Generate the bitmap for the current barcode
                Bitmap bmp = generator.GenerateBarCodeImage();
                barcodeImages.Add(bmp);
            }
        }

        // Determine final image dimensions (stack vertically)
        int maxWidth = 0;
        int totalHeight = 0;
        foreach (var img in barcodeImages)
        {
            if (img.Width > maxWidth) maxWidth = img.Width;
            totalHeight += img.Height;
        }

        // Create the final bitmap that will hold all barcodes
        using (var finalBitmap = new Bitmap(maxWidth, totalHeight))
        {
            using (var graphics = Graphics.FromImage(finalBitmap))
            {
                // Fill background with white
                graphics.Clear(Color.White);
                int offsetY = 0;

                // Draw each barcode image onto the final bitmap
                foreach (var img in barcodeImages)
                {
                    graphics.DrawImage(img, 0, offsetY, img.Width, img.Height);
                    offsetY += img.Height;
                    img.Dispose(); // Dispose individual barcode bitmap after drawing
                }
            }

            // Save the combined image as a TIFF file (single-page in this case)
            finalBitmap.Save(outputFile, ImageFormat.Tiff);
        }
    }
}