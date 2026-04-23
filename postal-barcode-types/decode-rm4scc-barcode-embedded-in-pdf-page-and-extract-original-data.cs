using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Path to the PDF file containing the RM4SCC barcode.
        const string pdfPath = "sample.pdf";

        // Verify that the PDF file exists.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document.
        using (var pdfDocument = new Document(pdfPath))
        {
            // Ensure the document has at least one page.
            if (pdfDocument.Pages.Count == 0)
            {
                Console.WriteLine("The PDF does not contain any pages.");
                return;
            }

            // Convert the first page to a PNG image stored in a memory stream.
            var resolution = new Resolution(300);
            var pngDevice = new PngDevice(resolution);
            using (var imageStream = new MemoryStream())
            {
                pngDevice.Process(pdfDocument.Pages[1], imageStream);
                imageStream.Position = 0; // Reset stream position for reading.

                // Initialize the barcode reader for RM4SCC decoding.
                using (var reader = new BarCodeReader(imageStream, DecodeType.RM4SCC))
                {
                    var results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine("No RM4SCC barcode detected on the page.");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            Console.WriteLine($"Decoded RM4SCC data: {result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}