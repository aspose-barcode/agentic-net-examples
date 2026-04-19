using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

namespace HanXinBarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Define the text to encode in the Han Xin barcode
            const string codeText = "HanXin Sample Text";

            // Create a BarcodeGenerator for Han Xin symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
            {
                // Set barcode colors suitable for web (limited palette)
                generator.Parameters.Barcode.BarColor = Color.Black;   // Bars color
                generator.Parameters.BackColor = Color.White;         // Background color

                // Optional: set resolution (dpi) for the output image
                generator.Parameters.Resolution = 96;

                // Save the barcode as a GIF image
                generator.Save("HanXinBarcode.gif");
            }

            Console.WriteLine("Han Xin barcode saved as HanXinBarcode.gif");
        }
    }
}