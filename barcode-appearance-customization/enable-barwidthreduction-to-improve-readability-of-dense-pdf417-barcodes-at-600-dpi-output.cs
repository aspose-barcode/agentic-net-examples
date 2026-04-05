using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a PDF417 barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417))
        {
            // Set the data to encode
            generator.CodeText = "Sample PDF417 barcode with dense data for testing BarWidthReduction.";

            // Set image resolution to 600 dpi
            generator.Parameters.Resolution = 600f;

            // Enable bar width reduction to improve readability of dense barcodes
            generator.Parameters.Barcode.BarWidthReduction.Point = 0.5f; // adjust value as needed

            // Save the generated barcode image
            generator.Save("pdf417.png");
        }

        Console.WriteLine("PDF417 barcode generated with BarWidthReduction at 600 dpi.");
    }
}