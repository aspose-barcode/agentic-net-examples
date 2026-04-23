using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Enable full core usage for barcode recognition
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        string inputFolder = "InputPdfs";

        // Ensure the input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder \"{inputFolder}\" does not exist.");
            return;
        }

        // Get PDF files (limit to a safe number for demo)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the input folder.");
            return;
        }

        // Process each PDF file in parallel
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                ProcessPdf(pdfPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing \"{Path.GetFileName(pdfPath)}\": {ex.Message}");
            }
        });
    }

    static void ProcessPdf(string pdfPath)
    {
        // Load the PDF document
        using (var pdfDoc = new Document(pdfPath))
        {
            int pageCount = pdfDoc.Pages.Count;
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                // Create a converter for the current page
                using (var pdfConverter = new PdfConverter(pdfDoc))
                {
                    pdfConverter.RenderingOptions.BarcodeOptimization = true;
                    pdfConverter.StartPage = pageNumber;
                    pdfConverter.EndPage = pageNumber;
                    pdfConverter.DoConvert();

                    // Render the page to an image stream
                    using (var imageStream = new MemoryStream())
                    {
                        pdfConverter.GetNextImage(imageStream);
                        imageStream.Position = 0;

                        // Read barcodes from the image stream
                        using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                        {
                            foreach (var result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"File: {Path.GetFileName(pdfPath)} | Page: {pageNumber} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}