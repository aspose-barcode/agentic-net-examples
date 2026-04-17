using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Ensure the output directory exists
            string outputDir = Path.Combine(Environment.CurrentDirectory, "Barcodes");
            Directory.CreateDirectory(outputDir);

            // Create a unique file name using a GUID
            string fileName = $"barcode_{Guid.NewGuid()}.png";
            string filePath = Path.Combine(outputDir, fileName);

            // Generate a Code128 barcode with custom colors
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Set foreground (bar) color
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
                // Set background color
                generator.Parameters.BackColor = Aspose.Drawing.Color.Yellow;

                // Save the barcode image to the unique file path
                generator.Save(filePath);
            }

            Console.WriteLine($"Barcode saved to: {filePath}");
        }
    }
}