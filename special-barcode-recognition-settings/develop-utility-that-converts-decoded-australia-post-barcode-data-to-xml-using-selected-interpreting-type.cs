// Title: Convert Australia Post Barcode Data to XML
// Description: Demonstrates decoding an Australia Post barcode and exporting its fields to an XML file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to generate an Australia Post barcode, decode it using the BarCodeReader, interpret customer information, and serialize the extracted data (FCC, DPID, and optional customer info) into XML. Developers working with postal barcode automation often need to extract and store barcode data in structured formats, and this sample illustrates the key API classes (BarcodeGenerator, BarCodeReader, CustomerInformationInterpretingType) and typical usage patterns.
// Prompt: Develop a utility that converts decoded Australia Post barcode data to XML using the selected interpreting type.
// Tags: australia post,barcode generation,barcode recognition,xml output,customer information interpreting,aspose.barcode

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides a console utility that generates an Australia Post barcode, decodes it,
/// and writes the extracted information to an XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the utility. Accepts an optional command‑line argument to specify
    /// the <see cref="CustomerInformationInterpretingType"/> used for both generation and recognition.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument may be a valid interpreting type.</param>
    static void Main(string[] args)
    {
        // Determine interpreting type from command‑line argument; default to CTable.
        CustomerInformationInterpretingType interpretingType = CustomerInformationInterpretingType.CTable;
        if (args.Length > 0)
        {
            if (Enum.TryParse(args[0], true, out CustomerInformationInterpretingType parsed))
                interpretingType = parsed;
            else
                Console.WriteLine($"Unrecognized interpreting type '{args[0]}', using default CTable.");
        }

        // Sample Australia Post barcode data: FCC (2) + DPID (8) + optional customer info.
        string sampleCodeText = "5912345678ABCde";

        // Generate the barcode image using the selected interpreting type.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, sampleCodeText))
        {
            // Apply the interpreting type for barcode generation.
            generator.Parameters.Barcode.AustralianPost.EncodingTable = interpretingType;

            using (Aspose.Drawing.Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Recognize the barcode from the generated image.
                using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AustraliaPost))
                {
                    // Apply the same interpreting type for recognition.
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = interpretingType;

                    // Optional: ignore ending filling patterns when using CTable.
                    if (interpretingType == CustomerInformationInterpretingType.CTable)
                        reader.BarcodeSettings.AustraliaPost.IgnoreEndingFillingPatternsForCTable = true;

                    // Read all detected barcodes (expecting a single result).
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No Australia Post barcode detected.");
                        return;
                    }

                    // Use the first result as the target barcode.
                    BarCodeResult result = results[0];
                    string codeText = result.CodeText ?? string.Empty;

                    // Validate that the decoded text contains at least FCC and DPID.
                    if (codeText.Length < 10)
                    {
                        Console.WriteLine("Decoded code text is too short to contain required FCC and DPID.");
                        return;
                    }

                    // Extract FCC (first 2 characters), DPID (next 8 characters), and any remaining customer information.
                    string fcc = codeText.Substring(0, 2);
                    string dpid = codeText.Substring(2, 8);
                    string customerInfo = codeText.Length > 10 ? codeText.Substring(10) : string.Empty;

                    // Build an XML document representing the decoded data.
                    XDocument xmlDoc = new XDocument(
                        new XElement("AustraliaPostBarcode",
                            new XElement("FCC", fcc),
                            new XElement("DPID", dpid),
                            new XElement("CustomerInformation", customerInfo)
                        )
                    );

                    // Save the XML document to the current working directory.
                    string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "AustraliaPostOutput.xml");
                    xmlDoc.Save(outputPath);
                    Console.WriteLine($"Decoded data saved to XML file: {outputPath}");
                }
            }
        }
    }
}