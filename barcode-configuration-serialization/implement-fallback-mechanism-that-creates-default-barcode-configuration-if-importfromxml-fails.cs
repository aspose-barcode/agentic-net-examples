using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string xmlFile = "barcodeConfig.xml";
        const string outputFile = "fallback.png";

        BarcodeGenerator generator = null;

        // Try to import configuration from XML
        if (File.Exists(xmlFile))
        {
            try
            {
                generator = BarcodeGenerator.ImportFromXml(xmlFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ImportFromXml failed: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"XML file '{xmlFile}' not found.");
        }

        // If import failed, create a default barcode generator
        if (generator == null)
        {
            Console.WriteLine("Creating default barcode configuration.");
            generator = new BarcodeGenerator(EncodeTypes.Code128, "Default123");
            // Example of setting some default parameters
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;
        }

        // Save the barcode image
        using (generator)
        {
            generator.Save(outputFile);
        }

        Console.WriteLine($"Barcode image saved to '{outputFile}'.");
    }
}