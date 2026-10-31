// Title: Generate MaxiCode barcode with concatenated secondary messages
// Description: Demonstrates how to combine multiple secondary messages into a single unstructured field for a MaxiCode barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on MaxiCode symbology. It showcases the use of MaxiCodeCodetextMode3, MaxiCodeStandardSecondMessage, and ComplexBarcodeGenerator to create a barcode with custom postal, country, and service data, plus an unstructured secondary message. Developers often need to embed multiple text fragments into a single field for logistics and shipping applications.
// Prompt: Use the MaxiCodeCodetext helper to concatenate multiple secondary messages into a single unstructured field.
// Tags: maxicode, secondarymessage, concatenation, complexbarcode, aspnet, csharp, barcode generation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates creating a MaxiCode barcode (Mode 3) with concatenated secondary messages.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds the combined secondary message, configures MaxiCode codetext, generates and saves the barcode.
    /// </summary>
    static void Main()
    {
        // Define sample secondary messages to concatenate
        List<string> secondaryMessages = new List<string>
        {
            "First part of message",
            "Second part of message",
            "Third part of message"
        };

        // Concatenate all messages into a single unstructured string
        string combinedMessage = string.Concat(secondaryMessages);

        // Initialize MaxiCode codetext for Mode 3 with required fields
        var maxiCodeCodetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Assign the concatenated secondary message to the standard second message object
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = combinedMessage
        };
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Prepare the output directory and file path
        string outputFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo");
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, "MaxiCodeUnstructured.png");

        // Generate the MaxiCode barcode and save it to the specified path
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}