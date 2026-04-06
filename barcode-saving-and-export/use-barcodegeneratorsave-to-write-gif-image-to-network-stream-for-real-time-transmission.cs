using System;
using Aspose.BarCode.Generation;

namespace BarcodeNetworkExample
{
    class Program
    {
        static void Main()
        {
            const string barcodeText = "1234567890";

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = barcodeText;
                generator.Save("barcode.png");
            }
        }
    }
}