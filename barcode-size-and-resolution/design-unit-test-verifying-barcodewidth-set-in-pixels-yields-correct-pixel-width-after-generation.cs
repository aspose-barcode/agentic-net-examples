using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Desired barcode width in pixels
        const float expectedWidthPixels = 300f;

        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Set auto‑size mode to use the explicit image width
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Assign the width in pixels
            generator.Parameters.ImageWidth.Pixels = expectedWidthPixels;

            // Generate the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Verify that the generated bitmap width matches the expected pixel width
                if (bitmap.Width != (int)expectedWidthPixels)
                {
                    Console.WriteLine($"Test Failed: Expected width {expectedWidthPixels}px, but got {bitmap.Width}px.");
                }
                else
                {
                    Console.WriteLine($"Test Passed: Barcode width is {bitmap.Width}px as expected.");
                }

                // Save the image for visual inspection (optional)
                bitmap.Save("barcode_test.png", Aspose.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}