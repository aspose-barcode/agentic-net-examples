using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode, reading it back, and cleaning up the temporary file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, reads it, displays the results, and deletes the temporary file.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated barcode image.
        string tempImagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // Generate a simple Code128 barcode and save it to the temporary file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(tempImagePath, BarCodeImageFormat.Png);
        }

        // Configure the barcode reader to utilize all CPU cores for faster recognition.
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        // Read the barcode from the generated image file.
        using (var reader = new BarCodeReader(tempImagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through all detected barcodes and output their type and text.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Detected Text: {result.CodeText}");
            }
        }

        // Clean up the temporary image file if it still exists.
        if (File.Exists(tempImagePath))
        {
            try
            {
                File.Delete(tempImagePath);
            }
            catch
            {
                // If deletion fails, ignore – the OS will eventually remove the file.
            }
        }
    }
}