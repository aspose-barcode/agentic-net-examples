using System;
using System.IO;
using System.Xml;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // Get image path from command line or use a default sample
        string imagePath = args.Length > 0 ? args[0] : "sample.png";

        // Validate that the file exists
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Define output XML file path
        string xmlPath = "Barcodes.xml";

        // Initialize the barcode reader with the image file
        using (BarCodeReader reader = new BarCodeReader(imagePath))
        {
            // Create an XML writer to store detection results
            using (XmlWriter writer = XmlWriter.Create(xmlPath, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Barcodes");

                // Read all barcodes from the image
                foreach (var result in reader.ReadBarCodes())
                {
                    writer.WriteStartElement("BarCode");
                    writer.WriteAttributeString("Type", result.CodeTypeName);
                    writer.WriteAttributeString("CodeText", result.CodeText);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement(); // </Barcodes>
                writer.WriteEndDocument();
            }
        }

        Console.WriteLine($"Detection results saved to {xmlPath}");
    }
}