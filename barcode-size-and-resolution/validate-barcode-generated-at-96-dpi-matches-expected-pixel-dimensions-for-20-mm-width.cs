using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Desired barcode width in millimeters
        float widthMillimeters = 20f;
        // Resolution in dots per inch (dpi)
        float resolutionDpi = 96f;
        // Convert millimeters to pixels: 1 inch = 25.4 mm
        float expectedPixels = widthMillimeters * resolutionDpi / 25.4f;

        // Create a barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set resolution (default is 96 dpi, set explicitly for clarity)
            generator.Parameters.Resolution = resolutionDpi;
            // Set the image width to the calculated pixel value
            generator.Parameters.ImageWidth.Pixels = expectedPixels;
            // Set the code text to encode
            generator.CodeText = "12345";

            // Save the barcode image
            generator.Save("barcode.png");

            // Retrieve the actual pixel width from the generator settings
            float actualPixels = generator.Parameters.ImageWidth.Pixels;

            // Validate that the actual width matches the expected width within a small tolerance
            const float tolerance = 0.5f;
            if (Math.Abs(actualPixels - expectedPixels) <= tolerance)
            {
                Console.WriteLine("Barcode width matches expected pixel dimensions.");
            }
            else
            {
                Console.WriteLine($"Mismatch: expected {expectedPixels:F2} px, actual {actualPixels:F2} px.");
            }
        }
    }
}