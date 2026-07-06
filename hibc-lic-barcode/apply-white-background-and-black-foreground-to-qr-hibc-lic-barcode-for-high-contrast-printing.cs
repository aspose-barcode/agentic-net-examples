// Title: Apply white background and black foreground to a QR HIBC LIC barcode
// Description: Demonstrates generating a QR HIBC LIC barcode with high‑contrast colors for printing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode creation using the ComplexBarcodeGenerator and HIBCLICSecondaryAndAdditionalDataCodetext classes. It shows how to configure color parameters for QR HIBC LIC barcodes, a common requirement for clear, high‑contrast print output in healthcare and logistics applications. Developers often need to customize foreground and background colors when generating barcodes for scanners and label printers.
// Prompt: Apply a white background and black foreground to a QR HIBC LIC barcode for high‑contrast printing.
// Tags: barcode, hibc, qr, lic, color, background, foreground, generation, aspnet, aspose.barcode

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a QR HIBC LIC barcode with a white background and black foreground,
/// illustrating color customization for high‑contrast printing scenarios.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates secondary and additional data for a QR HIBC LIC barcode,
    /// sets color parameters, and saves the resulting image.
    /// </summary>
    static void Main()
    {
        // Prepare secondary‑and‑additional data codetext for a QR HIBC LIC barcode
        var hibcCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC, // QR version of HIBC LIC
            LinkCharacter = '+',                 // Required link character
            Data = new SecondaryAndAdditionalData
            {
                LotNumber = "LOT123"            // Example secondary data
            }
        };

        // Generate the barcode with specified colors
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        {
            // Set foreground (barcode) color to black
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Set background color to white
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode image to a PNG file
            generator.Save("hibc_qr.png");
        }
    }
}