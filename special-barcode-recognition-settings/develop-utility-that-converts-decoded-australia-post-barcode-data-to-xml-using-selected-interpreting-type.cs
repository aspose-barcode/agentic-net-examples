using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation, reading, and XML export of an Australia Post barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it back, and writes the result to an XML file.
    /// </summary>
    static void Main()
    {
        // Sample Australia Post barcode data
        string barcodeData = "5912345678AB";

        // Choose the interpreting type for customer information (CTable, NTable, Other)
        CustomerInformationInterpretingType interpretingType = CustomerInformationInterpretingType.CTable;

        // Create a barcode generator for Australia Post format
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, barcodeData))
        {
            // Apply the selected interpreting type to the generator settings
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = interpretingType;

            // Generate the barcode image in memory
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Initialize a reader to decode the generated barcode image
                using (var reader = new BarCodeReader(barcodeImage, DecodeType.AustraliaPost))
                {
                    // Ensure the reader uses the same interpreting type as the generator
                    reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = interpretingType;

                    // Iterate over all decoded barcode results (should be one in this case)
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Build an XML document representing the barcode data and interpreting type
                        XDocument xmlDoc = new XDocument(
                            new XElement("AustraliaPostBarcode",
                                new XElement("CodeText", result.CodeText),
                                new XElement("InterpretingType", interpretingType.ToString())
                            )
                        );

                        // Output the XML document to the console
                        Console.WriteLine(xmlDoc);

                        // Define the output file path for the XML
                        string xmlPath = "AustraliaPostBarcode.xml";

                        // Save the XML document to disk
                        xmlDoc.Save(xmlPath);

                        // Inform the user where the XML file was saved
                        Console.WriteLine($"XML saved to {Path.GetFullPath(xmlPath)}");
                    }
                }
            }
        }
    }
}