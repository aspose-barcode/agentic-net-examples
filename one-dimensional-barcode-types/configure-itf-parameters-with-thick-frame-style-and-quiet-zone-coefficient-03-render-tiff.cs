// Title: Configure ITF14 barcode with thick frame and custom quiet zone, save as TIFF
// Description: Demonstrates how to set a thick frame border and a quiet‑zone coefficient for an ITF14 barcode using Aspose.BarCode, then render the result to a TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and ITF14 parameters such as BorderType, BorderThickness, and QuietZoneCoef. Typical scenarios include creating high‑resolution ITF14 barcodes for packaging, where a prominent frame and precise quiet‑zone control are required. Developers often need to adjust these settings before saving the barcode in various image formats.
// Prompt: Configure ITF parameters with thick frame style and quiet zone coefficient 0.3, render TIFF.
// Tags: itf14, barcode, generation, tiff, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that configures ITF14 barcode parameters (thick frame and quiet zone) and saves the image as TIFF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates an ITF14 barcode with a thick frame and a quiet‑zone coefficient, then writes it to a TIFF file.
    /// </summary>
    static void Main()
    {
        // Prepare output directory in the temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarCode_ITF_Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // ITF14 requires exactly 14 digits (the last digit is a checksum)
        string codeText = "12345678901231";

        // Desired parameters (as requested)
        float requestedQuietZoneCoef = 0.3f; // will be adjusted to meet API constraints
        float borderThicknessPixels = 15f;   // thick frame thickness

        // Create a barcode generator for ITF14
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, codeText))
        {
            // Set the border style to a frame and apply the thick border thickness
            generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;
            generator.Parameters.Barcode.ITF.BorderThickness.Pixels = borderThicknessPixels;

            // QuietZoneCoef must be an integer with a minimum value of 10; adjust if necessary
            int quietZoneCoef = (int)requestedQuietZoneCoef;
            if (quietZoneCoef < 10)
            {
                Console.WriteLine($"Requested QuietZoneCoef {requestedQuietZoneCoef} is below the minimum. Using default value 10.");
                quietZoneCoef = 10;
            }
            generator.Parameters.Barcode.ITF.QuietZoneCoef = quietZoneCoef;

            // Save the generated barcode as a TIFF image
            string outPath = Path.Combine(outputDir, "ITF14_ThickFrame_QuietZone.tiff");
            generator.Save(outPath, BarCodeImageFormat.Tiff);
            Console.WriteLine($"Barcode saved to: {outPath}");
        }
    }
}