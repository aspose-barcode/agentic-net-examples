// Title: Batch decode Swiss Post Parcel additional service barcodes from PDF
// Description: Demonstrates generating Swiss Post Parcel barcodes, embedding them into a PDF, and batch decoding them to retrieve the human‑readable service codes.
// Category-Description: This example belongs to the Aspose.BarCode PDF integration category, showcasing how to use BarcodeGenerator, BarCodeReader, and Aspose.Pdf to create, embed, and recognize barcodes in documents. Typical use cases include automated processing of shipping labels, batch verification of barcode data, and extracting encoded information from multi‑page PDFs. Developers often need to generate barcode images, insert them into PDFs, and later decode them efficiently.
// Prompt: Perform batch decoding of Swiss Post Parcel additional service code barcodes from a PDF and extract human‑readable text.
// Tags: swisspostparcel, barcode, batch decoding, pdf, aspose.barcode, aspose.pdf, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Example program that generates Swiss Post Parcel barcodes, embeds them into a PDF,
/// and then decodes each barcode from the PDF pages, outputting the results to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Executes the full workflow: create temporary files, generate barcodes,
    /// build a PDF, decode the barcodes, and clean up resources.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Prepare a temporary working folder
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "SwissPostBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the PDF that will contain the generated barcodes
        string pdfPath = Path.Combine(tempFolder, "Barcodes.pdf");

        // Sample Swiss Post Parcel additional service codes to encode
        List<string> serviceCodes = new List<string> { "0327", "0341", "0610" };

        // Keep barcode image streams alive until the PDF is saved
        List<MemoryStream> barcodeStreams = new List<MemoryStream>();

        // --------------------------------------------------------------------
        // 2. Generate barcode images for each service code
        // --------------------------------------------------------------------
        foreach (string code in serviceCodes)
        {
            MemoryStream ms = new MemoryStream();
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
            {
                // Configure visual appearance
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 40f;

                // Save barcode as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
            }
            ms.Position = 0;               // Reset stream position for later reading
            barcodeStreams.Add(ms);
        }

        // --------------------------------------------------------------------
        // 3. Build a PDF document with one barcode per page (evaluation limit: 4 pages)
        // --------------------------------------------------------------------
        using (Document pdfDoc = new Document())
        {
            int pageCount = Math.Min(barcodeStreams.Count, 4);
            for (int i = 0; i < pageCount; i++)
            {
                MemoryStream imgStream = barcodeStreams[i];
                imgStream.Position = 0;   // Ensure stream is at the beginning

                // Add a new page and place the barcode image on it
                Page page = pdfDoc.Pages.Add();
                page.Paragraphs.Add(new Aspose.Pdf.Image { ImageStream = imgStream });
            }
            pdfDoc.Save(pdfPath);
        }

        // --------------------------------------------------------------------
        // 4. Decode barcodes from each page of the PDF
        // --------------------------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath))
        {
            PdfConverter pdfConverter = new PdfConverter(pdfDoc);
            pdfConverter.RenderingOptions.BarcodeOptimization = true; // Enable barcode‑specific rendering

            int totalPages = pdfDoc.Pages.Count;
            for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
            {
                // Convert a single page to an image for barcode reading
                pdfConverter.StartPage = pageNumber;
                pdfConverter.EndPage = pageNumber;
                pdfConverter.DoConvert();

                using (MemoryStream pageImage = new MemoryStream())
                {
                    pdfConverter.GetNextImage(pageImage);
                    pageImage.Position = 0;

                    // Read barcodes from the page image
                    using (BarCodeReader reader = new BarCodeReader(pageImage, DecodeType.SwissPostParcel))
                    {
                        BarCodeResult[] results = reader.ReadBarCodes();
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"Page {pageNumber}: Type={result.CodeTypeName}, Text={result.CodeText}");
                        }
                    }
                }
            }
        }

        // --------------------------------------------------------------------
        // 5. Clean up temporary files and streams
        // --------------------------------------------------------------------
        foreach (MemoryStream ms in barcodeStreams)
        {
            ms.Dispose();
        }
        try { File.Delete(pdfPath); } catch { }
        try { Directory.Delete(tempFolder, true); } catch { }
    }
}