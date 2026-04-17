using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeFromXmlDemo
{
    class Program
    {
        /// <summary>
        /// Creates a BarcodeGenerator instance from an XML string.
        /// </summary>
        /// <param name="xmlContent">The XML that defines barcode settings.</param>
        /// <returns>A configured BarcodeGenerator.</returns>
        static BarcodeGenerator InitializeFromXml(string xmlContent)
        {
            // Convert the XML string to a UTF‑8 encoded memory stream.
            using (var xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlContent)))
            {
                // Import the settings from the stream and obtain a generator.
                return BarcodeGenerator.ImportFromXml(xmlStream);
            }
        }

        static void Main(string[] args)
        {
            // Sample XML defining a simple Code128 barcode with the text "12345".
            string barcodeXml = @"
<BarcodeGenerator>
    <CodeText>12345</CodeText>
    <BarcodeType>Code128</BarcodeType>
</BarcodeGenerator>";

            // Initialize the generator from the XML.
            using (var generator = InitializeFromXml(barcodeXml))
            {
                // Optional: adjust additional parameters if needed.
                // Example: set image size.
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Save the generated barcode image to a file.
                string outputPath = "barcode_from_xml.png";
                generator.Save(outputPath);
                Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(outputPath)}");
            }
        }
    }
}