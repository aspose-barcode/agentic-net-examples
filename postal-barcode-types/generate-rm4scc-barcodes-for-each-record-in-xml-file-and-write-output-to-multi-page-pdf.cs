using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace RM4SCCBarcodePdf
{
    class Program
    {
        static void Main(string[] args)
        {
            // Input XML file (fallback to sample if not provided)
            string xmlPath = args.Length > 0 ? args[0] : "sample.xml";

            if (!File.Exists(xmlPath))
            {
                Console.WriteLine($"XML file not found: {xmlPath}");
                return;
            }

            // Load XML and extract code texts (assumes <Record><Code>value</Code></Record>)
            XDocument doc;
            using (var fs = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            {
                doc = XDocument.Load(fs);
            }

            var codeElements = doc.Descendants("Record")
                                  .Select(r => r.Element("Code")?.Value)
                                  .Where(v => !string.IsNullOrWhiteSpace(v))
                                  .Take(10) // limit to a safe number for demo
                                  .ToList();

            if (!codeElements.Any())
            {
                Console.WriteLine("No valid <Code> elements found in the XML.");
                return;
            }

            // Create PDF document
            using (var pdfDoc = new Document())
            {
                foreach (var codeText in codeElements)
                {
                    // Generate RM4SCC barcode image into a memory stream
                    using (var barcodeStream = new MemoryStream())
                    {
                        using (var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, codeText))
                        {
                            // Optional: set image size
                            generator.Parameters.ImageWidth.Point = 200f;
                            generator.Parameters.ImageHeight.Point = 100f;
                            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                            // Save as PNG to the stream
                            generator.Save(barcodeStream, BarCodeImageFormat.Png);
                        }

                        barcodeStream.Position = 0; // reset for reading

                        // Add a new page for each barcode
                        var page = pdfDoc.Pages.Add();

                        // Insert the barcode image
                        var image = new Aspose.Pdf.Image
                        {
                            ImageStream = new MemoryStream(barcodeStream.ToArray())
                        };

                        // Center the image on the page
                        image.HorizontalAlignment = HorizontalAlignment.Center;
                        image.VerticalAlignment = VerticalAlignment.Center;

                        page.Paragraphs.Add(image);
                    }
                }

                // Save the multi‑page PDF
                string outputPdf = "RM4SCC_Barcodes.pdf";
                pdfDoc.Save(outputPdf);
                Console.WriteLine($"PDF saved to {outputPdf}");
            }
        }
    }
}