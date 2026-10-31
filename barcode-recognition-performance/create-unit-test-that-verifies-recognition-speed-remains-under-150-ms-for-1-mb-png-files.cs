// Title: QR Code Generation and Recognition Speed Test
// Description: Generates a large QR code PNG (~1 MB) and measures the time required to recognize it, ensuring the operation stays under a performance threshold.
// Category-Description: Demonstrates Aspose.BarCode operations for creating and reading QR codes, focusing on performance testing. Uses BarcodeGenerator for QR code creation and BarCodeReader for decoding, common tasks when validating barcode processing speed in automated pipelines.
// Prompt: Create a unit test that verifies recognition speed remains under 150 ms for 1‑MB PNG files.
// Tags: qr, generation, recognition, speed, png, barcodegenerator, barcodereader

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a large QR code image and measures the time required to recognize it.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, measures recognition speed, and reports the result.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSpeedTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated QR code image
        string barcodePath = Path.Combine(tempFolder, "large_qr.png");

        // Generate a large QR code image (aiming for ~1 MB)
        GenerateLargeQrBarcode(barcodePath);

        // Verify that the barcode file was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("FAILED: Barcode file was not created.");
            return;
        }

        // Output the size of the generated file
        long fileSize = new FileInfo(barcodePath).Length;
        Console.WriteLine($"Generated barcode size: {fileSize} bytes");

        // Measure how long it takes to recognize the barcode
        long elapsedMs = MeasureRecognitionTime(barcodePath);
        Console.WriteLine($"Recognition time: {elapsedMs} ms");

        // Evaluate the performance against the 150 ms threshold
        if (elapsedMs <= 150)
        {
            Console.WriteLine("PASSED: Recognition time is within the 150 ms limit.");
        }
        else
        {
            Console.WriteLine("FAILED: Recognition time exceeds the 150 ms limit.");
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test result
        }
    }

    /// <summary>
    /// Generates a QR code with enough data to produce an image close to 1 MB in size.
    /// </summary>
    /// <param name="filePath">The file path where the PNG image will be saved.</param>
    static void GenerateLargeQrBarcode(string filePath)
    {
        // Build a long text to increase QR code size
        string baseText = "Aspose.BarCode Speed Test ";
        string longText = baseText;
        while (longText.Length < 2000) // increase until image size grows
        {
            longText += baseText;
        }

        // Create QR barcode generator with the long text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, longText))
        {
            // Increase module size to enlarge the image
            generator.Parameters.Barcode.XDimension.Point = 5f;

            // Save the generated barcode as a PNG file
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Measures the time required to recognize a QR code in the specified image.
    /// </summary>
    /// <param name="imagePath">Path to the PNG image containing the QR code.</param>
    /// <returns>Elapsed time in milliseconds.</returns>
    static long MeasureRecognitionTime(string imagePath)
    {
        BaseDecodeType decodeType = DecodeType.QR;

        // Initialize the barcode reader for QR codes
        using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
        {
            Stopwatch watch = Stopwatch.StartNew();

            // Perform the recognition
            reader.ReadBarCodes();

            watch.Stop();

            // Optionally, verify that a barcode was found
            if (reader.FoundCount == 0)
            {
                Console.WriteLine("WARNING: No barcode detected.");
            }

            return watch.ElapsedMilliseconds;
        }
    }
}