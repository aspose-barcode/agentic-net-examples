// Title: Batch decode Swiss Post Parcel barcodes and generate Excel report
// Description: Demonstrates generating sample Swiss Post Parcel barcodes, decoding them in batch, and exporting the results to an Excel file.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader to decode them, and Aspose.Cells to build a spreadsheet report. Typical use cases include processing folders with mixed‑format images, extracting barcode data, and summarizing results for downstream systems. Developers often need to combine barcode handling with reporting utilities, making this pattern common in logistics and inventory applications.
// Prompt: Perform batch decoding of Swiss Post Parcel domestic barcodes from a mixed‑format folder and generate an Excel report.
// Tags: swisspostparcel, barcode, batch-decoding, excel, aspose.barcode, aspose.cells, report

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Cells;
using Aspose.Cells.Drawing;

namespace BatchSwissPostDecode
{
    /// <summary>
    /// Demonstrates batch generation, decoding, and reporting of Swiss Post Parcel barcodes.
    /// </summary>
    class Program
    {
        // Simple DTO to hold decoding results
        private class DecodedInfo
        {
            public string FilePath { get; set; }
            public string CodeType { get; set; }
            public string CodeText { get; set; }
        }

        /// <summary>
        /// Entry point. Generates sample barcodes, decodes them, and creates an Excel report.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Create a unique temporary folder for the sample files
            string tempFolder = Path.Combine(Path.GetTempPath(), "BatchSwissPost_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempFolder);

            // -----------------------------------------------------------------
            // Generate a few sample Swiss Post Parcel barcode images
            // -----------------------------------------------------------------
            List<string> barcodeFiles = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                string filePath = Path.Combine(tempFolder, $"SwissPostParcel_{i}.png");
                string codeText = $"12345678{i}"; // simple varying code text

                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, codeText))
                {
                    // Optional: adjust barcode appearance if needed
                    // generator.Parameters.Barcode.XDimension.Point = 2f;

                    // Save directly to PNG file
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                barcodeFiles.Add(filePath);
            }

            // -----------------------------------------------------------------
            // Decode the generated barcodes
            // -----------------------------------------------------------------
            List<DecodedInfo> decodedResults = new List<DecodedInfo>();
            foreach (string file in barcodeFiles)
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found, skipping: {file}");
                    continue;
                }

                try
                {
                    using (BarCodeReader reader = new BarCodeReader(file, DecodeType.SwissPostParcel))
                    {
                        foreach (var result in reader.ReadBarCodes())
                        {
                            decodedResults.Add(new DecodedInfo
                            {
                                FilePath = file,
                                CodeType = result.CodeTypeName,
                                CodeText = result.CodeText
                            });
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    // Handles "Image loading failed" or other argument issues
                    Console.WriteLine($"Failed to read '{file}': {ex.Message}");
                }
            }

            // -----------------------------------------------------------------
            // Create an Excel report with the decoding results
            // -----------------------------------------------------------------
            string reportPath = Path.Combine(tempFolder, "SwissPostParcelReport.xlsx");
            using (Workbook workbook = new Workbook())
            {
                Worksheet sheet = workbook.Worksheets[0];
                // Header row
                sheet.Cells[0, 0].PutValue("File");
                sheet.Cells[0, 1].PutValue("Barcode Type");
                sheet.Cells[0, 2].PutValue("Code Text");

                int row = 1;
                foreach (var info in decodedResults)
                {
                    sheet.Cells[row, 0].PutValue(info.FilePath);
                    sheet.Cells[row, 1].PutValue(info.CodeType);
                    sheet.Cells[row, 2].PutValue(info.CodeText);
                    row++;
                }

                workbook.Save(reportPath, SaveFormat.Xlsx);
            }

            Console.WriteLine($"Batch decoding completed. Report saved to: {reportPath}");
        }
    }
}