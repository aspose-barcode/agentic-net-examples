// Title: Generate QR Code with custom margin
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode and applying a two‑module margin for visual padding.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and layout customization. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and barcode parameter settings such as XDimension and Padding. Developers use these APIs to produce QR codes with precise sizing and padding for integration into documents, web pages, or printed media.
// Prompt: Generate QR Code barcode and set margin to two modules for visual padding.
// Tags: qr code, barcode generation, margin, padding, aspose.barcode, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode with a two‑module margin using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a QR Code, configures module size and padding, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a QR Code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Set the size of a single QR module (XDimension) to 2 points.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Calculate margin equal to two modules (2 * XDimension).
            float margin = generator.Parameters.Barcode.XDimension.Point * 2f;

            // Apply the calculated margin to all sides of the barcode.
            generator.Parameters.Barcode.Padding.Left.Point = margin;
            generator.Parameters.Barcode.Padding.Top.Point = margin;
            generator.Parameters.Barcode.Padding.Right.Point = margin;
            generator.Parameters.Barcode.Padding.Bottom.Point = margin;

            // Save the generated QR Code image to a PNG file.
            generator.Save("qr.png");
        }

        // Inform the user that the QR Code has been generated.
        Console.WriteLine("QR code generated: qr.png");
    }
}