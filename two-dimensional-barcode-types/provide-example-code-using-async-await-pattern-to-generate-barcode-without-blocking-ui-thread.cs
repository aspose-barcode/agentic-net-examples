using System;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates asynchronous barcode generation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image asynchronously on a background thread.
    /// </summary>
    /// <param name="type">The barcode symbology to use.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="outputPath">The file path where the image will be saved.</param>
    /// <returns>A task that resolves to the output file path.</returns>
    static async Task<string> GenerateBarcodeAsync(BaseEncodeType type, string codeText, string outputPath)
    {
        // Offload the generation to a thread‑pool thread to avoid blocking the caller.
        return await Task.Run(() =>
        {
            // Create a BarcodeGenerator with the specified type and text.
            using (var generator = new BarcodeGenerator(type, codeText))
            {
                // Optional: set image resolution (dots per inch).
                generator.Parameters.Resolution = 300f;

                // Save the generated barcode image to the specified path.
                generator.Save(outputPath);
            }

            // Return the path of the saved file to the caller.
            return outputPath;
        });
    }

    /// <summary>
    /// Asynchronous entry point of the console application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Define the barcode symbology and the text to encode.
        BaseEncodeType barcodeType = EncodeTypes.Code128;
        string sampleText = "123ABC";

        // Define the output file name for the generated image.
        string outputFile = "barcode.png";

        // Generate the barcode image without blocking the main thread.
        string savedPath = await GenerateBarcodeAsync(barcodeType, sampleText, outputFile);

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode image saved to: {savedPath}");
    }
}