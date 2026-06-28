using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation and recognition of a GS1 Code128 barcode,
/// showing the effect of the StripFNC setting on FNC1 characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it twice with different StripFNC settings,
    /// and outputs verification results to the console.
    /// </summary>
    static void Main()
    {
        // Sample GS1 Code128 text containing Application Identifiers.
        const string codeText = "(02)04006664241007(37)1(400)7019590754";

        // Create a memory stream to hold the generated barcode image.
        using (var ms = new MemoryStream())
        {
            // Generate the barcode image using Aspose.BarCode.
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
            {
                // Save the barcode as PNG (any format works for recognition).
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning for reading.
            ms.Position = 0;

            // ---------- First read: StripFNC = false ----------
            // FNC characters should be retained in the decoded text.
            string codeWithoutStrip;
            using (var reader = new BarCodeReader(ms, DecodeType.GS1Code128))
            {
                // Configure reader to keep FNC characters.
                reader.BarcodeSettings.StripFNC = false;

                // Read the first barcode found (there should be only one).
                var result = reader.ReadBarCodes().FirstOrDefault();

                // Extract the decoded text, or use empty string if none.
                codeWithoutStrip = result?.CodeText ?? string.Empty;
            }

            // Reset stream position again for the second read.
            ms.Position = 0;

            // ---------- Second read: StripFNC = true ----------
            // FNC characters should be removed from the decoded text.
            string codeWithStrip;
            using (var reader = new BarCodeReader(ms, DecodeType.GS1Code128))
            {
                // Configure reader to strip FNC characters.
                reader.BarcodeSettings.StripFNC = true;

                // Read the first barcode found.
                var result = reader.ReadBarCodes().FirstOrDefault();

                // Extract the decoded text, or use empty string if none.
                codeWithStrip = result?.CodeText ?? string.Empty;
            }

            // The FNC1 character is represented by char value 241 in Aspose.BarCode.
            char fncChar = (char)241;

            // Determine whether the unstripped result contains the FNC1 character.
            bool containsFnc = codeWithoutStrip.Contains(fncChar.ToString());

            // Output verification information to the console.
            Console.WriteLine("CodeText with StripFNC = false: " + codeWithoutStrip);
            Console.WriteLine("Contains FNC1 character: " + containsFnc);
            Console.WriteLine("CodeText with StripFNC = true : " + codeWithStrip);
            Console.WriteLine("CodeTexts are different: " + (codeWithoutStrip != codeWithStrip));

            // Simple assertion to confirm expected behavior.
            if (containsFnc && codeWithoutStrip != codeWithStrip)
            {
                Console.WriteLine("Test passed: FNC characters are retained when StripFNC is false and removed when true.");
            }
            else
            {
                Console.WriteLine("Test failed: Unexpected StripFNC behavior.");
            }
        }
    }
}