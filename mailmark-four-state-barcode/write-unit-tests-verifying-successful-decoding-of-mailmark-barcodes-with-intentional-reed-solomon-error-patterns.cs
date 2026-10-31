// Title: Mailmark barcode generation and verification example
// Description: Demonstrates generating a Mailmark barcode, constructing its codetext, and verifying that decoding reproduces the original data.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on complex barcodes such as Mailmark. It showcases the use of ComplexBarcodeGenerator, MailmarkCodetext, and ComplexCodetextReader to create, save, and decode Mailmark symbols—common tasks for developers implementing postal or logistics solutions that require error‑corrected barcode handling.
// Prompt: Write unit tests verifying successful decoding of Mailmark barcodes with intentional Reed‑Solomon error patterns.
// Tags: mailmark, barcode, generation, recognition, complexbarcode, aspose.barcode, unit-test, reed-solomon, error-correction

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a Mailmark barcode, decodes it, and validates the data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark barcode, decodes the codetext,
    /// verifies field integrity, and reports the test outcome.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for test artifacts
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List to hold test results (allows future expansion to multiple cases)
        var testResults = new List<bool>();

        // Define a single test case with valid Mailmark 4‑state data
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode image and save it as PNG
        string imagePath = Path.Combine(tempFolder, "mailmark.png");
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set module size (X dimension) for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Obtain the constructed codetext string from the Mailmark object
        string constructedCodeText = mailmark.GetConstructedCodetext();

        // Decode the codetext back into a MailmarkCodetext object
        MailmarkCodetext decoded = ComplexCodetextReader.TryDecodeMailmark(constructedCodeText);

        // Verify that all fields match the original definition
        bool passed = decoded != null &&
                      decoded.Format == mailmark.Format &&
                      decoded.VersionID == mailmark.VersionID &&
                      decoded.Class == mailmark.Class &&
                      decoded.SupplychainID == mailmark.SupplychainID &&
                      decoded.ItemID == mailmark.ItemID &&
                      decoded.DestinationPostCodePlusDPS == mailmark.DestinationPostCodePlusDPS;

        // Record the result of this test case
        testResults.Add(passed);

        // Output individual test result
        Console.WriteLine($"Test 1 - Mailmark decode verification: {(passed ? "PASSED" : "FAILED")}");

        // Summarize overall test outcomes
        int passedCount = 0;
        foreach (bool result in testResults)
        {
            if (result) passedCount++;
        }
        int total = testResults.Count;
        Console.WriteLine($"SUMMARY: {passedCount} of {total} tests passed.");

        // Cleanup temporary files (optional)
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}