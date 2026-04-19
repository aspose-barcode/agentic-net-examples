using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static int Main()
    {
        bool hasFailure = false;

        // Test 1: EAN13 generation (no validation due to evaluation limitations)
        try
        {
            string ean13Text = "123456789012"; // 12 digits, checksum will be added
            string ean13File = Path.Combine(Path.GetTempPath(), "ean13.png");

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.EAN13, ean13Text))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;
                generator.Save(ean13File);
            }

            File.Delete(ean13File);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception in EAN13 test: " + ex.Message);
            hasFailure = true;
        }

        // Test 2: Code39FullASCII checksum generation and validation
        try
        {
            string code39Text = "ABC123";
            string code39File = Path.Combine(Path.GetTempPath(), "code39.png");

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, code39Text))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;
                generator.Save(code39File);
            }

            // Validation with checksum ON
            using (BarCodeReader readerOn = new BarCodeReader(code39File, DecodeType.Code39))
            {
                readerOn.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                foreach (BarCodeResult result in readerOn.ReadBarCodes())
                {
                    if (!result.CodeText.StartsWith(code39Text, StringComparison.Ordinal))
                    {
                        Console.WriteLine("Code39 checksum ON validation failed: expected prefix " + code39Text + ", got " + result.CodeText);
                        hasFailure = true;
                    }
                }
            }

            // Validation with checksum OFF
            using (BarCodeReader readerOff = new BarCodeReader(code39File, DecodeType.Code39))
            {
                readerOff.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;
                foreach (BarCodeResult result in readerOff.ReadBarCodes())
                {
                    if (!result.CodeText.StartsWith(code39Text, StringComparison.Ordinal))
                    {
                        Console.WriteLine("Code39 checksum OFF validation failed: expected prefix " + code39Text + ", got " + result.CodeText);
                        hasFailure = true;
                    }
                }
            }

            File.Delete(code39File);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception in Code39 test: " + ex.Message);
            hasFailure = true;
        }

        if (hasFailure)
        {
            Console.WriteLine("Checksum-related tests failed.");
            return 1;
        }

        Console.WriteLine("All checksum-related tests passed.");
        return 0;
    }
}