using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "qr_mask3.bmp");

        // Create QR Code generator
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Note: Aspose.BarCode does not expose a property to set the QR mask pattern.
            // The encoder applies an optimal mask automatically.

            // Save as BMP
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}