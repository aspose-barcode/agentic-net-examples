// Title: Create MaxiCode Mode 3 Barcode with Structured Secondary Message
// Description: Demonstrates how to generate a MaxiCode Mode 3 barcode that includes a structured secondary message and export the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of MaxiCodeCodetextMode3, MaxiCodeStructuredSecondMessage, and ComplexBarcodeGenerator classes to create high‑density 2‑D barcodes for shipping and logistics applications. Developers often need to embed address information and other structured data within MaxiCode symbols for automated sorting and tracking.
// Prompt: Create a MaxiCode Mode 3 barcode using a structured secondary message and export the image as JPEG.
// Tags: maxicode, mode3, structured-secondary-message, jpeg, barcode-generation, aspose.barcode, complexbarcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a MaxiCode Mode 3 barcode with a structured secondary message
/// and saves it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a structured secondary message with address lines and year.
        var structuredMessage = new MaxiCodeStructuredSecondMessage();
        structuredMessage.Add("634 ALPHA DRIVE"); // Street address
        structuredMessage.Add("PITTSBURGH");      // City
        structuredMessage.Add("PA");              // State
        structuredMessage.Year = 99;              // Two‑digit year

        // Configure the MaxiCode Mode 3 codetext, including postal code, country code,
        // service category, and the previously created secondary message.
        var maxiCode = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",          // 6 alphanumeric characters
            CountryCode = 56,              // 3‑digit country code
            ServiceCategory = 999,
            SecondMessage = structuredMessage
        };

        // Generate the barcode using ComplexBarcodeGenerator and save it as a JPEG image.
        using (var generator = new ComplexBarcodeGenerator(maxiCode))
        {
            generator.Save("maxicode_mode3.jpeg");
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine("MaxiCode Mode 3 barcode saved as maxicode_mode3.jpeg");
    }
}