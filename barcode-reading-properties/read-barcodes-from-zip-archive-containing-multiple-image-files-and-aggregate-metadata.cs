// Title: Read barcodes from a zip archive and aggregate extended metadata
// Description: Demonstrates generating several barcode images with metadata, packaging them into a zip file, then reading each image to collect barcode type, text, and extended data.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating QR, Aztec, PDF417, and DotCode barcodes with structured‑append or macro metadata, and BarCodeReader for decoding all supported types. Developers often need to batch‑process images stored in archives, extract barcode information, and aggregate metadata for reporting or further processing.
// Prompt: Read barcodes from a zip archive containing multiple image files and aggregate metadata.
// Tags: barcode, qr, aztec, pdf417, dotcode, structuredappend, macro, zip, metadata, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcode images with metadata, zipping them, and extracting aggregated metadata using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample barcodes, archives them, reads back the barcodes, and prints aggregated metadata.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // 1. Create a temporary folder to store generated barcode images.
        // --------------------------------------------------------------------
        string imagesFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(imagesFolder);

        // List to keep track of generated image file paths.
        List<string> imageFiles = new List<string>();

        // --------------------------------------------------------------------
        // 2. Generate a QR code with Structured Append metadata.
        // --------------------------------------------------------------------
        string qrPath = Path.Combine(imagesFolder, "qr.png");
        using (BarcodeGenerator qrGen = new BarcodeGenerator(EncodeTypes.QR, "SampleQR"))
        {
            qrGen.Parameters.Barcode.XDimension.Pixels = 4f;
            qrGen.Parameters.Barcode.QR.StructuredAppend.TotalCount = 2;
            qrGen.Parameters.Barcode.QR.StructuredAppend.SequenceIndicator = 0;
            qrGen.Parameters.Barcode.QR.StructuredAppend.ParityByte = 123;
            qrGen.Save(qrPath, BarCodeImageFormat.Png);
        }
        imageFiles.Add(qrPath);

        // --------------------------------------------------------------------
        // 3. Generate an Aztec code with reader‑initialization and Structured Append metadata.
        // --------------------------------------------------------------------
        string aztecPath = Path.Combine(imagesFolder, "aztec.png");
        using (BarcodeGenerator aztecGen = new BarcodeGenerator(EncodeTypes.Aztec, "SampleAztec"))
        {
            aztecGen.Parameters.Barcode.XDimension.Pixels = 4f;
            aztecGen.Parameters.Barcode.Aztec.SymbolMode = AztecSymbolMode.FullRange;
            aztecGen.Parameters.Barcode.Aztec.IsReaderInitialization = true;
            aztecGen.Parameters.Barcode.Aztec.StructuredAppendBarcodeId = 2;
            aztecGen.Parameters.Barcode.Aztec.StructuredAppendBarcodesCount = 4;
            aztecGen.Parameters.Barcode.Aztec.StructuredAppendFileId = "File01";
            aztecGen.Save(aztecPath, BarCodeImageFormat.Png);
        }
        imageFiles.Add(aztecPath);

        // --------------------------------------------------------------------
        // 4. Generate a PDF417 barcode with macro metadata.
        // --------------------------------------------------------------------
        string pdf417Path = Path.Combine(imagesFolder, "pdf417.png");
        using (BarcodeGenerator pdfGen = new BarcodeGenerator(EncodeTypes.Pdf417, "SamplePDF417"))
        {
            pdfGen.Parameters.Barcode.XDimension.Pixels = 2f;
            pdfGen.Parameters.Barcode.Pdf417.MacroPdf417FileID = 10;
            pdfGen.Parameters.Barcode.Pdf417.MacroPdf417SegmentID = 1;
            pdfGen.Parameters.Barcode.Pdf417.MacroPdf417SegmentsCount = 3;
            pdfGen.Save(pdf417Path, BarCodeImageFormat.Png);
        }
        imageFiles.Add(pdf417Path);

        // --------------------------------------------------------------------
        // 5. Generate a DotCode barcode with Structured Append metadata.
        // --------------------------------------------------------------------
        string dotCodePath = Path.Combine(imagesFolder, "dotcode.png");
        using (BarcodeGenerator dotGen = new BarcodeGenerator(EncodeTypes.DotCode, "SampleDot"))
        {
            dotGen.Parameters.Barcode.XDimension.Pixels = 4f;
            dotGen.Parameters.Barcode.DotCode.IsReaderInitialization = true;
            dotGen.Parameters.Barcode.DotCode.StructuredAppendModeBarcodesCount = 4;
            dotGen.Parameters.Barcode.DotCode.StructuredAppendModeBarcodeId = 2;
            dotGen.Save(dotCodePath, BarCodeImageFormat.Png);
        }
        imageFiles.Add(dotCodePath);

        // --------------------------------------------------------------------
        // 6. Create a zip archive that contains all generated images.
        // --------------------------------------------------------------------
        string zipPath = Path.Combine(Path.GetTempPath(), "BarcodesZip_" + Guid.NewGuid().ToString("N") + ".zip");
        using (FileStream zipToCreate = new FileStream(zipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipToCreate, ZipArchiveMode.Update))
        {
            foreach (string file in imageFiles)
            {
                string entryName = Path.GetFileName(file);
                archive.CreateEntryFromFile(file, entryName);
            }
        }

        // Prepare a list to hold aggregated metadata records.
        List<MetadataRecord> records = new List<MetadataRecord>();

        // --------------------------------------------------------------------
        // 7. Read barcodes from the zip archive and collect extended metadata.
        // --------------------------------------------------------------------
        if (File.Exists(zipPath))
        {
            using (FileStream zipToOpen = new FileStream(zipPath, FileMode.Open, FileAccess.Read))
            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    // Process only supported image formats.
                    if (!entry.FullName.EndsWith(".png", StringComparison.OrdinalIgnoreCase) &&
                        !entry.FullName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) &&
                        !entry.FullName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) &&
                        !entry.FullName.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) &&
                        !entry.FullName.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    // Load the image entry into a memory stream for decoding.
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (Stream entryStream = entry.Open())
                        {
                            entryStream.CopyTo(ms);
                        }
                        ms.Position = 0;

                        // Decode all supported barcode types in the image.
                        using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                        {
                            foreach (BarCodeResult result in reader.ReadBarCodes())
                            {
                                // Create a metadata record for each detected barcode.
                                MetadataRecord record = new MetadataRecord
                                {
                                    FileName = entry.FullName,
                                    CodeTypeName = result.CodeTypeName,
                                    CodeText = result.CodeText,
                                    ExtendedData = new Dictionary<string, string>()
                                };

                                // QR metadata extraction.
                                if (result.Extended.QR != null)
                                {
                                    var qrExt = result.Extended.QR;
                                    record.ExtendedData["QR_BarCodesQuantity"] = qrExt.StructuredAppendModeBarCodesQuantity.ToString();
                                    record.ExtendedData["QR_BarCodeIndex"] = qrExt.StructuredAppendModeBarCodeIndex.ToString();
                                    record.ExtendedData["QR_ParityData"] = qrExt.StructuredAppendModeParityData.ToString();
                                }

                                // Aztec metadata extraction.
                                if (result.Extended.Aztec != null)
                                {
                                    var azExt = result.Extended.Aztec;
                                    record.ExtendedData["Aztec_IsReaderInitialization"] = azExt.IsReaderInitialization.ToString();
                                    record.ExtendedData["Aztec_StructuredAppendBarcodeId"] = azExt.StructuredAppendBarcodeId.ToString();
                                    record.ExtendedData["Aztec_StructuredAppendBarcodesCount"] = azExt.StructuredAppendBarcodesCount.ToString();
                                    record.ExtendedData["Aztec_StructuredAppendFileId"] = azExt.StructuredAppendFileId;
                                }

                                // DotCode metadata extraction.
                                if (result.Extended.DotCode != null)
                                {
                                    var dotExt = result.Extended.DotCode;
                                    record.ExtendedData["DotCode_IsReaderInitialization"] = dotExt.IsReaderInitialization.ToString();
                                    record.ExtendedData["DotCode_StructuredAppendBarcodesCount"] = dotExt.StructuredAppendModeBarcodesCount.ToString();
                                    record.ExtendedData["DotCode_StructuredAppendBarcodeId"] = dotExt.StructuredAppendModeBarcodeId.ToString();
                                }

                                // PDF417 metadata extraction.
                                if (result.Extended.Pdf417 != null)
                                {
                                    var pdfExt = result.Extended.Pdf417;
                                    record.ExtendedData["Pdf417_MacroFileID"] = pdfExt.MacroPdf417FileID.ToString();
                                    record.ExtendedData["Pdf417_MacroSegmentID"] = pdfExt.MacroPdf417SegmentID.ToString();
                                    record.ExtendedData["Pdf417_MacroSegmentsCount"] = pdfExt.MacroPdf417SegmentsCount.ToString();
                                }

                                records.Add(record);
                            }
                        }
                    }
                }
            }
        }

        // --------------------------------------------------------------------
        // 8. Output the aggregated metadata to the console.
        // --------------------------------------------------------------------
        Console.WriteLine("Aggregated Barcode Metadata:");
        foreach (var rec in records)
        {
            Console.WriteLine($"File: {rec.FileName}");
            Console.WriteLine($"  Type: {rec.CodeTypeName}");
            Console.WriteLine($"  Text: {rec.CodeText}");
            foreach (var kvp in rec.ExtendedData)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }
            Console.WriteLine();
        }

        // --------------------------------------------------------------------
        // 9. Clean up temporary files and directories.
        // --------------------------------------------------------------------
        try
        {
            foreach (string file in imageFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            if (Directory.Exists(imagesFolder))
                Directory.Delete(imagesFolder, true);
            if (File.Exists(zipPath))
                File.Delete(zipPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup warning: {ex.Message}");
        }
    }

    /// <summary>
    /// Simple DTO that holds barcode file name, type, text, and any extended metadata.
    /// </summary>
    class MetadataRecord
    {
        public string FileName { get; set; }
        public string CodeTypeName { get; set; }
        public string CodeText { get; set; }
        public Dictionary<string, string> ExtendedData { get; set; }
    }
}