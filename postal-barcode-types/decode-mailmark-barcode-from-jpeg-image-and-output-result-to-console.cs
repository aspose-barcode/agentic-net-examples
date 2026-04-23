using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Path to the JPEG image containing the Mailmark barcode
        const string imagePath = "mailmark.jpg";

        // Verify that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize the barcode reader for Mailmark symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.Mailmark))
        {
            // Perform the recognition
            var results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected in the image.");
                return;
            }

            // Process each detected barcode
            foreach (var result in results)
            {
                Console.WriteLine($"Raw CodeText: {result.CodeText}");

                // Decode the Mailmark complex codetext
                MailmarkCodetext mailmark = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                if (mailmark != null)
                {
                    Console.WriteLine("Decoded Mailmark details:");
                    Console.WriteLine($"  Format: {mailmark.Format}");
                    Console.WriteLine($"  VersionID: {mailmark.VersionID}");
                    Console.WriteLine($"  Class: {mailmark.Class}");
                    Console.WriteLine($"  SupplychainID: {mailmark.SupplychainID}");
                    Console.WriteLine($"  ItemID: {mailmark.ItemID}");
                    Console.WriteLine($"  DestinationPostCodePlusDPS: {mailmark.DestinationPostCodePlusDPS}");
                }
                else
                {
                    Console.WriteLine("Failed to decode Mailmark codetext.");
                }
            }
        }
    }
}