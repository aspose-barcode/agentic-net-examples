using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a PDF417 barcode generator with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
        {
            // Set the resolution to 600 dpi for high‑quality output
            generator.Parameters.Resolution = 600;

            // Reduce bar width to improve readability of dense barcodes
            // Assign a small reduction value (e.g., 0.1 point)
            generator.Parameters.Barcode.BarWidthReduction.Point = 0.1f;

            // Save the generated barcode image
            generator.Save("pdf417_600dpi.png");
        }

        Console.WriteLine("PDF417 barcode generated with BarWidthReduction at 600 dpi.");
    }
}