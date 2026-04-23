using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Define barcode parameters
        const string codeText = "1234567890";
        const string outputFile = "barcode.png";
        // Target foreground color #123456
        Color targetColor = Color.FromArgb(0x12, 0x34, 0x56);

        // Create barcode generator and configure it
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the foreground (bar) color
            generator.Parameters.Barcode.BarColor = targetColor;

            // Generate the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Verify that the target color appears in the image
                bool colorFound = false;
                for (int y = 0; y < bitmap.Height && !colorFound; y++)
                {
                    for (int x = 0; x < bitmap.Width && !colorFound; x++)
                    {
                        if (bitmap.GetPixel(x, y).ToArgb() == targetColor.ToArgb())
                        {
                            colorFound = true;
                        }
                    }
                }

                // Save the image
                bitmap.Save(outputFile, ImageFormat.Png);

                // Output verification result
                Console.WriteLine(colorFound
                    ? $"Success: The color #{targetColor.ToArgb():X6} was found in the saved barcode."
                    : $"Failure: The color #{targetColor.ToArgb():X6} was NOT found in the saved barcode.");
            }
        }
    }
}