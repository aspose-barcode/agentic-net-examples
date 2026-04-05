using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;

namespace BarcodeFromXmlDemo
{
    class Program
    {
        static void Main()
        {
            // Sample XML that defines barcode settings
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<BarcodeGenerator>
    <CodeText>123ABC</CodeText>
    <BarcodeType>Code128</BarcodeType>
</BarcodeGenerator>";

            GenerateBarcodeFromXmlString(xml);
        }

        static void GenerateBarcodeFromXmlString(string xmlString)
        {
            // Convert the XML string to a UTF‑8 encoded memory stream
            byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlString);
            using (var xmlStream = new MemoryStream(xmlBytes))
            {
                // Import the BarcodeGenerator configuration from the XML stream
                using (var generator = BarcodeGenerator.ImportFromXml(xmlStream))
                {
                    // Save the generated barcode image to a PNG file
                    generator.Save("barcode_from_xml.png");
                }
            }
        }
    }
}