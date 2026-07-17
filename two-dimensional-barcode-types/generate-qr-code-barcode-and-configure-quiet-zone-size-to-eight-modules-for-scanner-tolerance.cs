// Title: Generate QR Code with Custom Quiet Zone
// Description: Demonstrates creating a QR Code barcode and setting an eight‑module quiet zone to improve scanner tolerance.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters such as X‑dimension, quiet zone padding, and error correction level. It uses the BarcodeGenerator class to produce QR Code images, a common task for developers needing to embed scannable data in applications, websites, or printed media.
// Prompt: Generate QR Code barcode and configure quiet zone size to eight modules for scanner tolerance.
// Tags: qr code, quiet zone, barcode generation, aspose.barcode, image output

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a QR Code barcode, configures an eight‑module quiet zone, and saves the image to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the QR Code, applies custom padding, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Initialize a QR Code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the module (X‑dimension) size; 2 points per module provides a good default resolution.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Calculate quiet zone size: 8 modules × XDimension.
            float quietZone = 8f * generator.Parameters.Barcode.XDimension.Point;

            // Apply the calculated quiet zone as padding on all four sides of the barcode.
            generator.Parameters.Barcode.Padding.Left.Point   = quietZone;
            generator.Parameters.Barcode.Padding.Top.Point    = quietZone;
            generator.Parameters.Barcode.Padding.Right.Point  = quietZone;
            generator.Parameters.Barcode.Padding.Bottom.Point = quietZone;

            // Optional: increase error correction level to high for better data recovery.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR Code image to a PNG file.
            generator.Save("qr.png");
        }

        // Inform the user that the QR Code has been generated.
        Console.WriteLine("QR Code generated and saved as 'qr.png'.");
    }
}