using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Path to the multi‑page PDF file (adjust as needed)
        string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load PDF document
        using (var pdfDocument = new Document(pdfPath))
        {
            int pageCount = pdfDocument.Pages.Count;
            Console.WriteLine($"Processing PDF with {pageCount} page(s).");

            // Iterate through each page
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                Console.WriteLine($"\n--- Page {pageNumber} ---");

                // Render page to PNG image in memory
                using (var imageStream = new MemoryStream())
                {
                    var resolution = new Resolution(300);
                    var pngDevice = new PngDevice(resolution);
                    pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                    imageStream.Position = 0;

                    // Read all barcodes on the page
                    using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                    {
                        bool hibcFound = false;

                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            // Attempt to decode HIBC LIC complex codetext
                            var complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                            if (complex == null)
                                continue; // Not a HIBC LIC barcode

                            hibcFound = true;
                            Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                            Console.WriteLine($"Raw CodeText: {result.CodeText}");

                            // Handle combined, primary, or secondary codetext
                            switch (complex)
                            {
                                case HIBCLICCombinedCodetext combined:
                                    PrintCombinedData(combined);
                                    break;
                                case HIBCLICPrimaryDataCodetext primary:
                                    PrintPrimaryData(primary);
                                    break;
                                case HIBCLICSecondaryAndAdditionalDataCodetext secondary:
                                    PrintSecondaryData(secondary);
                                    break;
                                default:
                                    Console.WriteLine("Unsupported HIBC LIC codetext type.");
                                    break;
                            }
                        }

                        if (!hibcFound)
                        {
                            Console.WriteLine("No HIBC LIC barcode detected on this page.");
                        }
                    }
                }
            }
        }
    }

    static void PrintCombinedData(HIBCLICCombinedCodetext combined)
    {
        Console.WriteLine("=== Combined Data ===");
        if (combined.PrimaryData != null)
        {
            Console.WriteLine($"Product or Catalog Number: {combined.PrimaryData.ProductOrCatalogNumber}");
            Console.WriteLine($"Labeler Identification Code: {combined.PrimaryData.LabelerIdentificationCode}");
            Console.WriteLine($"Unit of Measure ID: {combined.PrimaryData.UnitOfMeasureID}");
        }

        if (combined.SecondaryAndAdditionalData != null)
        {
            var sec = combined.SecondaryAndAdditionalData;
            Console.WriteLine($"Expiry Date: {sec.ExpiryDate}");
            Console.WriteLine($"Expiry Date Format: {sec.ExpiryDateFormat}");
            Console.WriteLine($"Quantity: {sec.Quantity}");
            Console.WriteLine($"Lot Number: {sec.LotNumber}");
            Console.WriteLine($"Serial Number: {sec.SerialNumber}");
            Console.WriteLine($"Date of Manufacture: {sec.DateOfManufacture}");
        }
    }

    static void PrintPrimaryData(HIBCLICPrimaryDataCodetext primary)
    {
        Console.WriteLine("=== Primary Data ===");
        if (primary.Data != null)
        {
            Console.WriteLine($"Product or Catalog Number: {primary.Data.ProductOrCatalogNumber}");
            Console.WriteLine($"Labeler Identification Code: {primary.Data.LabelerIdentificationCode}");
            Console.WriteLine($"Unit of Measure ID: {primary.Data.UnitOfMeasureID}");
        }
    }

    static void PrintSecondaryData(HIBCLICSecondaryAndAdditionalDataCodetext secondary)
    {
        Console.WriteLine("=== Secondary and Additional Data ===");
        if (secondary.Data != null)
        {
            var sec = secondary.Data;
            Console.WriteLine($"Expiry Date: {sec.ExpiryDate}");
            Console.WriteLine($"Expiry Date Format: {sec.ExpiryDateFormat}");
            Console.WriteLine($"Quantity: {sec.Quantity}");
            Console.WriteLine($"Lot Number: {sec.LotNumber}");
            Console.WriteLine($"Serial Number: {sec.SerialNumber}");
            Console.WriteLine($"Date of Manufacture: {sec.DateOfManufacture}");
        }
    }
}