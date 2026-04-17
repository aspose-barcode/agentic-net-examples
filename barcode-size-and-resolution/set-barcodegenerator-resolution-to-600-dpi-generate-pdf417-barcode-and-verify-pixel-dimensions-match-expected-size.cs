using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Output file path
        string outputPath = "pdf417.png";

        // Create and configure the barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Hello Aspose PDF417"))
        {
            // Set resolution to 600 dpi
            generator.Parameters.Resolution = 600f;

            // Define expected pixel dimensions
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 150f;

            // Save the barcode image
            generator.Save(outputPath);
        }

        // Verify that the file was created
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Error: File '{outputPath}' was not created.");
            return;
        }

        // Load the generated image and check its dimensions
        using (var image = Image.FromFile(outputPath))
        {
            int expectedWidth = 300;
            int expectedHeight = 150;

            if (image.Width != expectedWidth || image.Height != expectedHeight)
            {
                Console.WriteLine($"Dimension mismatch: expected {expectedWidth}x{expectedHeight}, got {image.Width}x{image.Height}");
            }
            else
            {
                Console.WriteLine($"Barcode generated with correct dimensions: {image.Width}x{image.Height}");
            }
        }
    }
}