using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Desired dimensions in points
        const float desiredWidthPoints = 300f;
        const float desiredHeightPoints = 150f;

        // Create a barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the code text
            generator.CodeText = "123ABC";

            // Set image size using point units
            generator.Parameters.ImageWidth.Point = desiredWidthPoints;
            generator.Parameters.ImageHeight.Point = desiredHeightPoints;

            // Generate the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Actual pixel dimensions of the generated image
                int actualWidthPixels = bitmap.Width;
                int actualHeightPixels = bitmap.Height;

                // Expected pixel dimensions based on the set points and resolution
                float expectedWidthPixels = generator.Parameters.ImageWidth.Pixels;
                float expectedHeightPixels = generator.Parameters.ImageHeight.Pixels;

                Console.WriteLine($"Actual image size:   {actualWidthPixels}x{actualHeightPixels} pixels");
                Console.WriteLine($"Expected image size: {expectedWidthPixels}x{expectedHeightPixels} pixels");

                // Verify that the dimensions match
                bool widthMatches = Math.Abs(actualWidthPixels - expectedWidthPixels) < 0.01f;
                bool heightMatches = Math.Abs(actualHeightPixels - expectedHeightPixels) < 0.01f;

                if (widthMatches && heightMatches)
                {
                    Console.WriteLine("The generated image dimensions match the specified ImageWidth and ImageHeight.");
                }
                else
                {
                    Console.WriteLine("The generated image dimensions do NOT match the specified ImageWidth and ImageHeight.");
                }

                // Optionally save the image for visual inspection
                bitmap.Save("barcode.png");
            }
        }
    }
}