using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string outputPath = "lowres_barcode.png";

        // Ensure the output directory exists
        string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set low resolution to 72 DPI
            generator.Parameters.Resolution = 72f;

            // Define image size (optional, helps visibility at low DPI)
            generator.Parameters.ImageWidth.Point = 200f;
            generator.Parameters.ImageHeight.Point = 100f;

            // Save the barcode image
            generator.Save(outputPath);
        }

        // Verify the saved image's resolution using Aspose.Drawing
        if (File.Exists(outputPath))
        {
            using (var image = Image.FromFile(outputPath))
            {
                float horizontalDpi = image.HorizontalResolution;
                float verticalDpi = image.VerticalResolution;

                Console.WriteLine($"Saved barcode resolution: {horizontalDpi} DPI (horizontal), {verticalDpi} DPI (vertical)");
                Console.WriteLine($"Image dimensions: {image.Width}x{image.Height} pixels");
            }
        }
        else
        {
            Console.WriteLine("Barcode image was not created.");
        }
    }
}