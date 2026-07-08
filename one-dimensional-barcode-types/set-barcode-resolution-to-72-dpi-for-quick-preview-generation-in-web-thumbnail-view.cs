// Title: Generate barcode thumbnail with 72 DPI resolution
// Description: Demonstrates setting barcode resolution to 72 DPI and creating a small PNG thumbnail for web preview.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to configure resolution, size, and auto‑size mode using BarcodeGenerator and its Parameters. Developers often need to produce low‑resolution barcode images for quick previews, thumbnails, or email attachments, and this snippet shows the typical API usage for such scenarios.
// Prompt: Set barcode resolution to 72 DPI for quick preview generation in a web thumbnail view.
// Tags: barcode, code128, resolution, preview, thumbnail, png, aspnet, aspose.barcode, imagegeneration

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodePreview
{
    /// <summary>
    /// Generates a Code128 barcode image optimized for thumbnail preview with a resolution of 72 DPI.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point that creates and saves a low‑resolution barcode PNG suitable for web thumbnails.
        /// </summary>
        static void Main()
        {
            // Initialize the barcode generator with Code128 symbology and sample data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
            {
                // Set the image resolution to 72 DPI for faster rendering and smaller file size.
                generator.Parameters.Resolution = 72f;

                // Choose interpolation auto‑size mode to keep the barcode sharp at the target dimensions.
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Define the thumbnail dimensions in points (1 point = 1/72 inch).
                generator.Parameters.ImageWidth.Point = 150f;   // Approx. 2.08 inches wide
                generator.Parameters.ImageHeight.Point = 50f;   // Approx. 0.69 inches tall

                // Save the generated barcode as a PNG file named "thumbnail.png".
                generator.Save("thumbnail.png");
            }
        }
    }
}