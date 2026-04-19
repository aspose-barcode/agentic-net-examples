using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Input data for the barcode
        const string codeText = "123456";

        // Paths for temporary images
        string noChecksumPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_no_checksum.png");
        string checksumPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_checksum.png");

        // -------------------------------------------------
        // Generate barcode without showing checksum digit
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Do not show checksum in human readable text
            generator.Parameters.Barcode.ChecksumAlwaysShow = false;
            generator.Save(noChecksumPath);
        }

        // -------------------------------------------------
        // Generate barcode with checksum digit visible
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Show checksum in human readable text
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;
            generator.Save(checksumPath);
        }

        // -------------------------------------------------
        // Helper to read barcode and return decoded text and checksum
        // -------------------------------------------------
        (string decodedText, string checksum) ReadBarcode(string path)
        {
            using (var reader = new BarCodeReader(path, DecodeType.Code128))
            {
                var result = reader.ReadBarCodes().FirstOrDefault();
                if (result == null)
                    return (null, null);

                string decoded = result.CodeText;
                string chk = result.Extended.OneD.CheckSum;
                return (decoded, chk);
            }
        }

        // Read both images
        var (decodedNoChecksum, checksumNo) = ReadBarcode(noChecksumPath);
        var (decodedWithChecksum, checksumYes) = ReadBarcode(checksumPath);

        // -------------------------------------------------
        // Validate results
        // -------------------------------------------------
        bool test1 = decodedNoChecksum == codeText;
        bool test2 = decodedWithChecksum != codeText && !string.IsNullOrEmpty(checksumYes);
        bool test3 = decodedWithChecksum.Contains(checksumYes);

        Console.WriteLine("Test 1 - No checksum visible: " + (test1 ? "PASS" : "FAIL"));
        Console.WriteLine("Test 2 - Checksum visible (different text): " + (test2 ? "PASS" : "FAIL"));
        Console.WriteLine("Test 3 - Checksum digit appears in decoded text: " + (test3 ? "PASS" : "FAIL"));

        // Clean up temporary files
        try { File.Delete(noChecksumPath); } catch { }
        try { File.Delete(checksumPath); } catch { }
    }
}