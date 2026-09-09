// Title: Generate high‑resolution DataMatrix HIBC LIC barcode image
// Description: Demonstrates configuring Aspose.BarCode to generate a DataMatrix HIBC LIC barcode at 300 DPI, suitable for clear rendering in medical reports.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to set image resolution and module size for DataMatrix symbology. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Developers often need to produce high‑quality barcode images for print or PDF documents, especially in healthcare where HIBC LIC codes are required.
// Prompt: Configure the barcode generator to use high DPI (300) for sharper DataMatrix HIBC LIC images in medical reports.
// Tags: barcode, datamatrix, hibc, lic, high dpi, resolution, png, aspnet, aspnetcore, aspose.barcode, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a high‑resolution DataMatrix HIBC LIC barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output folder path relative to the current directory.
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputFolder);

        // Full path for the generated barcode image.
        string outputPath = Path.Combine(outputFolder, "DataMatrix_HIBC_LIC.png");

        // Initialize the barcode generator for DataMatrix symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ASPOSE"))
        {
            // Set the image resolution to 300 DPI for sharper output.
            generator.Parameters.Resolution = 300f;

            // Optionally adjust the module (pixel) size of the barcode.
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image has been saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}