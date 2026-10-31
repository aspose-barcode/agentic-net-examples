// Title: Batch decode Swiss Post Parcel additional service barcodes from PDF
// Description: Generates Swiss Post Parcel barcodes, embeds them into a PDF, then batch decodes them to extract the human‑readable service codes.
// Category-Description: This example demonstrates how to use Aspose.BarCode for barcode generation (BarcodeGenerator) and recognition (BarCodeReader) together with Aspose.Pdf to create a PDF document containing barcode images and then perform batch decoding. Typical scenarios include automated processing of shipping labels, bulk verification of parcel service codes, and integration of barcode data extraction into document workflows. Developers working with barcode symbologies, PDF generation, and bulk scanning will find these APIs essential.
// Prompt: Perform batch decoding of Swiss Post Parcel additional service code barcodes from a PDF and extract human‑readable text.
// Tags: swisspost, barcode, batch decoding, pdf, aspose.barcode, aspose.pdf, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;

/// <summary>
/// Demonstrates generating Swiss Post Parcel barcodes, embedding them into a PDF,
/// and batch decoding the barcodes to retrieve their textual content.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcode images, builds a PDF, reads the barcodes,
    /// and cleans up temporary resources.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary folder for the PDF and define the output path.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "SwissPostBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string pdfPath = Path.Combine(tempFolder, "SwissPostBarcodes.pdf");

        // --------------------------------------------------------------------
        // Sample additional service codes (max 4 for evaluation mode).
        // --------------------------------------------------------------------
        string[] serviceCodes = new string[] { "0327", "0341", "0203", "0610" };

        // --------------------------------------------------------------------
        // List to hold image streams for PDF pages.
        // --------------------------------------------------------------------
        List<MemoryStream> imageStreams = new List<MemoryStream>();

        // --------------------------------------------------------------------
        // Generate barcode images and store them in memory streams.
        // --------------------------------------------------------------------
        foreach (string code in serviceCodes)
        {
            try
            {
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
                {
                    // Configure barcode appearance.
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;
                    generator.Parameters.Barcode.BarHeight.Pixels = 40f;

                    using (Aspose.Drawing.Bitmap bitmap = generator.GenerateBarCodeImage())
                    {
                        MemoryStream ms = new MemoryStream();
                        bitmap.Save(ms, Aspose.Drawing.Imaging.ImageFormat.Png);
                        ms.Position = 0; // Reset stream position for later reading.
                        imageStreams.Add(ms);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to generate barcode for code '{code}': {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // Build a PDF document with the generated barcode images.
        // --------------------------------------------------------------------
        try
        {
            using (Document pdfDoc = new Document())
            {
                foreach (MemoryStream imgStream in imageStreams)
                {
                    Page page = pdfDoc.Pages.Add();
                    Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image { ImageStream = imgStream };
                    page.Paragraphs.Add(pdfImage);
                }

                pdfDoc.Save(pdfPath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating PDF: {ex.Message}");
            Cleanup(tempFolder, imageStreams);
            return;
        }

        // --------------------------------------------------------------------
        // Verify that the PDF was created before attempting to decode.
        // --------------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine("PDF file not found.");
            Cleanup(tempFolder, imageStreams);
            return;
        }

        // --------------------------------------------------------------------
        // Batch decode barcodes from the PDF using the Swiss Post Parcel symbology.
        // --------------------------------------------------------------------
        try
        {
            using (BarCodeReader reader = new BarCodeReader(pdfPath, DecodeType.SwissPostParcel))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected.");
                }
                else
                {
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error reading barcodes: {ex.Message}");
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and streams.
        // --------------------------------------------------------------------
        Cleanup(tempFolder, imageStreams);
    }

    /// <summary>
    /// Disposes all memory streams and deletes the temporary folder used for the PDF.
    /// </summary>
    /// <param name="folderPath">Path to the temporary folder.</param>
    /// <param name="streams">List of memory streams to dispose.</param>
    static void Cleanup(string folderPath, List<MemoryStream> streams)
    {
        foreach (MemoryStream ms in streams)
        {
            ms.Dispose();
        }

        try
        {
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
        }
        catch
        {
            // Ignore cleanup errors.
        }
    }
}