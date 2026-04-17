using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace DotCodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a BarcodeGenerator for DotCode with numeric CodeText
            using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "1234567890"))
            {
                // Save the barcode as BMP image
                generator.Save("dotcode.bmp");
            }
        }
    }
}