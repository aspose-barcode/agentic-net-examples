// Title: Validate UseMinimalXDimension with AllowIncorrectBarcodes
// Description: Demonstrates generating a Code128 barcode and reading it with both UseMinimalXDimension and AllowIncorrectBarcodes enabled to ensure no conflicts.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with QualitySettings for fine‑tuning decoding. Developers often need to adjust X‑dimension handling and tolerate imperfect barcodes; this snippet illustrates how to configure those options together.
// Prompt: Validate that setting both UseMinimalXDimension and AllowIncorrectBarcodes together does not cause conflicts.
// Tags: barcode symbology, generation, recognition, code128, useminimalxdimension, allowincorrectbarcodes, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that validates that enabling both UseMinimalXDimension and AllowIncorrectBarcodes
/// does not cause conflicts during barcode recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, reads it with specific quality settings,
    /// and outputs the decoding results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "code128.png");

        try
        {
            // ------------------------------------------------------------
            // Generate a simple Code128 barcode and save it as PNG
            // ------------------------------------------------------------
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
            {
                generator.Save(barcodePath, BarCodeImageFormat.Png);
            }

            // ------------------------------------------------------------
            // Read the barcode with both UseMinimalXDimension and AllowIncorrectBarcodes enabled
            // ------------------------------------------------------------
            using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                // Configure quality settings
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = 1f; // minimal X dimension in points
                reader.QualitySettings.AllowIncorrectBarcodes = true; // tolerate minor errors

                // Perform the read operation
                BarCodeResult[] results = reader.ReadBarCodes();

                // Output the number of barcodes found and their details
                Console.WriteLine($"Barcodes read: {results.Length}");
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}:{result.CodeText}");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any exceptions that occur during generation or reading
            Console.WriteLine("An exception occurred during processing:");
            Console.WriteLine(ex.Message);
        }
        finally
        {
            // Clean up temporary files and directories
            try
            {
                if (File.Exists(barcodePath))
                {
                    File.Delete(barcodePath);
                }
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                }
            }
            catch
            {
                // Suppress any cleanup exceptions to avoid masking earlier errors
            }
        }
    }
}