using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a QR code generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set a top caption
            generator.Parameters.CaptionAbove.Text = "Sample QR Code";

            // Align the top caption to center
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Save the barcode image
            generator.Save("qr_with_caption.png");
        }
    }
}