// Title: Generate Swiss Post Parcel International barcodes batch and merge into TIFF
// Description: Demonstrates creating multiple Swiss Post Parcel International barcodes and combining them into a single multi-page TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode generation and image processing category. It shows how to use BarcodeGenerator (EncodeTypes.SwissPostParcel) to produce barcode images, adjust dimensions, and then compose them using Aspose.Drawing's Bitmap and Graphics classes. Developers often need to batch‑create barcodes and export them as a combined image for shipping labels or archival purposes.
// Prompt: Generate a batch of Swiss Post Parcel international barcodes and combine them into a single TIFF image.
// Tags: barcode, swisspostparcel, generation, batch, tiff, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch generation of Swiss Post Parcel International barcodes and merging them into a single TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, composes them vertically, and saves the result as a TIFF image.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store the generated TIFF file
        string tempFolder = Path.Combine(Path.GetTempPath(), "SwissPostBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample Swiss Post Parcel International Mail codes to encode
        List<string> codes = new List<string>
        {
            "RM999605013CH",
            "RM999605014CH",
            "RM999605015CH",
            "RM999605016CH",
            "RM999605017CH"
        };

        // Collection to hold individual barcode bitmap images
        List<Bitmap> barcodeImages = new List<Bitmap>();

        // Generate a barcode image for each code
        foreach (string code in codes)
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
            {
                // Set barcode dimensions
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 40f;

                // Generate the bitmap and add it to the list
                Bitmap bmp = generator.GenerateBarCodeImage();
                barcodeImages.Add(bmp);
            }
        }

        // Calculate the size of the combined image (vertical stack)
        int maxWidth = 0;
        int totalHeight = 0;
        int spacing = 10; // pixels between consecutive barcodes

        foreach (Bitmap img in barcodeImages)
        {
            if (img.Width > maxWidth) maxWidth = img.Width;
            totalHeight += img.Height + spacing;
        }
        totalHeight -= spacing; // remove extra spacing after the last image

        // Create a new bitmap that will contain all barcodes stacked vertically
        using (Bitmap combined = new Bitmap(maxWidth, totalHeight))
        {
            using (Graphics g = Graphics.FromImage(combined))
            {
                // Fill background with white
                g.Clear(Aspose.Drawing.Color.White);
                int currentY = 0;

                // Draw each barcode image onto the combined bitmap
                foreach (Bitmap img in barcodeImages)
                {
                    g.DrawImage(img, 0, currentY);
                    currentY += img.Height + spacing;
                }
            }

            // Save the combined image as a TIFF file
            string outputPath = Path.Combine(tempFolder, "SwissPostBatch.tiff");
            combined.Save(outputPath, Aspose.Drawing.Imaging.ImageFormat.Tiff);
            Console.WriteLine($"Combined TIFF saved to: {outputPath}");
        }

        // Release resources held by individual barcode images
        foreach (Bitmap img in barcodeImages)
        {
            img.Dispose();
        }
    }
}