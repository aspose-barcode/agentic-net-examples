using System;
using System.IO;
using System.Xml;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: BarcodeDetect <imagePath>");
            return;
        }

        string imagePath = args[0];
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Prepare output XML path (same folder, same name with .xml extension)
        string xmlPath = Path.ChangeExtension(imagePath, ".xml");

        // Read barcodes from the image
        using (var reader = new BarCodeReader(imagePath))
        {
            // Create XML writer
            using (var writer = XmlWriter.Create(xmlPath, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("BarCodes");

                foreach (var result in reader.ReadBarCodes())
                {
                    writer.WriteStartElement("BarCode");
                    writer.WriteElementString("Type", result.CodeTypeName);
                    writer.WriteElementString("CodeText", result.CodeText);
                    writer.WriteEndElement(); // BarCode
                }

                writer.WriteEndElement(); // BarCodes
                writer.WriteEndDocument();
            }
        }

        Console.WriteLine($"Barcode detection results saved to: {xmlPath}");
    }
}