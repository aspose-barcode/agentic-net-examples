// Title: Dynamic MaxiCode Mode Selection Based on Input Parameter
// Description: Demonstrates how to set the MaxiCode barcode mode at runtime using a command‑line argument that simulates an API request.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with MaxiCodeCodetext classes and the MaxiCodeParameters.Mode property. Typical scenarios include shipping label creation, parcel tracking, and logistics applications where different MaxiCode modes (Mode 2, Mode 3) are required. Developers often need to choose the appropriate mode dynamically based on external data such as API request parameters.
// Prompt: Configure MaxiCodeParameters.Mode property dynamically based on an API request input parameter.
// Tags: maxicode, barcode, generation, complexbarcode, aspnet, c#, aspose.barcode, mode selection

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Generates a MaxiCode barcode with a mode selected at runtime based on a supplied argument.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Accepts an optional command‑line argument that determines the MaxiCode mode.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument represents the desired MaxiCode mode (e.g., "2" or "3").</param>
    static void Main(string[] args)
    {
        // ------------------------------------------------------------
        // Simulate receiving an API request parameter that specifies the MaxiCode mode.
        // If no argument is provided, default to Mode 3.
        // ------------------------------------------------------------
        string modeParam = args.Length > 0 ? args[0] : "3";

        // ------------------------------------------------------------
        // Map the string parameter to the corresponding MaxiCodeMode enum value.
        // ------------------------------------------------------------
        MaxiCodeMode selectedMode;
        if (modeParam == "2")
            selectedMode = MaxiCodeMode.Mode2;
        else if (modeParam == "3")
            selectedMode = MaxiCodeMode.Mode3;
        else
        {
            Console.WriteLine("Unsupported mode parameter. Defaulting to Mode3.");
            selectedMode = MaxiCodeMode.Mode3;
        }

        // ------------------------------------------------------------
        // Build the appropriate codetext object for the selected mode.
        // ------------------------------------------------------------
        IComplexCodetext codetext;
        if (selectedMode == MaxiCodeMode.Mode2)
        {
            // Mode 2 requires a 9‑digit postal code and supports a structured second message.
            var ct = new MaxiCodeCodetextMode2
            {
                PostalCode = "524032140", // 9 digits required for Mode2
                CountryCode = 56,
                ServiceCategory = 999
            };

            var second = new MaxiCodeStructuredSecondMessage();
            second.Add("634 ALPHA DRIVE");
            second.Add("PITTSBURGH");
            second.Add("PA");
            second.Year = 99;
            ct.SecondMessage = second;

            codetext = ct;
        }
        else // Mode3
        {
            // Mode 3 uses an alphanumeric postal code and a simple second message.
            var ct = new MaxiCodeCodetextMode3
            {
                PostalCode = "B1050",
                CountryCode = 56,
                ServiceCategory = 999
            };

            var second = new MaxiCodeStandardSecondMessage
            {
                Message = "Second message"
            };
            ct.SecondMessage = second;

            codetext = ct;
        }

        // ------------------------------------------------------------
        // Generate the barcode image using the selected mode and save it to a temporary file.
        // ------------------------------------------------------------
        string outputPath = Path.Combine(Path.GetTempPath(), $"MaxiCode_Mode{selectedMode}.png");
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            // Apply the dynamically determined mode to the generator parameters.
            generator.Parameters.Barcode.MaxiCode.Mode = selectedMode;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Inform the user where the barcode image has been saved.
        // ------------------------------------------------------------
        Console.WriteLine($"MaxiCode barcode generated with mode {selectedMode} at:");
        Console.WriteLine(outputPath);
    }
}