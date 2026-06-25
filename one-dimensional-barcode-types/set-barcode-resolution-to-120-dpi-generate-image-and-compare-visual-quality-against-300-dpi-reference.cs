using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Code128 barcodes at different DPI settings
/// and comparing the resulting image files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates two barcode images (120 DPI and 300 DPI),
    /// verifies their creation, displays their properties,
    /// and performs a simple file‑size comparison.
    /// </summary>
    static void Main()
    {
        // Barcode data to encode
        string codeText = "1234567890";

        // Destination file names for the generated images
        string path120 = "barcode_120.png";
        string path300 = "barcode_300.png";

        // ------------------------------------------------------------
        // Generate a barcode image at 120 DPI
        // ------------------------------------------------------------
        using (var generator120 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator120.Parameters.Resolution = 120f; // set image resolution to 120 DPI
            generator120.Save(path120);               // write the image to disk
        }

        // ------------------------------------------------------------
        // Generate a barcode image at 300 DPI (higher resolution)
        // ------------------------------------------------------------
        using (var generator300 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator300.Parameters.Resolution = 300f; // set image resolution to 300 DPI
            generator300.Save(path300);                // write the image to disk
        }

        // Verify that both image files were successfully created
        if (!File.Exists(path120) || !File.Exists(path300))
        {
            Console.WriteLine("Failed to generate one or both barcode images.");
            return;
        }

        // ------------------------------------------------------------
        // Load the generated images and output their dimensions,
        // resolution, and file size
        // ------------------------------------------------------------
        using (var img120 = Image.FromFile(path120))
        using (var img300 = Image.FromFile(path300))
        {
            Console.WriteLine(
                $"120 DPI image: {img120.Width}x{img120.Height} px, " +
                $"HRes={img120.HorizontalResolution}, VRes={img120.VerticalResolution}, " +
                $"Size={new FileInfo(path120).Length} bytes");

            Console.WriteLine(
                $"300 DPI image: {img300.Width}x{img300.Height} px, " +
                $"HRes={img300.HorizontalResolution}, VRes={img300.VerticalResolution}, " +
                $"Size={new FileInfo(path300).Length} bytes");
        }

        // ------------------------------------------------------------
        // Simple comparison based on file size (higher DPI usually yields larger file)
        // ------------------------------------------------------------
        long size120 = new FileInfo(path120).Length;
        long size300 = new FileInfo(path300).Length;

        if (size300 > size120)
        {
            Console.WriteLine("Higher DPI image has larger file size, indicating higher visual detail.");
        }
        else
        {
            Console.WriteLine("File size comparison does not show increased visual detail.");
        }
    }
}