using System;
using Aspose.BarCode.Generation;

namespace BarcodeToFile
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "1234567890";
                generator.Save("code128.png");
            }

            Console.WriteLine("Barcode image saved to code128.png");
        }
    }
}