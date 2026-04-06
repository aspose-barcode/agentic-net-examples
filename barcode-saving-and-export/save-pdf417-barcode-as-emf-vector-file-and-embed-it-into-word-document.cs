using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        string outputPath = "Pdf417Barcode.png";

        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Pdf417))
        {
            generator.CodeText = "Sample PDF417 Barcode";
            generator.Save(outputPath);
        }

        using (Bitmap bitmap = (Bitmap)Image.FromFile(outputPath))
        {
            bitmap.Save(outputPath, ImageFormat.Png);
        }
    }
}