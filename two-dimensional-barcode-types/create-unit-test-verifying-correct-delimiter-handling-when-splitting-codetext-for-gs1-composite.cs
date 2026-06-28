using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation and verification of a GS1 Composite barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a GS1 Composite barcode, reads it back, validates the CodeText,
    /// and cleans up temporary resources.
    /// </summary>
    static void Main()
    {
        // Prepare linear and 2D components for the GS1 Composite barcode.
        string linearPart = "(01)03212345678906";
        string twoDPart = "(21)A1B2C3D4E5F6G7H8";
        // Combine components using the '|' delimiter required for GS1 Composite.
        string combinedCodeText = $"{linearPart}|{twoDPart}";

        // Create a temporary file path for the generated barcode image.
        string tempFile = Path.Combine(Path.GetTempPath(), $"gs1composite_{Guid.NewGuid()}.png");

        // ---------- Barcode Generation ----------
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, combinedCodeText))
        {
            // Specify the linear component type (GS1 Code128) and the 2D component type (CC-A).
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional visual settings: X-dimension and bar height.
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 50f;

            // Save the generated barcode image to the temporary file.
            generator.Save(tempFile);
        }

        // ---------- Barcode Verification ----------
        using (var reader = new BarCodeReader(tempFile, DecodeType.GS1CompositeBar))
        {
            // Enable checksum validation (does not affect delimiter handling but ensures data integrity).
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Read all barcodes found in the image.
            var results = reader.ReadBarCodes();

            // Ensure at least one barcode was detected.
            if (results.Length == 0)
            {
                Console.WriteLine("FAIL: No barcode detected.");
                return;
            }

            // Compare the read CodeText with the original combined text.
            string readCodeText = results[0].CodeText;
            if (readCodeText == combinedCodeText)
            {
                Console.WriteLine("PASS: CodeText correctly preserved with '|' delimiter.");
            }
            else
            {
                Console.WriteLine($"FAIL: Expected '{combinedCodeText}' but got '{readCodeText}'.");
            }
        }

        // ---------- Cleanup ----------
        try
        {
            // Delete the temporary file if it exists.
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
        catch
        {
            // Ignored – cleanup failure should not affect test result.
        }
    }
}