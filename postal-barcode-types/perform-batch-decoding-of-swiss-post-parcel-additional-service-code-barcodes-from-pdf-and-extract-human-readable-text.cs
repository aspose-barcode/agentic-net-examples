// Title: Batch decode Swiss Post Parcel additional service barcodes from PDF
// Description: Demonstrates generating a PDF containing Swiss Post Parcel additional service barcodes and then batch decoding them to extract human‑readable text.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create SwissPostParcel barcodes, embed them in a PDF via Aspose.Pdf, and then employ BarCodeReader with DecodeType.SwissPostParcel to read the barcodes page‑by‑page. Developers working with shipping labels, parcel tracking, or bulk barcode processing often need to generate barcodes, embed them in documents, and later extract their data programmatically.
// Prompt: Perform batch decoding of Swiss Post Parcel additional service code barcodes from a PDF and extract human‑readable text.
// Tags: barcode, swisspostparcel, batch-decoding, pdf, aspose.barcode, aspose.pdf, generation, recognition, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Demonstrates batch decoding of Swiss Post Parcel additional service barcodes from a PDF.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a sample PDF with barcodes and decodes them.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the path for the sample PDF
        string pdfPath = Path.Combine(tempFolder, "SampleSwissPost.pdf");

        // Generate a PDF containing Swiss Post Parcel Additional Service barcodes (max 2 pages)
        GenerateSamplePdf(pdfPath);

        // Verify that the PDF was created successfully
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine("PDF file not found.");
            return;
        }

        // Decode all barcodes from the generated PDF
        DecodeBarcodesFromPdf(pdfPath);
    }

    /// <summary>
    /// Generates a PDF file with two pages, each containing a Swiss Post Parcel barcode.
    /// </summary>
    /// <param name="pdfPath">The full file path where the PDF will be saved.</param>
    static void GenerateSamplePdf(string pdfPath)
    {
        // Create a new PDF document
        using (var pdfDoc = new Document())
        {
            // Add two pages, each with a barcode image
            for (int i = 0; i < 2; i++)
            {
                // Generate barcode image in memory
                using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "0327"))
                {
                    // Configure barcode appearance
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;
                    generator.Parameters.Barcode.BarHeight.Pixels = 40f;
                    generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
                    generator.Parameters.CaptionAbove.Visible = true;
                    generator.Parameters.CaptionAbove.Alignment = TextAlignment.Left;
                    generator.Parameters.CaptionAbove.Text = "AR";
                    generator.Parameters.CaptionAbove.Font.Size.Pixels = 24f;

                    // Save barcode to a memory stream as PNG
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0;

                        // Add a new page to the PDF document
                        var page = pdfDoc.Pages.Add();

                        // Insert the barcode image into the page
                        var image = new Aspose.Pdf.Image
                        {
                            ImageStream = new MemoryStream(ms.ToArray())
                        };
                        page.Paragraphs.Add(image);
                    }
                }
            }

            // Persist the PDF to disk
            pdfDoc.Save(pdfPath);
        }
    }

    /// <summary>
    /// Opens the specified PDF, converts each page to an image, and decodes any Swiss Post Parcel barcodes found.
    /// </summary>
    /// <param name="pdfPath">The full file path of the PDF to process.</param>
    static void DecodeBarcodesFromPdf(string pdfPath)
    {
        // Load the PDF document
        using (var pdfDoc = new Document(pdfPath))
        {
            // Initialize the PDF converter for page‑by‑page rendering
            using (var converter = new PdfConverter(pdfDoc))
            {
                converter.RenderingOptions.BarcodeOptimization = true;

                // Iterate through all pages in the PDF
                for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    // Configure converter to process a single page
                    converter.StartPage = pageNumber;
                    converter.EndPage = pageNumber;
                    converter.DoConvert();

                    // Retrieve the rendered page image
                    using (var imageStream = new MemoryStream())
                    {
                        converter.GetNextImage(imageStream);
                        imageStream.Position = 0;

                        // Set up barcode reader for Swiss Post Parcel barcodes
                        BaseDecodeType decodeType = DecodeType.SwissPostParcel;
                        using (var reader = new BarCodeReader(imageStream, decodeType))
                        {
                            // Optimize performance settings
                            BarCodeReader.ProcessorSettings.UseAllCores = true;
                            reader.QualitySettings = QualitySettings.HighPerformance;

                            try
                            {
                                // Read all barcodes on the current page
                                var results = reader.ReadBarCodes();
                                foreach (var result in results)
                                {
                                    Console.WriteLine($"Page {pageNumber}: Type={result.CodeTypeName}, Text={result.CodeText}");
                                }
                            }
                            catch (ArgumentException ex)
                            {
                                // Handle cases where the image cannot be processed as a barcode
                                Console.WriteLine($"Page {pageNumber}: Image loading failed - {ex.Message}");
                            }
                        }
                    }
                }
            }
        }
    }
}