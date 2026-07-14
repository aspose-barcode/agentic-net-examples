// Title: Generate Swiss QR Code and return as image
// Description: Demonstrates creating a Swiss QR Bill barcode using Aspose.BarCode and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode symbologies such as Swiss QR Bill. It showcases the use of ComplexBarcodeGenerator and related classes to produce QR codes for financial documents, a common requirement for developers integrating payment features into web applications.
// Prompt: Integrate Swiss QR Code generation into an ASP.NET MVC controller action returning the barcode image as HTTP response.
// Tags: barcode symbology, generation, png, aspnet-mvc, aspose.barcode, swiss-qr

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a Swiss QR Bill barcode and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that builds the QR bill data, creates the barcode, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // NOTE: In a real ASP.NET MVC controller this logic would be placed inside an action method
        // and the image would be written directly to the HTTP response stream.

        // Prepare Swiss QR bill data
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Create the complex barcode generator using the prepared QR bill data
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Optional: set QR error correction level to improve readability under adverse conditions
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Generate the barcode image and write it to a memory stream
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);

                // Persist the image to a file for demonstration purposes
                File.WriteAllBytes("SwissQR.png", ms.ToArray());
            }
        }

        Console.WriteLine("Swiss QR Code image saved as SwissQR.png");
    }
}