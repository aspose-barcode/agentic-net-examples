using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Simulated user selections
        float selectedResolution = 300f;          // DPI
        float selectedImageWidth = 200f;          // points
        float selectedImageHeight = 100f;         // points
        float selectedXDimension = 2f;            // points
        float selectedBarHeight = 40f;            // points

        // Create a barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to encode
            generator.CodeText = "1234567890";

            // Apply user-selected resolution
            generator.Parameters.Resolution = selectedResolution;

            // Apply user-selected size and dimension settings
            generator.Parameters.ImageWidth.Point = selectedImageWidth;
            generator.Parameters.ImageHeight.Point = selectedImageHeight;
            generator.Parameters.Barcode.XDimension.Point = selectedXDimension;
            generator.Parameters.Barcode.BarHeight.Point = selectedBarHeight;

            // Save the generated barcode image
            generator.Save("barcode.png");
        }
    }
}