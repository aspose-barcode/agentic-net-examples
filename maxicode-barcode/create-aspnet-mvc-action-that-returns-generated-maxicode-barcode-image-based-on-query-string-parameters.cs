using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // Default values for a simple MaxiCode generation
        int mode = 2;                     // MaxiCode mode (2‑6)
        string postalCode = "524032140";  // 9‑digit for mode 2, 6‑char for mode 3
        int countryCode = 56;             // 3‑digit numeric country code
        int serviceCategory = 999;        // 3‑digit service category
        string message = "Test message";  // Second message (standard)

        // Override defaults with command‑line arguments when supplied
        if (args.Length > 0 && int.TryParse(args[0], out int parsedMode) && parsedMode >= 2 && parsedMode <= 6)
            mode = parsedMode;

        if (args.Length > 1)
            postalCode = args[1];

        if (args.Length > 2 && int.TryParse(args[2], out int parsedCountry))
            countryCode = parsedCountry;

        if (args.Length > 3 && int.TryParse(args[3], out int parsedService))
            serviceCategory = parsedService;

        if (args.Length > 4)
            message = args[4];

        // Build the appropriate MaxiCode codetext object based on the selected mode
        IComplexCodetext codetext;
        if (mode == 2)
        {
            var ct = new MaxiCodeCodetextMode2();
            ct.PostalCode = postalCode;
            ct.CountryCode = countryCode;
            ct.ServiceCategory = serviceCategory;
            var second = new MaxiCodeStandardSecondMessage { Message = message };
            ct.SecondMessage = second;
            codetext = ct;
        }
        else if (mode == 3)
        {
            var ct = new MaxiCodeCodetextMode3();
            ct.PostalCode = postalCode;
            ct.CountryCode = countryCode;
            ct.ServiceCategory = serviceCategory;
            var second = new MaxiCodeStandardSecondMessage { Message = message };
            ct.SecondMessage = second;
            codetext = ct;
        }
        else // modes 4, 5, 6 use the standard codetext class
        {
            var ct = new MaxiCodeStandardCodetext();
            ct.Mode = (MaxiCodeMode)mode;
            ct.Message = message;
            codetext = ct;
        }

        // Generate and save the barcode image
        string outputPath = "maxicode.png";
        try
        {
            using (var generator = new ComplexBarcodeGenerator(codetext))
            {
                // Save as PNG using the built‑in Save method
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"MaxiCode barcode (mode {mode}) saved to '{Path.GetFullPath(outputPath)}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating MaxiCode barcode: {ex.Message}");
        }
    }
}