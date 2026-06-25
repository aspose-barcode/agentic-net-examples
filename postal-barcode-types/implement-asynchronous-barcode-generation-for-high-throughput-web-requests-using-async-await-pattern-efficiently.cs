using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates asynchronous generation of Code128 bar‑code images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Asynchronously generates a barcode image and saves it to the specified path.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="outputPath">The full file path where the PNG image will be saved.</param>
    /// <returns>The same <paramref name="outputPath"/> after the image has been saved.</returns>
    private static async Task<string> GenerateBarcodeAsync(string codeText, string outputPath)
    {
        // Barcode generation is CPU‑bound; offload it to a thread‑pool thread.
        return await Task.Run(() =>
        {
            // Ensure the target directory exists before saving.
            string directory = Path.GetDirectoryName(outputPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create a generator for Code128 and configure its parameters.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Enable checksum for Code128 (required for data integrity).
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                // Set image resolution (optional, 300 DPI provides good quality).
                generator.Parameters.Resolution = 300f;

                // Save the generated barcode as a PNG file.
                generator.Save(outputPath);
            }

            // Return the path so callers can know where the file was written.
            return outputPath;
        });
    }

    /// <summary>
    /// Application entry point. Generates a set of barcode images asynchronously and reports their locations.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Sample barcode texts; in a real scenario these could be supplied by a user or service.
        string[] sampleTexts = new[]
        {
            "ABC123",
            "DEF456",
            "GHI789",
            "JKL012",
            "MNO345"
        };

        // Prepare an array to hold the asynchronous generation tasks.
        Task<string>[] generationTasks = new Task<string>[sampleTexts.Length];

        // Create a generation task for each sample text.
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            // Build a unique file name for each barcode.
            string fileName = $"barcode_{i + 1}.png";

            // Combine the directory and file name to get the full output path.
            string outputPath = Path.Combine("Barcodes", fileName);

            // Start the asynchronous generation and store the task.
            generationTasks[i] = GenerateBarcodeAsync(sampleTexts[i], outputPath);
        }

        // Wait for all barcode generation tasks to complete.
        string[] generatedFiles = await Task.WhenAll(generationTasks);

        // Report the results to the console.
        Console.WriteLine("Barcode generation completed. Files created:");
        foreach (var file in generatedFiles)
        {
            Console.WriteLine(Path.GetFullPath(file));
        }
    }
}