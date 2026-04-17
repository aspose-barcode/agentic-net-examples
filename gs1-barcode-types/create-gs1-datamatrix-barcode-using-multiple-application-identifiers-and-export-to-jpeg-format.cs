using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // GS1 DataMatrix with multiple Application Identifiers (AI)
        string codeText = "(01)01234567890123(10)ABC123(17)210101";

        // Create the barcode generator for GS1 DataMatrix
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set module size (X-dimension) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Define image dimensions in pixels
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 300f;

            // Set image resolution (dpi)
            generator.Parameters.Resolution = 300f;

            // Save the barcode as a JPEG file
            generator.Save("gs1datamatrix.jpg");
        }
    }
}