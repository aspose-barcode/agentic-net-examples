using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        const float minModuleSize = 0.5f; // Minimum module size in points for high‑density printing
        string codeText = "Hello";

        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Intentionally set a module size below the minimum to demonstrate validation
            generator.Parameters.Barcode.XDimension.Point = 0.4f;

            // Validate the module size
            float actualSize = generator.Parameters.Barcode.XDimension.Point;
            if (actualSize < minModuleSize)
            {
                Console.WriteLine($"Module size {actualSize}pt is below the minimum of {minModuleSize}pt. Adjusting.");
                generator.Parameters.Barcode.XDimension.Point = minModuleSize;
            }

            // Set a high resolution suitable for high‑density printing
            generator.Parameters.Resolution = 300; // DPI

            // Save the barcode image
            generator.Save("datamatrix.png");

            Console.WriteLine($"DataMatrix barcode saved with module size {generator.Parameters.Barcode.XDimension.Point}pt.");
        }
    }
}