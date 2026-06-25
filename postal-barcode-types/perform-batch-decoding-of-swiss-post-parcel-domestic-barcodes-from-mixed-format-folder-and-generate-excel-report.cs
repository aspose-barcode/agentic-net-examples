using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;
using Aspose.Cells;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Demonstrates scanning various image files and PDF pages for Swiss Post parcel barcodes,
/// then writes the results to an Excel report.
/// </summary>
class Program
{
    // Simple data holder for each decoded barcode
    class DecodeRecord
    {
        public string FileName { get; set; }
        public int PageNumber { get; set; } // 0 for non‑PDF images
        public string BarcodeType { get; set; }
        public string CodeText { get; set; }
        public double ReadingQuality { get; set; }
        public int Confidence { get; set; }
    }

    /// <summary>
    /// Entry point of the application. Scans a folder for supported image/PDF files,
    /// extracts Swiss Post parcel barcodes, and generates an Excel report.
    /// </summary>
    static void Main()
    {
        // Input folder (mixed formats) and output Excel file
        string inputFolder = "BarcodesInput";
        string outputExcel = "SwissPostReport.xlsx";

        // Ensure input folder exists; create a sample image if it does not
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder \"{inputFolder}\" does not exist. Creating sample folder.");
            Directory.CreateDirectory(inputFolder);
            // Create a placeholder image so the demo runs without external files
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "123456789012"))
            {
                generator.Save(Path.Combine(inputFolder, "sample.png"));
            }
        }

        var records = new List<DecodeRecord>();

        // Process each file in the folder
        foreach (var filePath in Directory.GetFiles(inputFolder))
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext == ".pdf")
            {
                // PDF files may contain multiple pages; each page is processed separately
                ProcessPdfFile(filePath, records);
            }
            else if (ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".tif" || ext == ".tiff")
            {
                // Image files are processed directly; page number is 0
                ProcessImageFile(filePath, 0, records);
            }
            else
            {
                // Unsupported file types are ignored with a console message
                Console.WriteLine($"Skipping unsupported file: {Path.GetFileName(filePath)}");
            }
        }

        // Generate Excel report from collected barcode data
        GenerateExcelReport(records, outputExcel);
        Console.WriteLine($"Report generated: {outputExcel}");
    }

    static void ProcessPdfFile(string pdfPath, List<DecodeRecord> records)
    {
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load PDF document using Aspose.Pdf
        using (var pdfDocument = new Document(pdfPath))
        {
            var pdfConverter = new PdfConverter(pdfDocument);
            pdfConverter.RenderingOptions.BarcodeOptimization = true;

            // Iterate through each page, converting it to an image stream for barcode reading
            for (int page = 1; page <= pdfDocument.Pages.Count; page++)
            {
                pdfConverter.StartPage = page;
                pdfConverter.EndPage = page;
                pdfConverter.DoConvert();

                using (var ms = new MemoryStream())
                {
                    pdfConverter.GetNextImage(ms);
                    ms.Position = 0;
                    ProcessImageStream(ms, Path.GetFileName(pdfPath), page, records);
                }
            }
        }
    }

    static void ProcessImageFile(string imagePath, int pageNumber, List<DecodeRecord> records)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Open image file as a stream and delegate to the common image processing method
        using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
        {
            ProcessImageStream(stream, Path.GetFileName(imagePath), pageNumber, records);
        }
    }

    static void ProcessImageStream(Stream imageStream, string fileName, int pageNumber, List<DecodeRecord> records)
    {
        // Initialize barcode reader for Swiss Post parcel barcodes
        using (var reader = new BarCodeReader(imageStream, DecodeType.SwissPostParcel))
        {
            // Use a balanced quality preset
            reader.QualitySettings = QualitySettings.NormalQuality;

            // Iterate over all detected barcodes in the image/stream
            foreach (var result in reader.ReadBarCodes())
            {
                var rec = new DecodeRecord
                {
                    FileName = fileName,
                    PageNumber = pageNumber,
                    BarcodeType = result.CodeTypeName,
                    CodeText = result.CodeText,
                    ReadingQuality = result.ReadingQuality,
                    Confidence = (int)result.Confidence
                };
                records.Add(rec);
            }
        }
    }

    static void GenerateExcelReport(List<DecodeRecord> records, string outputPath)
    {
        var workbook = new Workbook();
        var sheet = workbook.Worksheets[0];
        sheet.Name = "Swiss Post Parcel";

        // Header row
        sheet.Cells[0, 0].PutValue("File Name");
        sheet.Cells[0, 1].PutValue("Page #");
        sheet.Cells[0, 2].PutValue("Barcode Type");
        sheet.Cells[0, 3].PutValue("Code Text");
        sheet.Cells[0, 4].PutValue("Reading Quality");
        sheet.Cells[0, 5].PutValue("Confidence");

        // Populate rows with barcode data
        int row = 1;
        foreach (var rec in records)
        {
            sheet.Cells[row, 0].PutValue(rec.FileName);
            sheet.Cells[row, 1].PutValue(rec.PageNumber);
            sheet.Cells[row, 2].PutValue(rec.BarcodeType);
            sheet.Cells[row, 3].PutValue(rec.CodeText);
            sheet.Cells[row, 4].PutValue(rec.ReadingQuality);
            sheet.Cells[row, 5].PutValue(rec.Confidence);
            row++;
        }

        // Auto‑fit columns for readability
        sheet.AutoFitColumns();

        // Save the workbook to the specified path
        workbook.Save(outputPath);
    }
}