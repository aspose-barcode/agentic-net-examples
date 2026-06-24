using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code with a centered caption above it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a QR code, adds a top caption, centers it, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a QR code generator with the desired text content.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set the caption text that will appear above the QR code.
            generator.Parameters.CaptionAbove.Text = "Top Caption";

            // Align the above caption to the center of the barcode image.
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Save the generated barcode (including the caption) as a PNG file.
            generator.Save("qr_with_centered_top_caption.png");
        }

        // Inform the user that the QR code image has been created successfully.
        Console.WriteLine("QR code with centered top caption generated successfully.");
    }
}