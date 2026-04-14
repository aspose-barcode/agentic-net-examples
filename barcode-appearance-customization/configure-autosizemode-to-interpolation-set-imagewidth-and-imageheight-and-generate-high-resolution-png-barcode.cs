using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Use interpolation auto‑size mode.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the target image size (points).
            generator.Parameters.ImageWidth.Point = 600f;   // Width
            generator.Parameters.ImageHeight.Point = 300f; // Height

            // Set a high resolution (e.g., 300 DPI).
            generator.Parameters.Resolution = 300f;

            // Save the barcode as a high‑resolution PNG file.
            generator.Save("barcode.png");
        }

        Console.WriteLine("Barcode generated: barcode.png");
    }
}