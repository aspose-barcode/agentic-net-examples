// Title: Asynchronous QR Code Generation with Aspose.BarCode
// Description: Demonstrates generating a barcode asynchronously using Aspose.BarCode and saving it as a PNG file. Useful for high‑throughput web services that need non‑blocking barcode creation.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and BaseEncodeType classes to create barcodes on demand. Typical use cases include web APIs, microservices, and batch processing where non‑blocking I/O improves scalability. Developers often need to resolve symbologies dynamically, configure visual parameters, and write the resulting image to storage efficiently.
// Prompt: Implement asynchronous barcode generation for high‑throughput web requests using async/await pattern efficiently.
// Tags: qr code,barcode generation,async,await,aspose.barcode,output png

using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point for generating barcodes asynchronously using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Asynchronously processes command‑line arguments, generates a barcode, and writes the result path to the console.
    /// </summary>
    /// <param name="args">Optional arguments: symbology, code text, and output folder.</param>
    static async Task Main(string[] args)
    {
        // Parse command‑line arguments with sensible defaults
        string symbology = args.Length > 0 ? args[0] : "QR";
        string codeText = args.Length > 1 ? args[1] : "https://example.com";
        string outputFolder = args.Length > 2
            ? args[2]
            : Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));

        try
        {
            // Generate the barcode asynchronously and obtain the file path
            string resultPath = await GenerateBarcodeAsync(symbology, codeText, outputFolder);
            Console.WriteLine($"Barcode saved to: {resultPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occurred during generation
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a barcode image asynchronously based on the specified symbology and text.
    /// </summary>
    /// <param name="symbologyName">The name of the barcode symbology (e.g., "QR").</param>
    /// <param name="codeText">The data to encode in the barcode.</param>
    /// <param name="outputFolder">The folder where the generated image will be saved.</param>
    /// <returns>The full file path of the saved barcode image.</returns>
    private static async Task<string> GenerateBarcodeAsync(string symbologyName, string codeText, string outputFolder)
    {
        // Validate required parameters
        if (string.IsNullOrWhiteSpace(symbologyName))
            throw new ArgumentException("Symbology name must be provided.", nameof(symbologyName));
        if (string.IsNullOrWhiteSpace(codeText))
            throw new ArgumentException("Code text must be provided.", nameof(codeText));

        // Resolve the symbology name to a BaseEncodeType using reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
            throw new ArgumentException($"Unknown symbology: {symbologyName}", nameof(symbologyName));

        BaseEncodeType encodeType = field.GetValue(null) as BaseEncodeType;
        if (encodeType == null)
            throw new ArgumentException($"Failed to obtain encode type for symbology: {symbologyName}", nameof(symbologyName));

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        // Build a unique file name for the barcode image
        string fileName = $"{symbologyName}_{Guid.NewGuid().ToString("N")}.png";
        string filePath = Path.Combine(outputFolder, fileName);

        // Create and configure the barcode generator
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set visual parameters (example settings)
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Perform the potentially blocking save operation on a background thread
            await Task.Run(() => generator.Save(filePath, BarCodeImageFormat.Png));
        }

        // Return the path to the saved image
        return filePath;
    }
}