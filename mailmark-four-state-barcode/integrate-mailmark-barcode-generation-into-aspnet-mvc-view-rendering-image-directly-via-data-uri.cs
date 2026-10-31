// Title: Generate Mailmark 4-State Barcode and Output as Data URI
// Description: Demonstrates creating a Mailmark 4-State barcode with Aspose.BarCode, converting it to a Base64 data URI, and optionally saving the PNG file for reference.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator with MailmarkCodetext to produce high‑density barcodes. Typical scenarios include encoding postal information for mail automation and embedding the resulting image directly in web pages via data URIs. Developers often need to generate, render, and transmit barcode images without persisting them to disk, leveraging classes such as ComplexBarcodeGenerator, MailmarkCodetext, and BarCodeImageFormat.
/// Prompt: Integrate Mailmark barcode generation into an ASP.NET MVC view, rendering the image directly via data URI.
// Tags: mailmark, barcode, data-uri, png, aspnet-mvc, aspose.barcode, complexbarcode, generation

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Mailmark 4-State barcode, encodes it as a data URI,
/// and writes the URI to the console. The data URI can be used directly in an ASP.NET MVC view.
/// </summary>
class Program
{
    /// <summary>
    /// Generates the barcode, builds a Base64 data URI, saves the image to a temporary file,
    /// and outputs the URI and file path to the console.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder to store the generated PNG for debugging or reference.
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkDemo");
        Directory.CreateDirectory(tempFolder);
        string outputPath = Path.Combine(tempFolder, "Mailmark4State.png");

        // Define the Mailmark 4-State codetext with required fields.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode and capture the image in a MemoryStream.
        string dataUri;
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Adjust the X‑dimension (module size) for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            using (var ms = new MemoryStream())
            {
                // Save the barcode as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert the PNG bytes to a Base64 string and build the data URI.
                string base64 = Convert.ToBase64String(imageBytes);
                dataUri = $"data:image/png;base64,{base64}";

                // Optionally write the PNG file to disk for verification.
                File.WriteAllBytes(outputPath, imageBytes);
            }
        }

        // Output the data URI and the location of the saved PNG file.
        Console.WriteLine("Data URI for Mailmark 4-State barcode:");
        Console.WriteLine(dataUri);
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}