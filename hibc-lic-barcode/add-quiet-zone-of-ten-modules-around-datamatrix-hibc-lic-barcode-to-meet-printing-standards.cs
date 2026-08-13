// Title: Adding a Quiet Zone to a DataMatrix HIBC LIC Barcode
// Description: Demonstrates how to configure a DataMatrix HIBC LIC barcode with a ten‑module quiet zone using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameters such as XDimension and Padding. Typical use cases include creating compliant HIBC‑LIC barcodes for medical device labeling where a specific quiet zone is required. Developers often need to adjust module size, colors, and padding to meet printing standards.
// Prompt: Add a quiet zone of ten modules around a DataMatrix HIBC LIC barcode to meet printing standards.
// Tags: datamatrix, hibc, quiet zone, png, aspose.barcodes, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Program demonstrating adding a quiet zone to a DataMatrix HIBC LIC barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, applies a ten‑module quiet zone, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Sample HIBC LIC DataMatrix code text (Labeler ID + Product Number)
        const string codeText = "A99912345";

        // Create the barcode generator for HIBC DataMatrix LIC
        using (var generator = new BarcodeGenerator(EncodeTypes.HIBCDataMatrixLIC, codeText))
        {
            // Set module size (XDimension) – 2 points per module
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Calculate quiet zone size: 10 modules * XDimension
            float quietZone = 10f * generator.Parameters.Barcode.XDimension.Point;

            // Apply the quiet zone to all sides of the barcode
            generator.Parameters.Barcode.Padding.Left.Point = quietZone;
            generator.Parameters.Barcode.Padding.Top.Point = quietZone;
            generator.Parameters.Barcode.Padding.Right.Point = quietZone;
            generator.Parameters.Barcode.Padding.Bottom.Point = quietZone;

            // Optional: set foreground and background colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Define output file path
            string outputPath = "hibc_datamatrix.png";

            // Ensure the output directory exists
            string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}