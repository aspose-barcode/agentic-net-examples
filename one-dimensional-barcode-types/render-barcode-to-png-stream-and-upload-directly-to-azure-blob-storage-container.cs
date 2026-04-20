using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeToFile
{
    class Program
    {
        static void Main()
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "1234567890";
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;
                generator.Parameters.Resolution = 300;
                generator.Save("barcode.png");
            }

            Console.WriteLine("Barcode image saved successfully.");
        }
    }
}