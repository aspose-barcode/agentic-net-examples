using System;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Generate a sample barcode image.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set a reasonable XDimension to avoid generation issues.
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save("barcode.png");
        }

        // Create a blank white image that does not contain any barcode.
        using (var blank = new Bitmap(200, 200))
        {
            using (var graphics = Graphics.FromImage(blank))
            {
                graphics.Clear(Color.White);
            }
            blank.Save("blank.png");
        }

        int falsePositives = 0;

        // Attempt to read barcodes from the blank image with a low MinimalXDimension setting.
        using (var reader = new BarCodeReader("blank.png", DecodeType.Code128, DecodeType.QR))
        {
            // Use the mode that relies on MinimalXDimension (set to a low value implicitly).
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

            // Count any detected barcodes – these are false positives.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                falsePositives++;
                Console.WriteLine($"False positive detected: Type={result.CodeTypeName}, Text={result.CodeText}");
            }
        }

        Console.WriteLine($"Total false positives with low MinimalXDimension: {falsePositives}");
    }
}