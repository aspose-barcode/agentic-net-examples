using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeToDatabase
{
    class Program
    {
        static void Main()
        {
            const string gs1CodeText = "(01)12345678901231";

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, gs1CodeText))
            {
                generator.Parameters.ImageWidth.Point = 300;
                generator.Parameters.ImageHeight.Point = 150;
                generator.Parameters.Resolution = 300;

                generator.Save("barcode.png");
            }

            Console.WriteLine("GS1 Code 128 barcode generated and saved to file.");
        }
    }
}