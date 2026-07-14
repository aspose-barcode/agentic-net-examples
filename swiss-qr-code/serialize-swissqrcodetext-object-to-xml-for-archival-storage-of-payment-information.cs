// Title: Serialize SwissQR Code Text to XML
// Description: Demonstrates how to serialize a SwissQR code text object to XML for archival storage of payment information.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode operations category, focusing on Swiss QR Bill generation and data persistence. It showcases the use of Aspose.BarCode.ComplexBarcode.SwissQRCodetext and related classes to create payment QR codes, then serialize the object with System.Xml.Serialization for later retrieval. Developers working with payment QR codes, invoicing systems, or financial data archiving will find this pattern useful.
// Prompt: Serialize the SwissQRCodetext object to XML for archival storage of payment information.
// Tags: barcode symbology, serialization, xml, swissqr, aspose.barcode, complexbarcode

using System;
using System.IO;
using System.Xml.Serialization;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;

namespace SwissQRSerialization
{
    /// <summary>
    /// Provides an entry point that creates a SwissQR code text object,
    /// populates required billing fields, and serializes it to an XML file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method – builds a SwissQR bill, serializes it to XML, and outputs the file path.
        /// </summary>
        static void Main()
        {
            // Initialize a new SwissQRCodetext instance
            var swissQr = new SwissQRCodetext();

            // Populate mandatory bill fields
            swissQr.Bill.Creditor.Name = "John Doe";
            swissQr.Bill.Creditor.CountryCode = "CH";
            swissQr.Bill.Account = "CH9300762011623852957";
            swissQr.Bill.Amount = 199.95m;
            swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

            // Prepare XML serializer for the SwissQRCodetext type
            var serializer = new XmlSerializer(typeof(SwissQRCodetext));

            // Define output XML file path
            var xmlPath = "SwissQRCodetext.xml";

            // Serialize the object to the specified file
            using (var fileStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(fileStream, swissQr);
            }

            // Inform the user where the XML file was saved
            Console.WriteLine($"SwissQRCodetext has been serialized to: {Path.GetFullPath(xmlPath)}");
        }
    }
}