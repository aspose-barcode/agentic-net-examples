using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample barcode data
        const string codeText = "ABC1234567890";

        // Simulated user selections
        // Measurement unit: Points (could be Millimeters, Inches, etc.)
        // Resolution: 300 DPI
        const float resolutionDpi = 300f;
        const float imageWidthPoints = 300f;
        const float imageHeightPoints = 150f;
        const float xDimensionPoints = 2f;
        const float barHeightPoints = 50f;

        // Create barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set resolution
            generator.Parameters.Resolution = resolutionDpi;

            // Set measurement units using .Point members
            generator.Parameters.ImageWidth.Point = imageWidthPoints;
            generator.Parameters.ImageHeight.Point = imageHeightPoints;
            generator.Parameters.Barcode.XDimension.Point = xDimensionPoints;
            generator.Parameters.Barcode.BarHeight.Point = barHeightPoints;

            // Optional: set padding (5 points on each side by default)
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // Ensure AutoSizeMode is None to respect explicit dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Save barcode image to file
            const string outputFile = "barcode.png";
            generator.Save(outputFile, BarCodeImageFormat.Png);

            // Generate simple HTML snippet referencing the image
            string html = $"<html><body><h3>Generated Barcode</h3><img src=\"{outputFile}\" alt=\"Barcode\" /></body></html>";

            // Write HTML to console (could be saved to a .html file if needed)
            Console.WriteLine(html);
        }
    }
}