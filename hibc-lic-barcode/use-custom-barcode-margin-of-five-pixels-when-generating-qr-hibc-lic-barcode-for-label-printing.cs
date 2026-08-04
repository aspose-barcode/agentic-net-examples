// Title: Generate QR HIBC LIC Barcode with Custom 5‑Pixel Margin
// Description: Demonstrates how to create a QR HIBC LIC barcode using Aspose.BarCode, apply a five‑pixel margin on all sides, and save the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as HIBC LIC QR. It showcases the use of ComplexBarcodeGenerator, HIBCLICSecondaryAndAdditionalDataCodetext, and padding configuration. Developers creating label‑printing solutions often need to customize barcode margins for scanner readability and aesthetic layout.
// Prompt: Use a custom barcode margin of five pixels when generating a QR HIBC LIC barcode for label printing.
// Tags: qr, hibc, lic, barcode, margin, png, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a QR HIBC LIC barcode with a custom 5‑pixel margin.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates secondary data, configures the barcode, applies padding, and saves the image.
    /// </summary>
    static void Main()
    {
        // Prepare secondary data required for the HIBC LIC QR barcode (lot and serial numbers).
        var secondaryData = new SecondaryAndAdditionalData
        {
            LotNumber = "LOT123",
            SerialNumber = "SN456"
        };

        // Build the HIBC LIC QR codetext object, specifying the symbology and link character.
        var hibcCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC, // QR HIBC LIC symbology
            LinkCharacter = '+',                 // Required link character
            Data = secondaryData
        };

        // Create the barcode generator with the prepared codetext.
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        {
            // Apply a uniform margin of five pixels on all sides of the barcode.
            generator.Parameters.Barcode.Padding.Left.Pixels = 5f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 5f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 5f;

            // Define the output file path and save the barcode as a PNG image.
            string outputPath = "qr_hibc_lic.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved.
            Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
        }
    }
}