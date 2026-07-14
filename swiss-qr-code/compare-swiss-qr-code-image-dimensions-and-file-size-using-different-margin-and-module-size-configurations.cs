// Title: Compare Swiss QR Code image dimensions and file size with varying margins and module sizes
// Description: Demonstrates how different margin and module size settings affect the generated Swiss QR Code image dimensions and PNG file size.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on Swiss QR Code creation using ComplexBarcodeGenerator. It shows how to configure padding and XDimension (module size) to control image size, a common requirement for developers generating payment QR codes for Swiss QR‑bill standards.
// Prompt: Compare Swiss QR Code image dimensions and file size using different margin and module size configurations.
// Tags: swiss qr code, barcode generation, image dimensions, file size, margin, module size, aspnet.barcode, complexbarcodegenerator, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how margin and module size affect Swiss QR Code image dimensions and file size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates Swiss QR Codes with default and custom configurations and prints their dimensions and file sizes.
    /// </summary>
    static void Main()
    {
        // Define two configurations: a default setting and a custom one with larger margin and module size.
        var configs = new (string Name, float Margin, float ModuleSize)[]
        {
            ("Default", 5f, 2f),   // small margin, default module size
            ("Custom", 20f, 5f)    // larger margin and larger modules
        };

        // Iterate over each configuration, generate the barcode, and display results.
        foreach (var cfg in configs)
        {
            try
            {
                var result = GenerateSwissQR(cfg.Name, cfg.Margin, cfg.ModuleSize);
                Console.WriteLine($"{cfg.Name} Configuration:");
                Console.WriteLine($"  Image Width : {result.Width} px");
                Console.WriteLine($"  Image Height: {result.Height} px");
                Console.WriteLine($"  File Size   : {result.FileSize} bytes");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating {cfg.Name} configuration: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Generates a Swiss QR Code using the specified margin and module size, then returns its dimensions and PNG file size.
    /// </summary>
    /// <param name="configName">Name of the configuration (used for logging only).</param>
    /// <param name="margin">Padding (margin) to apply on all sides, in points.</param>
    /// <param name="moduleSize">Size of a single QR module (XDimension), in points.</param>
    /// <returns>Tuple containing image width, height, and file size in bytes.</returns>
    private static (int Width, int Height, long FileSize) GenerateSwissQR(string configName, float margin, float moduleSize)
    {
        // Prepare Swiss QR code text with mandatory bill fields.
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create a ComplexBarcodeGenerator for the Swiss QR code.
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Apply uniform padding (margin) on all sides.
            generator.Parameters.Barcode.Padding.Left.Point = margin;
            generator.Parameters.Barcode.Padding.Top.Point = margin;
            generator.Parameters.Barcode.Padding.Right.Point = margin;
            generator.Parameters.Barcode.Padding.Bottom.Point = margin;

            // Set the module size (XDimension) for the QR code.
            generator.Parameters.Barcode.XDimension.Point = moduleSize;

            // Generate the barcode image.
            using (Image image = generator.GenerateBarCodeImage())
            {
                // Save the image to a memory stream to determine the PNG file size.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    long fileSize = ms.Length;

                    // Return the image dimensions and file size.
                    return (image.Width, image.Height, fileSize);
                }
            }
        }
    }
}