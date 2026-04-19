using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare test data
        const string codeText = "Test123";
        const float defaultResolution = 96f;
        const float highResolution = 300f;

        // Create a barcode generator with default resolution (96 dpi)
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Ensure the generator uses the default resolution
            generator.Parameters.Resolution = defaultResolution;

            // Generate image at default resolution
            using (Bitmap bitmapLow = generator.GenerateBarCodeImage())
            {
                int lowWidth = bitmapLow.Width;
                int lowHeight = bitmapLow.Height;

                // Change resolution to 300 dpi
                generator.Parameters.Resolution = highResolution;

                // Generate image at high resolution
                using (Bitmap bitmapHigh = generator.GenerateBarCodeImage())
                {
                    int highWidth = bitmapHigh.Width;
                    int highHeight = bitmapHigh.Height;

                    // Expected dimensions after scaling proportionally
                    int expectedWidth = (int)Math.Round(lowWidth * highResolution / defaultResolution);
                    int expectedHeight = (int)Math.Round(lowHeight * highResolution / defaultResolution);

                    // Verify that both width and height are scaled proportionally
                    bool widthMatches = highWidth == expectedWidth;
                    bool heightMatches = highHeight == expectedHeight;

                    if (widthMatches && heightMatches)
                    {
                        Console.WriteLine("Resolution scaling test passed.");
                        Console.WriteLine($"Low DPI ({defaultResolution}): {lowWidth}x{lowHeight}");
                        Console.WriteLine($"High DPI ({highResolution}): {highWidth}x{highHeight}");
                    }
                    else
                    {
                        Console.WriteLine("Resolution scaling test failed.");
                        Console.WriteLine($"Expected: {expectedWidth}x{expectedHeight}, Actual: {highWidth}x{highHeight}");
                    }
                }
            }
        }
    }
}