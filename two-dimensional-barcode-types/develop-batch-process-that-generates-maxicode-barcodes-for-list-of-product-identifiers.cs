using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Output folder for generated barcodes
        string outputFolder = "MaxiCodeOutput";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Sample list of product identifiers
        string[] productIds = { "PROD001", "PROD002", "PROD003", "PROD004", "PROD005" };

        foreach (string id in productIds)
        {
            // Prepare standard codetext for Mode 4 (data only)
            var codetext = new MaxiCodeStandardCodetext();
            codetext.Mode = MaxiCodeMode.Mode4;
            codetext.Message = id;

            // File name for the barcode image
            string filePath = Path.Combine(outputFolder, $"{id}_maxicode.png");

            // Generate and save the MaxiCode barcode
            using (var generator = new ComplexBarcodeGenerator(codetext))
            {
                generator.Save(filePath);
            }
        }

        Console.WriteLine("MaxiCode barcodes have been generated.");
    }
}