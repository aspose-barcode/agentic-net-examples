using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Path to the image containing a MaxiCode barcode.
        string imagePath = "maxicode.png";

        // Verify that the image file exists.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        const int maxAttempts = 3;
        bool decoded = false;

        for (int attempt = 1; attempt <= maxAttempts && !decoded; attempt++)
        {
            try
            {
                // Use AllSupportedTypes as required for image streams.
                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    // Use the highest quality preset to improve recognition chances.
                    reader.QualitySettings = QualitySettings.MaxQuality;

                    // Read all barcodes from the image.
                    var results = reader.ReadBarCodes();

                    foreach (var result in results)
                    {
                        // Attempt to decode the MaxiCode specific codetext.
                        var maxiCodeCodetext = ComplexCodetextReader.TryDecodeMaxiCode(
                            result.Extended.MaxiCode.MaxiCodeMode,
                            result.CodeText);

                        if (maxiCodeCodetext != null)
                        {
                            Console.WriteLine($"Attempt {attempt}: Successfully decoded MaxiCode.");
                            Console.WriteLine($"Mode: {maxiCodeCodetext.GetMode()}");
                            Console.WriteLine($"Constructed Codetext: {maxiCodeCodetext.GetConstructedCodetext()}");
                            decoded = true;
                            break;
                        }
                    }

                    // If no results were found, treat it as a failure for this attempt.
                    if (!decoded)
                    {
                        Console.WriteLine($"Attempt {attempt}: No MaxiCode detected.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception and continue to the next attempt.
                Console.WriteLine($"Attempt {attempt}: Exception occurred - {ex.Message}");
            }
        }

        if (!decoded)
        {
            Console.WriteLine("Failed to decode MaxiCode after three attempts.");
        }
    }
}