using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample GS1 Code 128 data (AI 01 with a 14‑digit GTIN)
        const string codeText = "(01)12345678901231";

        // Create a barcode generator for GS1 Code 128
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Use interpolation mode so that ImageWidth/ImageHeight control the final size
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the desired image size: 200 × 100 pixels
            generator.Parameters.ImageWidth.Pixels = 200f;
            generator.Parameters.ImageHeight.Pixels = 100f;

            // Save the barcode as a JPEG file
            generator.Save("gs1code128.jpg");
        }
    }
}