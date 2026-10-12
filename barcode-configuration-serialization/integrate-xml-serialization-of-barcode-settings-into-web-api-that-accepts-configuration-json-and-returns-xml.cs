// Title: XML Serialization of Barcode Settings with Aspose.BarCode
// Description: Demonstrates how to deserialize barcode configuration from JSON, export the generator state to XML, re-import it, and generate a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category, showcasing the use of BarcodeGenerator, EncodeTypes, and ExportToXml/ImportFromXml APIs. Developers often need to persist barcode settings, exchange them between services, or store them for later reuse; this pattern illustrates typical JSON‑to‑XML conversion and image creation workflows.
// Prompt: Integrate XML serialization of barcode settings into a web API that accepts configuration JSON and returns XML.
// Tags: barcode, symbology, json, xml, serialization, aspose.barcode, generation, image, export, import

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Represents the barcode configuration that can be supplied as JSON.
/// </summary>
class BarcodeConfig
{
    public string Symbology { get; set; }
    public string CodeText { get; set; }
    public string BarColor { get; set; }
    public float XDimension { get; set; }
}

class Program
{
    /// <summary>
    /// Entry point that demonstrates deserialization, XML export/import, and image generation.
    /// </summary>
    static void Main()
    {
        // Sample JSON configuration representing barcode settings
        string json = @"{
            ""Symbology"": ""Code128"",
            ""CodeText"": ""1234567890"",
            ""BarColor"": ""Blue"",
            ""XDimension"": 2.0
        }";

        // Deserialize JSON into a BarcodeConfig object
        BarcodeConfig config = JsonSerializer.Deserialize<BarcodeConfig>(json);
        if (config == null)
        {
            Console.WriteLine("Failed to parse configuration.");
            return;
        }

        // Resolve the symbology name to the corresponding BaseEncodeType using reflection
        FieldInfo symField = typeof(EncodeTypes).GetField(config.Symbology);
        if (symField == null)
        {
            Console.WriteLine($"Unknown symbology: {config.Symbology}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)symField.GetValue(null);

        // Create a barcode generator with the resolved symbology and provided code text
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, config.CodeText))
        {
            // Set the bar color if the specified color name exists in Aspose.Drawing.Color
            PropertyInfo colorProp = typeof(Aspose.Drawing.Color).GetProperty(config.BarColor, BindingFlags.Public | BindingFlags.Static);
            if (colorProp != null)
            {
                generator.Parameters.Barcode.BarColor = (Aspose.Drawing.Color)colorProp.GetValue(null);
            }

            // Set the XDimension (module size) of the barcode
            generator.Parameters.Barcode.XDimension.Point = config.XDimension;

            // Export the generator's state to XML using a memory stream
            using (MemoryStream exportStream = new MemoryStream())
            {
                generator.ExportToXml(exportStream);
                exportStream.Position = 0;

                // Read the exported XML into a string
                string xml;
                using (StreamReader reader = new StreamReader(exportStream, Encoding.UTF8, true, 1024, leaveOpen: true))
                {
                    xml = reader.ReadToEnd();
                }

                Console.WriteLine("Exported XML:");
                Console.WriteLine(xml);

                // Import the generation state from the XML and generate the barcode image
                byte[] xmlBytes = Encoding.UTF8.GetBytes(xml);
                using (MemoryStream importStream = new MemoryStream())
                {
                    importStream.Write(xmlBytes, 0, xmlBytes.Length);
                    importStream.Position = 0;

                    using (BarcodeGenerator importedGen = BarcodeGenerator.ImportFromXml(importStream))
                    {
                        string outputPath = "generated_barcode.png";
                        importedGen.Save(outputPath, BarCodeImageFormat.Png);
                        Console.WriteLine($"Barcode image saved to: {outputPath}");
                    }
                }
            }
        }
    }
}