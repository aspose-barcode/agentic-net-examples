using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        TestAutoSizeNearest();
    }

    static void TestAutoSizeNearest()
    {
        // Maximum dimensions to request
        int maxWidth = 300;
        int maxHeight = 200;

        // Create a barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set AutoSizeMode to Nearest and specify target size
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Pixels = maxWidth;
            generator.Parameters.ImageHeight.Pixels = maxHeight;
            generator.CodeText = "1234567890";

            // Generate the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                int actualWidth = bitmap.Width;
                int actualHeight = bitmap.Height;

                bool widthOk = actualWidth <= maxWidth;
                bool heightOk = actualHeight <= maxHeight;

                Console.WriteLine($"Requested max width: {maxWidth}, actual width: {actualWidth}");
                Console.WriteLine($"Requested max height: {maxHeight}, actual height: {actualHeight}");

                if (widthOk && heightOk)
                {
                    Console.WriteLine("PASS: Image dimensions are within the expected limits.");
                }
                else
                {
                    Console.WriteLine("FAIL: Image dimensions exceed the expected limits.");
                }
            }
        }
    }
}