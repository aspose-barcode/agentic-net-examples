// Title: Generate ITF-14 barcode with custom frame thickness and save as PNG
// Description: Demonstrates encoding a GTIN into an ITF-14 barcode, customizing the frame border thickness, and exporting the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.ITF14. It covers setting barcode dimensions, applying a frame border, and saving the image, which are common tasks for developers creating product packaging barcodes or integrating GTIN encoding into .NET applications.
// Prompt: Generate ITF‑14 barcode encoding GTIN, applying custom frame thickness, save as PNG.
// Tags: itf-14, barcode, generation, png, frame-thickness, aspnet, aspnet-core, aspnet-barcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    /// <summary>
    /// Demonstrates generating an ITF‑14 barcode with a custom frame border and saving it as a PNG file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Creates output directory, configures the barcode generator,
        /// applies custom dimensions and border, saves the image, and writes the output path to console.
        /// </summary>
        static void Main()
        {
            // Define output directory in the system's temporary folder
            string outputDir = Path.Combine(Path.GetTempPath(), "ITF14BarcodeDemo");
            // Ensure the directory exists
            Directory.CreateDirectory(outputDir);
            // Build the full path for the PNG file
            string outputPath = Path.Combine(outputDir, "ITF14_GTIN.png");

            // Initialize the barcode generator with ITF-14 symbology and a sample GTIN value
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.ITF14, "01234567890128"))
            {
                // Set the X-dimension (module width) in points
                generator.Parameters.Barcode.XDimension.Point = 2f;
                // Configure the border type to a frame around the barcode
                generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;
                // Set the border thickness in points
                generator.Parameters.Barcode.ITF.BorderThickness.Point = 5f;
                // Save the generated barcode as a PNG image
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Output the location of the saved barcode image
            Console.WriteLine($"ITF-14 barcode saved to: {outputPath}");
        }
    }
}