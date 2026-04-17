using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create a sample MaxiCode Mode 3 codetext
        var maxiCode = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050A",          // 6 alphanumeric characters
            CountryCode = 56,               // 3‑digit country code
            ServiceCategory = 999           // 3‑digit service category
        };

        // Use a standard second message
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample message"
        };
        maxiCode.SecondMessage = secondMessage;

        // Validate fields before generation
        ValidateMaxiCodeMode3(maxiCode);

        // Generate the barcode and save to a PNG file
        using (var generator = new ComplexBarcodeGenerator(maxiCode))
        {
            generator.GenerateBarCodeImage();

            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                File.WriteAllBytes("maxicode_mode3.png", ms.ToArray());
            }
        }

        Console.WriteLine("MaxiCode Mode 3 barcode generated successfully.");
    }

    static void ValidateMaxiCodeMode3(MaxiCodeCodetextMode3 codetext)
    {
        if (codetext == null)
            throw new ArgumentNullException(nameof(codetext));

        // PostalCode must be exactly 6 alphanumeric characters
        if (string.IsNullOrEmpty(codetext.PostalCode) ||
            !Regex.IsMatch(codetext.PostalCode, @"^[A-Za-z0-9]{6}$"))
        {
            throw new ArgumentException("PostalCode must be exactly 6 alphanumeric characters for MaxiCode Mode 3.");
        }

        // CountryCode must be a 3‑digit number (0‑999)
        if (codetext.CountryCode < 0 || codetext.CountryCode > 999)
        {
            throw new ArgumentOutOfRangeException(nameof(codetext.CountryCode), "CountryCode must be between 0 and 999.");
        }

        // ServiceCategory must be a 3‑digit number (0‑999)
        if (codetext.ServiceCategory < 0 || codetext.ServiceCategory > 999)
        {
            throw new ArgumentOutOfRangeException(nameof(codetext.ServiceCategory), "ServiceCategory must be between 0 and 999.");
        }

        // SecondMessage must be provided
        if (codetext.SecondMessage == null)
        {
            throw new ArgumentException("SecondMessage cannot be null for MaxiCode Mode 3.");
        }
    }
}