using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Output folder for generated barcodes
        string outputFolder = "Barcodes";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Number of barcodes to generate (reduced for safe execution)
        int barcodeCount = 10;

        for (int i = 1; i <= barcodeCount; i++)
        {
            // Vary XDimension in millimeters (e.g., 0.5 mm, 1.0 mm, ...)
            float xDimensionMm = i * 0.5f;

            // Simple codetext for each barcode
            string codeText = $"CODE{i:D3}";

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set XDimension using the Millimeters unit member
                generator.Parameters.Barcode.XDimension.Millimeters = xDimensionMm;

                // Build file name and save as TIFF
                string fileName = Path.Combine(outputFolder, $"barcode_{i:D3}.tif");
                generator.Save(fileName, BarCodeImageFormat.Tiff);
            }
        }
    }
}