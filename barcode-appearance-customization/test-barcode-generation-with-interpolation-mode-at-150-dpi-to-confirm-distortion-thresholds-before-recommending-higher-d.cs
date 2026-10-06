// Title: Generate DataMatrix barcode with interpolation at 150 DPI
// Description: Demonstrates generating a DataMatrix barcode using Aspose.BarCode with interpolation auto‑size mode at 150 dpi, then saves it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to configure AutoSizeMode, resolution, and image dimensions. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes—common tools for developers who need to produce high‑quality barcodes for printing or digital display while evaluating DPI settings.
// Prompt: Test barcode generation with Interpolation mode at 150 dpi to confirm distortion thresholds before recommending higher DPI.
// Tags: datamatrix, barcode generation, interpolation, dpi, png, aspose.barcode, autosizemode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a DataMatrix barcode using interpolation auto‑size mode at 150 dpi
/// and writes the resulting PNG file to a temporary folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it, and outputs basic verification information.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory in the system temporary folder
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeInterpolationTest");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the generated PNG image
        string barcodePath = Path.Combine(outputDir, "barcode_interpolation_150dpi.png");

        // --------------------------------------------------------------------
        // Generate barcode with Interpolation auto‑size mode at 150 dpi
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ASPOSE"))
        {
            // Set auto‑size mode to use interpolation for scaling
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define resolution (dots per inch)
            generator.Parameters.Resolution = 150f;

            // Set explicit image dimensions (pixels)
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 300f;

            // Define module size for the DataMatrix barcode
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Save the barcode as a PNG file
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the image was saved and display its properties
        // --------------------------------------------------------------------
        if (File.Exists(barcodePath))
        {
            using (var bitmap = new Bitmap(barcodePath))
            {
                Console.WriteLine($"Barcode generated at: {barcodePath}");
                Console.WriteLine($"Image dimensions (pixels): Width = {bitmap.Width}, Height = {bitmap.Height}");
                Console.WriteLine("Resolution set to 150 dpi with Interpolation mode. Review the image for distortion before recommending higher DPI.");
            }
        }
        else
        {
            Console.WriteLine("Failed to generate barcode image.");
        }
    }
}