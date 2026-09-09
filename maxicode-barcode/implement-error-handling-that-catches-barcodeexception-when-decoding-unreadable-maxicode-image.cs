// Title: Decode MaxiCode with error handling for unreadable images
// Description: Demonstrates generating a MaxiCode barcode, then attempts to decode a corrupted image while handling potential BarcodeException.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Developers often need to generate barcodes for packaging or shipping labels and later validate or read them from scanned images, handling errors when the image is unreadable or corrupted.
// Prompt: Implement error handling that catches BarcodeException when decoding an unreadable MaxiCode image.
// Tags: maxicode, barcode, decoding, error-handling, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates a MaxiCode barcode, attempts to read a corrupted image,
/// and demonstrates proper exception handling for unreadable barcode data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary folder for the sample image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "maxicode.png");

        // --------------------------------------------------------------------
        // Generate a valid MaxiCode barcode image and save it to the temporary folder
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Prepare a corrupted image stream (random bytes) to simulate an unreadable barcode
        // --------------------------------------------------------------------
        byte[] corruptedData = new byte[10];
        new Random().NextBytes(corruptedData);
        using (var corruptedStream = new MemoryStream(corruptedData))
        {
            try
            {
                // Specify the decode type for MaxiCode
                BaseDecodeType decodeType = DecodeType.MaxiCode;

                // Attempt to read barcodes from the corrupted stream
                using (var reader = new BarCodeReader(corruptedStream, decodeType))
                {
                    var results = reader.ReadBarCodes();

                    // Check if any barcodes were detected
                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcodes detected.");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
                        }
                    }
                }
            }
            // Catch specific barcode processing errors
            catch (BarCodeException ex)
            {
                Console.WriteLine($"BarcodeException caught: {ex.Message}");
            }
            // Catch any other unexpected errors
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected exception: {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and directories
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored: cleanup failures should not interrupt program flow
        }
    }
}