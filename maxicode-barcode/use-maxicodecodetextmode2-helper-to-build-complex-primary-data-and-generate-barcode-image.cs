// Title: Generate MaxiCode Mode 2 barcode with complex data using Aspose.BarCode
// Description: Demonstrates building complex primary data for MaxiCode Mode 2 with the MaxiCodeCodetextMode2 helper and saving the barcode as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with MaxiCodeCodetextMode2 and MaxiCodeStructuredSecondMessage to create shipping‑label style barcodes. Developers working with logistics, parcel tracking, or any scenario requiring MaxiCode symbology can follow this pattern to construct detailed primary and secondary messages before rendering the image.
// Prompt: Use the MaxiCodeCodetextMode2 helper to build complex primary data and generate the barcode image.
// Tags: maxicode, barcode generation, complex barcode, aspose.barcode, image output, shipping label

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a MaxiCode Mode 2 barcode with structured primary and secondary data
/// and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds the MaxiCode data, generates the barcode image, and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "maxicode.png";

        // Build primary data for MaxiCode Mode 2 using the helper class.
        var maxiCodeData = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit US postal code
            CountryCode = 56,           // Example country code
            ServiceCategory = 999       // Example service category
        };

        // Build the structured second message (address lines and year).
        var secondMessage = new MaxiCodeStructuredSecondMessage();
        secondMessage.Add("634 ALPHA DRIVE");
        secondMessage.Add("PITTSBURGH");
        secondMessage.Add("PA");
        secondMessage.Year = 99; // Two‑digit year

        // Assign the second message to the MaxiCode data object.
        maxiCodeData.SecondMessage = secondMessage;

        // Generate the complex MaxiCode barcode using the ComplexBarcodeGenerator.
        using (var complexGenerator = new ComplexBarcodeGenerator(maxiCodeData))
        {
            // Produce the barcode image as an Aspose.Drawing.Bitmap.
            using (Bitmap image = complexGenerator.GenerateBarCodeImage())
            {
                // Save the bitmap to the specified file path.
                image.Save(outputPath);
            }
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}