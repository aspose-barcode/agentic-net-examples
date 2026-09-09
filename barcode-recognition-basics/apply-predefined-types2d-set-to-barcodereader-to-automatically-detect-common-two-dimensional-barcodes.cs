// Title: Detect Multiple 2D Barcodes Using Types2D Set
// Description: The example generates QR, DataMatrix, and Aztec barcodes, saves them as PNG files, and then reads them using the predefined Types2D decode set to automatically recognize common two‑dimensional symbologies.
// Category-Description: This sample belongs to the Aspose.BarCode barcode recognition category, demonstrating how to use BarCodeReader with the DecodeType.Types2D preset to detect various 2D symbologies without specifying each type individually. It showcases the BarcodeGenerator for creating sample images and the BarCodeReader for extracting code type and text, a typical workflow for developers needing quick multi‑format 2D barcode detection.
// Prompt: Apply the predefined Types2D set to BarCodeReader to automatically detect common two‑dimensional barcodes.
// Tags: barcode,2d,types2d,recognition,generation,aspose.barcode,csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating and reading multiple 2D barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample 2D barcodes, saves them, and reads them using the Types2D decode set.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcode images
        string tempDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define sample 2D barcodes to generate (QR, DataMatrix, Aztec)
        var samples = new[]
        {
            new { Encode = EncodeTypes.QR, Text = "Hello QR", File = Path.Combine(tempDir, "qr.png") },
            new { Encode = EncodeTypes.DataMatrix, Text = "Hello DM", File = Path.Combine(tempDir, "datamatrix.png") },
            new { Encode = EncodeTypes.Aztec, Text = "Hello Aztec", File = Path.Combine(tempDir, "aztec.png") }
        };

        // Generate barcode images and save them as PNG files
        foreach (var s in samples)
        {
            using (var generator = new BarcodeGenerator(s.Encode, s.Text))
            {
                // Set module size (pixel dimension) for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Save(s.File, BarCodeImageFormat.Png);
            }
        }

        // Read barcodes using the predefined Types2D set (auto-detect common 2D symbologies)
        Console.WriteLine("Reading barcodes with Types2D set:");
        foreach (var s in samples)
        {
            if (!File.Exists(s.File))
            {
                Console.WriteLine($"File not found: {s.File}");
                continue;
            }

            // Initialize reader with DecodeType.Types2D to automatically detect QR, DataMatrix, Aztec, etc.
            using (var reader = new BarCodeReader(s.File, DecodeType.Types2D))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"{Path.GetFileName(s.File)} -> {result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Optional cleanup: uncomment the line below to delete temporary files after execution
        // Directory.Delete(tempDir, true);
    }
}