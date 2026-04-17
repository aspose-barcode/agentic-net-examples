using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

namespace BarcodeDemo
{
    class Program
    {
        static void Main()
        {
            // Create standard codetext for MaxiCode Mode 4
            var maxiCodeCodetext = new MaxiCodeStandardCodetext();
            maxiCodeCodetext.Mode = MaxiCodeMode.Mode4;
            maxiCodeCodetext.Message = "Test message";

            // Generate and save the barcode as BMP
            using (var complexGenerator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                complexGenerator.Save("maxicode_mode4.bmp");
            }
        }
    }
}