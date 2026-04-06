using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a Code128 barcode generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text to encode
            generator.CodeText = "1234567890";

            // Enable anti‑aliasing for smoother edges
            generator.Parameters.UseAntiAlias = true;

            // Increase resolution for crisp screen display (e.g., 300 DPI)
            generator.Parameters.Resolution = 300f;

            // Optionally define image size (points)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the barcode as a PNG file
            generator.Save("barcode.png");
        }
    }
}