using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define file names for the generated barcode image and the exported XML.
        const string barcodeImagePath = "barcode.png";
        const string exportXmlPath = "recognition_state.xml";

        // Step 1: Generate a simple Code128 barcode and save it to a file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Save the barcode image.
            generator.Save(barcodeImagePath);
        }

        // Step 2: Recognize the barcode from the saved image.
        using (var reader = new BarCodeReader(barcodeImagePath, DecodeType.Code128))
        {
            // Read all barcodes found in the image.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                Console.WriteLine("BarCode CodeText: " + result.CodeText);
                // For 1D barcodes, also display value and checksum if available.
                if (result.Extended?.OneD != null)
                {
                    Console.WriteLine("BarCode Value: " + result.Extended.OneD.Value);
                    Console.WriteLine("BarCode Checksum: " + result.Extended.OneD.CheckSum);
                }
                Console.WriteLine("Recognition Confidence: " + result.Confidence);
                Console.WriteLine();
            }

            // Step 3: Export the recognition state (reader settings and results) to an XML file.
            bool exportSuccess = reader.ExportToXml(exportXmlPath);
            Console.WriteLine("Export to XML " + (exportSuccess ? "succeeded" : "failed") + ": " + Path.GetFullPath(exportXmlPath));
        }
    }
}