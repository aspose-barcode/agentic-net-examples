using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeSvgExport
{
    class Program
    {
        static void Main()
        {
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "custom_barcode.svg");

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39))
            {
                generator.CodeText = "Custom123";

                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
                generator.Parameters.Barcode.CodeTextParameters.Color = Color.DarkBlue;

                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;

                generator.Save(outputPath, BarCodeImageFormat.Svg);
            }

            Console.WriteLine($"Barcode saved to: {outputPath}");
        }
    }
}