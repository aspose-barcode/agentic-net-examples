using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeSvgExport
{
    class Program
    {
        static void Main()
        {
            const string outputPath = "custom_text_barcode.svg";

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39))
            {
                generator.CodeText = "ABC-1234";

                var ctParams = generator.Parameters.Barcode.CodeTextParameters;
                ctParams.Font.FamilyName = "Arial";
                ctParams.Font.Size.Point = 12f;
                ctParams.Color = Aspose.Drawing.Color.DarkBlue;
                ctParams.Alignment = TextAlignment.Center;
                ctParams.Location = CodeLocation.Below;

                generator.Save(outputPath, BarCodeImageFormat.Svg);
            }

            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}