// Title: Deserialize barcode settings from XML stored in a BLOB using MemoryStream
// Description: Demonstrates loading barcode configuration XML from a database BLOB, importing it into Aspose.BarCode, and generating an image.
// Prompt: Deserialize barcode settings from XML stored in a database BLOB field using a MemoryStream.
// Tags: barcode symbology, deserialization, xml, memorystream, aspose.barcode, code128, image generation

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that imports barcode settings from XML stored in a BLOB and generates a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads XML from a simulated BLOB, imports settings, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Sample XML that defines a Code128 barcode with codetext "12345".
        // In a real scenario this XML would be read from a database BLOB field.
        const string xmlContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
<BarcodeGenerator>
  <EncodeType>Code128</EncodeType>
  <CodeText>12345</CodeText>
  <Parameters>
    <Barcode>
      <CodeTextParameters>
        <Location>Below</Location>
        <Alignment>Center</Alignment>
        <Font>
          <FamilyName>Arial</FamilyName>
          <Size>
            <Point>10</Point>
          </Size>
        </Font>
      </CodeTextParameters>
    </Barcode>
  </Parameters>
</BarcodeGenerator>";

        // Convert the XML string to a byte array as it would be stored in a BLOB.
        byte[] xmlBytes = System.Text.Encoding.UTF8.GetBytes(xmlContent);

        // Load the XML from a MemoryStream and import the barcode settings.
        using (var xmlStream = new MemoryStream(xmlBytes))
        using (var generator = BarcodeGenerator.ImportFromXml(xmlStream))
        {
            // Verify that the generator was created successfully.
            Console.WriteLine($"Imported barcode type: {generator.BarcodeType.TypeName}");
            Console.WriteLine($"Imported codetext: {generator.CodeText}");

            // Save the generated barcode image to a file.
            const string outputPath = "imported_barcode.png";
            generator.Save(outputPath);
            Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(outputPath)}");
        }
    }
}