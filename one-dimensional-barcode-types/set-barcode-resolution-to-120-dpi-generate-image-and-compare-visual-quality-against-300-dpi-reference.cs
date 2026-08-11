// Title: Generate and Compare Barcode Images at Different DPI Settings
// Description: Demonstrates how to set barcode resolution to 120 DPI, generate a PNG image, and compare its pixel dimensions with a 300 DPI reference image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeImageFormat, and Aspose.Drawing.Image to create barcode images at specific resolutions. Typical scenarios include preparing barcodes for print media where DPI impacts visual quality and scanner readability. Developers often need to adjust resolution, export formats, and verify output size for optimal results.
// Prompt: Set barcode resolution to 120 DPI, generate image, and compare visual quality against 300 DPI reference.
// Tags: barcode, code128, resolution, dpi, image generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates two barcode images at different DPI settings
/// and compares their pixel dimensions to illustrate the effect of resolution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates low‑ and high‑resolution barcode images, then prints size comparison.
    /// </summary>
    static void Main()
    {
        // Ensure the output directory exists
        string outputFolder = "output";
        Directory.CreateDirectory(outputFolder);

        // Define file paths for the low‑resolution (120 DPI) and high‑resolution (300 DPI) images
        string lowResPath = Path.Combine(outputFolder, "barcode_120dpi.png");
        string highResPath = Path.Combine(outputFolder, "barcode_300dpi.png");

        // -------------------- Generate barcode at 120 DPI --------------------
        using (var generatorLow = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the desired resolution (dots per inch)
            generatorLow.Parameters.Resolution = 120f;
            // Save the barcode as a PNG file
            generatorLow.Save(lowResPath, BarCodeImageFormat.Png);
        }

        // -------------------- Generate barcode at 300 DPI --------------------
        using (var generatorHigh = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set a higher resolution for finer visual detail
            generatorHigh.Parameters.Resolution = 300f;
            // Save the barcode as a PNG file
            generatorHigh.Save(highResPath, BarCodeImageFormat.Png);
        }

        // -------------------- Load images and compare dimensions --------------------
        using (var lowImage = Image.FromFile(lowResPath))
        using (var highImage = Image.FromFile(highResPath))
        {
            // Output the pixel width and height of each image
            Console.WriteLine($"120 DPI image size:  {lowImage.Width}×{lowImage.Height} pixels");
            Console.WriteLine($"300 DPI image size:  {highImage.Width}×{highImage.Height} pixels");

            // Determine whether the higher DPI produced a larger pixel image
            if (highImage.Width > lowImage.Width && highImage.Height > lowImage.Height)
            {
                Console.WriteLine("Higher DPI produces a larger pixel image, indicating higher visual detail.");
            }
            else
            {
                Console.WriteLine("Unexpected size relationship between DPI settings.");
            }
        }
    }
}