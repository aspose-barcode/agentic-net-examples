// Title: Create Mailmark Codetext with Service Type, Routing Code, and Customer Reference
// Description: Demonstrates how to instantiate a MailmarkCodetext object, set its service type, routing code, and customer reference, and retrieve the constructed codetext string.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on Mailmark symbology. It showcases the MailmarkCodetext class, which is used to build Mailmark codetext strings required for postal services. Developers working with Mailmark often need to configure service type, routing information, and customer references before encoding the barcode.
// Prompt: Instantiate a MailmarkCodetext and set service type, routing code, and customer reference.
// Tags: mailmark, barcode, codetext, service-type, routing-code, customer-reference, aspose.barcode, complexbarcode

using System;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that builds a Mailmark codetext using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a MailmarkCodetext, configures required fields, and prints the resulting codetext.
    /// </summary>
    static void Main()
    {
        // Instantiate a new MailmarkCodetext object.
        var mailmark = new MailmarkCodetext();

        // Set the Mailmark format (4-state format).
        mailmark.Format = 4;

        // Set the version identifier.
        mailmark.VersionID = 1;

        // Set the service type (Class) as a string.
        mailmark.Class = "0";

        // Set the supply chain identifier.
        mailmark.SupplychainID = 384224;

        // Set the customer reference (ItemID) as an integer.
        mailmark.ItemID = 16563762;

        // Set the routing code (DestinationPostCodePlusDPS) with required trailing space.
        mailmark.DestinationPostCodePlusDPS = "EF61AH8T ";

        // Output the constructed Mailmark codetext to the console.
        Console.WriteLine("Constructed Mailmark codetext:");
        Console.WriteLine(mailmark.GetConstructedCodetext());
    }
}