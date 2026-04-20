using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare Mailmark codetext with required fields
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state format
            VersionID = 1,                  // integer version
            Class = "0",                    // class identifier
            SupplychainID = 384224,         // supply chain ID
            ItemID = 16563762,              // item ID
            DestinationPostCodePlusDPS = "EF61AH8T " // known‑valid destination
        };

        // Construct the full codetext (includes automatically calculated checksum)
        string constructedCodetext = mailmark.GetConstructedCodetext();

        // Generate the Mailmark barcode image
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set image size (required to avoid zero‑size error)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Set a reasonable bar height (required for Mailmark)
            generator.Parameters.Barcode.BarHeight.Point = 30f;

            // Save to a memory stream in PNG format
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // reset stream for reading

                // Decode the barcode and validate checksum presence
                using (BarCodeReader reader = new BarCodeReader(ms, DecodeType.Mailmark))
                {
                    // Ensure checksum validation is performed
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("Decoded CodeText: " + result.CodeText);
                        bool checksumMatches = string.Equals(result.CodeText, constructedCodetext, StringComparison.Ordinal);
                        Console.WriteLine("Checksum validation: " + (checksumMatches ? "PASS" : "FAIL"));
                    }
                }
            }
        }
    }
}