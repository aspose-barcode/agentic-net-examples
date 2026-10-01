// Title: Extract barcode region coordinates from a PNG image and store them in a CSV (simulated database)
// Description: This example generates a Code128 barcode, reads its location within the PNG file, and writes the region coordinates to a CSV file that can be replaced by a database insert.
// Category-Description: Shows how to use Aspose.BarCode's BarcodeGenerator for barcode creation and BarCodeReader for recognition, focusing on the Region property to obtain placement coordinates. Typical scenarios include inventory systems, document processing, and quality control where the exact position of a barcode must be recorded. Developers often need to combine generation, detection, and data persistence using classes like BarcodeGenerator, BarCodeReader, BarCodeResult, and QualitySettings.
// Prompt: Extract barcode placement region coordinates from a PNG file and store them in a database.
// Tags: barcode, code128, generation, recognition, region extraction, png, csv, database, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates extracting barcode placement region coordinates from a PNG image
/// and persisting them (simulated via CSV). The CSV output can be replaced with
/// actual database insertion logic as needed.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads its region,
    /// and writes the details to a CSV file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeRegionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the sample barcode image
        string barcodeImagePath = Path.Combine(tempFolder, "sample.png");

        // Generate a sample barcode (Code128) and save as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // No need to set size; defaults adapt to the code text
            generator.Save(barcodeImagePath, BarCodeImageFormat.Png);
        }

        // Verify the image file exists before processing
        if (!File.Exists(barcodeImagePath))
        {
            Console.WriteLine($"File not found: {barcodeImagePath}");
            return;
        }

        // Simulate database storage by writing to a CSV file
        string csvPath = Path.Combine(tempFolder, "barcode_regions.csv");
        using (var csvWriter = new StreamWriter(csvPath, false))
        {
            // Write CSV header
            csvWriter.WriteLine("FilePath,CodeText,CodeType,X,Y,Width,Height,Angle");

            // Read barcodes from the PNG image
            using (var reader = new BarCodeReader(barcodeImagePath, DecodeType.AllSupportedTypes))
            {
                // Optional: set high-performance quality settings
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Read all barcode results
                BarCodeResult[] results = reader.ReadBarCodes();

                foreach (var result in results)
                {
                    // Extract region rectangle
                    var rect = result.Region.Rectangle;

                    // Write details to CSV (simulating DB insert)
                    csvWriter.WriteLine($"{barcodeImagePath},{EscapeCsv(result.CodeText)},{EscapeCsv(result.CodeTypeName)},{rect.X},{rect.Y},{rect.Width},{rect.Height},{result.Region.Angle}");
                }
            }
        }

        Console.WriteLine($"Barcode region data written to: {csvPath}");
        // In a real implementation, replace the CSV write with actual database insertion logic.
    }

    // Helper to escape CSV fields that may contain commas or quotes
    private static string EscapeCsv(string field)
    {
        if (field == null) return "";
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }
}