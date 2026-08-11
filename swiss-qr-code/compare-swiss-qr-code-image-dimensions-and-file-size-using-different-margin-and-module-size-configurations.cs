// Title: Swiss QR Code Image Dimension and File Size Comparison
// Description: Demonstrates how to generate Swiss QR codes with varying padding and module size, then reports the resulting image dimensions and PNG file size.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and related parameter settings to create Swiss QR Bill barcodes. Typical use cases include generating payment QR codes with custom visual appearance, where developers need to control margins and module dimensions to meet branding or layout requirements. The example illustrates how to retrieve image size information and file size for different configurations, a common task when optimizing barcode rendering for web or print.
/// Prompt: Compare Swiss QR Code image dimensions and file size using different margin and module size configurations.
// Tags: swiss qr, barcode, image, file size, padding, module size, aspose.barcode, complexbarcode, png

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that compares Swiss QR Code image dimensions and file sizes
/// using different margin (padding) and module size configurations.
/// </summary>
class Program
{
    /// <summary>
    /// Executes the comparison and writes results to the console.
    /// </summary>
    static void Main()
    {
        // Define a set of configurations to test (padding in points, module size in points)
        var configurations = new (float Padding, float XDimension)[]
        {
            (5f, 2f),
            (10f, 3f),
            (15f, 4f)
        };

        // Iterate through each configuration, generate the QR code, and display metrics
        foreach (var config in configurations)
        {
            var result = GenerateSwissQr(config.Padding, config.XDimension);
            Console.WriteLine($"Padding: {config.Padding}pt, XDimension: {config.XDimension}pt");
            Console.WriteLine($"  Image Width: {result.Width}px, Height: {result.Height}px");
            Console.WriteLine($"  File Size: {result.FileSize} bytes");
            Console.WriteLine();
        }
    }

    // Generates a Swiss QR code with specified padding and module size.
    // Returns image width, height and the size of the PNG file in bytes.
    private static (int Width, int Height, long FileSize) GenerateSwissQr(float paddingPoints, float xDimensionPoints)
    {
        // Prepare Swiss QR bill data (mandatory fields)
        var qrCodeText = new SwissQRCodetext();
        qrCodeText.Bill.Creditor.Name = "John Doe";
        qrCodeText.Bill.Creditor.CountryCode = "CH";
        qrCodeText.Bill.Account = "CH9300762011623852957";
        qrCodeText.Bill.Amount = 199.95m;
        qrCodeText.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create the complex barcode generator with the prepared data
        using (var generator = new ComplexBarcodeGenerator(qrCodeText))
        {
            // Apply uniform padding (margin) on all sides
            generator.Parameters.Barcode.Padding.Left.Point = paddingPoints;
            generator.Parameters.Barcode.Padding.Top.Point = paddingPoints;
            generator.Parameters.Barcode.Padding.Right.Point = paddingPoints;
            generator.Parameters.Barcode.Padding.Bottom.Point = paddingPoints;

            // Set the module (X) dimension, which controls the size of each QR code square
            generator.Parameters.Barcode.XDimension.Point = xDimensionPoints;

            // Generate the barcode image as a bitmap to obtain pixel dimensions
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                int width = bitmap.Width;
                int height = bitmap.Height;

                // Save the bitmap to a memory stream in PNG format to determine file size
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    long size = ms.Length;
                    return (width, height, size);
                }
            }
        }
    }
}