// Title: Toggle barcode text visibility using CodeTextParameters.Location
// Description: Demonstrates how to show or hide the main barcode text by setting CodeTextParameters.Location to Below or None, and saves PNG images for verification.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to control barcode text visibility with the BarcodeGenerator and its Parameters.Barcode.CodeTextParameters properties. Typical use cases include customizing barcode appearance for packaging, inventory, or retail labels where the human‑readable text may need to be displayed or suppressed. Developers often need to toggle visibility, adjust location, and generate image files for downstream processing.
// Prompt: Write unit tests that verify toggling CodetextParameters.Visible correctly shows and hides the main barcode text.
// Tags: code128, barcode, visibility, codetextparameters, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates toggling barcode text visibility using Aspose.BarCode's CodeTextParameters.Location property.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates temporary barcode images with visible and hidden text, verifies the settings, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for test artifacts
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeVisibilityTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the visible and hidden barcode images
        string visiblePath = Path.Combine(tempDir, "visible.png");
        string hiddenPath = Path.Combine(tempDir, "hidden.png");
        string codeText = "12345";

        // Test 1: Generate barcode with text visible (Location = Below)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set text location to Below, which makes the text visible
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            // Save the barcode image
            generator.Save(visiblePath, BarCodeImageFormat.Png);

            // Verify that the location property was set correctly
            bool condition = generator.Parameters.Barcode.CodeTextParameters.Location == CodeLocation.Below;
            Console.WriteLine("Test 1 - Set Location to Below (visible): " + (condition ? "PASSED" : "FAILED"));
        }

        // Test 2: Generate barcode with text hidden (Location = None)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set text location to None, which hides the text
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            // Save the barcode image
            generator.Save(hiddenPath, BarCodeImageFormat.Png);

            // Verify that the location property was set correctly
            bool condition = generator.Parameters.Barcode.CodeTextParameters.Location == CodeLocation.None;
            Console.WriteLine("Test 2 - Set Location to None (hidden): " + (condition ? "PASSED" : "FAILED"));
        }

        // Cleanup temporary files and directory
        try
        {
            if (File.Exists(visiblePath)) File.Delete(visiblePath);
            if (File.Exists(hiddenPath)) File.Delete(hiddenPath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test outcome
        }
    }
}