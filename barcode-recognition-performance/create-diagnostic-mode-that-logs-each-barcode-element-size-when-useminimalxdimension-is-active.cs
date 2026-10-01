// Title: Generate and Read Code128 Barcode with Minimal X Dimension Logging
// Description: Demonstrates creating a Code128 barcode image, saving it as PNG, then reading it with Aspose.BarCode while enabling UseMinimalXDimension to log each barcode's element size.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing the BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. It highlights the XDimensionMode.UseMinimalXDimension setting used for diagnostic quality checks, a common need for developers optimizing barcode dimensions and ensuring readability across scanners.
// Prompt: Create a diagnostic mode that logs each barcode element size when UseMinimalXDimension is active.
// Tags: code128, barcode generation, barcode recognition, minimalxdimension, diagnostics, aspose.barcode, png, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, saving, and diagnostic reading with minimal X dimension logging.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it with minimal X dimension mode, and logs element sizes.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define barcode parameters
        string codeText = "Sample12345";
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a barcode image using the default auto-sizing
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file exists before attempting to read
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Read the barcode with UseMinimalXDimension enabled for diagnostic purposes
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Enable minimal X dimension mode
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

            // Perform the read operation
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Log each detected barcode element size
            foreach (var result in results)
            {
                // The region rectangle provides the bounding box of the detected barcode
                var bounds = result.Region.Rectangle;
                Console.WriteLine($"Barcode detected: Type={result.CodeTypeName}, Text={result.CodeText}");
                Console.WriteLine($"Element size - Width: {bounds.Width} px, Height: {bounds.Height} px");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program exit
        }
    }
}