// Title: Verify routing and service codes in generated Code128 barcode
// Description: Demonstrates generating a Code128 barcode containing routing and service codes, then reading it back to confirm the exact values.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator and BarCodeReader classes to create and validate barcodes. Typical use cases include encoding custom data strings such as routing and service identifiers and verifying them programmatically. Developers often need unit‑testable code that confirms the encoded content matches expectations.
// Prompt: Write unit tests that verify generated barcodes contain the exact routing and service code values.
// Tags: code128, barcode generation, barcode recognition, routing code, service code, unit test

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode containing routing and service codes,
/// then reads the barcode to verify the encoded text matches the expected values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program. Executes the routing and service code verification test
    /// and outputs the result to the console.
    /// </summary>
    static void Main()
    {
        // Run the test and output the result.
        bool testResult = TestRoutingAndServiceCode();
        Console.WriteLine(testResult ? "Test Passed" : "Test Failed");
    }

    /// <summary>
    /// Generates a barcode with predefined routing and service codes, saves it to a file,
    /// reads it back, and verifies that the decoded text matches the expected concatenated value.
    /// </summary>
    /// <returns>True if the decoded barcode text matches the expected value; otherwise, false.</returns>
    static bool TestRoutingAndServiceCode()
    {
        // Define expected routing and service codes.
        string routingCode = "R12345";
        string serviceCode = "S67890";

        // Combine them into the barcode text (format can be adjusted as needed).
        string expectedCodeText = routingCode + serviceCode;

        // Prepare a temporary file path for the generated barcode image.
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "routing_service.png");

        // Ensure any previous file is removed.
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }

        // Generate the barcode.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, expectedCodeText))
        {
            // Optional: set image size via AutoSizeMode.Interpolation to avoid manual dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Save the barcode image.
            generator.Save(imagePath);
        }

        // Verify that the file was created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return false;
        }

        // Read the barcode back and compare the decoded text.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Compare the decoded CodeText with the expected value.
                if (result.CodeText == expectedCodeText)
                {
                    // Success: exact routing and service codes are present.
                    return true;
                }
                else
                {
                    Console.WriteLine($"Decoded text mismatch. Expected: {expectedCodeText}, Got: {result.CodeText}");
                    return false;
                }
            }
        }

        // If no barcode was read, the test fails.
        Console.WriteLine("No barcode detected in the image.");
        return false;
    }
}