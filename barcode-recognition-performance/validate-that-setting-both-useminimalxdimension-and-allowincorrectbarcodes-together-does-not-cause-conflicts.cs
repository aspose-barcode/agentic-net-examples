// Title: Validate UseMinimalXDimension with AllowIncorrectBarcodes
// Description: Demonstrates generating a Code128 barcode, then reading it with both UseMinimalXDimension and AllowIncorrectBarcodes enabled to ensure no conflicts arise.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, focusing on X-dimension quality settings. Developers often need to fine‑tune X‑dimension handling and tolerate imperfect barcodes; this snippet illustrates how to configure XDimensionMode.UseMinimalXDimension together with AllowIncorrectBarcodes.
// Prompt: Validate that setting both UseMinimalXDimension and AllowIncorrectBarcodes together does not cause conflicts.
// Tags: barcode symbology, generation, recognition, code128, minimalxdimension, allowincorrectbarcodes, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that validates the combined use of UseMinimalXDimension and AllowIncorrectBarcodes
/// when reading a Code128 barcode generated with Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, reads it with specific quality settings,
    /// and confirms that no conflicts occur between UseMinimalXDimension and AllowIncorrectBarcodes.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "code128.png");

        try
        {
            // -------------------------------------------------
            // Generate a simple Code128 barcode and save as PNG
            // -------------------------------------------------
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "AsposeTest"))
            {
                // Set X-dimension to 2 pixels for clearer rendering
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Save(barcodePath, BarCodeImageFormat.Png);
            }

            // -------------------------------------------------
            // Read the barcode with both quality settings enabled
            // -------------------------------------------------
            using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                // Instruct the reader to use the minimal possible X-dimension
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = 1f;

                // Allow the reader to accept barcodes that may not fully conform to specifications
                reader.QualitySettings.AllowIncorrectBarcodes = true;

                // Perform the read operation
                BarCodeResult[] results = reader.ReadBarCodes();
                Console.WriteLine($"Barcodes read: {results.Length}");

                // Output each decoded result
                foreach (var result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}:{result.CodeText}");
                }
            }

            Console.WriteLine("Validation completed without conflicts.");
        }
        catch (Exception ex)
        {
            // Report any unexpected errors during generation or reading
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            // -------------------------------------------------
            // Clean up temporary files and directories
            // -------------------------------------------------
            try
            {
                if (File.Exists(barcodePath))
                    File.Delete(barcodePath);
                if (Directory.Exists(tempFolder))
                    Directory.Delete(tempFolder, true);
            }
            catch
            {
                // Suppress any cleanup exceptions to avoid masking earlier errors
            }
        }
    }
}