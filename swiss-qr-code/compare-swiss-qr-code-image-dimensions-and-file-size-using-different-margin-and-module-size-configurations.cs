// Title: Compare Swiss QR Code image dimensions and file size with varying margins and module sizes
// Description: Demonstrates how to generate Swiss QR Code barcodes with different margin and module size settings, then outputs the resulting image dimensions and PNG file size.
// Category-Description: Shows Aspose.BarCode complex barcode generation for Swiss QR Code, covering configuration of padding and X‑dimension, image rendering, and size measurement. Useful for developers needing to fine‑tune QR code appearance and evaluate storage impact. Includes classes ComplexBarcodeGenerator, SwissQRCodetext, BarcodeImageFormat and related parameters.
// Prompt: Compare Swiss QR Code image dimensions and file size using different margin and module size configurations.
// Tags: barcode, swiss qr code, image dimensions, file size, margin, module size, aspose.barcode, complexbarcodegenerator, png

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates Swiss QR Code barcodes with various margin and module size settings,
/// then reports the resulting image dimensions and PNG file size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Prepares Swiss QR Code data, iterates over different configurations,
    /// generates barcode images, and prints width, height, and file size for each configuration.
    /// </summary>
    static void Main()
    {
        // Prepare Swiss QR Code data (bill information)
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Reference = "210000000003139471430009017";
        swissQr.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };
        swissQr.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Define configurations: (margin in points, module size in points)
        var configurations = new List<(float margin, float moduleSize)>
        {
            (5f, 2f),
            (10f, 3f),
            (15f, 4f)
        };

        // Use ComplexBarcodeGenerator to create the barcode with each configuration
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            foreach (var config in configurations)
            {
                // Apply uniform margin (padding) on all sides
                generator.Parameters.Barcode.Padding.Left.Point = config.margin;
                generator.Parameters.Barcode.Padding.Top.Point = config.margin;
                generator.Parameters.Barcode.Padding.Right.Point = config.margin;
                generator.Parameters.Barcode.Padding.Bottom.Point = config.margin;

                // Set the module size (X dimension) which influences overall size
                generator.Parameters.Barcode.XDimension.Point = config.moduleSize;

                // Generate bitmap to obtain pixel dimensions
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    int width = bitmap.Width;
                    int height = bitmap.Height;

                    // Save barcode to a memory stream in PNG format to measure file size
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        long fileSize = ms.Length;

                        // Output the configuration and resulting metrics
                        Console.WriteLine($"Margin: {config.margin} pt, ModuleSize: {config.moduleSize} pt => Width: {width}px, Height: {height}px, Size: {fileSize} bytes");
                    }
                }
            }
        }
    }
}