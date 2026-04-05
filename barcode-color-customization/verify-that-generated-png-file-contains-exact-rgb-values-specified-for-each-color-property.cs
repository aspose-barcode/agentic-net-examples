using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Expected colors
        Color expectedBarColor = Color.FromArgb(0, 0, 255);      // Blue
        Color expectedBackColor = Color.FromArgb(255, 255, 0);   // Yellow
        string filePath = "barcode.png";

        // Generate barcode with specified colors
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Parameters.Barcode.BarColor = expectedBarColor;
            generator.Parameters.BackColor = expectedBackColor;
            generator.Save(filePath);
        }

        // Load the generated image
        using (var bitmap = new Bitmap(filePath))
        {
            // Verify background color (top‑left pixel)
            Color actualBackColor = bitmap.GetPixel(0, 0);
            bool backMatch = actualBackColor.ToArgb() == expectedBackColor.ToArgb();

            // Find a bar pixel by scanning the middle row
            int middleY = bitmap.Height / 2;
            Color actualBarColor = expectedBackColor; // default fallback
            for (int x = 0; x < bitmap.Width; x++)
            {
                Color pixel = bitmap.GetPixel(x, middleY);
                if (pixel.ToArgb() != expectedBackColor.ToArgb())
                {
                    actualBarColor = pixel;
                    break;
                }
            }
            bool barMatch = actualBarColor.ToArgb() == expectedBarColor.ToArgb();

            Console.WriteLine($"Background color matches: {backMatch}");
            Console.WriteLine($"Bar (foreground) color matches: {barMatch}");
        }

        // Optional: read the barcode to ensure it was generated correctly
        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected barcode text: {result.CodeText}");
            }
        }
    }
}