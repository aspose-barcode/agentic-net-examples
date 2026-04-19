using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Path for the exported XML configuration
        string xmlFile = "barcode_config.xml";

        // Create a BarcodeGenerator for Code128 with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set some example parameters
            generator.Parameters.Barcode.BarColor = Color.Blue;          // Barcode bar color
            generator.Parameters.Barcode.XDimension.Point = 2f;        // X-dimension in points

            // Export the generator's configuration to an XML file
            bool exported = generator.ExportToXml(xmlFile);
            Console.WriteLine($"Export to XML {(exported ? "succeeded" : "failed")} at: {xmlFile}");
        }
    }
}