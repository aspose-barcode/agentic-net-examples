using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample list of identifiers to encode
        var identifiers = new List<string>
        {
            "12345",
            "67890",
            "24680",
            "13579",
            "112233"
        };

        // Output folder for TIFF files
        string outputFolder = "Barcodes";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        foreach (var id in identifiers)
        {
            string filePath = Path.Combine(outputFolder, $"{id}.tif");

            // Create barcode generator for Codabar with the identifier as code text
            using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, id))
            {
                // Set start and stop symbols to 'C'
                generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C;
                generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.C;

                // Save barcode as TIFF image
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }
        }
    }
}