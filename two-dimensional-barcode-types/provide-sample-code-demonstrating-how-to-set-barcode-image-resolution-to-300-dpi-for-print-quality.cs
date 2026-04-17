using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeResolutionDemo
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 with sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Set the image resolution to 300 DPI for print quality.
                generator.Parameters.Resolution = 300f;

                // Optional: define image size when AutoSizeMode is not set to None.
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Save the barcode image to a PNG file.
                generator.Save("barcode_300dpi.png");
            }
        }
    }
}