using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a MaxiCode barcode (Mode 2) with concatenated secondary messages.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Builds a MaxiCode codetext, generates the barcode, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare secondary messages that will be combined into one string.
        // ------------------------------------------------------------
        string[] secondaryMessages = new[] { "First message", "Second message", "Third message" };
        string concatenatedMessage = string.Join(" ", secondaryMessages); // "First message Second message Third message"

        // ------------------------------------------------------------
        // Create a MaxiCode codetext object (Mode 2) and populate required primary fields.
        // ------------------------------------------------------------
        var maxiCode = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit US postal code
            CountryCode = 56,           // Example country code
            ServiceCategory = 999       // Example service category
        };

        // ------------------------------------------------------------
        // Assign the concatenated secondary message to the unstructured field.
        // ------------------------------------------------------------
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = concatenatedMessage
        };
        maxiCode.SecondMessage = secondMessage;

        // ------------------------------------------------------------
        // Build the full codetext string that will be encoded in the barcode.
        // ------------------------------------------------------------
        string fullCodeText = maxiCode.GetConstructedCodetext();

        // ------------------------------------------------------------
        // Generate the MaxiCode barcode image and save it as a PNG file.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, fullCodeText))
        {
            // Save the generated barcode to disk.
            generator.Save("maxicode.png");
        }

        // Inform the user that the barcode has been created.
        Console.WriteLine("MaxiCode barcode generated and saved as 'maxicode.png'.");
    }
}