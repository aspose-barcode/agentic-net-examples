using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare a temporary file path
        string tempFile = Path.Combine(Path.GetTempPath(), "SwissPostParcel.png");

        // Incorrect code text (missing checksum) for Swiss Post Parcel International barcode
        string incorrectCode = "1234567890";

        // Generate the barcode with auto‑correction enabled
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, incorrectCode))
        {
            // Do not throw on incorrect code text – let the generator correct it
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;
            // Ensure checksum generation is enabled
            generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;

            // Save the barcode image
            generator.Save(tempFile);
        }

        // Verify that the checksum was auto‑corrected by reading the barcode back
        using (var reader = new BarCodeReader(tempFile, DecodeType.SwissPostParcel))
        {
            // Enable checksum validation during recognition
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            bool checksumFound = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // For 1D barcodes the checksum is available in Extended.OneD.CheckSum
                string checksum = result.Extended.OneD.CheckSum;
                if (!string.IsNullOrEmpty(checksum))
                {
                    checksumFound = true;
                    Console.WriteLine($"Detected checksum: {checksum}");
                }
                else
                {
                    Console.WriteLine("Checksum not detected.");
                }
            }

            if (checksumFound)
                Console.WriteLine("Test passed: checksum auto‑correction works.");
            else
                Console.WriteLine("Test failed: checksum was not auto‑corrected.");
        }

        // Clean up the temporary file
        try
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}