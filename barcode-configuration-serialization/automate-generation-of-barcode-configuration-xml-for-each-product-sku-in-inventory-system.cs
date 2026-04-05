using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample list of product SKUs
        string[] skus = { "SKU001", "SKU002", "SKU003" };

        foreach (string sku in skus)
        {
            // Create a barcode generator for Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the encoded value to the SKU
                generator.CodeText = sku;

                // Export the barcode configuration to an XML file named after the SKU
                string xmlFileName = $"{sku}.xml";
                bool success = generator.ExportToXml(xmlFileName);
                if (!success)
                {
                    Console.WriteLine($"Failed to export XML for SKU: {sku}");
                }
            }
        }

        Console.WriteLine("Barcode configuration XML generation completed.");
    }
}