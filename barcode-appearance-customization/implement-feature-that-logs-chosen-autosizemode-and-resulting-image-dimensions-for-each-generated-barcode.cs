using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Define the AutoSizeMode values to test
        var modes = new[] { AutoSizeMode.None, AutoSizeMode.Nearest, AutoSizeMode.Interpolation };

        foreach (var mode in modes)
        {
            // Create a barcode generator for Code128
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "123ABC";

                // Set the desired AutoSizeMode
                generator.Parameters.AutoSizeMode = mode;

                // For Nearest and Interpolation modes, specify target image size
                if (mode != AutoSizeMode.None)
                {
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 150f;
                }

                // Generate the barcode image
                Bitmap bitmap = generator.GenerateBarCodeImage();

                // Log the AutoSizeMode and resulting image dimensions
                Console.WriteLine($"AutoSizeMode: {mode}, Width: {bitmap.Width}, Height: {bitmap.Height}");

                // Save the image to a file
                string fileName = $"barcode_{mode}.png";
                bitmap.Save(fileName, ImageFormat.Png);

                // Clean up the bitmap
                bitmap.Dispose();
            }
        }
    }
}