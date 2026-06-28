using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of Swiss QR Code images with varying margins and module sizes,
/// and reports their dimensions and file sizes.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Swiss QR Code image with the specified margin (padding) and module size (XDimension).
    /// The image is saved to <paramref name="filePath"/> and the same path is returned.
    /// </summary>
    /// <param name="filePath">Full path where the PNG image will be saved.</param>
    /// <param name="marginPoints">Margin (padding) to apply on all sides, expressed in points.</param>
    /// <param name="moduleSizePoints">Size of a single QR module (XDimension), expressed in points.</param>
    /// <returns>The file path of the saved image.</returns>
    static string GenerateSwissQr(string filePath, float marginPoints, float moduleSizePoints)
    {
        // Prepare Swiss QR bill data (valid sample values)
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create a ComplexBarcodeGenerator for the Swiss QR data
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Apply uniform margin (padding) on all four sides
            generator.Parameters.Barcode.Padding.Left.Point = marginPoints;
            generator.Parameters.Barcode.Padding.Top.Point = marginPoints;
            generator.Parameters.Barcode.Padding.Right.Point = marginPoints;
            generator.Parameters.Barcode.Padding.Bottom.Point = marginPoints;

            // Set the module size (XDimension) for the QR code
            generator.Parameters.Barcode.XDimension.Point = moduleSizePoints;

            // Save the generated barcode as a PNG file
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        return filePath;
    }

    /// <summary>
    /// Retrieves the width and height (in pixels) of an image file.
    /// </summary>
    /// <param name="imagePath">Path to the image file.</param>
    /// <returns>A tuple containing the width and height.</returns>
    static (int width, int height) GetImageDimensions(string imagePath)
    {
        using (var img = Image.FromFile(imagePath))
        {
            return (img.Width, img.Height);
        }
    }

    /// <summary>
    /// Entry point of the application. Generates Swiss QR Code images with different
    /// margin and module size configurations, then prints their dimensions and file sizes.
    /// </summary>
    static void Main()
    {
        // Define configurations: (margin in points, module size in points)
        var configurations = new List<(float margin, float module)>
        {
            (5f, 2f),
            (5f, 3f),
            (10f, 2f),
            (10f, 3f)
        };

        // Create a temporary directory to store the generated images
        string outputDir = Path.Combine(Path.GetTempPath(), "SwissQrDemo");
        Directory.CreateDirectory(outputDir);

        // Header for console output
        Console.WriteLine("Swiss QR Code dimension and file size comparison:");
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("{0,8} {1,8} | {2,6} {3,6} | {4,10}", "Margin", "Module", "Width", "Height", "FileSize");

        // Process each configuration
        foreach (var cfg in configurations)
        {
            // Build a descriptive file name based on the current configuration
            string fileName = $"SwissQR_M{cfg.margin}_X{cfg.module}.png";
            string filePath = Path.Combine(outputDir, fileName);

            // Generate the QR code image
            GenerateSwissQr(filePath, cfg.margin, cfg.module);

            // Retrieve image dimensions
            var (width, height) = GetImageDimensions(filePath);

            // Determine file size in bytes
            long fileSize = new FileInfo(filePath).Length;

            // Output the results for this configuration
            Console.WriteLine("{0,8} {1,8} | {2,6} {3,6} | {4,10} bytes", cfg.margin, cfg.module, width, height, fileSize);
        }

        // Inform the user where the images have been saved
        Console.WriteLine("\nImages saved to: " + outputDir);
    }
}