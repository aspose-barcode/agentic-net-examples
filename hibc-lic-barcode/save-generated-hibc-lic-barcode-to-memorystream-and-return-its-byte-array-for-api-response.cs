using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        byte[] barcodeBytes = GenerateHIBCLICBarcode();
        Console.WriteLine($"Generated barcode byte array length: {barcodeBytes.Length}");
        // In a real API, barcodeBytes would be returned in the response body.
    }

    static byte[] GenerateHIBCLICBarcode()
    {
        // Create secondary‑and‑additional data codetext for HIBC LIC.
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            // Choose the desired HIBC LIC symbology (Code128 in this example).
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            // Optional link character.
            LinkCharacter = 'L',
            // Populate secondary data (lot number, serial number, etc.).
            Data = new SecondaryAndAdditionalData
            {
                LotNumber = "LOT123",
                SerialNumber = "SERIAL123"
                // Additional fields such as ExpiryDate, Quantity, etc. can be set here if needed.
            }
        };

        // Generate the barcode and save it to a memory stream.
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                return memoryStream.ToArray();
            }
        }
    }
}