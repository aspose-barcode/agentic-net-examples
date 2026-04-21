using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define output file path
        string outputPath = "hibc_datamatrix.png";

        // Create a BarcodeGenerator for HIBC DataMatrix LIC
        using (var generator = new BarcodeGenerator(EncodeTypes.HIBCDataMatrixLIC))
        {
            // Sample HIBC code text (primary data). Adjust as needed for real use cases.
            generator.CodeText = "+A99912345";

            // Set high resolution for high‑density labels
            generator.Parameters.Resolution = 600f; // DPI

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}