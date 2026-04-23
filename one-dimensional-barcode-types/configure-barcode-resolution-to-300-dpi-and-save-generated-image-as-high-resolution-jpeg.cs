using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the image resolution to 300 DPI
            generator.Parameters.Resolution = 300f;

            // Save the barcode as a high‑resolution JPEG image
            generator.Save("barcode_300dpi.jpg");
        }
    }
}