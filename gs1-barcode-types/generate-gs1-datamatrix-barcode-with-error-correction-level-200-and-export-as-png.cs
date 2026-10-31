using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodes");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "GS1DataMatrix.png");

        // GS1 DataMatrix requires GS1 formatted code text, e.g., (01) followed by 14‑digit GTIN
        string gs1CodeText = "(01)12345678901231";

        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, gs1CodeText))
        {
            // Set ECC 200 error correction level
            generator.Parameters.Barcode.DataMatrix.EccType = DataMatrixEccType.Ecc200;

            // Optional: set module size
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputPath}");
    }
}