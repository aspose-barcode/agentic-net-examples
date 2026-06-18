using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it to a temporary file,
/// reading it back, and cleaning up the temporary file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it, outputs details, and deletes the temporary file.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the barcode image.
        string tempPath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // Generate a simple Code128 barcode with the value "123456" and save it to the temporary file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(tempPath);
        }

        // Initialize a barcode reader to decode all supported barcode types from the saved image.
        using (var reader = new BarCodeReader(tempPath, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes found in the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            // Iterate over each detected barcode and output its type, text, and bounding region.
            foreach (var result in results)
            {
                // result.CodeTypeName provides the barcode type name.
                // result.CodeText contains the decoded text.
                // result.Region.Rectangle gives the bounding rectangle of the barcode.
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}, Region: {result.Region.Rectangle}");
            }
        }

        // Attempt to delete the temporary file to clean up resources.
        try
        {
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during file deletion.
            Console.WriteLine($"Failed to delete temporary file: {ex.Message}");
        }
    }
}