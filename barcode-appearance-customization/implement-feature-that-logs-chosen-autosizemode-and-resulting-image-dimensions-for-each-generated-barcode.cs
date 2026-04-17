using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample barcode text
        string codeText = "1234567890";

        // Iterate through all AutoSizeMode values
        foreach (AutoSizeMode mode in Enum.GetValues(typeof(AutoSizeMode)))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set the AutoSizeMode
                generator.Parameters.AutoSizeMode = mode;

                // For modes that rely on image dimensions, specify them
                if (mode == AutoSizeMode.Nearest || mode == AutoSizeMode.Interpolation)
                {
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 150f;
                }

                // Generate the barcode image
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Log the chosen mode and resulting image size
                    Console.WriteLine($"AutoSizeMode: {mode}, Image dimensions: {bitmap.Width}x{bitmap.Height}");

                    // Save the image to a file
                    string fileName = $"barcode_{mode}.png";
                    bitmap.Save(fileName, ImageFormat.Png);
                }
            }
        }
    }
}