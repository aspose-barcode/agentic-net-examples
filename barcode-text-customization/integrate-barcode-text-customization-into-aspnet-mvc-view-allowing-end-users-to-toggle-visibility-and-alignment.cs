using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation using Aspose.BarCode in a console application.
/// The generated barcode image is saved to a temporary file and its Base64 representation
/// is printed to the console (suitable for embedding in HTML).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Simulates user settings, generates a Code128 barcode, configures human‑readable text,
    /// saves the image, and outputs the image as a Base64 string.
    /// </summary>
    static void Main()
    {
        // Simulated user settings -------------------------------------------------
        bool showHumanReadableText = true;                     // Toggle visibility of the human‑readable text
        TextAlignment textAlignment = TextAlignment.Center;   // Choose text alignment (Left, Center, Right)

        // Create a Code128 barcode generator --------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to encode
            generator.CodeText = "1234567890";

            // Configure human‑readable text visibility ---------------------------------
            if (showHumanReadableText)
            {
                // Show text below the barcode (default location)
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            }
            else
            {
                // Hide the text completely
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            }

            // Set the alignment of the text (if it is visible) -------------------------
            generator.Parameters.Barcode.CodeTextParameters.Alignment = textAlignment;

            // Optional: customize font size and family ---------------------------------
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;

            // Save the barcode image to a temporary file --------------------------------
            string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");
            generator.Save(outputPath);

            // Read the image bytes and output as Base64 (suitable for embedding in HTML)
            byte[] imageBytes = File.ReadAllBytes(outputPath);
            string base64 = Convert.ToBase64String(imageBytes);
            Console.WriteLine("Barcode image (Base64):");
            Console.WriteLine(base64);
        }
    }
}