using System;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Sample numeric input for Dutch KIX barcode
        string codeText = "1234567890";

        // Validate that the input contains only digits
        if (!Regex.IsMatch(codeText, @"^\d+$"))
            throw new ArgumentException("Code text must be numeric for Dutch KIX barcode.");

        // Create the barcode generator for Dutch KIX with the provided code text
        using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, codeText))
        {
            // Enable automatic checksum generation
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;

            // Optional: show checksum in human‑readable text
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the generated barcode image
            string outputPath = "dutchkix.png";
            generator.Save(outputPath);
            Console.WriteLine($"Dutch KIX barcode saved to {outputPath}");
        }

        // Demonstrate recognition with checksum validation enabled
        using (var reader = new BarCodeReader("dutchkix.png", DecodeType.DutchKIX))
        {
            // Enable checksum validation during recognition
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Recognized CodeText: {result.CodeText}");
                Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
                Console.WriteLine($"Value (without checksum): {result.Extended.OneD.Value}");
            }
        }
    }
}