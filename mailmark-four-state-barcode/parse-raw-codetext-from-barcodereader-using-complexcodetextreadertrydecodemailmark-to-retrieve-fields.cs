using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Create a sample Mailmark 4‑state codetext
        var mailmark = new MailmarkCodetext();
        mailmark.Format = 4;                     // 4‑state format
        mailmark.VersionID = 1;                  // version
        mailmark.Class = "0";                    // class
        mailmark.SupplychainID = 384224;         // supply chain
        mailmark.ItemID = 16563762;              // item identifier
        mailmark.DestinationPostCodePlusDPS = "EF61AH8T "; // known‑valid DPS

        // Generate the barcode image into a memory stream
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        using (var imageStream = new MemoryStream())
        {
            generator.Save(imageStream, BarCodeImageFormat.Png);
            imageStream.Position = 0;

            // Read the barcode from the generated image
            using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected CodeText: {result.CodeText}");

                    // Decode the raw CodeText using ComplexCodetextReader
                    var decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                    if (decoded == null)
                    {
                        Console.WriteLine("Failed to decode Mailmark codetext.");
                        continue;
                    }

                    // Output the decoded fields
                    Console.WriteLine($"Format: {decoded.Format}");
                    Console.WriteLine($"VersionID: {decoded.VersionID}");
                    Console.WriteLine($"Class: {decoded.Class}");
                    Console.WriteLine($"SupplychainID: {decoded.SupplychainID}");
                    Console.WriteLine($"ItemID: {decoded.ItemID}");
                    Console.WriteLine($"DestinationPostCodePlusDPS: '{decoded.DestinationPostCodePlusDPS}'");
                }
            }
        }
    }
}