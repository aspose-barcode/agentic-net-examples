using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for the generated barcode image
        const string filePath = "code128.png";

        // Create a Code128 barcode with checksum enabled and show the checksum digit
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Enable checksum generation
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
            // Show checksum digit in the human‑readable text
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the barcode image
            generator.Save(filePath);
        }

        // Read and decode the barcode, validating the checksum
        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            // Enable checksum validation during recognition
            reader.BarcodeSettings.ChecksumValidation = Aspose.BarCode.BarCodeRecognition.ChecksumValidation.On;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded Text: {result.CodeText}");

                // For 1D barcodes the checksum can be accessed via OneD extended parameters
                if (result.Extended?.OneD != null)
                {
                    Console.WriteLine($"Checksum (from recognition): {result.Extended.OneD.CheckSum}");
                }
                else
                {
                    Console.WriteLine("Checksum information not available.");
                }
            }
        }
    }
}