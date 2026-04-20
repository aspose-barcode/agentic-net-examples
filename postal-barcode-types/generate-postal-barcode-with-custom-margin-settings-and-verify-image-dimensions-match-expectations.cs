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
        // Define output file
        string outputPath = "postal_barcode.png";

        // Create barcode generator for Postnet (postal barcode)
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345"))
        {
            // Set custom image size in points
            generator.Parameters.ImageWidth.Point = 300f;   // 300 points width
            generator.Parameters.ImageHeight.Point = 150f; // 150 points height

            // Set custom padding (margins) in points
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 15f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 15f;

            // Ensure AutoSizeMode is None so ImageWidth/ImageHeight are respected
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Save the barcode image
            generator.Save(outputPath);
        }

        // Verify the saved image dimensions
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Error: Barcode image was not created.");
            return;
        }

        using (var image = (Bitmap)Image.FromFile(outputPath))
        {
            // Retrieve actual pixel dimensions
            int actualWidth = image.Width;
            int actualHeight = image.Height;

            // Calculate expected pixel dimensions based on points and resolution
            // 1 point = 1/72 inch
            // Pixels = inches * DPI
            float dpi = 96f; // default resolution
            int expectedWidth = (int)Math.Round((300f / 72f) * dpi);
            int expectedHeight = (int)Math.Round((150f / 72f) * dpi);

            // Output verification result
            Console.WriteLine($"Actual dimensions: {actualWidth}x{actualHeight} pixels");
            Console.WriteLine($"Expected dimensions: {expectedWidth}x{expectedHeight} pixels");

            if (actualWidth == expectedWidth && actualHeight == expectedHeight)
            {
                Console.WriteLine("Image dimensions match expectations.");
            }
            else
            {
                Console.WriteLine("Image dimensions do NOT match expectations.");
            }
        }
    }
}