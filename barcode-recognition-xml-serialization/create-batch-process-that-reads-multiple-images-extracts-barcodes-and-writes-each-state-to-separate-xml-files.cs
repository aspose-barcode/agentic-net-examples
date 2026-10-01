// Title: Batch barcode extraction and XML export
// Description: Demonstrates how to generate sample barcode images, read multiple files, extract all supported barcodes, and save each result to an individual XML file.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing the use of BarcodeGenerator for creating barcodes, BarCodeReader with DecodeType.AllSupportedTypes for recognition, and XmlWriter for structured output. Typical scenarios include automated scanning of image collections, inventory audits, and data migration where each image’s barcode data must be persisted in a machine‑readable format. Developers often need to loop through files, handle errors gracefully, and produce per‑file reports.
// Prompt: Create a batch process that reads multiple images, extracts barcodes, and writes each state to separate XML files.
// Tags: barcode, batch processing, extraction, xml, aspose.barcode, generation, recognition, decode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using System.Xml;

/// <summary>
/// Entry point for the batch barcode extraction example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates sample barcode images, reads each image, extracts all supported barcodes, and writes the results to separate XML files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch process
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Generate sample barcode images and collect their file paths
        List<string> imageFiles = GenerateSampleBarcodes(batchFolder);

        // Process each image: extract barcodes and write results to an XML file
        foreach (string imagePath in imageFiles)
        {
            // Prepare XML output path (same name as image, but with .xml extension)
            string xmlPath = Path.ChangeExtension(imagePath, ".xml");

            try
            {
                // Initialize the reader to decode all supported barcode types
                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    // Optional: configure reader settings if needed
                    // reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                    // reader.QualitySettings.AllowIncorrectBarcodes = true;

                    // Perform the recognition
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Write the recognition results to an XML file
                    using (XmlWriter writer = XmlWriter.Create(xmlPath, new XmlWriterSettings { Indent = true }))
                    {
                        writer.WriteStartDocument();
                        writer.WriteStartElement("Barcodes");

                        foreach (BarCodeResult result in results)
                        {
                            writer.WriteStartElement("BarCode");
                            writer.WriteElementString("CodeText", result.CodeText ?? string.Empty);
                            writer.WriteElementString("CodeType", result.CodeTypeName ?? string.Empty);
                            writer.WriteElementString("ReadingQuality", result.ReadingQuality.ToString());
                            writer.WriteEndElement(); // BarCode
                        }

                        writer.WriteEndElement(); // Barcodes
                        writer.WriteEndDocument();
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                // Handle cases where the image cannot be loaded (e.g., corrupted file)
                Console.WriteLine($"Skipping file '{Path.GetFileName(imagePath)}': {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log any other unexpected errors during processing
                Console.WriteLine($"Error processing file '{Path.GetFileName(imagePath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }

    /// <summary>
    /// Generates a set of sample barcode images and returns their file paths.
    /// </summary>
    /// <param name="folder">The folder where the images will be saved.</param>
    /// <returns>A list of file paths for the generated barcode images.</returns>
    private static List<string> GenerateSampleBarcodes(string folder)
    {
        var files = new List<string>();

        // Define sample data: tuple of (symbology, code text, file name)
        var samples = new (BaseEncodeType encode, string text, string fileName)[]
        {
            (EncodeTypes.Code128, "CODE128_SAMPLE", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM_SAMPLE", "datamatrix.png"),
            (EncodeTypes.Pdf417, "PDF417_SAMPLE_TEXT", "pdf417.png"),
            (EncodeTypes.Aztec, "AZTEC_SAMPLE", "aztec.png")
        };

        // Create each barcode image using the specified symbology and text
        foreach (var (encode, text, fileName) in samples)
        {
            string filePath = Path.Combine(folder, fileName);
            using (var generator = new BarcodeGenerator(encode, text))
            {
                // Minimal configuration; defaults are sufficient for demonstration
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            files.Add(filePath);
        }

        return files;
    }
}