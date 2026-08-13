// Title: Generate a high‑contrast HIBC QR LIC barcode with white background and black foreground
// Description: Demonstrates how to create a HIBC QR LIC barcode using Aspose.BarCode, applying a white background and black foreground for optimal print contrast.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, illustrating the use of the ComplexBarcodeGenerator and related codetext classes to produce specialized symbologies such as HIBC QR LIC. Developers often need to customize colors, embed secondary data, and export images in common formats. The snippet shows typical steps: preparing secondary data, configuring barcode parameters, and saving the result.
// Prompt: Apply a white background and black foreground to a QR HIBC LIC barcode for high‑contrast printing.
// Tags: hibc, qr, lic, color, png, complexbarcodegenerator, secondaryandadditionaldata

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a HIBC QR LIC barcode with high‑contrast colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates and saves a barcode image.
    /// </summary>
    static void Main()
    {
        // Prepare secondary data (lot and serial numbers) required for the HIBC LIC QR barcode.
        var secondaryData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123",
            SerialNumber = "SN456"
        };

        // Build the complex codetext object that defines the barcode type, link character, and secondary data.
        var hibcCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            LinkCharacter = '+', // Required link character for HIBC QR LIC.
            Data = secondaryData
        };

        // Generate the barcode using the complex barcode generator.
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        {
            // Set foreground (bars) to black.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            // Set background to white for high contrast.
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Define output file path and save the barcode as a PNG image.
            string outputPath = "HIBC_QR.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
        }
    }
}