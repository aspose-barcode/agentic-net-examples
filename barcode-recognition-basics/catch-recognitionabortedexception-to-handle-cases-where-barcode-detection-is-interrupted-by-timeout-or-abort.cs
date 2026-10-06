// Title: Handle barcode recognition timeout using RecognitionAbortedException
// Description: Demonstrates generating a QR code, attempting to read it with an extremely short timeout, and catching RecognitionAbortedException when the operation is aborted.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them. Typical scenarios include validating barcode readability under constrained conditions, handling timeouts, and managing exceptions. Developers often need to generate barcodes, read them from images, and gracefully handle recognition failures using key API classes such as BarcodeGenerator, BarCodeReader, and BarCodeResult.
/// Prompt: Catch RecognitionAbortedException to handle cases where barcode detection is interrupted by timeout or abort.
// Tags: barcode, qr, recognition, timeout, exception, aspose.barcode, generation, reading

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR barcode, attempts to read it with a minimal timeout,
/// and demonstrates handling of <see cref="RecognitionAbortedException"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads it with a short timeout,
    /// catches <see cref="RecognitionAbortedException"/>, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a QR barcode image and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file exists before attempting recognition
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image was not created.");
            return;
        }

        // Attempt to read the barcode with a very short timeout to trigger abort
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            // Set timeout to 1 millisecond to force a timeout scenario
            reader.Timeout = 1; // milliseconds

            try
            {
                // Perform barcode recognition
                BarCodeResult[] results = reader.ReadBarCodes();
                Console.WriteLine($"Found {results.Length} barcode(s):");
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
            catch (RecognitionAbortedException ex)
            {
                // Handle the case where recognition was aborted due to timeout
                Console.WriteLine($"Recognition aborted after timeout: {ex.Message}");
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}