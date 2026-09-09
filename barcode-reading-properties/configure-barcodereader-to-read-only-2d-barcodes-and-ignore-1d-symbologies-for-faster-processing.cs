// Title: Read Only 2D Barcodes with BarCodeReader
// Description: Demonstrates configuring Aspose.BarCode's BarCodeReader to detect only 2‑dimensional symbologies, ignoring 1D barcodes for faster processing.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to limit decoding to 2D symbologies using the BarCodeReader class. Typical use cases include high‑throughput scanning where only QR, DataMatrix, PDF417, etc., are relevant, reducing CPU load. Developers often need to set DecodeType to Types2D to speed up processing and avoid unwanted 1D results.
// Prompt: Configure BarCodeReader to read only 2D barcodes and ignore 1D symbologies for faster processing.
// Tags: barcode, 2d, symbology, recognition, aspose.barcode, decode, reader, performance

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates configuring BarCodeReader to read only 2D barcodes and ignore 1D symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Generates sample QR and Code128 barcodes, then reads only the 2D barcode from each file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the sample barcodes
        string qrPath = Path.Combine(tempFolder, "qr.png");
        string code128Path = Path.Combine(tempFolder, "code128.png");

        // Generate a QR Code (2D) and save it as PNG
        using (BarcodeGenerator qrGenerator = new BarcodeGenerator(EncodeTypes.QR, "Hello 2D"))
        {
            qrGenerator.Save(qrPath, BarCodeImageFormat.Png);
        }

        // Generate a Code128 barcode (1D) and save it as PNG
        using (BarcodeGenerator code128Generator = new BarcodeGenerator(EncodeTypes.Code128, "Hello1D"))
        {
            code128Generator.Save(code128Path, BarCodeImageFormat.Png);
        }

        // List of files to be processed by the reader
        string[] files = new[] { qrPath, code128Path };

        foreach (string file in files)
        {
            // Verify that the file exists before attempting to read it
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Configure BarCodeReader to detect only 2D barcodes (ignore 1D symbologies)
            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Types2D))
            {
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine($"{Path.GetFileName(file)}: No 2D barcode detected.");
                }
                else
                {
                    // Output each detected 2D barcode's type and text
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"{Path.GetFileName(file)}: {result.CodeTypeName} - {result.CodeText}");
                    }
                }
            }
        }

        // Optional cleanup of the temporary folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any errors during cleanup
        }
    }
}