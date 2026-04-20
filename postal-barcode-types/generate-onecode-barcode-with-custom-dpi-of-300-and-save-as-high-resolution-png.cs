using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Numeric codetext of valid length for OneCode (20 digits)
        const string codeText = "12345678901234567890";

        // Create the barcode generator for OneCode symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.OneCode, codeText))
        {
            // Set the image resolution to 300 DPI (float literal with suffix)
            generator.Parameters.Resolution = 300f;

            // Save the barcode as a high‑resolution PNG file
            generator.Save("OneCode.png");
        }
    }
}