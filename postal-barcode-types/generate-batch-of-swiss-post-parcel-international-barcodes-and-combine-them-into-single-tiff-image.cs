// Title: Generate Swiss Post Parcel Barcodes and Combine into a TIFF
// Description: Demonstrates creating multiple Swiss Post Parcel International barcodes and merging them into a single multi-page TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode generation and Aspose.Drawing imaging category. It shows how to use BarcodeGenerator (EncodeTypes.SwissPostParcel) to produce barcode images, manipulate them with Bitmap and Graphics, and save the combined result as a TIFF file. Developers often need batch barcode creation for shipping, logistics, or inventory systems, and this pattern illustrates typical steps for image composition and file output.
// Prompt: Generate a batch of Swiss Post Parcel international barcodes and combine them into a single TIFF image.
// Tags: swisspostparcel, batch, tiff, aspose.barcode, aspose.drawing

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a set of Swiss Post Parcel International barcodes
/// and combines them into a single TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcode images, stacks them vertically,
    /// and saves the result as a TIFF file.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output folder for the generated TIFF
        string outputFolder = Path.Combine(Path.GetTempPath(), "SwissPostBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, "SwissPostBatch.tiff");

        // Sample Swiss Post Parcel International codes to encode
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
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
            {
                // Configure barcode appearance
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 40f;

                // Render the barcode to a bitmap
                Bitmap bmp = generator.GenerateBarCodeImage();
                barcodeImages.Add(bmp);
            }
        }

        // Calculate the dimensions of the combined image (vertical stack)
        int maxWidth = 0;
        int totalHeight = 0;
        int spacing = 10; // Pixels between consecutive barcodes
        foreach (var img in barcodeImages)
        {
            if (img.Width > maxWidth) maxWidth = img.Width;
            totalHeight += img.Height + spacing;
        }
        totalHeight -= spacing; // Remove extra spacing after the last image

        // Create a new bitmap that will hold all barcodes together
        using (Bitmap combined = new Bitmap(maxWidth, totalHeight))
        {
            using (Graphics g = Graphics.FromImage(combined))
            {
                // Fill background with white
                g.Clear(Aspose.Drawing.Color.White);
                int yOffset = 0;

                // Draw each barcode image onto the combined bitmap
                foreach (var img in barcodeImages)
                {
                    g.DrawImage(img, 0, yOffset, img.Width, img.Height);
                    yOffset += img.Height + spacing;
                }
            }

            // Save the combined bitmap as a TIFF file
            combined.Save(outputPath, ImageFormat.Tiff);
        }

        // Release resources held by individual barcode bitmaps
        foreach (var img in barcodeImages)
        {
            img.Dispose();
        }

        Console.WriteLine("Combined TIFF saved to: " + outputPath);
    }
}