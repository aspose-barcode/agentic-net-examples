using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // List of XML state files to merge (adjust paths as needed)
        string[] xmlFiles = new string[]
        {
            "state1.xml",
            "state2.xml",
            "state3.xml"
        };

        // Collection to hold detection results
        var summaryEntries = new List<XElement>();

        foreach (string xmlFile in xmlFiles)
        {
            if (!File.Exists(xmlFile))
                continue; // Skip missing files

            // Load barcode generator from XML state
            using (var generator = BarcodeGenerator.ImportFromXml(xmlFile))
            {
                // Generate a temporary image file for recognition
                string tempImage = Path.GetTempFileName() + ".png";
                generator.Save(tempImage);

                // Recognize barcodes in the generated image
                using (var reader = new BarCodeReader(tempImage))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Build XML element for each detected barcode
                        var barcodeElement = new XElement("Barcode",
                            new XAttribute("SourceFile", xmlFile),
                            new XAttribute("Type", result.CodeTypeName),
                            new XAttribute("CodeText", result.CodeText));

                        // Include checksum if available (1D barcodes)
                        try
                        {
                            string checksum = result.Extended.OneD.CheckSum;
                            if (!string.IsNullOrEmpty(checksum))
                                barcodeElement.Add(new XAttribute("Checksum", checksum));
                        }
                        catch
                        {
                            // Ignore if checksum not applicable
                        }

                        summaryEntries.Add(barcodeElement);
                    }
                }

                // Clean up temporary image
                try { File.Delete(tempImage); } catch { }
            }
        }

        // Create summary XML document
        var doc = new XDocument(
            new XElement("BarcodesSummary",
                summaryEntries));

        // Save summary to file
        string summaryPath = "summary.xml";
        doc.Save(summaryPath);

        Console.WriteLine($"Summary XML saved to: {Path.GetFullPath(summaryPath)}");
    }
}