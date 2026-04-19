using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeDimensionsDemo
{
    class Program
    {
        static void Main()
        {
            // Example: generate a Code128 barcode and obtain its pixel dimensions
            var (width, height) = GetBarcodeDimensions("123456", EncodeTypes.Code128);
            Console.WriteLine($"Width: {width} px, Height: {height} px");
        }

        // Returns the width and height (in pixels) of a generated barcode image
        static (int width, int height) GetBarcodeDimensions(string codeText, BaseEncodeType encodeType)
        {
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Use default sizing (AutoSizeMode.None) to get the natural image size
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;

                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    return (bitmap.Width, bitmap.Height);
                }
            }
        }
    }
}