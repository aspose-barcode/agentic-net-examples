// Title: HIBC Secondary Data Barcode Generation and Decoding
// Description: Demonstrates generating a HIBC Code128 LIC barcode that carries secondary data (expiration date, lot number) and decoding it to retrieve the expiration date for inventory processing.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode operations collection. It showcases the use of ComplexBarcodeGenerator, ComplexCodetextReader, and HIBCLICSecondaryAndAdditionalDataCodetext to create and read HIBC‑LIC barcodes. Typical scenarios include healthcare product tracking, pharmaceutical inventory, and any application that needs to embed and later extract secondary information such as expiry dates. Developers often need to generate compliant HIBC barcodes, embed additional data, and reliably decode that data for downstream processing.
// Prompt: Cast the returned HIBCLICComplexCodetext to HIBCLICSecondaryAndAdditionalDataCodetext to access expiration date for inventory processing.
// Tags: hibc, secondary-data, barcode-generation, barcode-decoding, complex-barcode, aspose.barcode

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a HIBC Code128 LIC barcode containing secondary data, saves the image,
/// reads it back, and extracts the expiration date for inventory processing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, saving, decoding,
    /// and extraction of the expiration date from the decoded complex codetext.
    /// </summary>
    static void Main()
    {
        // Prepare secondary data with an expiration date and lot number
        var secondaryData = new SecondaryAndAdditionalData
        {
            ExpiryDate = DateTime.Today.AddDays(30),          // Set expiry 30 days from today
            ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,     // Use MMDDYY format as required by HIBC
            LotNumber = "LOT123"                             // Example lot identifier
        };

        // Create the complex codetext that holds only secondary data
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC, // Specify HIBC Code128 LIC symbology
            LinkCharacter = '+',                     // Standard link character for HIBC
            Data = secondaryData                     // Attach the secondary data object
        };

        // Generate the barcode image using the complex codetext
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        using (Bitmap bitmap = generator.GenerateBarCodeImage())
        {
            // Optionally save the image to verify generation (not required for processing)
            bitmap.Save("hibc_secondary.png", ImageFormat.Png);

            // Decode the barcode from the generated image
            using (var reader = new BarCodeReader(bitmap, DecodeType.HIBCCode128LIC))
            {
                var results = reader.ReadBarCodes();

                // Ensure at least one barcode was detected
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected.");
                    return;
                }

                // Retrieve the raw codetext from the first detection result
                string rawCodeText = results[0].CodeText;

                // Parse the raw codetext into a strongly‑typed complex codetext object
                var parsed = ComplexCodetextReader.TryDecodeHIBCLIC(rawCodeText);

                // Cast to the specific secondary‑data codetext type to access expiration information
                if (parsed is HIBCLICSecondaryAndAdditionalDataCodetext secondaryResult)
                {
                    // Access the expiration date for inventory processing
                    DateTime expiry = secondaryResult.Data.ExpiryDate;
                    Console.WriteLine($"Expiration Date: {expiry:yyyy-MM-dd}");
                }
                else
                {
                    Console.WriteLine("Decoded codetext is not of secondary data type.");
                }
            }
        }
    }
}