// Title: Generate Mailmark 2D Barcode with Routing, Service, and Customer Data
// Description: Demonstrates creating a Mailmark2DCodetext instance, assigning routing, service, and customer data, and generating a PNG barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator and Mailmark2DCodetext to produce Mailmark 2D barcodes, a common requirement for postal automation and tracking solutions. Developers often need to set routing, service, and custom payload fields before rendering the barcode in various image formats.
// Prompt: Create a Mailmark2DCodetext instance and assign routing, service, and customer data values.
// Tags: mailmark, 2d, barcode, generation, png, aspose.barcode, complexbarcode, codetext, dataencoding

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that builds a Mailmark 2D barcode by populating routing,
/// service, and customer data fields, then saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a Mailmark2DCodetext, sets required
    /// properties, generates the barcode, and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Instantiate a Mailmark2DCodetext object to hold barcode data.
        var mailmark2d = new Mailmark2DCodetext();

        // Set routing information: destination postcode plus DPS (trailing space required).
        mailmark2d.DestinationPostCodeAndDPS = "EF61AH8T ";

        // Set service information: information type ID, class, version, and RTS flag.
        mailmark2d.InformationTypeID = "0"; // example service type
        mailmark2d.Class = "1";             // example class
        mailmark2d.VersionID = "1";         // version identifier
        mailmark2d.RTSFlag = "0";           // return‑to‑sender flag

        // Set customer-specific data and its encoding mode.
        mailmark2d.CustomerContent = "CUSTOMER123";
        mailmark2d.CustomerContentEncodeMode = DataMatrixEncodeMode.C40;

        // Populate additional required fields with sample values.
        mailmark2d.SupplyChainID = 384224;
        mailmark2d.ItemID = 16563762;
        mailmark2d.UPUCountryID = "GB";

        // Use ComplexBarcodeGenerator to create the barcode based on the codetext.
        using (var generator = new ComplexBarcodeGenerator(mailmark2d))
        {
            // Define output file path and save the barcode as a PNG image.
            const string outputPath = "mailmark2d.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the file was saved.
            Console.WriteLine($"Mailmark 2D barcode saved to {Path.GetFullPath(outputPath)}");
        }
    }
}