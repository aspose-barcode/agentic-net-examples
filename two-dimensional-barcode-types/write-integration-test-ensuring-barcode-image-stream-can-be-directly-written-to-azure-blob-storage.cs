using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        string outputPath = "code128.png";

        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            generator.Save(outputPath);
        }

        Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
    }
}