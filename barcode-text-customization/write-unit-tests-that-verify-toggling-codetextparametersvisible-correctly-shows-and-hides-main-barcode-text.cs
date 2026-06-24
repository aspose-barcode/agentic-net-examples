using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcodes with visible and hidden human‑readable text,
/// compares the resulting image heights, and cleans up temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates two barcode images (one with text, one without),
    /// compares their heights, outputs the test result, and deletes the temporary files.
    /// </summary>
    static void Main()
    {
        // Prepare temporary file paths for the generated barcode images.
        string tempDir = Path.GetTempPath();
        string visiblePath = Path.Combine(tempDir, "barcode_visible.png");
        string hiddenPath = Path.Combine(tempDir, "barcode_hidden.png");

        // ------------------------------------------------------------
        // Generate barcode with visible human‑readable text (default location)
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Ensure the text is placed below the bars (default behavior).
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            // Save the image to the temporary path.
            generator.Save(visiblePath);
        }

        // ------------------------------------------------------------
        // Generate barcode with hidden human‑readable text
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Hide the text by setting its location to None.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            // Save the image to the temporary path.
            generator.Save(hiddenPath);
        }

        // ------------------------------------------------------------
        // Load the generated images to compare their heights
        // ------------------------------------------------------------
        int visibleHeight;
        int hiddenHeight;

        using (var imgVisible = Image.FromFile(visiblePath))
        {
            visibleHeight = imgVisible.Height;
        }

        using (var imgHidden = Image.FromFile(hiddenPath))
        {
            hiddenHeight = imgHidden.Height;
        }

        // ------------------------------------------------------------
        // Evaluate the result: image without text should be shorter
        // ------------------------------------------------------------
        if (hiddenHeight < visibleHeight)
        {
            Console.WriteLine(
                "Test passed: hidden text image height ({0}) is less than visible text image height ({1}).",
                hiddenHeight, visibleHeight);
        }
        else
        {
            Console.WriteLine(
                "Test failed: hidden text image height ({0}) is not less than visible text image height ({1}).",
                hiddenHeight, visibleHeight);
        }

        // ------------------------------------------------------------
        // Clean up temporary files
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(visiblePath))
                File.Delete(visiblePath);
            if (File.Exists(hiddenPath))
                File.Delete(hiddenPath);
        }
        catch
        {
            // Ignored – cleanup failure should not affect test result.
        }
    }
}