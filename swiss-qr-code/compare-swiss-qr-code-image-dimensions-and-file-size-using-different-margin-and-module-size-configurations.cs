// Title: Swiss QR Code Image Dimension and File Size Comparison
// Description: Demonstrates how to generate Swiss QR Bill barcodes with different module sizes and margins, then compares the resulting image dimensions and PNG file sizes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcodes such as Swiss QR Bills. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and related parameter settings (XDimension, Padding) to control visual appearance. Developers commonly need to adjust module size and margins for branding or layout requirements and may need to assess the impact on image dimensions and file size.
// Prompt: Compare Swiss QR Code image dimensions and file size using different margin and module size configurations.
// Tags: swissqr, barcode, generation, image, file-size, margin, module-size, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Provides an example that generates Swiss QR Bill barcodes with varying module size and padding,
/// then outputs image dimensions and file size for comparison.
/// </summary>
class Program
{
    /// <summary>
    /// Simple container for image width, height and file size.
    /// </summary>
    struct ImageInfo
    {
        public int Width;
        public int Height;
        public long FileSize;
    }

    /// <summary>
    /// Generates a Swiss QR Bill barcode image using the specified module size (XDimension) and padding (margin).
    /// Returns the image dimensions and the PNG file size.
    /// </summary>
    /// <param name="xDimensionPixels">Module size in pixels.</param>
    /// <param name="paddingPoints">Uniform padding on all sides, expressed in points.</param>
    /// <returns>ImageInfo containing width, height and file size.</returns>
    static ImageInfo GenerateSwissQR(float xDimensionPixels, float paddingPoints)
    {
        // Create Swiss QR Bill codetext with required fields
        SwissQRCodetext swissQRCode = new SwissQRCodetext();
        swissQRCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQRCode.Bill.Account = "CH4431999123000889012";
        swissQRCode.Bill.Amount = 1000.25m;
        swissQRCode.Bill.Currency = "CHF";
        swissQRCode.Bill.Reference = "210000000003139471430009017";
        swissQRCode.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };
        swissQRCode.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Initialize the complex barcode generator with the Swiss QR code data
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(swissQRCode))
        {
            // Apply the requested module size
            generator.Parameters.Barcode.XDimension.Pixels = xDimensionPixels;

            // Apply uniform padding (margin) on all four sides
            generator.Parameters.Barcode.Padding.Left.Point = paddingPoints;
            generator.Parameters.Barcode.Padding.Right.Point = paddingPoints;
            generator.Parameters.Barcode.Padding.Top.Point = paddingPoints;
            generator.Parameters.Barcode.Padding.Bottom.Point = paddingPoints;

            // Generate a bitmap to obtain the actual image dimensions
            int width, height;
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                width = bitmap.Width;
                height = bitmap.Height;
            }

            // Save the barcode to a memory stream in PNG format to determine file size
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                long size = ms.Length;
                return new ImageInfo { Width = width, Height = height, FileSize = size };
            }
        }
    }

    /// <summary>
    /// Entry point that runs two configurations and prints a side‑by‑side comparison of width, height and PNG file size.
    /// </summary>
    static void Main()
    {
        // Configuration 1: smaller module size and smaller margin
        ImageInfo info1 = GenerateSwissQR(2f, 5f);

        // Configuration 2: larger module size and larger margin
        ImageInfo info2 = GenerateSwissQR(4f, 10f);

        // Output details for the first configuration
        Console.WriteLine("Configuration 1 (XDimension=2px, Padding=5pt):");
        Console.WriteLine($"  Width:  {info1.Width} px");
        Console.WriteLine($"  Height: {info1.Height} px");
        Console.WriteLine($"  File size: {info1.FileSize} bytes");
        Console.WriteLine();

        // Output details for the second configuration
        Console.WriteLine("Configuration 2 (XDimension=4px, Padding=10pt):");
        Console.WriteLine($"  Width:  {info2.Width} px");
        Console.WriteLine($"  Height: {info2.Height} px");
        Console.WriteLine($"  File size: {info2.FileSize} bytes");
        Console.WriteLine();

        // Simple numeric comparison between the two configurations
        Console.WriteLine("Comparison:");
        Console.WriteLine($"  Width increase: {(info2.Width - info1.Width)} px");
        Console.WriteLine($"  Height increase: {(info2.Height - info1.Height)} px");
        Console.WriteLine($"  File size increase: {(info2.FileSize - info1.FileSize)} bytes");
    }
}