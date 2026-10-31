// Title: Read barcodes from a zip archive and aggregate metadata
// Description: Demonstrates generating several barcode types, packaging them into a zip file, then reading each image to extract barcode data and extended metadata.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create QR, Aztec, DotCode, and PDF417 barcodes with structured‑append or macro metadata, compress them into a ZIP archive, and then employ BarCodeReader with DecodeType.AllSupportedTypes to decode the images and collect extended information. Developers working with bulk barcode processing, archival storage, or metadata extraction will find this pattern useful.
// Prompt: Read barcodes from a zip archive containing multiple image files and aggregate metadata.
// Tags: barcode, generation, recognition, zip, metadata, qr, aztec, dotcode, pdf417, aspose.barcode

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates barcodes, stores them in a ZIP archive,
/// reads each image back, and aggregates the extracted barcode metadata.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List of generated barcode file paths
        var barcodeFiles = new List<string>();

        // ------------------------------------------------------------
        // Generate QR code with structured append metadata
        // ------------------------------------------------------------
        string qrPath = Path.Combine(tempFolder, "qr.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleQR"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.QR.StructuredAppend.TotalCount = 2;
            generator.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = 0;
            generator.Parameters.Barcode.QR.StructuredAppend.ParityByte = 123;
            generator.Save(qrPath, BarCodeImageFormat.Png);
        }
        barcodeFiles.Add(qrPath);

        // ------------------------------------------------------------
        // Generate Aztec code with metadata
        // ------------------------------------------------------------
        string aztecPath = Path.Combine(tempFolder, "aztec.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Aztec, "SampleAztec"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.Aztec.SymbolMode = AztecSymbolMode.FullRange;
            generator.Parameters.Barcode.Aztec.IsReaderInitialization = true;
            generator.Parameters.Barcode.Aztec.StructuredAppendBarcodeId = 2;
            generator.Parameters.Barcode.Aztec.StructuredAppendBarcodesCount = 3;
            generator.Parameters.Barcode.Aztec.StructuredAppendFileId = "File01";
            generator.Save(aztecPath, BarCodeImageFormat.Png);
        }
        barcodeFiles.Add(aztecPath);

        // ------------------------------------------------------------
        // Generate DotCode with metadata
        // ------------------------------------------------------------
        string dotPath = Path.Combine(tempFolder, "dotcode.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "SampleDot"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.DotCode.IsReaderInitialization = true;
            generator.Parameters.Barcode.DotCode.StructuredAppendModeBarcodesCount = 2;
            generator.Parameters.Barcode.DotCode.StructuredAppendModeBarcodeId = 1;
            generator.Save(dotPath, BarCodeImageFormat.Png);
        }
        barcodeFiles.Add(dotPath);

        // ------------------------------------------------------------
        // Generate PDF417 with macro metadata
        // ------------------------------------------------------------
        string pdf417Path = Path.Combine(tempFolder, "pdf417.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "SamplePDF417"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Parameters.Barcode.Pdf417.MacroPdf417FileID = 100;
            generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentID = 1;
            generator.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount = 2;
            generator.Save(pdf417Path, BarCodeImageFormat.Png);
        }
        barcodeFiles.Add(pdf417Path);

        // ------------------------------------------------------------
        // Create a zip archive containing the generated images
        // ------------------------------------------------------------
        string zipPath = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N") + ".zip");
        ZipFile.CreateFromDirectory(tempFolder, zipPath);

        // Prepare a list to hold aggregated metadata
        var aggregated = new List<string>();

        // ------------------------------------------------------------
        // Open the zip archive and read each image
        // ------------------------------------------------------------
        using (var zip = ZipFile.OpenRead(zipPath))
        {
            foreach (var entry in zip.Entries)
            {
                // Simple filter for common image extensions
                string ext = Path.GetExtension(entry.Name).ToLowerInvariant();
                if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".bmp" && ext != ".gif")
                    continue;

                using (var entryStream = entry.Open())
                using (var ms = new MemoryStream())
                {
                    entryStream.CopyTo(ms);
                    ms.Position = 0;

                    BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
                    using (var reader = new BarCodeReader(ms, decodeType))
                    {
                        foreach (var result in reader.ReadBarCodes())
                        {
                            var lines = new List<string>();
                            lines.Add($"File: {entry.Name}");
                            lines.Add($"  CodeType: {result.CodeTypeName}");
                            lines.Add($"  CodeText: {result.CodeText}");

                            // QR metadata
                            if (result.Extended.QR != null)
                            {
                                var qr = result.Extended.QR;
                                if (qr.StructuredAppendModeBarCodesQuantity != -1)
                                    lines.Add($"  QR StructuredAppend Quantity: {qr.StructuredAppendModeBarCodesQuantity}");
                                if (qr.StructuredAppendModeBarCodeIndex != -1)
                                    lines.Add($"  QR StructuredAppend Index: {qr.StructuredAppendModeBarCodeIndex}");
                                if (qr.StructuredAppendModeParityData != -1)
                                    lines.Add($"  QR StructuredAppend Parity: {qr.StructuredAppendModeParityData}");
                            }

                            // Aztec metadata
                            if (result.Extended.Aztec != null)
                            {
                                var az = result.Extended.Aztec;
                                lines.Add($"  Aztec IsReaderInitialization: {az.IsReaderInitialization}");
                                lines.Add($"  Aztec StructuredAppend BarcodeId: {az.StructuredAppendBarcodeId}");
                                lines.Add($"  Aztec StructuredAppend BarcodesCount: {az.StructuredAppendBarcodesCount}");
                                lines.Add($"  Aztec StructuredAppend FileId: {az.StructuredAppendFileId}");
                            }

                            // DotCode metadata
                            if (result.Extended.DotCode != null)
                            {
                                var dot = result.Extended.DotCode;
                                lines.Add($"  DotCode IsReaderInitialization: {dot.IsReaderInitialization}");
                                lines.Add($"  DotCode StructuredAppend BarcodesCount: {dot.StructuredAppendModeBarcodesCount}");
                                lines.Add($"  DotCode StructuredAppend BarcodeId: {dot.StructuredAppendModeBarcodeId}");
                            }

                            // PDF417 metadata
                            if (result.Extended.Pdf417 != null)
                            {
                                var pdf = result.Extended.Pdf417;
                                lines.Add($"  PDF417 MacroFileID: {pdf.MacroPdf417FileID}");
                                lines.Add($"  PDF417 MacroSegmentID: {pdf.MacroPdf417SegmentID}");
                                lines.Add($"  PDF417 MacroSegmentsCount: {pdf.MacroPdf417SegmentsCount}");
                            }

                            aggregated.Add(string.Join(Environment.NewLine, lines));
                        }
                    }
                }
            }
        }

        // ------------------------------------------------------------
        // Output aggregated metadata
        // ------------------------------------------------------------
        Console.WriteLine("Aggregated Barcode Metadata:");
        foreach (var entryInfo in aggregated)
        {
            Console.WriteLine(entryInfo);
            Console.WriteLine(new string('-', 40));
        }

        // ------------------------------------------------------------
        // Cleanup temporary files (optional)
        // ------------------------------------------------------------
        try { Directory.Delete(tempFolder, true); } catch { }
        try { File.Delete(zipPath); } catch { }
    }
}