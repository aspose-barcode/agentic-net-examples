using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the encrypted PDF and its password.
        const string pdfPath = "sample_encrypted.pdf";
        const string pdfPassword = "password";

        // Verify that the PDF file exists.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the encrypted PDF document using the provided password.
        using (var pdfDoc = new Document(pdfPath, pdfPassword))
        {
            // Initialize the PDF converter.
            using (var pdfConverter = new PdfConverter(pdfDoc))
            {
                // Enable barcode optimization for faster rendering.
                pdfConverter.RenderingOptions.BarcodeOptimization = true;

                // Iterate through each page of the PDF.
                for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Render the current page to an image stream.
                    using (var pageImageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(pageImageStream);
                        pageImageStream.Position = 0;

                        // Read barcodes from the rendered image.
                        using (var reader = new BarCodeReader(pageImageStream, DecodeType.AllSupportedTypes))
                        {
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Page {pageNumber}: Type = {result.CodeTypeName}, Text = {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}