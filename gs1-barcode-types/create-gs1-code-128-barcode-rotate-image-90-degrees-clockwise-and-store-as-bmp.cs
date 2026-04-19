using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        // Sample GS1 Code 128 data with Application Identifier (01) for GTIN
        const string codeText = "(01)12345678901231";

        // Create the barcode generator for GS1 Code 128
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Rotate the barcode image 90 degrees clockwise
            generator.Parameters.RotationAngle = 90f;

            // Save the barcode as a BMP file
            generator.Save("gs1code128.bmp");
        }
    }
}