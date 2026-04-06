using System;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string filePath = "code39.png";
        const string codeText = "CODE39TEST123";
        const int iterations = 1000;

        // Generate a Code 39 barcode with checksum enabled
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            // Enable checksum generation for the barcode
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            // Save the barcode image to a file
            generator.Save(filePath);
        }

        // Measure recognition time with checksum validation enabled
        long timeWithChecksum = MeasureRecognition(filePath, iterations, ChecksumValidation.On);
        // Measure recognition time with checksum validation disabled
        long timeWithoutChecksum = MeasureRecognition(filePath, iterations, ChecksumValidation.Off);

        Console.WriteLine($"Recognition time with checksum validation ON : {timeWithChecksum} ms");
        Console.WriteLine($"Recognition time with checksum validation OFF: {timeWithoutChecksum} ms");
    }

    static long MeasureRecognition(string imagePath, int iterations, ChecksumValidation validationMode)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        for (int i = 0; i < iterations; i++)
        {
            // Initialize the reader for Code 39 decoding
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code39))
            {
                // Use high‑performance settings to focus on speed
                reader.QualitySettings = QualitySettings.HighPerformance;
                // Set the desired checksum validation mode
                reader.BarcodeSettings.ChecksumValidation = validationMode;

                // Perform the recognition
                foreach (var result in reader.ReadBarCodes())
                {
                    // Access the result to ensure the read operation is executed
                    var _ = result.CodeText;
                }
            }
        }

        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }
}