// Title: Generate Mailmark 2D Barcode and Output as Data URI in ASP.NET MVC
// Description: This example creates a Mailmark 2D barcode using Aspose.BarCode, encodes the PNG image to Base64, and builds a data URI that can be embedded directly in an MVC view.
// Category-Description: Demonstrates Aspose.BarCode complex barcode generation for Mailmark symbology, covering the use of ComplexBarcodeGenerator, Mailmark2DCodetext, and image export. Typical scenarios include creating printable mail items or embedding barcode images in web pages. Developers working with postal barcodes often need to generate PNG streams and embed them via data URIs for seamless MVC integration.
// Prompt: Integrate Mailmark barcode generation into an ASP.NET MVC view, rendering the image directly via data URI.
// Tags: mailmark, barcode, complexbarcode, datauri, png, aspnet mvc, base64, generation

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a Mailmark 2D barcode and conversion to a data URI for use in ASP.NET MVC views.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, encodes it, and writes the data URI to the console.
    /// </summary>
    static void Main()
    {
        // NOTE: In a real ASP.NET MVC application the generated Base64 string would be
        // embedded in an <img src="data:image/png;base64,..." /> tag in a view.
        // Here we demonstrate the core barcode generation and output the data URI to the console.

        // ------------------------------------------------------------
        // Prepare Mailmark 2D codetext with required fields
        // ------------------------------------------------------------
        var mailmark2D = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 123,
            ItemID = 1234,
            CustomerContent = "CUSTOM"
        };
        // Set the specific DataMatrix type for Mailmark
        mailmark2D.DataMatrixType = Mailmark2DType.Type_7;

        // ------------------------------------------------------------
        // Generate the barcode using ComplexBarcodeGenerator
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            // Adjust the X-dimension (module size) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // --------------------------------------------------------
            // Save the barcode image to a memory stream in PNG format
            // --------------------------------------------------------
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the PNG byte array to a Base64 string
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Build the data URI that can be used directly in an <img> tag
                string dataUri = $"data:image/png;base64,{base64}";

                // Output the data URI (in MVC you would pass this to the view)
                Console.WriteLine(dataUri);
            }
        }
    }
}