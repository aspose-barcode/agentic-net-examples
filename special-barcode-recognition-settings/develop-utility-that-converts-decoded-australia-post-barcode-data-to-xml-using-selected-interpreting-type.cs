// Title: Convert Australia Post Barcode to XML Using CTable Interpreting Type
// Description: Demonstrates generating an Australia Post barcode, reading it, and converting the decoded data to an XML document.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator, BarCodeReader, and related settings to create an Australia Post barcode, decode it, and format the result as XML. Developers working with postal symbologies often need to generate barcodes for mailing and then parse the encoded customer information for integration with backend systems.
// Prompt: Develop a utility that converts decoded Australia Post barcode data to XML using the selected interpreting type.
// Tags: australia post, barcode generation, barcode recognition, xml output, aspose.barcode, aspose.barcode.generation, aspose.barcode.recognition

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates an Australia Post barcode, reads it back,
/// and outputs the decoded information as an XML document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "AustraliaPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define barcode data and file path
        string codeText = "6201234567ASPOSE";
        string imagePath = Path.Combine(tempFolder, "AustraliaPostCTable.png");

        // Generate Australia Post barcode with CTable interpreting type
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Set visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Specify the interpreting type for customer information
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

            // Save the barcode image to the temporary folder
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode and convert the result to XML
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AustraliaPost))
        {
            // Ensure the reader uses the same interpreting type as the generator
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

            // Iterate through all detected barcodes (only one expected)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Build an XML document with the decoded information
                XDocument xmlDoc = new XDocument(
                    new XElement("AustraliaPostBarcode",
                        new XElement("CodeType", result.CodeTypeName),
                        new XElement("CodeText", result.CodeText),
                        new XElement("InterpretingType", CustomerInformationInterpretingType.CTable.ToString())
                    )
                );

                // Output the XML to the console
                Console.WriteLine(xmlDoc);
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}