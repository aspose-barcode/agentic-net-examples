// Title: Generate Mailmark 2D barcode with routing, service, and customer data
// Description: Demonstrates creating a Mailmark2DCodetext, assigning routing, service class, and custom content, then generating a Mailmark 2D barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the Mailmark2DCodetext class, which is used to build Mailmark 2‑D barcodes for postal services. Typical use cases include setting routing information, service class, and customer‑specific data before rendering the barcode as an image. Developers working with postal automation or logistics often need to generate such barcodes programmatically.
// Prompt: Create a Mailmark2DCodetext instance and assign routing, service, and customer data values.
// Tags: mailmark, 2d, barcode, complexbarcode, generation, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that builds a Mailmark 2‑D barcode by populating routing,
/// service, and customer data fields, then saves the barcode as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a Mailmark2DCodetext, configures its
    /// required fields, generates the barcode, and writes the output file name
    /// to the console.
    /// </summary>
    static void Main()
    {
        // Instantiate a Mailmark2DCodetext object which holds all barcode data.
        var mailmark2d = new Mailmark2DCodetext();

        // ------------------------------------------------------------
        // Required routing and service information
        // ------------------------------------------------------------
        mailmark2d.VersionID = "1";                 // Version identifier (single character)
        mailmark2d.InformationTypeID = "0";         // Routing information (Information Type ID)
        mailmark2d.Class = "1";                     // Service class identifier
        mailmark2d.RTSFlag = "0";                   // Return‑to‑sender flag

        // ------------------------------------------------------------
        // Additional mandatory fields (sample values)
        // ------------------------------------------------------------
        mailmark2d.SupplyChainID = 384224;          // Supply chain identifier
        mailmark2d.ItemID = 16563762;               // Unique item identifier
        mailmark2d.DestinationPostCodeAndDPS = "EF61AH8T "; // Destination postcode + DPS (trailing space required)
        mailmark2d.ReturnToSenderPostCode = "SW1A1AA";     // Return‑to‑sender postcode (no DPS)
        mailmark2d.UPUCountryID = "GB";            // UPU country identifier

        // ------------------------------------------------------------
        // Customer‑specific content
        // ------------------------------------------------------------
        mailmark2d.CustomerContent = "CUSTOMER DATA";
        mailmark2d.CustomerContentEncodeMode = DataMatrixEncodeMode.C40; // Encode mode for the customer content

        // Optional: specify DataMatrix type (default is sufficient for most scenarios)
        // mailmark2d.DataMatrixType = DataMatrixType.DataMatrix; // Uncomment if needed and enum exists

        // ------------------------------------------------------------
        // Generate the barcode and save it as a PNG file
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(mailmark2d))
        {
            generator.Save("Mailmark2D.png");
        }

        Console.WriteLine("Mailmark 2D barcode generated: Mailmark2D.png");
    }
}