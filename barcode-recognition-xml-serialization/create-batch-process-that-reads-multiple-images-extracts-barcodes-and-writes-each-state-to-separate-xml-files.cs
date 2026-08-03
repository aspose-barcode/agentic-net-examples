// Title: Batch barcode extraction to XML
// Description: Demonstrates reading multiple images, extracting all supported barcodes, and saving each result to an XML file per image.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showing how to use BarCodeReader with DecodeType.AllSupportedTypes, XmlWriter, and BarcodeGenerator for sample data. Developers often need to process batches of images, extract barcode information, and store results in structured formats such as XML for downstream systems.
// Prompt: Create a batch process that reads multiple images, extracts barcodes, and writes each state to separate XML files.
// Tags: barcode recognition, batch processing, xml output, decodeall, aspose.barcode, csharp

using System;
using System.IO;
using System.Xml;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch processing of barcode images: generating sample barcodes, reading them, and writing results to XML files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, processes each image, extracts barcodes, and writes XML output.
    /// </summary>
    static void Main()
    {
        // Define working folder for generated and processed files
        string workFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(workFolder))
        {
            Directory.CreateDirectory(workFolder);
        }

        // -----------------------------------------------------------------
        // Step 1: Generate a few sample barcode images (self‑contained demo)
        // -----------------------------------------------------------------
        GenerateSampleBarcodes(workFolder);

        // -----------------------------------------------------------------
        // Step 2: Process each image, extract barcodes and write XML files
        // -----------------------------------------------------------------
        string[] imageFiles = Directory.GetFiles(workFolder, "*.png");
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            // Prepare XML writer for the output file (same name, .xml extension)
            string xmlPath = Path.ChangeExtension(imagePath, ".xml");
            using (XmlWriter writer = XmlWriter.Create(xmlPath, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Barcodes");

                // Read all supported barcodes from the current image
                using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        writer.WriteStartElement("BarCode");
                        writer.WriteAttributeString("Type", result.CodeTypeName);
                        writer.WriteAttributeString("CodeText", result.CodeText ?? string.Empty);

                        // Include region information if available
                        if (result.Region != null)
                        {
                            var rect = result.Region.Rectangle;
                            writer.WriteAttributeString("X", rect.X.ToString());
                            writer.WriteAttributeString("Y", rect.Y.ToString());
                            writer.WriteAttributeString("Width", rect.Width.ToString());
                            writer.WriteAttributeString("Height", rect.Height.ToString());
                        }

                        writer.WriteEndElement(); // BarCode
                    }
                }

                writer.WriteEndElement(); // Barcodes
                writer.WriteEndDocument();
            }

            Console.WriteLine($"Processed '{Path.GetFileName(imagePath)}' -> '{Path.GetFileName(xmlPath)}'");
        }

        Console.WriteLine("Batch processing completed.");
    }

    // Generates a small set of sample barcode images in the specified folder.
    private static void GenerateSampleBarcodes(string folder)
    {
        // Sample data: (symbology, text, file name)
        var samples = new (BaseEncodeType encode, string text, string file)[]
        {
            (EncodeTypes.Code128, "Sample123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png")
        };

        foreach (var (encode, text, file) in samples)
        {
            string path = Path.Combine(folder, file);
            using (var generator = new BarcodeGenerator(encode, text))
            {
                // Simple settings – default size and colors are fine for the demo
                generator.Save(path, BarCodeImageFormat.Png);
            }
        }
    }
}