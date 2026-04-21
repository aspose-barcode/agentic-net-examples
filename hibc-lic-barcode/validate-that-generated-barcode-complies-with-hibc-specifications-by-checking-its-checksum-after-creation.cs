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
        // Prepare complex codetext for HIBC LIC (secondary and additional data)
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext();
        complexCodetext.BarcodeType = EncodeTypes.HIBCCode128LIC;
        complexCodetext.LinkCharacter = 'L';
        complexCodetext.Data = new SecondaryAndAdditionalData
        {
            ExpiryDate = DateTime.Today.AddMonths(6),
            ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
            Quantity = 10,
            LotNumber = "LOT123",
            SerialNumber = "SERIAL123",
            DateOfManufacture = DateTime.Today.AddMonths(-1)
        };

        // Generate barcode image into a memory stream
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Ensure checksum is generated
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;

            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // Decode the barcode with checksum validation enabled
                using (var reader = new BarCodeReader(ms, DecodeType.HIBCCode128LIC))
                {
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                    var results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcode detected.");
                        return;
                    }

                    foreach (var result in results)
                    {
                        Console.WriteLine($"CodeText: {result.CodeText}");
                        // For 1D barcodes the checksum value is available in Extended.OneD.CheckSum
                        var checksum = result.Extended?.OneD?.CheckSum;
                        if (checksum != null)
                        {
                            Console.WriteLine($"Checksum (decoded): {checksum}");
                            Console.WriteLine("Checksum validation: Passed");
                        }
                        else
                        {
                            Console.WriteLine("Checksum information not available.");
                        }
                    }
                }
            }
        }
    }
}