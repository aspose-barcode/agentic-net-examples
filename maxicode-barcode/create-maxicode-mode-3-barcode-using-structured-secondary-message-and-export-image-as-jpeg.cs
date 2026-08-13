// Title: Create MaxiCode Mode 3 barcode with structured secondary message and save as JPEG
// Description: Demonstrates how to build a MaxiCode Mode 3 barcode, include a structured secondary message, and export the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It shows usage of the MaxiCodeCodetextMode3, MaxiCodeStructuredSecondMessage, and ComplexBarcodeGenerator classes to configure postal information and secondary messages. Developers working with shipping labels, logistics, or any application that requires MaxiCode symbology can use this pattern to create and render barcodes in various image formats.
// Prompt: Create a MaxiCode Mode 3 barcode using a structured secondary message and export the image as JPEG.
// Tags: maxicode, barcode, generation, jpeg, complexbarcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a MaxiCode Mode 3 barcode with a structured secondary message
/// and saves the resulting image as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the secondary message, configures the MaxiCode payload,
    /// generates the barcode, and writes the JPEG image to disk.
    /// </summary>
    static void Main()
    {
        // Build a structured secondary message containing address lines and year
        var structuredMessage = new MaxiCodeStructuredSecondMessage();
        structuredMessage.Add("634 ALPHA DRIVE");
        structuredMessage.Add("PITTSBURGH");
        structuredMessage.Add("PA");
        structuredMessage.Year = 99;

        // Configure the MaxiCode Mode 3 codetext with postal data and the secondary message
        var maxiCodeCodetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = structuredMessage
        };

        // Generate the barcode using ComplexBarcodeGenerator and save it as a JPEG image
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Jpeg);
                File.WriteAllBytes("maxicode_mode3.jpg", memoryStream.ToArray());
            }
        }
    }
}