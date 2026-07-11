// Title: Generate Swiss Post Parcel International Barcodes and Combine into TIFF
// Description: Creates multiple Swiss Post Parcel International barcodes and merges them into a single multi-page TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode generation and image manipulation category. It demonstrates how to use BarcodeGenerator (EncodeTypes.SwissPostParcel) to produce barcodes, adjust rendering parameters, and then combine the resulting Bitmap objects using Aspose.Drawing.Graphics. Typical use cases include batch processing of shipping labels, creating composite documents, and exporting barcode collections to common image formats such as TIFF.
// Prompt: Generate a batch of Swiss Post Parcel international barcodes and combine them into a single TIFF image.
// Tags: swisspostparcel, barcode, generation, tiff, image, combine, aspose.barcode, aspose.drawing

using System;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch generation of Swiss Post Parcel International barcodes and combines them into a single TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, merges them vertically, and saves the result as a TIFF file.
    /// </summary>
    static void Main()
    {
        // Sample Swiss Post Parcel International code texts (replace with real data as needed)
        var codeTexts = new List<string>
        {
            "12345678901234567890",
            "09876543210987654321",
            "11223344556677889900",
            "00112233445566778899",
            "99988877766655544433"
        };

        // Store generated barcode images
        var barcodes = new List<Bitmap>();

        // Generate each barcode
        foreach (var text in codeTexts)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, text))
            {
                // Optional: set module size for better visibility
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Use interpolation mode to let the generator size the image automatically
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Generate the image and add it to the collection
                var bmp = generator.GenerateBarCodeImage();
                barcodes.Add(bmp);
            }
        }

        // Determine combined image dimensions (max width and total height)
        int maxWidth = 0;
        int totalHeight = 0;
        foreach (var bmp in barcodes)
        {
            if (bmp.Width > maxWidth) maxWidth = bmp.Width;
            totalHeight += bmp.Height;
        }

        // Create a new bitmap to hold all barcodes vertically
        using (var combined = new Bitmap(maxWidth, totalHeight))
        {
            using (var graphics = Graphics.FromImage(combined))
            {
                // Fill background with white
                graphics.Clear(Aspose.Drawing.Color.White);

                // Draw each barcode image one below the other
                int offsetY = 0;
                foreach (var bmp in barcodes)
                {
                    graphics.DrawImage(bmp, 0, offsetY, bmp.Width, bmp.Height);
                    offsetY += bmp.Height;
                }
            }

            // Save the combined image as a TIFF file
            combined.Save("SwissPostParcelBatch.tiff", ImageFormat.Tiff);
        }

        // Dispose individual barcode bitmaps
        foreach (var bmp in barcodes)
        {
            bmp.Dispose();
        }

        Console.WriteLine("Batch TIFF image created: SwissPostParcelBatch.tiff");
    }
}