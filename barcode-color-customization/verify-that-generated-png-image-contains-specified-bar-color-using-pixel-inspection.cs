using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define the barcode text and the desired bar color
        const string barcodeText = "1234567890";
        Color barColor = Color.Red;

        // Generate the barcode image with the specified bar color
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
        {
            generator.Parameters.Barcode.BarColor = barColor;
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Save the image (optional, for visual verification)
                const string filePath = "barcode.png";
                barcodeBitmap.Save(filePath);

                // Verify that the image contains at least one pixel with the bar color
                bool containsBarColor = false;
                for (int y = 0; y < barcodeBitmap.Height && !containsBarColor; y++)
                {
                    for (int x = 0; x < barcodeBitmap.Width; x++)
                    {
                        Color pixelColor = barcodeBitmap.GetPixel(x, y);
                        if (pixelColor.ToArgb() == barColor.ToArgb())
                        {
                            containsBarColor = true;
                            break;
                        }
                    }
                }

                Console.WriteLine(containsBarColor
                    ? "Verification succeeded: bar color found in the image."
                    : "Verification failed: bar color not found in the image.");
            }
        }
    }
}