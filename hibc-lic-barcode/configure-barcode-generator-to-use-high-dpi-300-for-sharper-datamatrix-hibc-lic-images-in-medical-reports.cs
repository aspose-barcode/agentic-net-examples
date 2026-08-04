// Title: Generate high‑resolution HIBC DataMatrix LIC barcode image
// Description: Demonstrates configuring Aspose.BarCode to produce a 300 DPI DataMatrix HIBC LIC barcode, suitable for clear rendering in medical reports.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to set image resolution, module size, and colors using the BarcodeGenerator class. Typical use cases include creating high‑quality barcodes for healthcare documentation, inventory, and labeling where precise scanning is required. Developers often need to adjust DPI and visual parameters to meet regulatory and readability standards.
// Prompt: Configure the barcode generator to use high DPI (300) for sharper DataMatrix HIBC LIC images in medical reports.
// Tags: datamatrix, hibc, barcode-generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a high‑resolution HIBC DataMatrix LIC barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Sample HIBC DataMatrix LIC codetext (replace with actual medical report data as needed)
        string codeText = "A12345B67890";

        // Define the output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "HIBCDataMatrixLIC.png");

        // Ensure the output directory exists before saving the image
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize the barcode generator with HIBC DataMatrix LIC symbology and the provided code text
        using (var generator = new BarcodeGenerator(EncodeTypes.HIBCDataMatrixLIC, codeText))
        {
            // Set high DPI resolution (300 DPI) for a sharper image suitable for printing
            generator.Parameters.Resolution = 300;

            // Optional: adjust the module (X) dimension for better readability
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Define barcode colors: black bars on a white background
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}