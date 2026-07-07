// Title: Generate MaxiCode Barcode in ASP.NET MVC Action
// Description: Demonstrates creating a MaxiCode barcode image using Aspose.BarCode based on query string parameters.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with MaxiCode codetext classes (MaxiCodeCodetextMode2, MaxiCodeCodetextMode3, MaxiCodeStandardCodetext) to produce PNG images. Typical scenarios include shipping labels, parcel tracking, and logistics applications where MaxiCode is required. Developers often need to build MVC actions that return barcode images directly to the client, and this snippet illustrates the core API calls and parameter handling.
// Prompt: Create an ASP.NET MVC action that returns a generated MaxiCode barcode image based on query string parameters.
// Tags: maxicode, barcode, generation, aspnet mvc, image, png, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Console program that mimics an ASP.NET MVC action for generating a MaxiCode barcode image.
/// In a real MVC controller the logic would be placed inside an action method returning a FileResult.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that parses parameters, builds the appropriate MaxiCode codetext,
    /// generates the barcode image, and saves it as a PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments used as stand‑in for query string values.</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Default parameters (used when not enough command‑line arguments are supplied)
        // --------------------------------------------------------------------
        int mode = 2;                     // MaxiCode mode (2,3,4,5,6)
        string postalCode = "524032140";  // 9‑digit for mode 2, 6‑char for mode 3
        int countryCode = 56;             // 3‑digit numeric country code
        int serviceCategory = 999;        // 3‑digit service category
        string message = "Sample message"; // Standard second message

        // --------------------------------------------------------------------
        // Parse command‑line arguments if provided (simulating query string)
        // Expected order: mode postalCode countryCode serviceCategory message
        // --------------------------------------------------------------------
        try
        {
            if (args.Length > 0) mode = int.Parse(args[0]);
            if (args.Length > 1) postalCode = args[1];
            if (args.Length > 2) countryCode = int.Parse(args[2]);
            if (args.Length > 3) serviceCategory = int.Parse(args[3]);
            if (args.Length > 4) message = args[4];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Argument parsing error: {ex.Message}");
            Console.WriteLine("Using default values.");
        }

        // --------------------------------------------------------------------
        // Validate the requested MaxiCode mode
        // --------------------------------------------------------------------
        if (mode < 2 || mode > 6)
        {
            Console.WriteLine("Invalid MaxiCode mode. Supported values are 2,3,4,5,6.");
            return;
        }

        // --------------------------------------------------------------------
        // Build the appropriate codetext object based on the selected mode
        // --------------------------------------------------------------------
        IComplexCodetext codetext;
        if (mode == 2)
        {
            var ct = new MaxiCodeCodetextMode2
            {
                PostalCode = postalCode,
                CountryCode = countryCode,
                ServiceCategory = serviceCategory,
                SecondMessage = new MaxiCodeStandardSecondMessage { Message = message }
            };
            codetext = ct;
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
            codetext = ct;
        }
        else // modes 4,5,6 use standard codetext
        {
            var ct = new MaxiCodeStandardCodetext
            {
                Mode = mode switch
                {
                    4 => MaxiCodeMode.Mode4,
                    5 => MaxiCodeMode.Mode5,
                    6 => MaxiCodeMode.Mode6,
                    _ => throw new ArgumentOutOfRangeException()
                },
                Message = message
            };
            codetext = ct;
        }

        // --------------------------------------------------------------------
        // Define the output file path (in a real MVC action this would be streamed)
        // --------------------------------------------------------------------
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "maxicode.png");

        // --------------------------------------------------------------------
        // Generate the barcode image and save it as PNG
        // --------------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            // Generate the bitmap (optional, GenerateBarCodeImage returns the bitmap)
            generator.GenerateBarCodeImage();

            // Save the image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"MaxiCode barcode generated (mode {mode}) and saved to: {outputPath}");
    }
}