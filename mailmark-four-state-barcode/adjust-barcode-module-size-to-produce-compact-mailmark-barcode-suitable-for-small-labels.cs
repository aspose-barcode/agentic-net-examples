// Title: Generate a compact Mailmark barcode for small labels
// Description: Demonstrates how to adjust the module size and padding of a Mailmark barcode to create a compact image suitable for small label printing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode symbologies such as Mailmark. It showcases the use of ComplexBarcodeGenerator, MailmarkCodetext, and barcode parameter settings like XDimension, AutoSizeMode, and padding. Developers often need to create high‑density barcodes for limited space applications, and this snippet provides a concise reference for those scenarios.
// Prompt: Adjust barcode module size to produce a compact Mailmark barcode suitable for small labels.
// Tags: mailmark, barcode, module size, compact, small labels, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Program demonstrating compact Mailmark barcode generation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Mailmark barcode with reduced module size and padding, then saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Prepare Mailmark codetext with required fields
        var mailmark = new MailmarkCodetext();
        mailmark.Format = 4; // unspecified/default format for compact barcode
        mailmark.VersionID = 1;
        mailmark.Class = "0";
        mailmark.SupplychainID = 384224;
        mailmark.ItemID = 16563762;
        mailmark.DestinationPostCodePlusDPS = "EF61AH8T ";

        // Create ComplexBarcodeGenerator for Mailmark
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Use interpolation auto-size mode so that XDimension controls overall size
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set a small module (dot) size for compactness
            generator.Parameters.Barcode.XDimension.Point = 0.5f; // 0.5 point per module

            // Reduce padding to keep the barcode tight on small labels
            generator.Parameters.Barcode.Padding.Left.Point = 1f;
            generator.Parameters.Barcode.Padding.Top.Point = 1f;
            generator.Parameters.Barcode.Padding.Right.Point = 1f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 1f;

            // Optional: set foreground and background colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Define output path
            string outputPath = "mailmark_compact.png";

            // Ensure the directory exists
            string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Save the barcode image
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Compact Mailmark barcode saved to: {outputPath}");
        }
    }
}