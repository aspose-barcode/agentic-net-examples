// Title: ITF14 Barcode Generation with Thick Frame and TIFF Output
// Description: Demonstrates configuring an ITF14 barcode with a thick frame border and saving it as a TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to customize ITF symbology parameters such as border style, thickness, and quiet‑zone coefficient. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, typical for developers needing precise barcode appearance for packaging or inventory systems.
// Prompt: Configure ITF parameters with thick frame style and quiet zone coefficient 0.3, render TIFF.
// Tags: itf, barcode, generation, tiff, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates an ITF14 barcode with a thick frame border and saves it as a TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures the barcode,
    /// and writes the resulting TIFF file to disk.
    /// </summary>
    static void Main()
    {
        // Prepare output directory in the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "ITFBarcodeExample");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "ITF14_ThickFrame.tiff");

        // Create ITF14 barcode generator with a sample 14‑digit code
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, "12345678901231"))
        {
            // Set the border style to a full frame around the barcode
            generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;

            // Define a thick border (5 pixels) for better visual emphasis
            generator.Parameters.Barcode.ITF.BorderThickness.Pixels = 5;

            // Quiet zone coefficient: API requires an integer (minimum 10). 
            // The requested 0.3 is not supported; using the minimum valid value.
            generator.Parameters.Barcode.ITF.QuietZoneCoef = 10;

            // Save the generated barcode as a TIFF image
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"ITF barcode saved to: {outputPath}");
    }
}