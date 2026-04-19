using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Sample code text without checksum
        string originalCodeText = "ABC123";

        // Generate Code39 barcode, disable checksum generation and hide human‑readable text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, originalCodeText))
        {
            // Do not generate checksum
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

            // Hide the human‑readable text (including any checksum)
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the barcode image
            generator.Save("code39.png");
        }

        // Read the generated barcode and verify that no checksum is present
        using (var reader = new BarCodeReader("code39.png", DecodeType.Code39))
        {
            // Disable checksum validation to avoid false failures
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("Detected CodeText: " + result.CodeText);
                Console.WriteLine("Value without checksum: " + result.Extended.OneD.Value);

                // Verify that the detected text matches the original input (no checksum)
                if (result.CodeText == originalCodeText)
                    Console.WriteLine("CodeText matches original (no checksum).");
                else
                    Console.WriteLine("CodeText does NOT match original.");

                if (result.Extended.OneD.Value == originalCodeText)
                    Console.WriteLine("Extended Value matches original (no checksum).");
                else
                    Console.WriteLine("Extended Value does NOT match original.");
            }
        }
    }
}