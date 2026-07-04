// Title: Initialize BarcodeGenerator from XML string
// Description: Demonstrates reading barcode configuration XML from a string, converting it to a stream, and creating a BarcodeGenerator via ImportFromXml. Useful for dynamically configuring barcodes.
// Prompt: Develop a function that reads XML from a string and initializes a BarcodeGenerator using ImportFromXml(Stream).
// Tags: barcode symbology, import, xml, stream, aspose.barcodes, csharp

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a <see cref="BarcodeGenerator"/> from XML configuration
/// and saves the generated barcode image to a file.
/// </summary>
class Program
{
    /// <summary>
    /// Reads XML from a string, creates a <see cref="MemoryStream"/>, and initializes a <see cref="BarcodeGenerator"/>
    /// using <see cref="BarcodeGenerator.ImportFromXml(Stream)"/>.
    /// </summary>
    /// <param name="xmlContent">The XML string containing barcode configuration.</param>
    /// <returns>A new instance of <see cref="BarcodeGenerator"/> configured according to the XML.</returns>
    static BarcodeGenerator CreateGeneratorFromXml(string xmlContent)
    {
        // Convert the XML string to UTF‑8 encoded bytes.
        byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlContent);

        // Wrap the byte array in a MemoryStream; the stream is disposed after ImportFromXml finishes.
        using (var xmlStream = new MemoryStream(xmlBytes))
        {
            // ImportFromXml reads the stream and returns a new BarcodeGenerator instance.
            BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlStream);
            return generator; // Caller is responsible for disposing the generator.
        }
    }

    /// <summary>
    /// Entry point of the program. Demonstrates creating a barcode from XML and saving it as an image file.
    /// </summary>
    static void Main()
    {
        // Sample XML that defines a Code128 barcode with the text "12345".
        string xml = @"
<BarcodeGenerator>
    <EncodeType>Code128</EncodeType>
    <CodeText>12345</CodeText>
</BarcodeGenerator>";

        // Initialize the generator from the XML string.
        using (var generator = CreateGeneratorFromXml(xml))
        {
            // Save the generated barcode image to a file.
            generator.Save("barcode_from_xml.png");
            Console.WriteLine("Barcode generated and saved as 'barcode_from_xml.png'.");
        }
    }
}