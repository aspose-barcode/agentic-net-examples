// Title: Demonstrate color immutability after saving barcode image
// Description: Shows that changing barcode colors after calling Save does not affect the previously saved image file.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how the BarcodeGenerator and related drawing classes (Aspose.Drawing, Aspose.Drawing.Imaging) are used to create, customize, and persist barcode images. Developers often need to generate multiple variants of a barcode with different visual styles while ensuring earlier files remain unchanged. The snippet demonstrates best‑practice handling of color properties and file output, a common requirement when producing batch‑processed barcodes for packaging, labeling, or inventory systems.
// Prompt: Demonstrate that modifying color properties after calling Save does not alter the already saved image.
// Tags: code128, color, save, bitmap, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, saves it, changes its colors,
/// saves a second image, and then verifies that the first saved file remains unchanged.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, saves two images with
    /// different color settings, and compares a pixel from each file to confirm independence.
    /// </summary>
    static void Main()
    {
        // Define absolute file paths for the original and modified barcode images.
        string originalPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_original.png");
        string modifiedPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_modified.png");

        // Create a BarcodeGenerator for Code128, set initial colors, and save the first image.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Set the barcode (foreground) color to red and the background to white.
            generator.Parameters.Barcode.BarColor = Color.Red;
            generator.Parameters.BackColor = Color.White;

            // Persist the first image to disk.
            generator.Save(originalPath, BarCodeImageFormat.Png);

            // Change colors after the first save: barcode to blue, background to yellow.
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.BackColor = Color.Yellow;

            // Persist the second image to disk.
            generator.Save(modifiedPath, BarCodeImageFormat.Png);
        }

        // Load both saved images to compare a single pixel and demonstrate that they are independent.
        using (var originalImg = (Bitmap)Image.FromFile(originalPath))
        using (var modifiedImg = (Bitmap)Image.FromFile(modifiedPath))
        {
            // Sample the top‑left pixel (0,0) from each image.
            var origPixel = originalImg.GetPixel(0, 0);
            var modPixel = modifiedImg.GetPixel(0, 0);

            // Output ARGB values for visual verification.
            Console.WriteLine($"Original top‑left pixel ARGB: 0x{origPixel.ToArgb():X8}");
            Console.WriteLine($"Modified top‑left pixel ARGB: 0x{modPixel.ToArgb():X8}");

            // Compare the pixel values to confirm that the images differ as expected.
            if (origPixel.ToArgb() != modPixel.ToArgb())
                Console.WriteLine("The images have different colors as expected – changes after Save do not affect the already saved file.");
            else
                Console.WriteLine("The images appear identical – unexpected behavior.");
        }
    }
}