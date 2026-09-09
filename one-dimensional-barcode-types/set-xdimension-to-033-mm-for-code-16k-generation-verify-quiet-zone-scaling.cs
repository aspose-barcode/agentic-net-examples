// Title: Code 16K barcode generation with custom XDimension and quiet zone scaling
// Description: Demonstrates how to set the XDimension to 0.33 mm for a Code 16K barcode and generate images with different quiet‑zone scaling coefficients.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings such as XDimension and quiet‑zone coefficients. Typical use cases include customizing barcode size for printing and verifying how quiet‑zone scaling affects the final image. Developers working with barcode rendering often need to adjust these parameters to meet layout and scanning requirements.
// Prompt: Set XDimension to 0.33 mm for Code 16K generation, verify quiet zone scaling.
// Tags: code16k, xdimension, quietzone, barcode generation, aspnet, aspnetcore, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates Code 16K barcodes with a specific XDimension and varying quiet‑zone coefficients,
/// then saves the results as PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output folder, configures the barcode generator,
    /// and produces two images with different quiet‑zone scaling settings.
    /// </summary>
    static void Main()
    {
        // Determine a temporary folder to store the generated barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "Code16KDemo");
        if (!Directory.Exists(outputDir))
        {
            // Create the folder if it does not already exist
            Directory.CreateDirectory(outputDir);
        }

        // Initialize a Code 16K barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, "Aspose.BarCode"))
        {
            // Set the module width (XDimension) to 0.33 mm
            generator.Parameters.Barcode.XDimension.Millimeters = 0.33f;

            // ---- First configuration: quiet‑zone coefficients set to 10 ----
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 10;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 10;
            string pathQZ10 = Path.Combine(outputDir, "Code16K_XDim_0_33mm_QZ10.png");
            // Save the barcode image with the first quiet‑zone setting
            generator.Save(pathQZ10, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved barcode with XDimension=0.33mm, QuietZoneCoef=10 to: {pathQZ10}");

            // ---- Second configuration: quiet‑zone coefficients set to 20 ----
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 20;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 20;
            string pathQZ20 = Path.Combine(outputDir, "Code16K_XDim_0_33mm_QZ20.png");
            // Save the barcode image with the second quiet‑zone setting
            generator.Save(pathQZ20, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved barcode with XDimension=0.33mm, QuietZoneCoef=20 to: {pathQZ20}");
        }

        Console.WriteLine("Barcode generation completed.");
    }
}