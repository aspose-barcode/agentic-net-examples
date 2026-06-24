using System;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation and reading of a HIBC QR LIC barcode with secondary data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode containing secondary data, reads it back, and displays the decoded expiration date.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare secondary data with an expiration date and lot number
        // ------------------------------------------------------------
        var secondaryData = new SecondaryAndAdditionalData
        {
            ExpiryDate = DateTime.Today.AddMonths(6),          // Set expiry to six months from today
            ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,      // Use MMDDYY format for the date
            LotNumber = "LOT123"                               // Example lot number
        };

        // ------------------------------------------------------------
        // Create the complex codetext object for HIBC QR LIC barcode
        // ------------------------------------------------------------
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC, // Specify barcode type
            LinkCharacter = '+',                 // Define link character
            Data = secondaryData                 // Attach the secondary data prepared above
        };

        // ------------------------------------------------------------
        // Generate the barcode image from the complex codetext
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
        {
            // ------------------------------------------------------------
            // Read the barcode from the generated image using a barcode reader
            // ------------------------------------------------------------
            using (var reader = new BarCodeReader(barcodeImage, DecodeType.HIBCQRLIC))
            {
                // Retrieve all decoded barcode results
                var results = reader.ReadBarCodes();

                // Iterate through each result (there should be only one in this example)
                foreach (var result in results)
                {
                    // ------------------------------------------------------------
                    // Decode the raw codetext string into a complex codetext object
                    // ------------------------------------------------------------
                    var decoded = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);

                    // Check if decoding produced the expected secondary data type
                    if (decoded is HIBCLICSecondaryAndAdditionalDataCodetext secondaryDecoded)
                    {
                        // Access the expiration date from the decoded secondary data
                        DateTime expiry = secondaryDecoded.Data.ExpiryDate;

                        // Output the decoded expiration date in ISO format
                        Console.WriteLine($"Decoded Expiration Date: {expiry:yyyy-MM-dd}");
                    }
                    else
                    {
                        // Inform the user if decoding failed or returned an unexpected type
                        Console.WriteLine("Failed to decode as HIBCLICSecondaryAndAdditionalDataCodetext.");
                    }
                }
            }
        }
    }
}