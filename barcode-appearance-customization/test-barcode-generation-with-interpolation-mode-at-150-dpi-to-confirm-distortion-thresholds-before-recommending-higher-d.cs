using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample code text
        const string codeText = "Test12345";

        // Generate barcode with 150 DPI using Interpolation auto‑size mode
        using (var generator150 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set interpolation mode
            generator150.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set resolution to 150 DPI
            generator150.Parameters.Resolution = 150f;

            // Define image size (points) – required when using Interpolation mode
            generator150.Parameters.ImageWidth.Point = 300f;
            generator150.Parameters.ImageHeight.Point = 150f;

            // Save the image
            generator150.Save("barcode_150dpi.png");
            Console.WriteLine("Generated barcode at 150 DPI: barcode_150dpi.png");
        }

        // Generate barcode with higher DPI (e.g., 300) for comparison
        using (var generator300 = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator300.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator300.Parameters.Resolution = 300f;
            generator300.Parameters.ImageWidth.Point = 300f;
            generator300.Parameters.ImageHeight.Point = 150f;
            generator300.Save("barcode_300dpi.png");
            Console.WriteLine("Generated barcode at 300 DPI: barcode_300dpi.png");
        }

        // Simple recommendation based on DPI
        Console.WriteLine("If the 150 dpi image shows noticeable distortion, consider using a higher DPI (e.g., 300 dpi).");
    }
}