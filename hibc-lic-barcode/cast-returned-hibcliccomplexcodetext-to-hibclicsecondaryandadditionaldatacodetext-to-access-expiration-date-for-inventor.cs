using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare secondary and additional data codetext with an expiry date
        var secondaryCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext();
        secondaryCodetext.BarcodeType = EncodeTypes.HIBCQRLIC;
        secondaryCodetext.LinkCharacter = 'L';
        secondaryCodetext.Data = new SecondaryAndAdditionalData
        {
            ExpiryDate = DateTime.Today.AddDays(30),
            ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
            LotNumber = "LOT123",
            SerialNumber = "SERIAL123"
        };

        // Generate the barcode image
        using (var generator = new ComplexBarcodeGenerator(secondaryCodetext))
        {
            using (var image = generator.GenerateBarCodeImage())
            {
                // Decode the barcode from the generated image
                using (var reader = new BarCodeReader(image, DecodeType.HIBCQRLIC))
                {
                    var results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcode detected.");
                        return;
                    }

                    string codeText = results[0].CodeText;

                    // Decode the codetext to the specific secondary data type
                    var decoded = ComplexCodetextReader.TryDecodeHIBCLIC(codeText) as HIBCLICSecondaryAndAdditionalDataCodetext;
                    if (decoded == null)
                    {
                        Console.WriteLine("Failed to decode as HIBCLICSecondaryAndAdditionalDataCodetext.");
                        return;
                    }

                    // Access and display the expiry date
                    Console.WriteLine("Expiry date: " + decoded.Data.ExpiryDate.ToString("yyyy-MM-dd"));
                }
            }
        }
    }
}