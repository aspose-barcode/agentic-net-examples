// Title: Generate MaxiCode barcode image (simulated MVC action)
// Description: This example creates a MaxiCode barcode image using Aspose.BarCode based on input parameters, illustrating how such logic could be placed inside an ASP.NET MVC action that returns the image as a FileResult.
// Category-Description: Demonstrates Aspose.BarCode complex barcode generation, specifically MaxiCode symbology. Shows usage of ComplexBarcodeGenerator, MaxiCodeCodetextMode2/Mode3, and related parameter settings. Useful for developers implementing barcode generation in web applications, needing to customize mode, postal code, country code, service category, and secondary message.
// Prompt: Create an ASP.NET MVC action that returns a generated MaxiCode barcode image based on query string parameters.
// Tags: maxicode, barcode, generation, aspnet mvc, complexbarcode, image output, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a MaxiCode barcode image based on parameters (simulating an ASP.NET MVC action).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that builds a MaxiCode barcode using supplied arguments and saves it to a temporary PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments representing mode, postal code, country code, service category, and secondary message.</param>
    static void Main(string[] args)
    {
        // Simulate query‑string parameters via command‑line arguments.
        // Expected order: mode postalCode countryCode serviceCategory secondaryMessage
        // Example: 2 524032140 56 999 "Free text message"
        string modeArg = args.Length > 0 ? args[0] : "2";
        string postalCode = args.Length > 1 ? args[1] : "524032140";
        string countryCodeArg = args.Length > 2 ? args[2] : "56";
        string serviceCategoryArg = args.Length > 3 ? args[3] : "999";
        string secondaryMessage = args.Length > 4 ? args[4] : "Free text message";

        // Validate mode (must be 2 or 3)
        if (!int.TryParse(modeArg, out int mode) || (mode != 2 && mode != 3))
        {
            Console.WriteLine("Mode must be 2 or 3.");
            return;
        }

        // Validate country code
        if (!int.TryParse(countryCodeArg, out int countryCode))
        {
            Console.WriteLine("Invalid country code.");
            return;
        }

        // Validate service category
        if (!int.TryParse(serviceCategoryArg, out int serviceCategory))
        {
            Console.WriteLine("Invalid service category.");
            return;
        }

        // Build the appropriate MaxiCode codetext object based on the selected mode
        IComplexCodetext codetext;
        if (mode == 2)
        {
            // Mode 2 requires a 9‑digit numeric postal code
            if (postalCode.Length != 9 || !long.TryParse(postalCode, out _))
            {
                Console.WriteLine("Mode 2 postal code must be exactly 9 numeric digits.");
                return;
            }

            var second = new MaxiCodeStandardSecondMessage { Message = secondaryMessage };
            codetext = new MaxiCodeCodetextMode2
            {
                PostalCode = postalCode,
                CountryCode = countryCode,
                ServiceCategory = serviceCategory,
                SecondMessage = second
            };
        }
        else // mode == 3
        {
            var second = new MaxiCodeStandardSecondMessage { Message = secondaryMessage };
            codetext = new MaxiCodeCodetextMode3
            {
                PostalCode = postalCode,
                CountryCode = countryCode,
                ServiceCategory = serviceCategory,
                SecondMessage = second
            };
        }

        // Generate the barcode and save it to a temporary PNG file
        string outputPath = Path.Combine(Path.GetTempPath(), $"MaxiCode_Mode{mode}_{Guid.NewGuid():N}.png");
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            // Common barcode settings
            generator.Parameters.Barcode.XDimension.Pixels = 15f;
            generator.Parameters.Barcode.MaxiCode.Mode = mode == 2 ? MaxiCodeMode.Mode2 : MaxiCodeMode.Mode3;
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}