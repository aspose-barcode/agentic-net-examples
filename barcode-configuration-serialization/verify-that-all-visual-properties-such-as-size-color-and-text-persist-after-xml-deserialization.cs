using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a barcode, customizing its visual appearance,
/// exporting its configuration to XML, importing it back, and verifying that
/// all visual properties are preserved.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator with Code128 symbology and initial text.
        using (var originalGenerator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // -------------------------------------------------
            // Configure visual appearance of the barcode.
            // -------------------------------------------------

            // Set barcode and background colors.
            originalGenerator.Parameters.Barcode.BarColor = Color.Blue;
            originalGenerator.Parameters.BackColor = Color.Yellow;

            // Set image dimensions and X-dimension (module width).
            originalGenerator.Parameters.ImageWidth.Point = 300f;
            originalGenerator.Parameters.ImageHeight.Point = 150f;
            originalGenerator.Parameters.Barcode.XDimension.Point = 2f;

            // Apply a rotation to the entire image.
            originalGenerator.Parameters.RotationAngle = 45f;

            // Configure a caption displayed above the barcode.
            originalGenerator.Parameters.CaptionAbove.Visible = true;
            originalGenerator.Parameters.CaptionAbove.Text = "Above Caption";
            originalGenerator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            originalGenerator.Parameters.CaptionAbove.Font.Size.Point = 12f;
            originalGenerator.Parameters.CaptionAbove.TextColor = Color.Red;

            // -------------------------------------------------
            // Export the generator's configuration to an XML stream.
            // -------------------------------------------------
            using (var xmlStream = new MemoryStream())
            {
                originalGenerator.ExportToXml(xmlStream);
                xmlStream.Position = 0; // Reset stream position for reading.

                // -------------------------------------------------
                // Import a new generator instance from the XML data.
                // -------------------------------------------------
                using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlStream))
                {
                    // Verify that all visual properties persisted after deserialization.
                    bool allMatch = CompareGenerators(originalGenerator, importedGenerator);
                    Console.WriteLine("All visual properties persisted after XML deserialization: " + allMatch);
                }
            }
        }
    }

    /// <summary>
    /// Compares two <see cref="BarcodeGenerator"/> instances to ensure that
    /// their visual properties and code text are identical.
    /// </summary>
    /// <param name="original">The original generator.</param>
    /// <param name="imported">The generator imported from XML.</param>
    /// <returns>True if all compared properties match; otherwise, false.</returns>
    static bool CompareGenerators(BarcodeGenerator original, BarcodeGenerator imported)
    {
        // Compare barcode and background colors.
        if (!original.Parameters.Barcode.BarColor.Equals(imported.Parameters.Barcode.BarColor)) return false;
        if (!original.Parameters.BackColor.Equals(imported.Parameters.BackColor)) return false;

        // Compare image dimensions and X-dimension using a tolerance for floating‑point values.
        const float tolerance = 0.001f;
        if (Math.Abs(original.Parameters.ImageWidth.Point - imported.Parameters.ImageWidth.Point) > tolerance) return false;
        if (Math.Abs(original.Parameters.ImageHeight.Point - imported.Parameters.ImageHeight.Point) > tolerance) return false;
        if (Math.Abs(original.Parameters.Barcode.XDimension.Point - imported.Parameters.Barcode.XDimension.Point) > tolerance) return false;

        // Compare rotation angle.
        if (Math.Abs(original.Parameters.RotationAngle - imported.Parameters.RotationAngle) > tolerance) return false;

        // Compare caption properties.
        if (original.Parameters.CaptionAbove.Visible != imported.Parameters.CaptionAbove.Visible) return false;
        if (original.Parameters.CaptionAbove.Text != imported.Parameters.CaptionAbove.Text) return false;
        if (original.Parameters.CaptionAbove.Font.FamilyName != imported.Parameters.CaptionAbove.Font.FamilyName) return false;
        if (Math.Abs(original.Parameters.CaptionAbove.Font.Size.Point - imported.Parameters.CaptionAbove.Font.Size.Point) > tolerance) return false;
        if (!original.Parameters.CaptionAbove.TextColor.Equals(imported.Parameters.CaptionAbove.TextColor)) return false;

        // Compare the encoded text.
        if (original.CodeText != imported.CodeText) return false;

        // All checks passed.
        return true;
    }
}