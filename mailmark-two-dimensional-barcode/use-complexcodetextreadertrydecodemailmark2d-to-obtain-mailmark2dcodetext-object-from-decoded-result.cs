using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Path to the image containing a Mailmark 2D barcode.
        string imagePath = "mailmark2d.png";

        // Verify that the image file exists.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a barcode reader that supports all barcode types.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes from the image.
            var results = reader.ReadBarCodes();

            foreach (var result in results)
            {
                // Obtain the raw codetext from the detection result.
                string rawCodeText = result.CodeText;

                // Attempt to decode the codetext as a Royal Mail Mailmark 2D barcode.
                Mailmark2DCodetext mailmark2D = ComplexCodetextReader.TryDecodeMailmark2D(rawCodeText);

                if (mailmark2D != null)
                {
                    Console.WriteLine("Mailmark 2D decoded successfully:");
                    Console.WriteLine($"Class: {mailmark2D.Class}");
                    Console.WriteLine($"VersionID: {mailmark2D.VersionID}");
                    Console.WriteLine($"InformationTypeID: {mailmark2D.InformationTypeID}");
                    Console.WriteLine($"ItemID: {mailmark2D.ItemID}");
                    Console.WriteLine($"SupplyChainID: {mailmark2D.SupplyChainID}");
                    Console.WriteLine($"DestinationPostCodeAndDPS: {mailmark2D.DestinationPostCodeAndDPS}");
                    // Add more fields as needed.
                }
                else
                {
                    Console.WriteLine("Failed to decode Mailmark 2D codetext.");
                }
            }
        }
    }
}