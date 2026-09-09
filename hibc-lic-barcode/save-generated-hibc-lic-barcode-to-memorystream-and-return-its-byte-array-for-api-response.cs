// Title: Generate HIBC LIC Barcode and Return Byte Array
// Description: This example creates a HIBC QR LIC barcode with secondary data, saves it to a MemoryStream, and extracts the PNG byte array for use in API responses.
// Category-Description: Demonstrates Aspose.BarCode complex barcode generation using ComplexBarcodeGenerator and HIBCLICSecondaryAndAdditionalDataCodetext. Typical scenarios include creating HIBC‑QR‑LIC barcodes for healthcare labeling, embedding secondary data such as expiry dates and lot numbers, and returning the image as a byte array from web APIs. Developers often need to configure barcode dimensions, choose image formats, and work with streams for efficient transmission.
// Prompt: Save the generated HIBC LIC barcode to a MemoryStream and return its byte array for an API response.
// Tags: barcode, hibc, lic, png, memorystream, byte array, aspnet, aspose.barcode, complexbarcode, api response

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a HIBC QR LIC barcode with secondary data,
/// saving it to a MemoryStream, and obtaining the PNG byte array.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that builds the barcode, writes it to a MemoryStream,
    /// and outputs the byte array length and a hex preview.
    /// </summary>
    static void Main()
    {
        // Prepare HIBC LIC secondary data codetext
        var complexCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = new SecondaryAndAdditionalData
            {
                ExpiryDate = DateTime.Now.AddDays(30),
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                Quantity = 30,
                LotNumber = "LOT123",
                SerialNumber = "SERIAL123",
                DateOfManufacture = DateTime.Now.AddMonths(-1)
            },
            LinkCharacter = '+'
        };

        // Generate barcode and save to a MemoryStream
        using (var memoryStream = new MemoryStream())
        {
            using (var generator = new ComplexBarcodeGenerator(complexCodetext))
            {
                // Set X‑dimension (module width) to 10 pixels for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 10;

                // Save the barcode image as PNG into the memory stream
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Retrieve the PNG bytes from the stream
            byte[] barcodeBytes = memoryStream.ToArray();

            // Output diagnostic information (length and first few bytes as hex)
            Console.WriteLine($"Generated HIBC LIC barcode byte array length: {barcodeBytes.Length}");
            int previewLength = Math.Min(10, barcodeBytes.Length);
            Console.Write("First bytes: ");
            for (int i = 0; i < previewLength; i++)
            {
                Console.Write($"{barcodeBytes[i]:X2} ");
            }
            Console.WriteLine();
        }
    }
}