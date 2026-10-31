// Title: Validate UseMinimalXDimension with AllowIncorrectBarcodes
// Description: Demonstrates generating a Code128 barcode and reading it with both UseMinimalXDimension and AllowIncorrectBarcodes enabled to ensure no conflicts occur.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with QualitySettings for advanced decoding options. Developers often need to fine‑tune X‑dimension handling and tolerate imperfect barcodes; this snippet illustrates how to configure those settings without errors, serving as a reference for similar use cases.
// Prompt: Validate that setting both UseMinimalXDimension and AllowIncorrectBarcodes together does not cause conflicts.
// Tags: barcode, code128, generation, recognition, useminimalxdimension, allowincorrectbarcodes, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates that enabling both UseMinimalXDimension and AllowIncorrectBarcodes
/// on the <see cref="BarCodeReader"/> does not cause conflicts.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it with specific quality settings,
    /// and verifies that no exceptions are thrown.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        try
        {
            // -------------------------------------------------
            // Generate a simple Code128 barcode and save as PNG
            // -------------------------------------------------
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
            {
                generator.Save(barcodePath, BarCodeImageFormat.Png);
            }

            // -------------------------------------------------
            // Read the barcode with both UseMinimalXDimension and AllowIncorrectBarcodes enabled
            // -------------------------------------------------
            using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                // Configure quality settings
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = 1f; // minimal X dimension in points
                reader.QualitySettings.AllowIncorrectBarcodes = true; // tolerate minor errors

                // Perform the read operation
                BarCodeResult[] results = reader.ReadBarCodes();

                // Output the number of barcodes detected
                Console.WriteLine($"Barcodes read: {results.Length}");
                foreach (var result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }

            // Indicate successful completion without conflicts
            Console.WriteLine("Completed without conflicts.");
        }
        catch (Exception ex)
        {
            // Report any unexpected exceptions
            Console.WriteLine($"Exception occurred: {ex.Message}");
        }
        finally
        {
            // -------------------------------------------------
            // Clean up temporary files and directories
            // -------------------------------------------------
            if (File.Exists(barcodePath))
            {
                try { File.Delete(barcodePath); } catch { }
            }

            try { Directory.Delete(tempFolder, true); } catch { }
        }
    }
}