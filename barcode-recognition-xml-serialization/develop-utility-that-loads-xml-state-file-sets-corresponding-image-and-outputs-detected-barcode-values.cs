using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Path to the XML state file (adjust as needed)
        const string xmlPath = "barcode_state.xml";

        // Verify that the XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML state file not found: {xmlPath}");
            return;
        }

        // Load barcode generator settings from the XML file
        using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Generate the barcode image in memory
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Create a reader that will detect all supported barcode types
                using (BarCodeReader reader = new BarCodeReader(barcodeImage, DecodeType.AllSupportedTypes))
                {
                    // Read all barcodes from the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"Detected Text: {result.CodeText}");
                        Console.WriteLine();
                    }

                    // If no barcodes were found, inform the user
                    if (reader.FoundCount == 0)
                    {
                        Console.WriteLine("No barcodes were detected in the generated image.");
                    }
                }
            }
        }
    }
}