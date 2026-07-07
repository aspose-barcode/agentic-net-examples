// Title: Generate MaxiCode barcode with concatenated secondary messages
// Description: Demonstrates how to use MaxiCodeCodetext helper to combine multiple secondary messages into a single unstructured field and generate a MaxiCode barcode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on MaxiCode symbology. It showcases the use of MaxiCodeCodetextMode2, MaxiCodeStandardSecondMessage, and ComplexBarcodeGenerator classes to create postal‑oriented MaxiCode barcodes. Developers often need to embed additional data such as secondary messages, postal codes, and service categories when generating MaxiCode for shipping and logistics applications.
// Prompt: Use the MaxiCodeCodetext helper to concatenate multiple secondary messages into a single unstructured field.
// Tags: maxicode, secondary messages, concatenation, complex barcode, generation, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a MaxiCode barcode (Mode 2) with concatenated secondary messages using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Concatenates secondary messages, builds MaxiCode codetext, and saves the barcode as PNG.
    /// </summary>
    static void Main()
    {
        // Sample secondary messages to be concatenated
        string[] secondaryMessages = { "Item A", "Item B", "Item C" };

        // Concatenate messages into a single unstructured string (space‑separated)
        string combinedMessage = string.Join(" ", secondaryMessages);

        // Create MaxiCode codetext for Mode 2 (postal info + data)
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit US postal code
            CountryCode = 056,          // USA numeric country code
            ServiceCategory = 999       // Example service category
        };

        // Assign the concatenated message as a standard (unstructured) second message
        var standardSecondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = combinedMessage
        };
        maxiCodeCodetext.SecondMessage = standardSecondMessage;

        // Generate the MaxiCode barcode and save it to a PNG file
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                File.WriteAllBytes("maxicode.png", memoryStream.ToArray());
            }
        }

        Console.WriteLine("MaxiCode barcode generated and saved as 'maxicode.png'.");
    }
}