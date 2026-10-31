// Title: Generate Swiss Post Parcel barcodes from Excel and log checksum verification
// Description: Demonstrates creating Swiss Post Parcel barcodes from identifiers stored in an Excel spreadsheet, saving images, reading them back to verify checksum, and logging results.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for SwissPostParcel symbology, BarCodeReader for decoding, and Aspose.Cells for Excel handling. Typical scenarios include batch barcode creation from data sources, checksum validation, and automated logging for quality control.
// Prompt: Generate Swiss Post Parcel international barcodes from a spreadsheet and include checksum verification logs.
// Tags: barcode, swisspostparcel, generation, recognition, checksum, excel, aspose.barcode, aspose.cells

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Cells;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Swiss Post Parcel barcodes from an Excel file,
/// verifying them via reading, and logging checksum correction information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary workspace, writes sample identifiers to an Excel file,
    /// generates barcode images, reads them back to verify checksum, and writes a log file.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary directory for all generated files
        string baseDir = Path.Combine(Path.GetTempPath(), "SwissPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(baseDir);

        // Define paths for the Excel source file and the checksum log file
        string excelPath = Path.Combine(baseDir, "Identifiers.xlsx");
        string logPath = Path.Combine(baseDir, "ChecksumLog.txt");

        // Sample identifiers: one with correct checksum, one missing checksum, one with wrong checksum
        List<string> sampleIds = new List<string>
        {
            "RM999605013CH", // correct checksum
            "RM99960501CH",  // missing checksum
            "RM999605017CH"  // wrong checksum
        };

        // Create an Excel workbook and write the sample identifiers into the first column
        using (Workbook wb = new Workbook())
        {
            Worksheet ws = wb.Worksheets[0];
            ws.Cells[0, 0].PutValue("Identifier");
            for (int i = 0; i < sampleIds.Count; i++)
            {
                ws.Cells[i + 1, 0].PutValue(sampleIds[i]);
            }
            wb.Save(excelPath, SaveFormat.Xlsx);
        }

        // Read identifiers back from the Excel file
        List<string> identifiers = new List<string>();
        using (Workbook wb = new Workbook(excelPath))
        {
            Worksheet ws = wb.Worksheets[0];
            int row = 1;
            while (!string.IsNullOrEmpty(ws.Cells[row, 0].StringValue))
            {
                identifiers.Add(ws.Cells[row, 0].StringValue);
                row++;
            }
        }

        // Process each identifier: generate barcode, read it back, compare checksums, and log the result
        int index = 1;
        foreach (string id in identifiers)
        {
            string imagePath = Path.Combine(baseDir, $"Barcode_{index}.png");
            string readText = GenerateAndReadBarcode(id, imagePath);
            bool checksumCorrected = !string.Equals(id, readText, StringComparison.Ordinal);
            string logLine = $"Original:{id} | Generated:{readText} | ChecksumCorrected:{checksumCorrected}";
            Console.WriteLine(logLine);
            File.AppendAllText(logPath, logLine + Environment.NewLine);
            index++;
        }

        Console.WriteLine($"Processing complete. Files saved to: {baseDir}");
    }

    /// <summary>
    /// Generates a Swiss Post Parcel barcode image from the supplied text, saves it,
    /// then reads the barcode back to obtain the possibly corrected code text.
    /// </summary>
    /// <param name="codeText">The identifier to encode.</param>
    /// <param name="imagePath">File path where the barcode image will be saved.</param>
    /// <returns>The code text read from the generated barcode image.</returns>
    static string GenerateAndReadBarcode(string codeText, string imagePath)
    {
        // Generate barcode image using SwissPostParcel symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Read the barcode back to verify checksum (or obtain corrected value)
        string resultText = string.Empty;
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.SwissPostParcel))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                resultText = result.CodeText;
                break; // only need the first result
            }
        }

        return resultText;
    }
}