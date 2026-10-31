// Title: Encode HIBC LIC Combined QR Code
// Description: Demonstrates how to create a combined primary and secondary HIBC LIC data structure and generate a QR code image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to use the HIBCLICCombinedCodetext class together with ComplexBarcodeGenerator to encode both primary and secondary fields for HIBC QR‑LIC symbology. Developers working with healthcare or pharmaceutical labeling often need to embed product, lot, expiry, and serial information in a single QR code; this snippet illustrates the typical API usage for such scenarios.
// Prompt: Encode combined primary and secondary fields using HIBCLICCombinedCodetext and output a QR code file.
// Tags: hibc, lic, combined, qr, barcode, generation, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates encoding combined primary and secondary HIBC LIC data into a QR code using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR code and saves it to the Output folder.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory where the generated image will be stored.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }
        string outputPath = Path.Combine(outputDir, "HIBCLICCombined.png");

        // Create a combined HIBC LIC codetext object and specify the QR‑LIC symbology.
        HIBCLICCombinedCodetext combinedCodetext = new HIBCLICCombinedCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC
        };

        // Populate primary data fields (product information).
        combinedCodetext.PrimaryData = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999",
            UnitOfMeasureID = 1
        };

        // Populate secondary and additional data fields (lot, expiry, etc.).
        combinedCodetext.SecondaryAndAdditionalData = new SecondaryAndAdditionalData
        {
            ExpiryDate = DateTime.Now,
            ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
            Quantity = 30,
            LotNumber = "LOT123",
            SerialNumber = "SERIAL123",
            DateOfManufacture = DateTime.Now
        };

        // Generate the barcode using ComplexBarcodeGenerator.
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(combinedCodetext))
        {
            // Set the X‑dimension (module size) for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Save the generated QR code as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"HIBC LIC combined QR code saved to: {outputPath}");
    }
}