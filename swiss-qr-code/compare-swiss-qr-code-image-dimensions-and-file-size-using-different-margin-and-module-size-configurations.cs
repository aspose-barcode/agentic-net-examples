// Title: Compare Swiss QR Code dimensions and file size with varying margins and module sizes
// Description: Demonstrates how to generate Swiss QR Code barcodes with different margin and module size settings, then compares the resulting image dimensions and file sizes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Swiss QR Code. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and image handling classes to adjust XDimension and padding. Developers often need to fine‑tune visual appearance and file size of generated barcodes for printing or digital distribution, and this snippet provides a clear pattern for experimenting with those parameters.
// Prompt: Compare Swiss QR Code image dimensions and file size using different margin and module size configurations.
// Tags: swiss qr code, barcode generation, image dimensions, file size, margin, module size, complexbarcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates Swiss QR Code images with different margin and module size configurations,
/// then outputs their dimensions and file sizes for comparison.
/// </summary>
class Program
{
    /// <summary>
    /// Simple container for margin and module size settings.
    /// </summary>
    struct Config
    {
        public float Margin;
        public float ModuleSize;
    }

    /// <summary>
    /// Entry point of the example. Creates output directory, generates barcodes,
    /// and prints image dimensions and file sizes for each configuration.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output folder
        string outputDir = Path.Combine(Path.GetTempPath(), "SwissQR_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define a set of configurations to compare
        Config[] configs = new Config[]
        {
            new Config { Margin = 2f, ModuleSize = 2f },
            new Config { Margin = 5f, ModuleSize = 4f },
            new Config { Margin = 10f, ModuleSize = 6f }
        };

        // Iterate over each configuration, generate the barcode, and collect metrics
        for (int i = 0; i < configs.Length; i++)
        {
            Config cfg = configs[i];

            // Build Swiss QR Code data (Swiss QR Bill version 2.0)
            var swissQRCode = new SwissQRCodetext();
            swissQRCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
            swissQRCode.Bill.Account = "CH9300762011623852957";
            swissQRCode.Bill.Amount = 199.95m;
            swissQRCode.Bill.Currency = "CHF";
            swissQRCode.Bill.Creditor = new Address
            {
                Name = "John Doe",
                CountryCode = "CH"
            };

            // Determine file path for the generated image
            string filePath = Path.Combine(outputDir, $"SwissQR_{i + 1}.png");

            // Generate the barcode with the current margin and module size settings
            using (var generator = new ComplexBarcodeGenerator(swissQRCode))
            {
                generator.Parameters.Barcode.XDimension.Pixels = cfg.ModuleSize;
                generator.Parameters.Barcode.Padding.Left.Point = cfg.Margin;
                generator.Parameters.Barcode.Padding.Top.Point = cfg.Margin;
                generator.Parameters.Barcode.Padding.Right.Point = cfg.Margin;
                generator.Parameters.Barcode.Padding.Bottom.Point = cfg.Margin;

                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Load the saved image to retrieve its dimensions
            int width, height;
            using (Image img = Image.FromFile(filePath))
            {
                width = img.Width;
                height = img.Height;
            }

            // Get the file size in bytes
            long fileSize = new FileInfo(filePath).Length;

            // Output the comparison results to the console
            Console.WriteLine($"Config {i + 1}: Margin={cfg.Margin}, ModuleSize={cfg.ModuleSize} => Width={width}px, Height={height}px, Size={fileSize} bytes");
        }
    }
}