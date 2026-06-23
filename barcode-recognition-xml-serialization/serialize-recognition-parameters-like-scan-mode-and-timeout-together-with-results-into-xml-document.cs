using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Demonstrates generating a barcode, reading it, and exporting recognition parameters and results to XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode, reads it, and writes recognition data to an XML file.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with the value "12345"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Store the generated barcode image in a memory stream
            using (var imageStream = new MemoryStream())
            {
                // Save the barcode as a PNG image into the stream
                generator.Save(imageStream, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading
                imageStream.Position = 0;

                // Initialize a barcode reader that can decode all supported symbologies
                using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                {
                    // Configure reader settings
                    reader.Timeout = 2000; // 2‑second timeout
                    reader.QualitySettings = QualitySettings.HighPerformance;
                    reader.QualitySettings.AllowIncorrectBarcodes = true;

                    // Export the current recognition parameters to an XML document
                    XDocument parametersXml;
                    using (var paramStream = new MemoryStream())
                    {
                        reader.ExportToXml(paramStream);
                        paramStream.Position = 0;
                        parametersXml = XDocument.Load(paramStream);
                    }

                    // Perform barcode recognition and obtain results
                    var results = reader.ReadBarCodes();

                    // Build a combined XML document containing both parameters and recognition results
                    var finalDoc = new XDocument(
                        new XElement("RecognitionResult",
                            // Include the exported parameters
                            new XElement("Parameters", parametersXml.Root.Elements()),
                            // Include each recognized barcode as a separate element
                            new XElement("Barcodes",
                                results.Select(r => new XElement("Barcode",
                                    new XElement("Type", r.CodeTypeName),
                                    new XElement("CodeText", r.CodeText),
                                    new XElement("Confidence", r.Confidence),
                                    new XElement("ReadingQuality", r.ReadingQuality),
                                    new XElement("Angle", r.Region.Angle),
                                    new XElement("Region",
                                        new XElement("X", r.Region.Rectangle.X),
                                        new XElement("Y", r.Region.Rectangle.Y),
                                        new XElement("Width", r.Region.Rectangle.Width),
                                        new XElement("Height", r.Region.Rectangle.Height)
                                    )
                                ))
                            )
                        )
                    );

                    // Save the final XML document to a file
                    const string outputPath = "RecognitionResult.xml";
                    finalDoc.Save(outputPath);
                    Console.WriteLine($"Recognition data saved to '{outputPath}'.");
                }
            }
        }
    }
}