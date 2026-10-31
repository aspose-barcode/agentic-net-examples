// Title: Decode corrupted MaxiCode barcode with error handling
// Description: Demonstrates generating a MaxiCode barcode, intentionally corrupting the image to simulate an unreadable barcode, and decoding it while handling potential errors.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and common error handling patterns. Developers working with barcode imaging often need to generate barcodes, process scanned images, and gracefully handle unreadable or damaged inputs. This snippet serves as a reference for such scenarios, highlighting key API classes like BarcodeGenerator, BarCodeReader, and DecodeType.
// Prompt: Implement error handling that catches BarcodeException when decoding an unreadable MaxiCode image.
// Tags: maxicode, barcode, decoding, error handling, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode barcode, corrupting it, and attempting to decode with error handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates, corrupts, and decodes a MaxiCode barcode while handling possible errors.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "maxicode.png");

        // Generate a valid MaxiCode barcode and save it as a PNG image
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Corrupt the image file to simulate an unreadable barcode
        if (File.Exists(imagePath))
        {
            byte[] data = File.ReadAllBytes(imagePath);
            // Overwrite the middle portion of the file with zeros
            int start = data.Length / 4;
            int length = data.Length / 2;
            for (int i = start; i < start + length && i < data.Length; i++)
            {
                data[i] = 0;
            }
            File.WriteAllBytes(imagePath, data);
        }

        // Verify that the image file exists before attempting decoding
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Image file not found.");
            return;
        }

        // Specify the decode type for MaxiCode
        BaseDecodeType decodeType = DecodeType.MaxiCode;

        try
        {
            // Initialize the barcode reader for the corrupted image
            using (var reader = new BarCodeReader(imagePath, decodeType))
            {
                // Attempt to read barcodes from the image
                BarCodeResult[] results = reader.ReadBarCodes();

                // Check if any barcodes were detected
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected.");
                }
                else
                {
                    // Output each detected barcode's type and text
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any exception that occurs during decoding (e.g., unreadable image)
            Console.WriteLine($"Error while decoding MaxiCode: {ex.Message}");
        }
        finally
        {
            // Clean up temporary files and directories
            try
            {
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
            catch
            {
                // Suppress any cleanup errors to avoid masking the original exception
            }
        }
    }
}