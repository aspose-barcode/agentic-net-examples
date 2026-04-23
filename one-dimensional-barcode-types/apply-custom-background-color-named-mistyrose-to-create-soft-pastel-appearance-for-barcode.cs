using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the data to encode
                generator.CodeText = "1234567890";

                // Apply a soft pastel background color
                generator.Parameters.BackColor = Color.MistyRose;

                // Save the barcode image to a file
                generator.Save("barcode_mistyrose.png");
            }
        }
    }
}