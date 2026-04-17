using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample GS1 Code 128 data (AI 01 for GTIN)
        string codeText = "(01)12345678901231";

        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Enable anti‑aliasing
            generator.Parameters.UseAntiAlias = true;

            // Set colors (will be saved in 24‑bit PNG)
            generator.Parameters.BackColor = Color.White;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Generate the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save as PNG (24‑bit color depth)
                bitmap.Save("gs1code128.png", ImageFormat.Png);
            }
        }
    }
}