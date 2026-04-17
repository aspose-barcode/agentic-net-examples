using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeXmlGenerator
{
    class Program
    {
        static void Main()
        {
            // Sample list of product SKUs
            var skus = new List<string> { "SKU001", "SKU002", "SKU003", "SKU004", "SKU005" };

            // Directory to store generated XML files
            string outputDir = "BarcodesXml";
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            foreach (var sku in skus)
            {
                // Create a barcode generator for Code128 symbology with the SKU as the code text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, sku))
                {
                    // Set image dimensions
                    generator.Parameters.ImageWidth.Pixels = 300f;
                    generator.Parameters.ImageHeight.Pixels = 150f;

                    // Set barcode-specific properties
                    generator.Parameters.Barcode.BarHeight.Pixels = 100f;
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;
                    generator.Parameters.Barcode.BarColor = Color.Black;
                    generator.Parameters.BackColor = Color.White;

                    // Export the barcode configuration to an XML file
                    string xmlPath = Path.Combine(outputDir, $"{sku}.xml");
                    bool exported = generator.ExportToXml(xmlPath);
                    if (!exported)
                    {
                        Console.WriteLine($"Failed to export XML for SKU: {sku}");
                    }
                }
            }

            Console.WriteLine("Barcode XML generation completed.");
        }
    }
}