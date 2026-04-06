using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const float expectedWidthPixels = 300f;
        const string fileName = "barcode.png";

        // Create barcode generator, set width in pixels and save the image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Pixels = expectedWidthPixels;
            generator.CodeText = "Test123";
            generator.Save(fileName);
        }

        // Load the saved image and verify its pixel width
        using (var image = Image.FromFile(fileName))
        {
            int actualWidth = image.Width;
            if (actualWidth == (int)expectedWidthPixels)
            {
                Console.WriteLine("PASS: BarCodeWidth matches expected pixel width.");
            }
            else
            {
                Console.WriteLine($"FAIL: Expected width {expectedWidthPixels}px, but got {actualWidth}px.");
            }
        }
    }
}