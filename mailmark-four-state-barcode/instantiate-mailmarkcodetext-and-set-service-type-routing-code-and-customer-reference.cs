// Title: Instantiate MailmarkCodetext with required fields
// Description: Demonstrates creating a MailmarkCodetext object and setting its mandatory properties for use in mailmark barcode generation.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode category, focusing on the MailmarkCodetext class. Developers use this API to build mailmark barcodes for postal services, configuring fields such as format, version, class, supply chain ID, item ID, and destination postcode. Typical scenarios include preparing data for bulk mailing, tracking, and compliance with postal standards.
// Prompt: Instantiate a MailmarkCodetext and set service type, routing code, and customer reference.
// Tags: mailmark, barcode, complexbarcode, aspose.barcode, codetext, c#, example

using System;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates how to instantiate a MailmarkCodetext object and assign its required fields.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a MailmarkCodetext instance and displays information about unavailable properties.
    /// </summary>
    static void Main()
    {
        // Create a MailmarkCodetext instance and populate all mandatory fields.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Inform the user that the object has been successfully instantiated.
        Console.WriteLine("MailmarkCodetext instantiated with required fields.");

        // Note: The requested ServiceType, RoutingCode, and CustomerReference properties are not part of the MailmarkCodetext class.
        Console.WriteLine("ServiceType, RoutingCode, and CustomerReference are not available in MailmarkCodetext.");
    }
}