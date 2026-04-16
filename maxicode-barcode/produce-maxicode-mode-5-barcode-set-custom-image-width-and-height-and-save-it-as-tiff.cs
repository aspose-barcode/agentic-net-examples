using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

namespace Example
{
    class Program
    {
        static void Main()
        {
            // Create standard codetext for MaxiCode Mode 5
            var maxiCodeCodetext = new MaxiCodeStandardCodetext();
            maxiCodeCodetext.Mode = MaxiCodeMode.Mode5;
            maxiCodeCodetext.Message = "Test message";

            // Generate the barcode, set custom image size, and save as TIFF
            using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                // Set custom image dimensions (points)
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 200f;

                // Save the barcode image as TIFF
                generator.Save("maxicode_mode5.tif");
            }
        }
    }
}