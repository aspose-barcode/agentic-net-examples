// Title: Generate MaxiCode barcode image and output as Base64 PNG
// Description: This console example demonstrates how to create a MaxiCode barcode (mode 2 or 3) using Aspose.BarCode and output the PNG image as a Base64 string. It shows how to set postal code, country code, service category, and a secondary message.
// Category-Description: Aspose.BarCode examples for complex barcode generation illustrate the use of ComplexBarcodeGenerator and specific codetext classes (e.g., MaxiCodeCodetextMode2, MaxiCodeCodetextMode3). Developers commonly need to generate MaxiCode symbols for shipping and logistics, customize fields such as postal code and service category, and return the image in web scenarios (e.g., ASP.NET MVC actions). This snippet provides a reusable pattern for creating and encoding the barcode image.
// Prompt: Create an ASP.NET MVC action that returns a generated MaxiCode barcode image based on query string parameters.
// Tags: maxicode, barcode, generation, png, base64, aspnet-mvc, aspnet, aspnet-mvc-action, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a MaxiCode barcode (mode 2 or 3) and outputs the PNG image as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that parses command‑line arguments, creates the appropriate MaxiCode codetext,
    /// generates the barcode image, and writes the Base64‑encoded PNG to the console.
    /// </summary>
    /// <param name="args">
    /// Expected arguments:
    ///   0 – mode (2 or 3)
    ///   1 – postalCode
    ///   2 – countryCode (int)
    ///   3 – serviceCategory (int)
    ///   4 – message (standard second message)
    /// </param>
    static void Main(string[] args)
    {
        // Validate that all required arguments are supplied.
        if (args.Length < 5)
        {
            Console.WriteLine("Usage: <mode> <postalCode> <countryCode> <serviceCategory> <message>");
            return;
        }

        // Parse input parameters.
        int mode = int.Parse(args[0]);
        string postalCode = args[1];
        int countryCode = int.Parse(args[2]);
        int serviceCategory = int.Parse(args[3]);
        string message = args[4];

        // Create the appropriate MaxiCode codetext object based on the selected mode.
        MaxiCodeCodetext maxiCodeCodetext;
        if (mode == 2)
        {
            var ct = new MaxiCodeCodetextMode2
            {
                PostalCode = postalCode,
                CountryCode = countryCode,
                ServiceCategory = serviceCategory,
                SecondMessage = new MaxiCodeStandardSecondMessage { Message = message }
            };
            maxiCodeCodetext = ct;
        }
        else if (mode == 3)
        {
            var ct = new MaxiCodeCodetextMode3
            {
                PostalCode = postalCode,
                CountryCode = countryCode,
                ServiceCategory = serviceCategory,
                SecondMessage = new MaxiCodeStandardSecondMessage { Message = message }
            };
            maxiCodeCodetext = ct;
        }
        else
        {
            Console.WriteLine("Supported modes are 2 and 3.");
            return;
        }

        // Generate the barcode using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            using (var memory = new MemoryStream())
            {
                // Save the barcode as PNG into the memory stream.
                generator.Save(memory, BarCodeImageFormat.Png);
                byte[] pngBytes = memory.ToArray();

                // Convert the PNG bytes to a Base64 string and write to console.
                string base64 = Convert.ToBase64String(pngBytes);
                Console.WriteLine(base64);
            }
        }

        // Exit with success code.
        Environment.Exit(0);
    }
}