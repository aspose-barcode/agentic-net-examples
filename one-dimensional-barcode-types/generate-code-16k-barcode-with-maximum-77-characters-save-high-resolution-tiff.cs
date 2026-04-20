using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample Code16K text with exactly 77 characters
        string codeText = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDE";

        // Create the barcode generator for Code16K
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Set high resolution (300 DPI)
            generator.Parameters.Resolution = 300f;

            // Optional: adjust aspect ratio if needed (default is 1)
            // generator.Parameters.Barcode.Code16K.AspectRatio = 1f;

            // Save the barcode as a high‑resolution TIFF image
            generator.Save("code16k.tif");
        }
    }
}