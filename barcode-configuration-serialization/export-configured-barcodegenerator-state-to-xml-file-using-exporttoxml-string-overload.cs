using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Set some visual properties
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.BackColor = Color.White;

            // Configure size-related properties using unit members
            generator.Parameters.Barcode.XDimension.Point = 2f;      // smallest bar width
            generator.Parameters.Barcode.BarHeight.Point = 40f;    // height of bars
            generator.Parameters.ImageWidth.Point = 300f;          // image width
            generator.Parameters.ImageHeight.Point = 150f;         // image height

            // Export the current configuration to an XML file
            string xmlPath = "barcodeConfig.xml";
            bool success = generator.ExportToXml(xmlPath);

            Console.WriteLine($"Export to XML {(success ? "succeeded" : "failed")}. File: {xmlPath}");
        }
    }
}