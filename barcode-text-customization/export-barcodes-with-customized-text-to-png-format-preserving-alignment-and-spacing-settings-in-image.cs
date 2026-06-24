using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of various barcode types with custom styling using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates Code128, QR, and DataMatrix barcodes with
    /// custom captions, padding, colors, and other visual settings, then saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory for generated barcode images
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // --------------------------------------------------------------------
        // Example 1: Generate a Code128 barcode with custom caption and padding
        // --------------------------------------------------------------------
        string code128Path = Path.Combine(outputDir, "code128.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC-1234"))
        {
            // Set barcode and background colors
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.BackColor = Color.White;

            // Define padding around the barcode (left, top, right, bottom) in points
            generator.Parameters.Barcode.Padding.Left.Point   = 10f;
            generator.Parameters.Barcode.Padding.Top.Point    = 10f;
            generator.Parameters.Barcode.Padding.Right.Point  = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Configure caption displayed above the barcode
            generator.Parameters.CaptionAbove.Text          = "Product Code";
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;
            generator.Parameters.CaptionAbove.TextColor    = Color.DarkGreen;
            generator.Parameters.CaptionAbove.Alignment   = TextAlignment.Center;

            // Set alignment and styling for the human‑readable code text
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
            generator.Parameters.Barcode.CodeTextParameters.Color     = Color.Black;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Consolas";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;

            // Save the barcode image as PNG
            generator.Save(code128Path, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Example 2: Generate a QR code with custom caption, padding, and error correction
        // --------------------------------------------------------------------
        string qrPath = Path.Combine(outputDir, "qr.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set high error correction level for the QR code
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Define padding around the QR code
            generator.Parameters.Barcode.Padding.Left.Point   = 5f;
            generator.Parameters.Barcode.Padding.Top.Point    = 5f;
            generator.Parameters.Barcode.Padding.Right.Point  = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // Configure caption displayed below the QR code
            generator.Parameters.CaptionBelow.Text          = "Visit Our Site";
            generator.Parameters.CaptionBelow.Font.FamilyName = "Calibri";
            generator.Parameters.CaptionBelow.Font.Size.Point = 11f;
            generator.Parameters.CaptionBelow.TextColor    = Color.Maroon;
            generator.Parameters.CaptionBelow.Alignment   = TextAlignment.Center;

            // Save the QR code image as PNG
            generator.Save(qrPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Example 3: Generate a DataMatrix barcode with custom caption and spacing
        // --------------------------------------------------------------------
        string dmPath = Path.Combine(outputDir, "datamatrix.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleData123"))
        {
            // Use nearest auto‑size mode to fit the content within specified dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Point  = 200f;
            generator.Parameters.ImageHeight.Point = 200f;

            // Define padding around the DataMatrix barcode
            generator.Parameters.Barcode.Padding.Left.Point   = 8f;
            generator.Parameters.Barcode.Padding.Top.Point    = 8f;
            generator.Parameters.Barcode.Padding.Right.Point  = 8f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 8f;

            // Configure caption displayed above the DataMatrix barcode
            generator.Parameters.CaptionAbove.Text          = "DataMatrix Sample";
            generator.Parameters.CaptionAbove.Font.FamilyName = "Tahoma";
            generator.Parameters.CaptionAbove.Font.Size.Point = 10f;
            generator.Parameters.CaptionAbove.TextColor    = Color.Purple;
            generator.Parameters.CaptionAbove.Alignment   = TextAlignment.Center;

            // Save the DataMatrix image as PNG
            generator.Save(dmPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Inform the user where the barcode images have been saved
        // --------------------------------------------------------------------
        Console.WriteLine("Barcodes have been generated in: " + outputDir);
    }
}