using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample quiet zone coefficients
        int leftQuietZoneCoef = 15;   // left quiet zone in xDimension units
        int rightQuietZoneCoef = 5;   // right quiet zone in xDimension units

        // Create a Code16K barcode generator with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, "12345678901234567890"))
        {
            // Apply quiet zone coefficients
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = leftQuietZoneCoef;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = rightQuietZoneCoef;

            // Optional: set image size for better preview
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Generate the barcode image
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Save preview to file
                barcodeImage.Save("Code16K_QuietZonePreview.png", ImageFormat.Png);
            }
        }

        Console.WriteLine("Barcode preview generated: Code16K_QuietZonePreview.png");
    }
}