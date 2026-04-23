using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a Planet barcode generator with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "12345"))
        {
            // Set XDimension to 0.75 millimeters
            generator.Parameters.Barcode.XDimension.Millimeters = 0.75f;

            // Save the barcode image
            string outputPath = "planet.png";
            generator.Save(outputPath);

            // Verify the XDimension value
            float xDimMm = generator.Parameters.Barcode.XDimension.Millimeters;
            Console.WriteLine($"XDimension set to: {xDimMm} mm");

            // Optionally, generate the bitmap and output its width in pixels
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                Console.WriteLine($"Generated image width: {bitmap.Width} pixels");
                Console.WriteLine($"Generated image height: {bitmap.Height} pixels");
            }
        }
    }
}