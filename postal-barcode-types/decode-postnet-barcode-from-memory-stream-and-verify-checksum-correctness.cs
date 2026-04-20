using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Generate a Postnet barcode image and store it in a memory stream.
        using (var memoryStream = new MemoryStream())
        {
            // Sample postal code (5 digits). The generator will calculate and append the checksum.
            using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345"))
            {
                // Ensure checksum is generated.
                generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
                // Save the barcode image to the memory stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset stream position for reading.
            memoryStream.Position = 0;

            // Decode the barcode from the memory stream.
            using (var reader = new BarCodeReader(memoryStream, DecodeType.Postnet))
            {
                // Enable checksum validation during recognition.
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine("Decoded CodeText: " + result.CodeText);

                    // For 1D barcodes the checksum is available via Extended.OneD.CheckSum.
                    string checksum = result.Extended.OneD.CheckSum;
                    Console.WriteLine("Extracted Checksum: " + (checksum ?? "N/A"));

                    // Verify that the checksum matches the last character of the CodeText (if present).
                    bool isChecksumValid = !string.IsNullOrEmpty(checksum) &&
                                           result.CodeText.EndsWith(checksum, StringComparison.Ordinal);
                    Console.WriteLine("Checksum Valid: " + isChecksumValid);
                }
            }
        }
    }
}