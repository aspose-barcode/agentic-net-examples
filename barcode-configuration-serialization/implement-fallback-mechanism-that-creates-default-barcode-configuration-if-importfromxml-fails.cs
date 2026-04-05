using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        const string xmlPath = "barcodeConfig.xml";
        const string outputPath = "barcode.png";

        BarcodeGenerator generator = null;

        try
        {
            if (File.Exists(xmlPath))
            {
                // Attempt to load configuration from XML
                generator = BarcodeGenerator.ImportFromXml(xmlPath);
            }
        }
        catch (Exception)
        {
            // Import failed – will fall back to default configuration
        }

        if (generator == null)
        {
            // Create a default barcode generator (Code128) with a sample code text
            generator = new BarcodeGenerator(EncodeTypes.Code128, "Default123");

            // Example of setting size‑related parameters using unit members
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 40f;
        }

        // Ensure proper disposal of the generator
        using (generator)
        {
            // Save the generated barcode image
            generator.Save(outputPath);
        }

        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}