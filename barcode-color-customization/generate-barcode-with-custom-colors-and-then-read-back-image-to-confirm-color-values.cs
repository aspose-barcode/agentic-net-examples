using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string filePath = "custom_color_barcode.png";

        // Create a barcode generator with Code128 symbology and set the code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "ABC123";

            // Set custom colors: blue bars on a yellow background
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.BackColor = Color.Yellow;

            // Save the barcode image
            generator.Save(filePath);
        }

        // Load the saved image using Aspose.Drawing to verify colors
        using (var bitmap = new Bitmap(filePath))
        {
            // Sample a pixel from the top‑left corner (should be background)
            Color backgroundPixel = bitmap.GetPixel(0, 0);
            // Sample a pixel from the center (likely part of a bar)
            int centerX = bitmap.Width / 2;
            int centerY = bitmap.Height / 2;
            Color barPixel = bitmap.GetPixel(centerX, centerY);

            Console.WriteLine($"Background pixel color: A={backgroundPixel.A}, R={backgroundPixel.R}, G={backgroundPixel.G}, B={backgroundPixel.B}");
            Console.WriteLine($"Bar pixel color:        A={barPixel.A}, R={barPixel.R}, G={barPixel.G}, B={barPixel.B}");

            bool backgroundMatches = backgroundPixel.ToArgb() == Color.Yellow.ToArgb();
            bool barMatches = barPixel.ToArgb() == Color.Blue.ToArgb();

            Console.WriteLine($"Background color matches expected: {backgroundMatches}");
            Console.WriteLine($"Bar color matches expected:        {barMatches}");
        }

        // Optional: Use BarCodeReader to confirm the barcode can be read
        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Recognized CodeText: {result.CodeText}");
            }
        }
    }
}