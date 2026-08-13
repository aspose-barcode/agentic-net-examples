// Title: Import barcode settings from XML with namespace handling
// Description: Demonstrates using Aspose.BarCode's ImportFromXml to generate a barcode from an XML configuration that includes namespaces and extra metadata.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to load barcode settings from an XML file using the BarcodeGenerator class. Typical use cases involve configuring barcodes via external XML files, handling namespaces, and integrating metadata. Developers often need to import settings, generate images, and verify readability in automated workflows.
// Prompt: Test that ImportFromXml correctly interprets XML namespaces when the file includes additional metadata.
// Tags: barcode symbology, generation, png, importfromxml, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that imports barcode generation settings from an XML file,
/// creates a barcode image, and verifies that the barcode can be read back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Writes an XML configuration, imports it,
    /// generates a barcode image, and reads the barcode to confirm correctness.
    /// </summary>
    static void Main()
    {
        // Define XML configuration with a namespace and extra metadata
        string xmlContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
<BarcodeGenerator xmlns=""http://schemas.aspose.com/barcode/2021"">
  <EncodeType>Code128</EncodeType>
  <CodeText>Test123</CodeText>
  <Metadata>
    <Author>TestUser</Author>
    <Comment>Sample barcode generated from XML</Comment>
  </Metadata>
</BarcodeGenerator>";

        // Paths for the temporary XML file and the resulting barcode image
        string xmlPath = "barcode_config.xml";
        string imagePath = "imported_barcode.png";

        // Write the XML configuration to a file on disk
        File.WriteAllText(xmlPath, xmlContent);

        // Import barcode generator settings from the XML file
        using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the generated barcode image in PNG format
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created and can be decoded
        if (File.Exists(imagePath))
        {
            // Initialize a reader for Code128 barcodes
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Iterate through all detected barcodes and output their decoded text
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine("Decoded CodeText: " + result.CodeText);
                }
            }
        }
        else
        {
            Console.WriteLine("Failed to generate barcode image.");
        }
    }
}