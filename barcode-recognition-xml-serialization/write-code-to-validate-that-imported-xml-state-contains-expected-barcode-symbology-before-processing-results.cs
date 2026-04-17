using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Path to the XML configuration that defines barcode properties
        const string xmlPath = "barcodeConfig.xml";

        // Verify that the XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Expected symbology (change as needed)
        BaseEncodeType expectedEncodeType = EncodeTypes.Code128;

        // Import barcode generator settings from the XML file
        using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Validate that the imported generator uses the expected symbology
            if (!generator.BarcodeType.Equals(expectedEncodeType))
            {
                Console.WriteLine($"Unexpected symbology in XML. Found: {generator.BarcodeType}, Expected: {expectedEncodeType}");
                return;
            }

            // Generate the barcode image in memory
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Create a reader that looks for the expected decode type
                using (BarCodeReader reader = new BarCodeReader(barcodeImage, DecodeType.Code128))
                {
                    bool anyFound = false;
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        anyFound = true;
                        // Verify that the recognized barcode type matches the expectation
                        if (result.CodeType.Equals(expectedEncodeType))
                        {
                            Console.WriteLine($"Valid barcode recognized. CodeText: {result.CodeText}");
                        }
                        else
                        {
                            Console.WriteLine($"Barcode type mismatch. Recognized: {result.CodeType}, Expected: {expectedEncodeType}");
                        }
                    }

                    if (!anyFound)
                    {
                        Console.WriteLine("No barcodes were recognized in the generated image.");
                    }
                }
            }
        }
    }
}