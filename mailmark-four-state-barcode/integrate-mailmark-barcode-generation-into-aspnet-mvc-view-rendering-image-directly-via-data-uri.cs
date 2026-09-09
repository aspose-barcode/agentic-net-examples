// Title: Generate Mailmark 2D Barcode and Output as Data URI
// Description: Demonstrates creating a Mailmark 2D barcode using Aspose.BarCode, converting it to a PNG image in memory, and encoding it as a Base64 data URI for direct embedding in an ASP.NET MVC view.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator with Mailmark2DCodetext, configuring barcode parameters, and rendering the result as an image. Developers working with postal barcodes, especially Mailmark, often need to generate barcodes on the fly and embed them in web pages without writing files to disk.
// Prompt: Integrate Mailmark barcode generation into an ASP.NET MVC view, rendering the image directly via data URI.
// Tags: mailmark, barcode, generation, datauri, png, asp.net mvc, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Mailmark 2D barcode, encodes it as a PNG,
/// and outputs a Base64 data URI suitable for embedding in an ASP.NET MVC view.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the data URI to the console.
    /// </summary>
    static void Main()
    {
        // Create Mailmark 2D codetext with sample data.
        var mailmark2D = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 123,
            ItemID = 1234,
            DestinationPostCodeAndDPS = "EF61AH8T ",
            ReturnToSenderPostCode = " QWE2 ",
            CustomerContent = "CUSTOM",
            DataMatrixType = Mailmark2DType.Type_7
        };

        // Initialize the complex barcode generator with the Mailmark codetext.
        using (var generator = new ComplexBarcodeGenerator(mailmark2D))
        {
            // Adjust the X-dimension (module size) for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the PNG bytes to a Base64 string.
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Build the data URI that can be used directly in an <img> tag.
                string dataUri = $"data:image/png;base64,{base64}";

                // Output the data URI (in a real MVC view this would be passed to the Razor template).
                Console.WriteLine(dataUri);
            }
        }
    }
}