using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates creation, encoding, and decoding of MaxiCode Mode 2 codetext using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a MaxiCode Mode 2 codetext object and set the required fields.
        var maxiCodeCodetext = new MaxiCodeCodetextMode2();
        maxiCodeCodetext.PostalCode = "524032140";      // 9‑digit US postal code
        maxiCodeCodetext.CountryCode = 56;             // 3‑digit country code (leading zeros are optional)
        maxiCodeCodetext.ServiceCategory = 999;        // 3‑digit service category

        // Create and assign the standard second message.
        var secondMessage = new MaxiCodeStandardSecondMessage();
        secondMessage.Message = "Test message";
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Generate the formatted codetext string from the object.
        string generatedCodetext = maxiCodeCodetext.GetConstructedCodetext();

        // Decode the generated codetext back into a MaxiCode object.
        var decodedCodetext = ComplexCodetextReader.TryDecodeMaxiCode(MaxiCodeMode.Mode2, generatedCodetext) as MaxiCodeCodetextMode2;

        // Simple assertion helper to validate conditions.
        void Assert(bool condition, string message)
        {
            if (!condition)
            {
                Console.WriteLine("ASSERTION FAILED: " + message);
                Environment.Exit(1);
            }
        }

        // Verify that decoding succeeded and all fields match the original values.
        Assert(decodedCodetext != null, "Decoded codetext should not be null.");
        Assert(decodedCodetext.PostalCode == maxiCodeCodetext.PostalCode, "PostalCode mismatch.");
        Assert(decodedCodetext.CountryCode == maxiCodeCodetext.CountryCode, "CountryCode mismatch.");
        Assert(decodedCodetext.ServiceCategory == maxiCodeCodetext.ServiceCategory, "ServiceCategory mismatch.");

        // Verify the second message content.
        var decodedSecond = decodedCodetext.SecondMessage as MaxiCodeStandardSecondMessage;
        Assert(decodedSecond != null, "SecondMessage type mismatch.");
        Assert(decodedSecond.Message == secondMessage.Message, "SecondMessage content mismatch.");

        // If all assertions pass, the generated codetext matches the expected format.
        Console.WriteLine("MaxiCode Mode 2 codetext generation test passed.");
    }
}