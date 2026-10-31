using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        string outputDir = Path.Combine(Path.GetTempPath(), "DataMatrixDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "DataMatrixAuto.png");

        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.DataMatrix, "Aspose"))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 4f;
            gen.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.Auto;
            gen.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine("DataMatrix barcode saved to: " + outputPath);
    }
}