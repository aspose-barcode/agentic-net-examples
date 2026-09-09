// Title: Handle barcode recognition abort using RecognitionAbortedException
// Description: Demonstrates generating a QR code, attempting to read it with an extremely short timeout, and catching RecognitionAbortedException when the operation is aborted.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a barcode image and BarCodeReader to decode it. Typical scenarios include scanning barcodes with strict time constraints, where developers need to handle abort conditions gracefully using RecognitionAbortedException. The example highlights key API classes such as BarcodeGenerator, BarCodeReader, EncodeTypes, DecodeType, and BarCodeResult.
// Prompt: Catch RecognitionAbortedException to handle cases where barcode detection is interrupted by timeout or abort.
// Tags: qr code, barcode generation, barcode recognition, timeout, exception handling, aspose.barcode, recognitionabortedexception

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, recognition with a forced timeout, and handling of RecognitionAbortedException.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates a QR code, attempts to read it with a minimal timeout,
    /// catches any RecognitionAbortedException, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample_qr.png");

        // Generate a QR code image and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Initialize a barcode reader with a very short timeout to force an abort
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            reader.Timeout = 1; // Timeout in milliseconds

            try
            {
                // Attempt to read barcodes from the image
                BarCodeResult[] results = reader.ReadBarCodes();
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
            catch (RecognitionAbortedException ex)
            {
                // Handle the case where recognition was aborted due to timeout
                Console.WriteLine($"Recognition aborted: {ex.Message}");
            }
        }

        // Clean up temporary files and directory
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failures should not affect program exit
        }
    }
}