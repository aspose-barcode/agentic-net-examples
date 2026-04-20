using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare Mailmark codetext with valid sample data
        var mailmark = new MailmarkCodetext();
        mailmark.Format = 4;                     // 4‑state format
        mailmark.VersionID = 1;                  // version
        mailmark.Class = "0";                    // test/null class
        mailmark.SupplychainID = 384224;         // example supply chain ID
        mailmark.ItemID = 16563762;              // example item ID
        mailmark.DestinationPostCodePlusDPS = "EF61AH8T "; // known‑valid destination

        // Create ComplexBarcodeGenerator for Mailmark
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Ensure fixed size (no auto‑sizing) so BarHeight is respected
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set a small bar height suitable for small labels (points)
            generator.Parameters.Barcode.BarHeight.Point = 5f;

            // Reduce module width to make the barcode more compact
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Optional: minimal padding to save space
            generator.Parameters.Barcode.Padding.Left.Point = 0f;
            generator.Parameters.Barcode.Padding.Top.Point = 0f;
            generator.Parameters.Barcode.Padding.Right.Point = 0f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 0f;

            // Optional: set foreground and background colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode image
            const string outputPath = "MailmarkCompact.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Mailmark barcode saved to {outputPath}");
        }
    }
}