using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Output folder for generated barcodes
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputFolder);

        // Base XDimension in millimeters
        float baseXDim = 0.5f;
        float step = 0.1f;

        for (int i = 1; i <= 100; i++)
        {
            // Create a barcode generator for Code128 with unique text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"CODE{i:D3}"))
            {
                // Set XDimension in millimeters (incremental)
                generator.Parameters.Barcode.XDimension.Millimeters = baseXDim + (i - 1) * step;

                // Optional: set other parameters (e.g., bar height) if desired
                // generator.Parameters.Barcode.BarHeight.Millimeters = 15f;

                // Build file name and save as TIFF
                string fileName = Path.Combine(outputFolder, $"barcode_{i:D3}.tiff");
                generator.Save(fileName);
            }
        }
    }
}