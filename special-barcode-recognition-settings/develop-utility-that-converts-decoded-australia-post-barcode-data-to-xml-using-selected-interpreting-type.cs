// Title: Convert Australia Post Barcode Data to XML Using Interpreting Types
// Description: Demonstrates generating Australia Post barcodes, decoding them with a selected interpreting type, and exporting the results to an XML document.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator, BarCodeReader, and CustomerInformationInterpretingType to create Australia Post barcodes, read them, and serialize decoded information to XML. Developers working with postal symbologies often need to generate test barcodes, interpret customer data, and integrate results into XML workflows.
// Prompt: Develop a utility that converts decoded Australia Post barcode data to XML using the selected interpreting type.
// Tags: australia post,barcode generation,barcode recognition,xml output,customerinformationinterpretingtype,aspose.barcode

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates Australia Post barcodes, decodes them using a specified
/// <see cref="CustomerInformationInterpretingType"/>, and writes the decoded information to an XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated images and output XML.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AusPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample data for each interpreting type (C‑Table, N‑Table, Other).
        var samples = new (string FileName, string CodeText, CustomerInformationInterpretingType Interpreting)[]
        {
            ("CTable.png", "6201234567ASPOSE", CustomerInformationInterpretingType.CTable),
            ("NTable.png", "620123456701234", CustomerInformationInterpretingType.NTable),
            ("Other.png", "6201234567321032103210", CustomerInformationInterpretingType.Other)
        };

        // -----------------------------------------------------------------
        // Generate barcode images for each sample using the selected interpreting type.
        // -----------------------------------------------------------------
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.FileName);
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, sample.CodeText))
            {
                // Set visual parameters.
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;

                // Apply the interpreting type to control how customer information is encoded.
                generator.Parameters.Barcode.AustralianPost.EncodingTable = sample.Interpreting;

                // Save the generated barcode as a PNG image.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // -----------------------------------------------------------------
        // Decode each generated barcode and build an XML document with the results.
        // -----------------------------------------------------------------
        XElement root = new XElement("Barcodes");

        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.FileName);
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Initialize the reader for Australia Post barcodes.
            using (var reader = new BarCodeReader(filePath, DecodeType.AustraliaPost))
            {
                // Ensure the reader uses the same interpreting type as was used during generation.
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = sample.Interpreting;

                // Read all barcodes found in the image (normally one per image).
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Create an XML element representing the decoded barcode.
                    XElement barcodeElement = new XElement("Barcode",
                        new XElement("InterpretingType", sample.Interpreting.ToString()),
                        new XElement("CodeText", result.CodeText),
                        new XElement("CodeType", result.CodeTypeName));

                    root.Add(barcodeElement);
                }
            }
        }

        // Save the assembled XML document to the temporary folder.
        XDocument doc = new XDocument(root);
        string xmlOutputPath = Path.Combine(tempFolder, "DecodedAustraliaPost.xml");
        doc.Save(xmlOutputPath);

        // Output the location of the generated XML and display its contents.
        Console.WriteLine("Decoded XML saved to: " + xmlOutputPath);
        Console.WriteLine(doc);
    }
}