// Title: Generate QR Code with Zero Margin and Save as JPEG
// Description: Demonstrates creating a QR Code barcode, removing all padding, and exporting it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using the BarcodeGenerator class. It shows setting padding properties to control margins, a common requirement when integrating barcodes into tight layouts or UI designs. Developers often need to customize QR Code output for web, print, or mobile applications, and this snippet provides a concise reference.
// Prompt: Generate a QR Code barcode with margin set to zero and save as JPEG image.
// Tags: qr code, barcode generation, zero margin, jpeg output, aspose.barcode, padding

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode with zero margin and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR Code, removes all padding, and saves the result.
    /// </summary>
    static void Main()
    {
        // Initialize a QR Code generator with the desired text/content.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set all padding (margin) sides to zero to eliminate extra whitespace.
            generator.Parameters.Barcode.Padding.Left.Point = 0f;
            generator.Parameters.Barcode.Padding.Top.Point = 0f;
            generator.Parameters.Barcode.Padding.Right.Point = 0f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 0f;

            // Save the generated barcode as a JPEG image file.
            generator.Save("qr.jpg");
        }
    }
}