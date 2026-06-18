using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

/// <summary>
/// Demonstrates reading barcodes from an encrypted PDF using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Opens an encrypted PDF, converts each page to an image,
    /// and extracts any barcodes found on the pages.
    /// </summary>
    static void Main()
    {
        // Sample encrypted PDF path and password.
        string pdfPath = "encrypted.pdf";
        string password = "myPassword";

        // Verify that the PDF file exists.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the encrypted PDF using the password.
        using (var pdfDocument = new Document(pdfPath, password))
        {
            // Iterate through each page of the PDF.
            for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
            {
                var page = pdfDocument.Pages[pageIndex];

                // Convert the page to a PNG image stored in a memory stream.
                using (var imageStream = new MemoryStream())
                {
                    var pngDevice = new PngDevice();
                    pngDevice.Process(page, imageStream);
                    imageStream.Position = 0; // Reset stream position for reading.

                    // Load the image into a Bitmap for barcode recognition.
                    using (var bitmap = new Bitmap(imageStream))
                    {
                        // Create a BarCodeReader that scans for all supported barcode types.
                        using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                        {
                            // Enumerate all detected barcodes on the current page.
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Page {pageIndex}: Type = {result.CodeTypeName}, Text = {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}