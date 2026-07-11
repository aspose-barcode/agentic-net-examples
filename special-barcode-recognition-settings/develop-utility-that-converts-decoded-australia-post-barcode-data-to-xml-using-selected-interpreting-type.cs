// Title: Convert Australia Post barcode data to XML using interpreting type
// Description: Demonstrates generating an Australia Post barcode, recognizing it, and converting the decoded data into an XML representation.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on Australia Post symbology. It showcases the use of BarcodeGenerator, BarCodeReader, and related settings such as CustomerInformationInterpretingType. Developers often need to generate barcodes, read them back, and transform the decoded information into structured formats like XML for integration with other systems.
// Prompt: Develop a utility that converts decoded Australia Post barcode data to XML using the selected interpreting type.
// Tags: australia post, barcode generation, barcode recognition, xml output, customerinformationinterpretingtype, aspose.barcode

using System;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates converting decoded Australia Post barcode data to XML using a selected interpreting type.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the utility. Generates a barcode, reads it, and outputs XML.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Sample Australia Post barcode data (postal code + customer information)
        string codeText = "5912345678ABCde";

        // Choose the interpreting type for the customer information field
        CustomerInformationInterpretingType interpretingType = CustomerInformationInterpretingType.CTable;

        // Create a barcode generator for Australia Post symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Apply the selected interpreting type to the generator settings
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = interpretingType;

            // Generate the barcode image as a bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Initialize a barcode reader for Australia Post decoding
                using (var reader = new BarCodeReader(bitmap, DecodeType.AustraliaPost))
                {
                    // Apply the same interpreting type to the reader settings
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = interpretingType;

                    // When using CTable, optionally ignore ending filling patterns
                    if (interpretingType == CustomerInformationInterpretingType.CTable)
                    {
                        reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;
                    }

                    // Iterate over detected barcodes (only one expected in this example)
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Build a simple XML representation of the decoded barcode data
                        XElement xml = new XElement(
                            "AustraliaPostBarcode",
                            new XAttribute("InterpretingType", interpretingType),
                            new XElement("CodeText", result.CodeText ?? string.Empty));

                        // Write the XML to the console
                        Console.WriteLine(xml);
                    }
                }
            }
        }
    }
}