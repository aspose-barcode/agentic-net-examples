using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Output file name
        const string outputFile = "upca_barcode.tif";

        // Create a UPC‑A barcode generator with a valid 12‑digit code
        using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, "012345678905"))
        {
            // Set image resolution to 600 DPI
            generator.Parameters.Resolution = 600f;

            // Save the barcode as a TIFF image
            generator.Save(outputFile, BarCodeImageFormat.Tiff);
        }

        Console.WriteLine($"Barcode saved to {outputFile}");
    }
}