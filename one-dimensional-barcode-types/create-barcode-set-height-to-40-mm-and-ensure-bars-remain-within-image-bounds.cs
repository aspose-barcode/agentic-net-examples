using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Ensure automatic sizing is disabled
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the height of the bars to 40 mm
            generator.Parameters.Barcode.BarHeight.Millimeters = 40f;

            // Set the image height slightly larger than the bar height to keep bars within bounds
            generator.Parameters.ImageHeight.Millimeters = 50f;

            // Save the barcode image to a file
            generator.Save("barcode.png");
        }
    }
}