using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Path to the password‑protected PDF (or image container) and the password.
        string pdfPath = "protected.pdf";
        string password = "mySecret";

        // Verify the file exists.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the encrypted PDF using the supplied password.
        using (Document pdfDoc = new Document(pdfPath, password))
        {
            // Prepare the PDF converter for rendering pages to images.
            using (PdfConverter pdfConverter = new PdfConverter(pdfDoc))
            {
                // Enable barcode optimization for better detection.
                pdfConverter.RenderingOptions.BarcodeOptimization = true;

                // Process each page individually.
                for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        // Render the current page to the stream as PNG.
                        pdfConverter.GetNextImage(imageStream);
                        imageStream.Position = 0;

                        // Use BarCodeReader to detect all supported barcodes in the rendered image.
                        using (BarCodeReader reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                        {
                            // Optional: improve detection on difficult images.
                            reader.QualitySettings.InverseImage = InverseImageMode.Enabled;

                            // Perform recognition.
                            BarCodeResult[] results = reader.ReadBarCodes();

                            // Output results for the current page.
                            if (results.Length == 0)
                            {
                                Console.WriteLine($"Page {pageNumber}: No barcodes found.");
                            }
                            else
                            {
                                Console.WriteLine($"Page {pageNumber}: Detected {results.Length} barcode(s).");
                                foreach (BarCodeResult result in results)
                                {
                                    Console.WriteLine($"  Type: {result.CodeTypeName}");
                                    Console.WriteLine($"  Text: {result.CodeText}");
                                    Console.WriteLine($"  Confidence: {result.Confidence}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}