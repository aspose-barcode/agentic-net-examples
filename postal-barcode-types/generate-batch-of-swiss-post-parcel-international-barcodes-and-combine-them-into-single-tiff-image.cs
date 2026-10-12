// Title: Generate Swiss Post Parcel International barcodes and merge into a TIFF
// Description: Demonstrates creating multiple Swiss Post Parcel (International) barcodes and combining them into a single multi-page TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and image processing category. It showcases the use of BarcodeGenerator, barcode parameters, and Aspose.Drawing to render barcodes, then merges them using Graphics into a combined TIFF file. Developers working with bulk barcode creation, batch printing, or archival image formats will find this pattern useful for generating composite images for shipping, logistics, or documentation purposes.
// Prompt: Generate a batch of Swiss Post Parcel international barcodes and combine them into a single TIFF image.
// Tags: swisspost, parcel, international, barcode, generation, tiff, image, aspose.barcode, aspose.drawing

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an example that generates Swiss Post Parcel International barcodes
/// and merges them into a single TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates barcode images, combines them, and saves the result.
    /// </summary>
    static void Main()
    {
        // Sample Swiss Post International Mail codes (some may lack checksum; the library will handle it)
        List<string> codes = new List<string>
        {
            "RM999605013CH",
            "AB123456789CH",
            "CD987654321CH",
            "EF000111222CH",
            "GH555666777CH"
        };

        // Generate individual barcode images and store them in a list
        List<Bitmap> barcodeImages = new List<Bitmap>();
        foreach (string code in codes)
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
            {
                // Configure barcode appearance
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 40f;

                // Render barcode to a bitmap
                Bitmap bmp = generator.GenerateBarCodeImage();
                barcodeImages.Add(bmp);
            }
        }

        // Determine the dimensions needed for the combined image
        int maxWidth = barcodeImages.Max(b => b.Width);
        int totalHeight = barcodeImages.Sum(b => b.Height);

        // Create a new bitmap that will hold all barcodes stacked vertically
        using (Bitmap combined = new Bitmap(maxWidth, totalHeight))
        {
            using (Graphics graphics = Graphics.FromImage(combined))
            {
                int offsetY = 0;
                // Draw each barcode bitmap onto the combined image
                foreach (Bitmap bmp in barcodeImages)
                {
                    graphics.DrawImage(bmp, new Rectangle(0, offsetY, bmp.Width, bmp.Height));
                    offsetY += bmp.Height;
                }
            }

            // Save the combined image as a TIFF file
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "CombinedSwissPostInternational.tiff");
            combined.Save(outputPath, ImageFormat.Tiff);
            Console.WriteLine($"Combined TIFF saved to: {outputPath}");
        }

        // Release resources held by individual barcode images
        foreach (Bitmap bmp in barcodeImages)
        {
            bmp.Dispose();
        }
    }
}