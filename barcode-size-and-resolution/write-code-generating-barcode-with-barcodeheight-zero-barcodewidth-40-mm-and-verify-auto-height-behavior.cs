using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string filePath = "barcode.png";

        // Generate barcode with fixed width (40 mm) and auto-size height
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Millimeters = 40f;
            // Do not set BarHeight; auto-size will determine appropriate height
            generator.Save(filePath);
        }

        // Load the saved image to verify dimensions
        using (Bitmap bitmap = new Bitmap(filePath))
        {
            Console.WriteLine($"Generated image size: {bitmap.Width}x{bitmap.Height} pixels");
        }

        // Read the barcode and output detected region
        using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                var rect = result.Region.Rectangle;
                Console.WriteLine($"Detected region: X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");
            }
        }
    }
}