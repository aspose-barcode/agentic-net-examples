using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for the barcode image with Code128 emulation enabled
        string imagePathEmulated = "micropdf417_emulated.png";

        // Create a MicroPdf417 barcode with FNC1 in second position (mode 908) and enable Code128 emulation
        using (var generator = new BarcodeGenerator(EncodeTypes.MicroPdf417, "a\u001d1222322323"))
        {
            generator.Parameters.Barcode.Pdf417.IsCode128Emulation = true;
            generator.Save(imagePathEmulated, BarCodeImageFormat.Png);
        }

        // Verify that the image was created
        if (!File.Exists(imagePathEmulated))
        {
            Console.WriteLine("Failed to generate the barcode image with Code128 emulation.");
            return;
        }

        // Read the barcode and output the Code128 emulation flag
        using (var reader = new BarCodeReader(imagePathEmulated, DecodeType.MicroPdf417))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"[Emulated] CodeText: {result.CodeText}");
                Console.WriteLine($"[Emulated] IsCode128Emulation: {result.Extended.Pdf417.IsCode128Emulation}");
            }
        }

        // Path for the barcode image without Code128 emulation
        string imagePathNormal = "micropdf417_normal.png";

        // Create a MicroPdf417 barcode with the same data but without enabling Code128 emulation
        using (var generator = new BarcodeGenerator(EncodeTypes.MicroPdf417, "a\u001d1222322323"))
        {
            // IsCode128Emulation remains false by default
            generator.Save(imagePathNormal, BarCodeImageFormat.Png);
        }

        // Verify that the second image was created
        if (!File.Exists(imagePathNormal))
        {
            Console.WriteLine("Failed to generate the barcode image without Code128 emulation.");
            return;
        }

        // Read the barcode and output the Code128 emulation flag (expected to be false)
        using (var reader = new BarCodeReader(imagePathNormal, DecodeType.MicroPdf417))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"[Normal] CodeText: {result.CodeText}");
                Console.WriteLine($"[Normal] IsCode128Emulation: {result.Extended.Pdf417.IsCode128Emulation}");
            }
        }
    }
}