// Title: Barcode resolution comparison between 120 DPI and 300 DPI
// Description: Demonstrates how to set the resolution of a barcode image using Aspose.BarCode, generate PNG files at 120 DPI and 300 DPI, and compare their pixel dimensions to evaluate visual quality.
// Category-Description: This example belongs to the Aspose.BarCode image generation category. It showcases the BarcodeGenerator class, resolution settings, and image export via BarCodeImageFormat. Typical scenarios include creating high‑resolution barcodes for print media, verifying that DPI settings affect pixel size, and helping developers choose appropriate resolution values for their applications. Searchable snippet for developers looking for barcode DPI manipulation and quality comparison.
// Prompt: Set barcode resolution to 120 DPI, generate image, and compare visual quality against 300 DPI reference.
// Tags: barcode, resolution, code128, png, aspose.barcode, image-generation, comparison

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates Code128 barcodes at two different DPI settings (120 DPI and 300 DPI),
/// saves them as PNG files, and compares their pixel dimensions to illustrate the
/// impact of resolution on visual quality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Creates output directory, generates barcode images,
    /// and prints a simple dimension‑based quality comparison to the console.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for the generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeResolutionDemo");
        Directory.CreateDirectory(outputDir);

        // Common barcode data and file paths for the two DPI variants.
        string codeText = "Sample123";
        string file120 = Path.Combine(outputDir, "barcode_120dpi.png");
        string file300 = Path.Combine(outputDir, "barcode_300dpi.png");

        // ------------------------------------------------------------
        // Generate a 120 DPI barcode image.
        // ------------------------------------------------------------
        using (var generator120 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the X‑dimension (module width) to 1 mm.
            generator120.Parameters.Barcode.XDimension.Millimeters = 1f;
            // Define the image resolution.
            generator120.Parameters.Resolution = 120f;
            // Save the barcode as a PNG file.
            generator120.Save(file120, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Generate a 300 DPI barcode image (reference for higher quality).
        // ------------------------------------------------------------
        using (var generator300 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator300.Parameters.Barcode.XDimension.Millimeters = 1f;
            generator300.Parameters.Resolution = 300f;
            generator300.Save(file300, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Load both images and compare their pixel dimensions.
        // ------------------------------------------------------------
        using (Bitmap bmp120 = new Bitmap(file120))
        using (Bitmap bmp300 = new Bitmap(file300))
        {
            Console.WriteLine($"120 DPI image size: {bmp120.Width}x{bmp120.Height} pixels");
            Console.WriteLine($"300 DPI image size: {bmp300.Width}x{bmp300.Height} pixels");

            if (bmp300.Width > bmp120.Width && bmp300.Height > bmp120.Height)
            {
                Console.WriteLine("Higher DPI image has larger pixel dimensions, indicating higher visual quality.");
            }
            else
            {
                Console.WriteLine("Unexpected dimension comparison result.");
            }
        }
    }
}