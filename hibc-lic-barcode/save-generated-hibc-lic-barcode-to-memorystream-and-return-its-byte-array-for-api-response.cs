// Title: Generate HIBC LIC Barcode and Return PNG Byte Array
// Description: Demonstrates creating a HIBC Code128 LIC barcode with secondary data, saving it to a MemoryStream, and obtaining the PNG byte array for API responses.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator, HIBCLICSecondaryAndAdditionalDataCodetext, and related data classes to produce HIBC LIC barcodes. Developers often need to embed secondary information such as lot numbers, serial numbers, and dates, then deliver the barcode image as a byte array for web APIs or other services.
// Prompt: Save the generated HIBC LIC barcode to a MemoryStream and return its byte array for an API response.
// Tags: hibc, lic, barcode generation, png, memory stream, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.Generation; // for BarCodeImageFormat
using Aspose.Drawing.Imaging; // for ImageFormat if needed (not used here)

/// <summary>
/// Example program that creates a HIBC LIC barcode with secondary data,
/// saves it to a MemoryStream, and outputs the resulting byte array.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes diagnostic information to the console.
    /// </summary>
    static void Main()
    {
        // Prepare secondary and additional data for the HIBC LIC barcode.
        var secondaryCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            // Select the HIBC Code128 LIC symbology.
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            // The link character is required by the HIBC specification.
            LinkCharacter = '+',
            // Populate the secondary data fields.
            Data = new SecondaryAndAdditionalData
            {
                LotNumber = "LOT123",
                SerialNumber = "SER123",
                Quantity = 10,
                ExpiryDate = DateTime.Now.AddMonths(6),
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                DateOfManufacture = DateTime.Now.AddMonths(-1)
            }
        };

        // Generate the barcode and write it to a MemoryStream in PNG format.
        using (var generator = new ComplexBarcodeGenerator(secondaryCodetext))
        using (var memoryStream = new MemoryStream())
        {
            // Save the barcode image to the stream.
            generator.Save(memoryStream, BarCodeImageFormat.Png);

            // Retrieve the raw PNG bytes from the stream.
            byte[] barcodeBytes = memoryStream.ToArray();

            // Output diagnostic information (length and Base64 representation) for verification.
            Console.WriteLine($"Barcode byte array length: {barcodeBytes.Length}");
            Console.WriteLine($"Base64: {Convert.ToBase64String(barcodeBytes)}");
        }
    }
}