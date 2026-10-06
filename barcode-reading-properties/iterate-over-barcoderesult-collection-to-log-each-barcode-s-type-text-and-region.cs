// Title: Iterate over BarCodeResult collection to log barcode details
// Description: Generates sample barcodes, reads them back, and logs each barcode's type, text, and region.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create barcodes, BarCodeReader to decode them, and BarCodeResult to access detailed information such as code type, text, and region. Typical use cases include batch barcode processing, automated verification, and extracting positional data for downstream workflows. Developers often need to iterate over recognition results to log or act upon each detected symbol.
// Prompt: Iterate over BarCodeResult collection to log each barcode's type, text, and region.
// Tags: barcode symbology, generation, recognition, result iteration, console output, aspose.barcode, c#

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcodes, reading them, and iterating over <see cref="BarCodeResult"/> collection to log details.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, reads them, and outputs type, text, and region information.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store generated barcode images.
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcodes to generate: type, text, and output file name.
        var samples = new List<(BaseEncodeType encode, string text, string fileName)>
        {
            (EncodeTypes.Code128, "ABC123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DataMatrixSample", "datamatrix.png")
        };

        // Generate each barcode image and save it to the temporary folder.
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.fileName);
            using (var generator = new BarcodeGenerator(sample.encode, sample.text))
            {
                // Set X-dimension for better visual quality.
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Read each generated barcode image and log its details.
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.fileName);
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Retrieve all recognized barcode results from the image.
                BarCodeResult[] results = reader.ReadBarCodes();
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"File: {sample.fileName}");
                    Console.WriteLine($"Type: {result.CodeTypeName}");
                    Console.WriteLine($"Text: {result.CodeText}");
                    var rect = result.Region.Rectangle;
                    Console.WriteLine($"Region: X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}, Angle={result.Region.Angle}");
                }
            }
        }

        // Attempt to clean up the temporary folder; ignore any errors.
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Cleanup ignored
        }
    }
}