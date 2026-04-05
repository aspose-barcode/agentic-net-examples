using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static int totalTests = 0;
    static int failedTests = 0;

    static void Main()
    {
        RunTest("EAN13 checksum generation and validation (On)", TestEan13ChecksumOn);
        RunTest("EAN13 checksum generation and validation (Off)", TestEan13ChecksumOff);
        RunTest("Code39 checksum enabled", TestCode39ChecksumEnabled);
        RunTest("Code39 checksum disabled", TestCode39ChecksumDisabled);

        Console.WriteLine();
        Console.WriteLine($"Total checksum tests run: {totalTests}");
        Console.WriteLine($"Total failed checksum tests: {failedTests}");
    }

    static void RunTest(string testName, Action testAction)
    {
        totalTests++;
        try
        {
            testAction();
            Console.WriteLine($"{testName}: Passed");
        }
        catch (Exception ex)
        {
            failedTests++;
            Console.WriteLine($"{testName}: Failed - {ex.Message}");
        }
    }

    static void TestEan13ChecksumOn()
    {
        string tempFile = Path.ChangeExtension(Path.GetTempFileName(), ".png");
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "123456789012"))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
                generator.Save(tempFile);
            }

            using (var reader = new BarCodeReader(tempFile, DecodeType.EAN13))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                var results = reader.ReadBarCodes();
                if (results == null || results.Length == 0)
                    throw new Exception("No barcode detected.");

                var result = results[0];
                // Expected full code text includes checksum digit (8)
                if (result.CodeText != "1234567890128")
                    throw new Exception($"Unexpected CodeText: {result.CodeText}");
                // Verify checksum value reported by the reader
                if (string.IsNullOrEmpty(result.Extended.OneD.CheckSum))
                    throw new Exception("Checksum not reported.");
            }
        }
        finally
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    static void TestEan13ChecksumOff()
    {
        string tempFile = Path.ChangeExtension(Path.GetTempFileName(), ".png");
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "123456789012"))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
                generator.Save(tempFile);
            }

            using (var reader = new BarCodeReader(tempFile, DecodeType.EAN13))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;
                var results = reader.ReadBarCodes();
                if (results == null || results.Length == 0)
                    throw new Exception("No barcode detected.");

                var result = results[0];
                // Even with validation off, CodeText should still be readable
                if (result.CodeText != "1234567890128")
                    throw new Exception($"Unexpected CodeText: {result.CodeText}");
            }
        }
        finally
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    static void TestCode39ChecksumEnabled()
    {
        string tempFile = Path.ChangeExtension(Path.GetTempFileName(), ".png");
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "CODE39"))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.Yes;
                generator.Save(tempFile);
            }

            using (var reader = new BarCodeReader(tempFile, DecodeType.Code39))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                var results = reader.ReadBarCodes();
                if (results == null || results.Length == 0)
                    throw new Exception("No barcode detected.");

                var result = results[0];
                if (string.IsNullOrEmpty(result.Extended.OneD.CheckSum))
                    throw new Exception("Checksum not generated for Code39.");
            }
        }
        finally
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    static void TestCode39ChecksumDisabled()
    {
        string tempFile = Path.ChangeExtension(Path.GetTempFileName(), ".png");
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "CODE39"))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = Aspose.BarCode.Generation.EnableChecksum.No;
                generator.Save(tempFile);
            }

            using (var reader = new BarCodeReader(tempFile, DecodeType.Code39))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                var results = reader.ReadBarCodes();
                if (results == null || results.Length == 0)
                    throw new Exception("No barcode detected.");

                var result = results[0];
                if (!string.IsNullOrEmpty(result.Extended.OneD.CheckSum))
                    throw new Exception("Checksum should not be present for Code39 when disabled.");
            }
        }
        finally
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }
}