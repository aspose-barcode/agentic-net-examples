using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating barcodes with per‑side padding using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes with specified padding and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Define sample barcodes with their symbology name, code text and per‑side padding (in points)
        var samples = new (string SymName, string CodeText, float Left, float Top, float Right, float Bottom)[]
        {
            ("Code128", "ABC123456", 5f, 10f, 5f, 10f),
            ("QR", "https://example.com", 20f, 20f, 20f, 20f),
            ("DataMatrix", "DataMatrixSample", 2f, 2f, 2f, 2f),
            ("Pdf417", "PDF417 Sample Text", 15f, 5f, 15f, 5f)
        };

        // Iterate over each sample definition
        foreach (var sample in samples)
        {
            // Resolve symbology name to BaseEncodeType via reflection
            var field = typeof(EncodeTypes).GetField(sample.SymName);
            if (field == null)
            {
                // If the symbology is not found, report and skip this entry
                Console.WriteLine($"Symbology '{sample.SymName}' not found. Skipping.");
                continue;
            }

            // Retrieve the enum value representing the barcode type
            var encodeType = (BaseEncodeType)field.GetValue(null);

            // Create a barcode generator for the resolved type and supplied code text
            using (var generator = new BarcodeGenerator(encodeType, sample.CodeText))
            {
                // Apply per‑side padding (points) to the barcode
                generator.Parameters.Barcode.Padding.Left.Point = sample.Left;
                generator.Parameters.Barcode.Padding.Top.Point = sample.Top;
                generator.Parameters.Barcode.Padding.Right.Point = sample.Right;
                generator.Parameters.Barcode.Padding.Bottom.Point = sample.Bottom;

                // Optional: define image size to ensure enough space for padding
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Build the output file name and save the barcode image
                string fileName = $"{sample.SymName}_Padded.png";
                generator.Save(fileName);

                // Inform the user about the generated file and its padding values
                Console.WriteLine($"Generated {fileName} with padding L:{sample.Left} T:{sample.Top} R:{sample.Right} B:{sample.Bottom}");
            }
        }
    }
}