// Title: Generate HIBC LIC barcode with custom colors
// Description: Demonstrates creating a HIBC LIC barcode using Aspose.BarCode, applying a blue foreground and light‑gray background for branding purposes.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, illustrating how to work with HIBC LIC symbology via the ComplexBarcodeGenerator and related data classes. Developers commonly use these APIs to embed product information, lot numbers, and serial numbers in healthcare labeling, customizing appearance to match brand guidelines.
// Prompt: Generate HIBC LIC barcodes with custom foreground color (blue) and background color (light gray) for branding.
// Tags: hibc, lic, barcode, color, customization, png, aspnet, aspnetcore, aspose.barcode, complexbarcode, generation

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a HIBC LIC barcode with custom foreground and background colors using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates secondary data, builds complex codetext, generates the barcode image, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Define secondary data for the HIBC LIC barcode (lot and serial numbers)
        var secondaryData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123",
            SerialNumber = "SN456"
        };

        // Create the complex codetext object that includes barcode type, link character, and secondary data
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC, // Use Code128 LIC symbology
            LinkCharacter = '+',                     // Required link character for HIBC LIC
            Data = secondaryData
        };

        // Generate the barcode image with custom colors using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Set foreground (bars) color to blue for branding
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set background color to light gray for contrast
            generator.Parameters.BackColor = Color.LightGray;

            // Define output file path and save the barcode as PNG
            string outputPath = "hibc_lic_custom.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved
            Console.WriteLine($"HIBC LIC barcode saved to: {Path.GetFullPath(outputPath)}");
        }
    }
}