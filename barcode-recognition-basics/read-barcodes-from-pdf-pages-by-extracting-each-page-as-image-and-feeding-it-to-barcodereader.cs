using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Drawing;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the PDF file (adjust as needed)
        const string pdfPath = "sample.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load PDF document
        using (var pdfDocument = new Document(pdfPath))
        {
            // Process up to 4 pages to respect the element limit rule
            int pageCount = Math.Min(pdfDocument.Pages.Count, 4);
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                // Convert the current PDF page to a PNG image stored in a memory stream
                using (var imageStream = new MemoryStream())
                {
                    var resolution = new Resolution(300);
                    var pngDevice = new PngDevice(resolution);
                    pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                    imageStream.Position = 0;

                    // Load the image into an Aspose.Drawing.Bitmap
                    using (var bitmap = new Bitmap(imageStream))
                    {
                        // Initialize the barcode reader
                        using (var reader = new BarCodeReader())
                        {
                            // Detect all supported barcode types
                            reader.BarCodeReadType = DecodeType.AllSupportedTypes;

                            // Assign the bitmap image to the reader
                            reader.SetBarCodeImage(bitmap);

                            // Read barcodes on the current page
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