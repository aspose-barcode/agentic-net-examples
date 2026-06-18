using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creating a barcode generator from an XML definition
/// and saving the resulting barcode image to a file.
/// </summary>
class Program
{
    // Reads XML from a string and creates a BarcodeGenerator using ImportFromXml(Stream)
    static BarcodeGenerator CreateGeneratorFromXml(string xmlContent)
    {
        // Convert the XML string to a UTF‑8 byte array
        byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlContent);

        // Wrap the byte array in a memory stream for ImportFromXml
        using (var xmlStream = new MemoryStream(xmlBytes))
        {
            // ImportFromXml creates a new BarcodeGenerator instance based on the XML definition
            return BarcodeGenerator.ImportFromXml(xmlStream);
        }
    }

    /// <summary>
    /// Entry point of the application. Generates a barcode from XML and saves it as an image file.
    /// </summary>
    static void Main()
    {
        // Sample XML defining a Code128 barcode with some text.
        // The exact schema must match Aspose.BarCode's expected format.
        string barcodeXml = @"
<BarcodeGenerator>
    <EncodeType>Code128</EncodeType>
    <CodeText>12345ABC</CodeText>
</BarcodeGenerator>";

        // Initialize the generator from the XML string
        using (var generator = CreateGeneratorFromXml(barcodeXml))
        {
            // Define the output file path for the barcode image
            string outputPath = "barcode_from_xml.png";

            // Save the generated barcode image to the specified file
            generator.Save(outputPath);

            // Inform the user where the image was saved
            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
    }
}