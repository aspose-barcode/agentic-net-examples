using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Path to the image containing the Mailmark 2D barcode
        string imagePath = "mailmark2d.png";

        // Verify that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader for DataMatrix decoding
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            // Iterate over detected barcodes
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                Console.WriteLine($"Raw CodeText: {result.CodeText}");

                // Attempt to decode the Mailmark 2D codetext
                Mailmark2DCodetext mailmark = ComplexCodetextReader.TryDecodeMailmark2D(result.CodeText);
                if (mailmark != null)
                {
                    Console.WriteLine("Mailmark 2D decoded successfully:");
                    Console.WriteLine($"  VersionID: {mailmark.VersionID}");
                    Console.WriteLine($"  InformationTypeID: {mailmark.InformationTypeID}");
                    Console.WriteLine($"  Class: {mailmark.Class}");
                    Console.WriteLine($"  RTSFlag: {mailmark.RTSFlag}");
                    Console.WriteLine($"  SupplyChainID: {mailmark.SupplyChainID}");
                    Console.WriteLine($"  ItemID: {mailmark.ItemID}");
                    Console.WriteLine($"  DestinationPostCodeAndDPS: {mailmark.DestinationPostCodeAndDPS}");
                    Console.WriteLine($"  ReturnToSenderPostCode: {mailmark.ReturnToSenderPostCode}");
                    Console.WriteLine($"  CustomerContent: {mailmark.CustomerContent}");
                }
                else
                {
                    Console.WriteLine("Failed to decode Mailmark 2D codetext.");
                }
            }
        }
    }
}