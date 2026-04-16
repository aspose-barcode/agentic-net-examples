using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a sample MaxiCode (Mode 2) with a standard second message
        var maxiCode = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",          // 9‑digit US postal code
            CountryCode = 56,                  // Example country code
            ServiceCategory = 999
        };
        var secondMsg = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample secondary message"
        };
        maxiCode.SecondMessage = secondMsg;

        // Generate the barcode image into a memory stream
        byte[] barcodeBytes;
        using (var generator = new ComplexBarcodeGenerator(maxiCode))
        {
            using (Image image = generator.GenerateBarCodeImage())
            {
                using (var ms = new MemoryStream())
                {
                    image.Save(ms, ImageFormat.Png);
                    barcodeBytes = ms.ToArray();
                }
            }
        }

        // Decode the barcode from the byte array
        using (var reader = new BarCodeReader(new MemoryStream(barcodeBytes), DecodeType.AllSupportedTypes))
        {
            // Explicitly set the image source as required by the rules
            reader.SetBarCodeImage(new MemoryStream(barcodeBytes));

            foreach (var result in reader.ReadBarCodes())
            {
                // Ensure the barcode is a MaxiCode
                if (result.Extended?.MaxiCode == null)
                    continue;

                // Decode the codetext according to the MaxiCode mode
                var decoded = ComplexCodetextReader.TryDecodeMaxiCode(
                    result.Extended.MaxiCode.MaxiCodeMode,
                    result.CodeText);

                // Handle Mode 2 (or Mode 3) structured codetext
                if (decoded is MaxiCodeCodetextMode2 mode2)
                {
                    Console.WriteLine($"Postal Code (primary): {mode2.PostalCode}");
                    Console.WriteLine($"Country Code (primary): {mode2.CountryCode}");
                    Console.WriteLine($"Service Category (primary): {mode2.ServiceCategory}");

                    // Retrieve secondary message
                    if (mode2.SecondMessage is MaxiCodeStandardSecondMessage stdMsg)
                    {
                        Console.WriteLine($"Secondary Message (standard): {stdMsg.Message}");
                    }
                    else if (mode2.SecondMessage is MaxiCodeStructuredSecondMessage structMsg)
                    {
                        Console.WriteLine("Secondary Message (structured):");
                        foreach (var identifier in structMsg.Identifiers)
                        {
                            Console.WriteLine($"  {identifier}");
                        }
                    }
                }
                else if (decoded is MaxiCodeCodetextMode3 mode3)
                {
                    Console.WriteLine($"Postal Code (primary): {mode3.PostalCode}");
                    Console.WriteLine($"Country Code (primary): {mode3.CountryCode}");
                    Console.WriteLine($"Service Category (primary): {mode3.ServiceCategory}");

                    if (mode3.SecondMessage is MaxiCodeStandardSecondMessage stdMsg)
                    {
                        Console.WriteLine($"Secondary Message (standard): {stdMsg.Message}");
                    }
                    else if (mode3.SecondMessage is MaxiCodeStructuredSecondMessage structMsg)
                    {
                        Console.WriteLine("Secondary Message (structured):");
                        foreach (var identifier in structMsg.Identifiers)
                        {
                            Console.WriteLine($"  {identifier}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Decoded MaxiCode type is not handled in this example.");
                }
            }
        }
    }
}