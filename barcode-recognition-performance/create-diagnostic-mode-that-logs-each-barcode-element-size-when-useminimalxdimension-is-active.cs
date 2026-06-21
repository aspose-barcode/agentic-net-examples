using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it to a temporary file,
/// reading it back using Aspose.BarCode, and outputting detected barcode information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, reads it, displays results, and cleans up the temporary file.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated barcode image.
        string tempImagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // Generate a Code128 barcode with the specified text and save it to the temporary file.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(tempImagePath);
        }

        // Verify that the barcode image was successfully created.
        if (!File.Exists(tempImagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize a barcode reader to detect all supported barcode types in the image.
        using (BarCodeReader reader = new BarCodeReader(tempImagePath, DecodeType.AllSupportedTypes))
        {
            // Configure the reader to use the minimal X-dimension for better detection.
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

            // Read all barcodes present in the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were found, inform the user and exit.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected.");
                return;
            }

            // Iterate through each detected barcode and display its type and text.
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine(new string('-', 40));
            }
        }

        // Attempt to delete the temporary barcode image; ignore any errors during cleanup.
        try
        {
            File.Delete(tempImagePath);
        }
        catch
        {
            // Cleanup errors are intentionally ignored.
        }
    }
}