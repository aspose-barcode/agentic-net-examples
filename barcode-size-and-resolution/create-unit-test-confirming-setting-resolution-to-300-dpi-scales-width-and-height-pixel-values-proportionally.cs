using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation at different resolutions and verifies scaling behavior.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates two barcodes (96 dpi and 300 dpi), compares their pixel dimensions,
    /// and reports whether the scaling matches the expected factor.
    /// </summary>
    static void Main()
    {
        // Sample barcode data to encode.
        const string codeText = "Test123";

        // Define temporary file paths for the generated PNG images.
        string tempPath1 = Path.Combine(Path.GetTempPath(), "barcode_96.png");
        string tempPath2 = Path.Combine(Path.GetTempPath(), "barcode_300.png");

        // ------------------------------------------------------------
        // Generate barcode with default resolution (96 dpi).
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Save(tempPath1, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Generate barcode with a higher resolution (300 dpi).
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the desired resolution before saving.
            generator.Parameters.Resolution = 300f; // 300 dpi
            generator.Save(tempPath2, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Load the generated images to retrieve their pixel dimensions.
        // ------------------------------------------------------------
        int width96, height96, width300, height300;
        using (var img96 = Image.FromFile(tempPath1))
        {
            width96 = img96.Width;
            height96 = img96.Height;
        }
        using (var img300 = Image.FromFile(tempPath2))
        {
            width300 = img300.Width;
            height300 = img300.Height;
        }

        // ------------------------------------------------------------
        // Compute the expected dimensions after scaling from 96 dpi to 300 dpi.
        // ------------------------------------------------------------
        double scaleFactor = 300.0 / 96.0;
        int expectedWidth = (int)Math.Round(width96 * scaleFactor);
        int expectedHeight = (int)Math.Round(height96 * scaleFactor);

        // ------------------------------------------------------------
        // Verify that the actual dimensions are within a 1‑pixel tolerance.
        // ------------------------------------------------------------
        bool widthMatches = Math.Abs(width300 - expectedWidth) <= 1;
        bool heightMatches = Math.Abs(height300 - expectedHeight) <= 1;

        if (widthMatches && heightMatches)
        {
            Console.WriteLine("PASS: Resolution scaling works as expected.");
            Console.WriteLine($"Original (96 dpi): {width96}x{height96} px");
            Console.WriteLine($"Scaled (300 dpi):  {width300}x{height300} px");
        }
        else
        {
            Console.WriteLine("FAIL: Resolution scaling did not produce expected dimensions.");
            Console.WriteLine($"Original (96 dpi): {width96}x{height96} px");
            Console.WriteLine($"Scaled (300 dpi):  {width300}x{height300} px");
            Console.WriteLine($"Expected (≈):      {expectedWidth}x{expectedHeight} px");
        }

        // ------------------------------------------------------------
        // Clean up temporary files.
        // ------------------------------------------------------------
        try { if (File.Exists(tempPath1)) File.Delete(tempPath1); } catch { }
        try { if (File.Exists(tempPath2)) File.Delete(tempPath2); } catch { }
    }
}