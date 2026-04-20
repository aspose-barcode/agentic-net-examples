using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define the output configuration file path
        string configPath = "PostalBarcodeDefaults.xml";

        // Create a generator for a postal symbology (Postnet) to access postal parameters
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet))
        {
            // Set default XDimension (smallest bar width) in points
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Set default BarHeight for 1D barcodes in points
            generator.Parameters.Barcode.BarHeight.Point = 30f;

            // Set default FilledBars behavior (true = bars are filled)
            generator.Parameters.Barcode.FilledBars = true;

            // Export the current settings to an XML configuration file
            generator.ExportToXml(configPath);
        }

        // Inform the user that the configuration file has been created
        Console.WriteLine($"Configuration file created at: {Path.GetFullPath(configPath)}");
    }
}