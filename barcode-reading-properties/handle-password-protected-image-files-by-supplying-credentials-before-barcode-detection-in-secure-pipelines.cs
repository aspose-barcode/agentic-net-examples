// Title: Detect Barcodes in Password‑Protected PDF Files
// Description: Demonstrates opening a password‑protected PDF, rendering each page to an image, and scanning for barcodes using Aspose.BarCode.
// Category-Description: Shows how to work with secured PDF documents in the Aspose.BarCode suite. The example uses Aspose.Pdf Document and PdfConverter to render pages, then Aspose.BarCode.BarCodeRecognition.BarCodeReader to detect all supported barcode symbologies. Typical scenarios include automated processing pipelines where PDFs are encrypted and need barcode extraction without manual intervention.
// Prompt: Handle password‑protected image files by supplying credentials before barcode detection in secure pipelines.
// Tags: pdf, password, barcode detection, aspose.barcode, aspose.pdf, image rendering, decodeall

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Example program that opens a password‑protected PDF, converts each page to an image,
/// and reads any barcodes present using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Specifies the PDF path and password, then starts processing.
    /// </summary>
    static void Main()
    {
        // Sample PDF path and password. Adjust as needed.
        string pdfPath = "protected_sample.pdf";
        string password = "secret";

        ProcessPasswordProtectedPdf(pdfPath, password);
    }

    /// <summary>
    /// Opens the PDF with the supplied password, renders each page to an image,
    /// and scans the image for barcodes of any supported type.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF file.</param>
    /// <param name="password">Password required to open the PDF.</param>
    static void ProcessPasswordProtectedPdf(string pdfPath, string password)
    {
        // Verify the file exists before attempting to open it.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Attempt to open the PDF using the provided password.
        try
        {
            using (var pdfDocument = new Document(pdfPath, password))
            {
                // Initialize a converter to render PDF pages as images.
                using (var pdfConverter = new PdfConverter(pdfDocument))
                {
                    // Enable barcode optimization to improve detection speed/accuracy.
                    pdfConverter.RenderingOptions.BarcodeOptimization = true;

                    int pageCount = pdfDocument.Pages.Count;
                    for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                    {
                        // Set the converter to process only the current page.
                        pdfConverter.StartPage = pageNumber;
                        pdfConverter.EndPage = pageNumber;

                        // Perform the conversion for the selected page.
                        pdfConverter.DoConvert();

                        // Retrieve the rendered image into a memory stream.
                        using (var imageStream = new MemoryStream())
                        {
                            pdfConverter.GetNextImage(imageStream);
                            imageStream.Position = 0; // Reset stream position for reading.

                            // Attempt to read barcodes from the rendered image.
                            try
                            {
                                using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                                {
                                    bool anyFound = false;
                                    foreach (var result in reader.ReadBarCodes())
                                    {
                                        anyFound = true;
                                        Console.WriteLine($"Page {pageNumber}: Type = {result.CodeTypeName}, Text = {result.CodeText}");
                                    }

                                    if (!anyFound)
                                    {
                                        Console.WriteLine($"Page {pageNumber}: No barcodes detected.");
                                    }
                                }
                            }
                            // Specific handling for image loading failures.
                            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
                            {
                                Console.WriteLine($"Page {pageNumber}: Unable to load rendered image. {ex.Message}");
                            }
                            // General exception handling for barcode reading errors.
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Page {pageNumber}: Barcode reading error. {ex.Message}");
                            }
                        }
                    }
                }
            }
        }
        // Handle errors opening the PDF (e.g., incorrect password).
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to open PDF. Ensure the password is correct. Error: {ex.Message}");
        }
    }
}