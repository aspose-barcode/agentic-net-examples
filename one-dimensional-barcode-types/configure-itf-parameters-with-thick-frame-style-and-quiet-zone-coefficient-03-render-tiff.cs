// Title: ITF14 barcode generation with thick frame and custom quiet zone
// Description: Demonstrates configuring an ITF14 barcode with a thick frame style and a quiet zone coefficient, then rendering it as a TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to customize ITF symbology using the BarcodeGenerator class. It shows setting frame border type, border thickness, and quiet zone coefficient—common tasks when tailoring barcodes for packaging or inventory systems. Developers often need to adjust these parameters to meet printing standards and scanner requirements. 
// Prompt: Configure ITF parameters with thick frame style and quiet zone coefficient 0.3, render TIFF.
// Tags: itf14, barcode, generation, frame, quietzone, tiff, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating an ITF14 barcode with a thick frame and custom quiet zone, saved as a TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the output directory, configures barcode parameters, and saves the image.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory where the barcode image will be stored
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the full file path for the resulting TIFF image
        string outputPath = Path.Combine(outputDir, "ITF14_Frame_Thick_QuietZone.tiff");

        // Create a BarcodeGenerator for ITF14 symbology with the specified data
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, "12345678901231"))
        {
            // Configure the barcode to use a frame border type
            generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;

            // Set a thick frame border (example: 5 points)
            generator.Parameters.Barcode.ITF.BorderThickness.Point = 5f;

            // Set the quiet zone coefficient (minimum valid integer value)
            generator.Parameters.Barcode.ITF.QuietZoneCoef = 10;

            // Save the generated barcode as a TIFF image
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}