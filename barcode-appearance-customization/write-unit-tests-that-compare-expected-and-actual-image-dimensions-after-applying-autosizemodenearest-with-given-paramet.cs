using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Test 1: target size 200x100
        RunTest("Test1", 200f, 100f);

        // Test 2: target size 300x150
        RunTest("Test2", 300f, 150f);
    }

    static void RunTest(string testName, float targetWidth, float targetHeight)
    {
        // Create barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Apply AutoSizeMode.Nearest and set target dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Point = targetWidth;
            generator.Parameters.ImageHeight.Point = targetHeight;

            // Generate image into memory stream
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // Load image using Aspose.Drawing
                using (var image = Image.FromStream(ms))
                {
                    int actualWidth = image.Width;
                    int actualHeight = image.Height;

                    // Validate dimensions: should not exceed targets and preserve aspect ratio
                    bool widthOk = actualWidth <= (int)targetWidth;
                    bool heightOk = actualHeight <= (int)targetHeight;
                    bool aspectOk = Math.Abs((float)actualWidth / actualHeight - targetWidth / targetHeight) < 0.01f;

                    if (widthOk && heightOk && aspectOk)
                    {
                        Console.WriteLine($"{testName}: PASS (Actual: {actualWidth}x{actualHeight})");
                    }
                    else
                    {
                        Console.WriteLine($"{testName}: FAIL (Actual: {actualWidth}x{actualHeight}, Expected max: {targetWidth}x{targetHeight})");
                    }
                }
            }
        }
    }
}