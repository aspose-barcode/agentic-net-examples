// Title: Decode MaxiCode barcode with retry mechanism
// Description: Demonstrates generating a MaxiCode barcode, saving it as PNG, and attempting to decode it up to three times, handling failures gracefully.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating a MaxiCode symbol and BarCodeReader for decoding it. Typical scenarios include automated scanning systems where transient read errors require retry logic. Developers often need to combine these APIs to produce barcodes, store them, and reliably extract data in production environments.
// Prompt: Implement a retry mechanism that attempts to decode a MaxiCode barcode up to three times on failure.
// Tags: maxicode, barcode generation, barcode recognition, retry, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates a MaxiCode barcode, saves it to a temporary file,
/// and attempts to decode it with a retry mechanism.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "maxicode.png");

        // Generate a simple MaxiCode barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Test"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 15f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Retry decoding up to three times
        const int maxAttempts = 3;
        bool decoded = false;

        for (int attempt = 1; attempt <= maxAttempts && !decoded; attempt++)
        {
            try
            {
                // Attempt to read the barcode from the saved image
                using (var reader = new BarCodeReader(imagePath, DecodeType.MaxiCode))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Attempt {attempt}: Decoded text: {result.CodeText}");
                        decoded = true;
                        break; // Exit loop after first successful read
                    }
                }

                if (!decoded)
                {
                    Console.WriteLine($"Attempt {attempt}: No barcode detected.");
                }
            }
            catch (Exception ex)
            {
                // Log any exception that occurs during the read attempt
                Console.WriteLine($"Attempt {attempt}: Exception - {ex.Message}");
            }
        }

        if (!decoded)
        {
            Console.WriteLine("Failed to decode MaxiCode after 3 attempts.");
        }

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}