// Title: Export barcodes with custom text alignment and spacing to PNG
// Description: Demonstrates how to generate Code128, QR, and DataMatrix barcodes with customized human‑readable text positioning, alignment, and spacing, then save them as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and related parameter classes (Barcode, CodeTextParameters, Padding) to control visual appearance of barcodes. Typical use cases include creating printable barcode assets with precise layout requirements, such as aligning text above or below the symbol and adjusting margins. Developers often need to fine‑tune colors, module size, and padding to match branding or packaging specifications.
// Prompt: Export barcodes with customized text to PNG format, preserving alignment and spacing settings in the image.
// Tags: barcode, code128, qr, datamatrix, text alignment, spacing, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates exporting barcodes with customized text alignment and spacing to PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates sample barcodes and saves them as PNG images.
    /// </summary>
    static void Main()
    {
        // Determine output folder for generated barcode images
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            // Create the folder if it does not exist
            Directory.CreateDirectory(outputFolder);
        }

        // ------------------------------------------------------------
        // Example 1: Code128 barcode with custom text alignment and spacing
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC-1234"))
        {
            // Set barcode and background colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Define module size (XDimension) and bar height
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 50f;

            // Configure human‑readable text: location below the barcode, centered, with spacing
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 5f; // space between barcode and text

            // Set individual paddings (left, top, right, bottom) around the barcode image
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // Save the barcode as a PNG file
            string filePath = Path.Combine(outputFolder, "code128.png");
            generator.Save(filePath, BarCodeImageFormat.Png);
            Console.WriteLine($"Generated: {filePath}");
        }

        // ------------------------------------------------------------
        // Example 2: QR code with custom text alignment and spacing
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set barcode and background colors
            generator.Parameters.Barcode.BarColor = Color.DarkBlue;
            generator.Parameters.BackColor = Color.LightYellow;

            // Define module size; QR codes ignore BarHeight
            generator.Parameters.Barcode.XDimension.Point = 3f;

            // Configure human‑readable text: location above the QR code, right‑aligned, with spacing
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 8f;

            // Set uniform padding around the QR code image
            generator.Parameters.Barcode.Padding.Left.Point = 12f;
            generator.Parameters.Barcode.Padding.Top.Point = 12f;
            generator.Parameters.Barcode.Padding.Right.Point = 12f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 12f;

            // Save the QR code as a PNG file
            string filePath = Path.Combine(outputFolder, "qr.png");
            generator.Save(filePath, BarCodeImageFormat.Png);
            Console.WriteLine($"Generated: {filePath}");
        }

        // ------------------------------------------------------------
        // Example 3: DataMatrix with custom text alignment and spacing
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "DataMatrix123"))
        {
            // Set barcode and background colors
            generator.Parameters.Barcode.BarColor = Color.Green;
            generator.Parameters.BackColor = Color.White;

            // Define module size for DataMatrix
            generator.Parameters.Barcode.XDimension.Point = 1.5f;

            // Configure human‑readable text: location below the symbol, left‑aligned, with spacing
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 4f;

            // Set uniform padding around the DataMatrix image
            generator.Parameters.Barcode.Padding.Left.Point = 8f;
            generator.Parameters.Barcode.Padding.Top.Point = 8f;
            generator.Parameters.Barcode.Padding.Right.Point = 8f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 8f;

            // Save the DataMatrix as a PNG file
            string filePath = Path.Combine(outputFolder, "datamatrix.png");
            generator.Save(filePath, BarCodeImageFormat.Png);
            Console.WriteLine($"Generated: {filePath}");
        }

        Console.WriteLine("Barcode generation completed.");
    }
}