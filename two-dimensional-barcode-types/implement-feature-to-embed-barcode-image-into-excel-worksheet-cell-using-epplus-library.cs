using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string outputPath = "barcode.png";

        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 100f;

            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                bitmap.Save(outputPath, ImageFormat.Png);
            }
        }

        Console.WriteLine($"Barcode saved to: {Path.GetFullPath(outputPath)}");
    }
}