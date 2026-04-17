using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Desired barcode width in millimeters
        const float targetWidthMm = 20f;
        // Desired resolution in DPI
        const float resolutionDpi = 96f;

        // Calculate expected pixel width: (mm / 25.4) * DPI, rounded to nearest integer
        int expectedPixelWidth = (int)Math.Round(targetWidthMm / 25.4f * resolutionDpi);

        // Create a barcode generator (Code128 used as example)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set image width to 20 mm
            generator.Parameters.ImageWidth.Millimeters = targetWidthMm;
            // Set resolution to 96 DPI
            generator.Parameters.Resolution = resolutionDpi;

            // Generate the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                int actualPixelWidth = bitmap.Width;

                Console.WriteLine($"Expected pixel width: {expectedPixelWidth}");
                Console.WriteLine($"Actual pixel width:   {actualPixelWidth}");

                if (actualPixelWidth == expectedPixelWidth)
                {
                    Console.WriteLine("Validation passed: pixel width matches expected value.");
                }
                else
                {
                    Console.WriteLine("Validation failed: pixel width does not match expected value.");
                }
            }
        }
    }
}