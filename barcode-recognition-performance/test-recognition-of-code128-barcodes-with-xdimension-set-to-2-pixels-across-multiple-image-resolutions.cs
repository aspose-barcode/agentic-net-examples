using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Define resolutions to test (DPI)
        int[] resolutions = { 72, 150, 300 };
        string outputDir = "Barcodes";

        // Ensure output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        foreach (int dpi in resolutions)
        {
            string filePath = Path.Combine(outputDir, $"code128_{dpi}dpi.png");

            // Generate Code128 barcode with XDimension = 2 pixels and specific resolution
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test12345"))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Resolution = dpi;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Verify the file was created
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Failed to create barcode image at {filePath}");
                continue;
            }

            // Recognize the barcode using XDimensionMode.Normal (classic 2+ pixels)
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                reader.QualitySettings.XDimension = XDimensionMode.Normal;

                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Resolution {dpi} DPI: Detected CodeText = '{result.CodeText}', Type = {result.CodeTypeName}");
                }
            }
        }
    }
}