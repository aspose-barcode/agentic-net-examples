using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating and decoding barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcode images,
    /// decodes them asynchronously, displays the results, and cleans up temporary files.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder for sample barcode images
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodesSample");
        if (!Directory.Exists(tempFolder))
        {
            Directory.CreateDirectory(tempFolder);
        }

        // --------------------------------------------------------------------
        // Sample code texts for generating barcodes
        // --------------------------------------------------------------------
        string[] sampleTexts = new string[]
        {
            "ABC123",
            "DEF456",
            "GHI789",
            "JKL012",
            "MNO345"
        };

        // --------------------------------------------------------------------
        // Generate barcode images and save them to the temporary folder
        // --------------------------------------------------------------------
        foreach (string text in sampleTexts)
        {
            string imagePath = Path.Combine(tempFolder, $"{text}.png");
            BaseEncodeType encodeType = EncodeTypes.Code128;

            // Create a barcode generator for the current text and save the image
            using (var generator = new BarcodeGenerator(encodeType, text))
            {
                generator.Save(imagePath);
            }
        }

        // --------------------------------------------------------------------
        // Asynchronously decode all generated images using TPL
        // --------------------------------------------------------------------
        Task<string>[] decodeTasks = new Task<string>[sampleTexts.Length];
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string imagePath = Path.Combine(tempFolder, $"{sampleTexts[i]}.png");

            // Start a task that reads the barcode from the image file
            decodeTasks[i] = Task.Run(() =>
            {
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"File not found: {imagePath}");
                    return null;
                }

                // Use BarCodeReader to read all supported barcode types
                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Return the first detected barcode text
                        return result.CodeText;
                    }
                }

                // No barcode detected
                return null;
            });
        }

        // --------------------------------------------------------------------
        // Await all decoding tasks and collect the results
        // --------------------------------------------------------------------
        string[] decodedTexts = await Task.WhenAll(decodeTasks);

        // --------------------------------------------------------------------
        // Output decoding results to the console
        // --------------------------------------------------------------------
        Console.WriteLine("Decoded barcode texts:");
        foreach (var decoded in decodedTexts)
        {
            if (!string.IsNullOrEmpty(decoded))
            {
                Console.WriteLine(decoded);
            }
            else
            {
                Console.WriteLine("(no barcode detected)");
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and folder
        // --------------------------------------------------------------------
        try
        {
            foreach (string file in Directory.GetFiles(tempFolder))
            {
                File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}