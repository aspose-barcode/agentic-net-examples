// Title: Toggle barcode codetext visibility using Aspose.BarCode
// Description: Demonstrates how to show or hide the main barcode text by adjusting CodeTextParameters.Location, and verifies the effect by comparing image file sizes.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to control barcode text visibility with the BarcodeGenerator and its Parameters.Barcode.CodeTextParameters properties. Typical use cases include creating clean barcode images without human‑readable text or displaying the text below the barcode. Developers often need to toggle visibility for UI design, printing, or compliance requirements.
// Prompt: Write unit tests that verify toggling CodetextParameters.Visible correctly shows and hides the main barcode text.
// Tags: pdf417, codetext visibility, barcode generation, aspnet, aspose.barcode, image size verification

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates toggling the visibility of barcode codetext and performs a simple size‑based verification.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates two PDF417 barcodes – one with visible codetext and one with hidden codetext – then compares their file sizes.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store generated images
        string tempDir = Path.Combine(Path.GetTempPath(), "CodetextTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the visible and hidden codetext images
        string visiblePath = Path.Combine(tempDir, "visible.png");
        string hiddenPath = Path.Combine(tempDir, "hidden.png");

        // Generate barcode with visible codetext (default location: Below)
        using (var generatorVisible = new BarcodeGenerator(EncodeTypes.Pdf417, "SampleText"))
        {
            generatorVisible.Parameters.Barcode.Pdf417.Rows = 12;
            generatorVisible.Parameters.Barcode.XDimension.Pixels = 2;
            generatorVisible.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generatorVisible.Save(visiblePath, BarCodeImageFormat.Png);
        }

        // Generate barcode with hidden codetext (no text displayed)
        using (var generatorHidden = new BarcodeGenerator(EncodeTypes.Pdf417, "SampleText"))
        {
            generatorHidden.Parameters.Barcode.Pdf417.Rows = 12;
            generatorHidden.Parameters.Barcode.XDimension.Pixels = 2;
            generatorHidden.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            generatorHidden.Save(hiddenPath, BarCodeImageFormat.Png);
        }

        // Simple verification: hidden image should be smaller in file size than visible image
        long visibleSize = new FileInfo(visiblePath).Length;
        long hiddenSize = new FileInfo(hiddenPath).Length;

        bool testPassed = hiddenSize < visibleSize;

        Console.WriteLine($"Visible image size: {visibleSize} bytes");
        Console.WriteLine($"Hidden image size:  {hiddenSize} bytes");
        Console.WriteLine(testPassed
            ? "PASSED: Hidden codetext image is smaller, indicating text was hidden."
            : "FAILED: Hidden codetext image is not smaller than visible image.");

        // Cleanup generated files and temporary directory (optional)
        try
        {
            File.Delete(visiblePath);
            File.Delete(hiddenPath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}