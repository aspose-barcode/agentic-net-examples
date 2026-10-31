using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const float MIN_MODULE_SIZE = 0.5f; // points

        string codeText = "ASPOSE";
        string outputPath = Path.Combine(Path.GetTempPath(), "DataMatrix.png");

        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Set module (XDimension) size in points
            generator.Parameters.Barcode.XDimension.Point = 0.4f;

            // Validate module size against minimum requirement
            float moduleSize = generator.Parameters.Barcode.XDimension.Point;
            if (moduleSize < MIN_MODULE_SIZE)
            {
                Console.WriteLine($"Module size {moduleSize}pt is below the minimum {MIN_MODULE_SIZE}pt for high‑density printing.");
            }
            else
            {
                Console.WriteLine($"Module size {moduleSize}pt meets the minimum requirement.");
            }

            // Generate and save the barcode image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Load the generated image to report its pixel dimensions
        using (var image = Image.FromFile(outputPath))
        {
            Console.WriteLine($"Generated barcode image size: {image.Width}x{image.Height} pixels.");
        }

        // Clean up the temporary file
        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }
    }
}