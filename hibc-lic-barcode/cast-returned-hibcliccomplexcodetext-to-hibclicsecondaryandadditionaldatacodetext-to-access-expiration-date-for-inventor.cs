// Title: Decode HIBC QRLIC barcode and access secondary data (expiry date)
// Description: Demonstrates generating a HIBC QRLIC barcode with secondary and additional data, then decoding it to retrieve the expiration date and lot number.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode operations collection. It showcases how to use ComplexBarcodeGenerator to create a HIBCLICSecondaryAndAdditionalDataCodetext, save the barcode as PNG, and employ BarCodeReader with DecodeType.HIBCQRLIC to read and interpret the complex codetext. Developers working with healthcare inventory or regulatory labeling often need to embed and extract secondary data such as expiry dates, lot numbers, and other attributes using the HIBC QRLIC symbology.
// Prompt: Cast the returned HIBCLICComplexCodetext to HIBCLICSecondaryAndAdditionalDataCodetext to access expiration date for inventory processing.
// Tags: hibc, secondary-and-additional-data, barcode generation, barcode recognition, png, complexbarcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a HIBC QRLIC barcode containing secondary data,
/// then decodes the barcode and extracts the expiration date and lot number.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates, saves, reads, and processes a HIBC QRLIC barcode.
    /// </summary>
    static void Main()
    {
        // Prepare secondary data with an expiration date and lot number
        var secondaryData = new SecondaryAndAdditionalData
        {
            ExpiryDate = DateTime.Today,
            ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
            LotNumber = "LOT123"
        };

        // Create a complex codetext object that holds the secondary data
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            LinkCharacter = '+',
            Data = secondaryData
        };

        // Generate the barcode image and store it in a memory stream
        using (var imageStream = new MemoryStream())
        {
            using (var generator = new ComplexBarcodeGenerator(complexCodetext))
            {
                // Save the generated barcode as PNG into the stream
                generator.Save(imageStream, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning for reading
            imageStream.Position = 0;

            // Decode the barcode image from the memory stream
            using (var reader = new BarCodeReader(imageStream, DecodeType.HIBCQRLIC))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // Attempt to decode the raw codetext into a complex codetext object
                    var decoded = ComplexCodetextReader.TryDecodeHIBCLIC(result.CodeText);

                    // Cast to the specific secondary-and-additional-data type to access expiry information
                    if (decoded is HIBCLICSecondaryAndAdditionalDataCodetext secondary)
                    {
                        Console.WriteLine("Expiry date: " + secondary.Data.ExpiryDate);
                        Console.WriteLine("Lot number: " + secondary.Data.LotNumber);
                    }
                    else
                    {
                        Console.WriteLine("Decoded codetext is not of the expected secondary data type.");
                    }
                }
            }
        }
    }
}