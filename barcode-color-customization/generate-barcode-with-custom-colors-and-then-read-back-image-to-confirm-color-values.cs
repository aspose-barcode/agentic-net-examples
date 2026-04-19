using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Path for the generated barcode image
        string imagePath = "custom_color_barcode.png";

        // Create a barcode generator with Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Set custom foreground (bars) and background colors
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.BackColor = Color.Yellow;

            // Save the barcode image to file
            generator.Save(imagePath);
        }

        // Verify that the image file was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the saved image and inspect pixel colors
        using (var bitmap = (Bitmap)Image.FromFile(imagePath))
        {
            // Sample a pixel from the top‑left corner (expected background)
            Color backgroundPixel = bitmap.GetPixel(0, 0);

            // Sample a pixel from the centre (likely part of a bar)
            int centerX = bitmap.Width / 2;
            int centerY = bitmap.Height / 2;
            Color barPixel = bitmap.GetPixel(centerX, centerY);

            // Output the ARGB values of the sampled pixels
            Console.WriteLine($"Background pixel ARGB: 0x{backgroundPixel.ToArgb():X8}");
            Console.WriteLine($"Bar pixel ARGB:        0x{barPixel.ToArgb():X8}");

            // Verify colors against the expected values
            bool backgroundMatches = backgroundPixel.ToArgb() == Color.Yellow.ToArgb();
            bool barMatches = barPixel.ToArgb() == Color.Blue.ToArgb();

            Console.WriteLine($"Background color matches expected: {backgroundMatches}");
            Console.WriteLine($"Bar color matches expected:        {barMatches}");
        }

        // Optionally, read the barcode to confirm it is still recognizable
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Recognized barcode type: {result.CodeTypeName}");
                Console.WriteLine($"Recognized barcode text: {result.CodeText}");
            }
        }
    }
}