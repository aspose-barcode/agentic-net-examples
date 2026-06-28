using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of MaxiCode barcodes using Aspose.BarCode.
/// Accepts optional command‑line arguments to customize the barcode data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Parses command‑line arguments, builds the appropriate complex codetext,
    /// and outputs a Base64‑encoded PNG image of the generated MaxiCode barcode.
    /// </summary>
    /// <param name="args">
    /// Optional arguments:
    /// 0 – mode (int, 2‑6),
    /// 1 – postal code (string),
    /// 2 – country code (int),
    /// 3 – service category (int),
    /// 4 – second message (string).
    /// </param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Default values for demonstration (used when no command‑line args)
        // --------------------------------------------------------------------
        int mode = 2;                     // MaxiCode mode (2‑6)
        string postalCode = "524032140";  // 9‑digit for mode 2, 6‑char for mode 3
        int countryCode = 56;             // 3‑digit numeric country code
        int serviceCategory = 999;        // 3‑digit service category
        string message = "Test message";  // Second message (standard)

        // --------------------------------------------------------------------
        // Parse command‑line arguments if they are provided
        // --------------------------------------------------------------------
        if (args.Length >= 1 && int.TryParse(args[0], out int parsedMode))
            mode = parsedMode;

        if (args.Length >= 2)
            postalCode = args[1];

        if (args.Length >= 3 && int.TryParse(args[2], out int parsedCountry))
            countryCode = parsedCountry;

        if (args.Length >= 4 && int.TryParse(args[3], out int parsedService))
            serviceCategory = parsedService;

        if (args.Length >= 5)
            message = args[4];

        // --------------------------------------------------------------------
        // Validate the selected mode (only 2‑6 are supported)
        // --------------------------------------------------------------------
        if (mode < 2 || mode > 6)
        {
            Console.WriteLine("Invalid MaxiCode mode. Supported values are 2 to 6.");
            return;
        }

        // --------------------------------------------------------------------
        // Build the appropriate complex codetext object based on the mode
        // --------------------------------------------------------------------
        IComplexCodetext complexCodetext;

        if (mode == 2)
        {
            // Mode 2 includes postal code, country code, service category, and a second message
            var ct = new MaxiCodeCodetextMode2
            {
                PostalCode = postalCode,
                CountryCode = countryCode,
                ServiceCategory = serviceCategory,
                SecondMessage = new MaxiCodeStandardSecondMessage { Message = message }
            };
            complexCodetext = ct;
        }
        else if (mode == 3)
        {
            // Mode 3 is similar to mode 2 but with a different data layout
            var ct = new MaxiCodeCodetextMode3
            {
                PostalCode = postalCode,
                CountryCode = countryCode,
                ServiceCategory = serviceCategory,
                SecondMessage = new MaxiCodeStandardSecondMessage { Message = message }
            };
            complexCodetext = ct;
        }
        else
        {
            // Modes 4, 5, and 6 use the standard codetext structure
            var ct = new MaxiCodeStandardCodetext
            {
                Mode = (MaxiCodeMode)mode,
                Message = message
            };
            complexCodetext = ct;
        }

        // --------------------------------------------------------------------
        // Generate the MaxiCode barcode image and output it as Base64 PNG
        // --------------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(complexCodetext))
        {
            // Optional: set image resolution (dots per inch)
            generator.Parameters.Resolution = 300f;

            using (var ms = new MemoryStream())
            {
                // Save the barcode to a memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the image bytes to a Base64 string for easy display/transmission
                byte[] imageBytes = ms.ToArray();
                string base64 = Convert.ToBase64String(imageBytes);
                Console.WriteLine(base64);
            }
        }
    }
}