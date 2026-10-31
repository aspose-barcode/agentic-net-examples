// Title: Generate and Decode HIBC QR LIC Barcode with Secondary Data (Expiration Date)
// Description: Demonstrates creating a HIBC QR LIC barcode that includes secondary and additional data such as an expiration date, then decoding it to retrieve that information.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator, BarCodeReader, and HIBCLICSecondaryAndAdditionalDataCodetext to embed and extract inventory‑related data. Developers working with healthcare or supply‑chain labeling often need to generate HIBC QR LIC barcodes with secondary data and later read back fields like expiration dates, lot numbers, or quantities.
// Prompt: Cast the returned HIBCLICComplexCodetext to HIBCLICSecondaryAndAdditionalDataCodetext to access expiration date for inventory processing.
// Tags: hibc, lic, secondary data, barcode generation, barcode recognition, complex barcode, expiration date, inventory

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a HIBC QR LIC barcode containing secondary data (including an expiration date),
/// saves it to a temporary file, and then reads the barcode back to extract the expiration date.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and decodes it to display the expiration date.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary folder for the barcode image
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "HIBCLICDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "hibclic_secondary.png");

        // ------------------------------------------------------------
        // Create HIBC LIC secondary data codetext with expiration date and other fields
        // ------------------------------------------------------------
        var secondaryCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = new SecondaryAndAdditionalData
            {
                ExpiryDate = DateTime.Today.AddDays(30),
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                Quantity = 10,
                LotNumber = "LOT123"
            },
            LinkCharacter = '+'
        };

        // ------------------------------------------------------------
        // Generate the barcode image and save it to the temporary path
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(secondaryCodetext))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 10;
            generator.Save(barcodePath);
        }

        // ------------------------------------------------------------
        // Verify that the barcode image was created before attempting to read it
        // ------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image was not created.");
            return;
        }

        // ------------------------------------------------------------
        // Read and decode the barcode, casting the result to the secondary data type
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.HIBCQRLIC))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Decode the raw HIBC LIC codetext
                HIBCLICComplexCodetext complex = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);

                // Cast to the specific secondary‑and‑additional‑data type to access custom fields
                var secondary = complex as HIBCLICSecondaryAndAdditionalDataCodetext;
                if (secondary != null)
                {
                    Console.WriteLine($"Expiration Date: {secondary.Data.ExpiryDate:yyyy-MM-dd}");
                }
                else
                {
                    Console.WriteLine("Decoded codetext is not secondary and additional data type.");
                }
            }
        }
    }
}