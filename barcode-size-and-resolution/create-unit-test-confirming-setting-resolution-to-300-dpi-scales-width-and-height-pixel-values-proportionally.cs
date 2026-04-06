using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator with default settings (resolution = 96 dpi)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Generate image at default resolution
            using (Bitmap defaultBmp = generator.GenerateBarCodeImage())
            {
                int defaultWidth = defaultBmp.Width;
                int defaultHeight = defaultBmp.Height;

                // Set resolution to 300 dpi
                generator.Parameters.Resolution = 300f;

                // Generate image at new resolution
                using (Bitmap highResBmp = generator.GenerateBarCodeImage())
                {
                    int highResWidth = highResBmp.Width;
                    int highResHeight = highResBmp.Height;

                    // Expected dimensions after scaling
                    float scaleFactor = 300f / 96f;
                    int expectedWidth = (int)Math.Round(defaultWidth * scaleFactor);
                    int expectedHeight = (int)Math.Round(defaultHeight * scaleFactor);

                    bool widthMatches = highResWidth == expectedWidth;
                    bool heightMatches = highResHeight == expectedHeight;

                    if (widthMatches && heightMatches)
                    {
                        Console.WriteLine("PASS: Resolution scaling works as expected.");
                        Console.WriteLine($"Default (96 dpi): {defaultWidth}x{defaultHeight}");
                        Console.WriteLine($"300 dpi: {highResWidth}x{highResHeight}");
                    }
                    else
                    {
                        Console.WriteLine("FAIL: Resolution scaling did not produce expected dimensions.");
                        Console.WriteLine($"Expected: {expectedWidth}x{expectedHeight}");
                        Console.WriteLine($"Actual:   {highResWidth}x{highResHeight}");
                    }
                }
            }
        }
    }
}