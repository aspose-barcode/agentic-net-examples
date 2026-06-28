using System;
using System.IO;
using System.Text;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Demonstrates how to extract HIBC barcodes from each page of a PDF file using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts an optional command‑line argument specifying the PDF file path.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument may be a PDF file path.</param>
    static void Main(string[] args)
    {
        // Determine the PDF file path: use the first argument if supplied, otherwise default to "sample.pdf".
        string pdfPath = args.Length > 0 ? args[0] : "sample.pdf";

        // Verify that the file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF document for reading.
        using (var pdfDoc = new Document(pdfPath))
        {
            // Initialize a PdfConverter to render PDF pages as images.
            using (var pdfConverter = new PdfConverter(pdfDoc))
            {
                // Enable barcode optimization to improve detection performance.
                pdfConverter.RenderingOptions.BarcodeOptimization = true;

                // Process each page in the PDF.
                for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    // Configure the converter to render only the current page.
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Store the rendered page image in a memory stream.
                    using (var pageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(pageStream);
                        pageStream.Position = 0; // Reset stream position for reading.

                        // Use BarCodeReader to detect all supported barcodes in the image.
                        using (var reader = new BarCodeReader(pageStream, DecodeType.AllSupportedTypes))
                        {
                            var results = reader.ReadBarCodes();
                            var sb = new StringBuilder();

                            // Examine each detected barcode.
                            foreach (var result in results)
                            {
                                // Process only barcodes whose type name contains "HIBC".
                                if (result.CodeTypeName != null && result.CodeTypeName.Contains("HIBC"))
                                {
                                    // Attempt to decode complex HIBC LIC data for richer information.
                                    var hibc = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);
                                    if (hibc != null)
                                    {
                                        sb.Append(result.CodeText);
                                        sb.Append(" (decoded)");
                                    }
                                    else
                                    {
                                        sb.Append(result.CodeText);
                                    }

                                    sb.Append("; ");
                                }
                            }

                            // Prepare output: either the concatenated HIBC data or a not‑found message.
                            string combinedData = sb.Length > 0
                                ? sb.ToString().TrimEnd(' ', ';')
                                : "No HIBC barcodes found";

                            Console.WriteLine($"Page {pageNumber}: {combinedData}");
                        }
                    }
                }
            }
        }
    }
}