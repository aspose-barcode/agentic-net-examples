// Title: Australia Post Barcode Generation, Decoding, and XML Export
// Description: This example creates Australia Post barcodes for different interpreting types, decodes them, and writes the decoded information to an XML file.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition for Australia Post symbology. It uses BarcodeGenerator, BarCodeReader, and related parameter classes to encode data, set the CustomerInformationInterpretingType, and extract decoded values. Developers often need to generate barcodes for testing, read them from images, and export results to structured formats such as XML for integration with other systems.
// Prompt: Develop a utility that converts decoded Australia Post barcode data to XML using the selected interpreting type.
// Tags: australia post, barcode generation, barcode decoding, xml output, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating Australia Post barcodes, decoding them, and exporting the results to XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, reads them back, and creates an XML document with the decoded data.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated images and XML output
        string tempFolder = Path.Combine(Path.GetTempPath(), "AustraliaPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample data for each interpreting type
        var samples = new List<(string FileName, string CodeText, CustomerInformationInterpretingType InterpretingType)>
        {
            ("CTable.png", "6201234567ASPOSE", CustomerInformationInterpretingType.CTable),
            ("NTable.png", "620123456701234", CustomerInformationInterpretingType.NTable),
            ("Other.png", "6201234567321032103210", CustomerInformationInterpretingType.Other)
        };

        // Generate barcode images based on the sample data
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.FileName);
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, sample.CodeText))
            {
                // Set visual parameters
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;

                // Apply the specific interpreting type for Australia Post encoding
                generator.Parameters.Barcode.AustralianPost.EncodingTable = sample.InterpretingType;

                // Save the barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Prepare the root element for the XML document
        var root = new XElement("Barcodes");
        BaseDecodeType decodeType = DecodeType.AustraliaPost;

        // Decode each generated barcode and add its information to the XML
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.FileName);
            using (var reader = new BarCodeReader(filePath, decodeType))
            {
                // Ensure the reader uses the same interpreting type as was used during generation
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = sample.InterpretingType;

                // Read all barcodes found in the image (typically one per file)
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    var barcodeElement = new XElement("Barcode",
                        new XAttribute("File", sample.FileName),
                        new XAttribute("InterpretingType", sample.InterpretingType.ToString()),
                        new XElement("CodeText", result.CodeText ?? string.Empty),
                        new XElement("CodeType", result.CodeTypeName ?? string.Empty));

                    root.Add(barcodeElement);
                }
            }
        }

        // Create the XML document and save it to the temporary folder
        var doc = new XDocument(root);
        string xmlPath = Path.Combine(tempFolder, "DecodedBarcodes.xml");
        doc.Save(xmlPath);

        // Output locations and XML content for user reference
        Console.WriteLine("Barcode images and XML have been generated in:");
        Console.WriteLine(tempFolder);
        Console.WriteLine("XML content:");
        Console.WriteLine(doc);
    }
}