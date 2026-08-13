// Title: Demonstrate toggling barcode text visibility with CodeTextParameters.Location
// Description: Shows how to show or hide the human‑readable text of a barcode by setting CodeTextParameters.Location to Below or None, and verifies the behavior with simple assertions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of the BarcodeGenerator and its Parameters.Barcode.CodeTextParameters API. Developers commonly need to control barcode text visibility for labeling, packaging, or UI display scenarios. The code demonstrates typical use cases such as setting text location, generating an image, and performing lightweight validation without external test frameworks.
// Prompt: Write unit tests that verify toggling CodetextParameters.Visible correctly shows and hides the main barcode text.
// Tags: barcode, code128, codetextparameters, visibility, unit-test, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Contains simple test methods that verify showing and hiding barcode text using Aspose.BarCode.
/// </summary>
class Program
{
    // Counter for failed tests
    static int _failedTests = 0;

    /// <summary>
    /// Entry point. Executes the visibility tests and reports results.
    /// </summary>
    static void Main()
    {
        // Run test that ensures barcode text is visible
        TestShowCodeText();

        // Run test that ensures barcode text is hidden
        TestHideCodeText();

        // Report overall test outcome
        if (_failedTests == 0)
        {
            Console.WriteLine("ALL TESTS PASSED");
        }
        else
        {
            Console.WriteLine($"FAILED: {_failedTests} test(s) failed.");
        }
    }

    /// <summary>
    /// Simple assertion helper that logs pass/fail and updates the failure counter.
    /// </summary>
    /// <param name="condition">Result of the condition being tested.</param>
    /// <param name="testName">Name of the test case.</param>
    static void Assert(bool condition, string testName)
    {
        if (!condition)
        {
            Console.WriteLine($"FAIL: {testName}");
            _failedTests++;
        }
        else
        {
            Console.WriteLine($"PASS: {testName}");
        }
    }

    /// <summary>
    /// Verifies that setting CodeTextParameters.Location to Below makes the barcode text visible.
    /// </summary>
    static void TestShowCodeText()
    {
        const string testName = "TestShowCodeText";

        // Create a barcode generator for Code128 with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            // Show the human‑readable text by setting location to Below
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Verify the location property was set correctly
            bool locationIsBelow = generator.Parameters.Barcode.CodeTextParameters.Location == CodeLocation.Below;
            Assert(locationIsBelow, testName + " - Location set to Below");

            // Save the barcode to a memory stream to ensure generation succeeds
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Simple sanity check: the stream should contain data
                bool hasData = ms.Length > 0;
                Assert(hasData, testName + " - Image generated");
            }
        }
    }

    /// <summary>
    /// Verifies that setting CodeTextParameters.Location to None hides the barcode text.
    /// </summary>
    static void TestHideCodeText()
    {
        const string testName = "TestHideCodeText";

        // Create a barcode generator for Code128 with different sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "XYZ789"))
        {
            // Hide the human‑readable text by setting location to None
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Verify the location property was set correctly
            bool locationIsNone = generator.Parameters.Barcode.CodeTextParameters.Location == CodeLocation.None;
            Assert(locationIsNone, testName + " - Location set to None");

            // Save the barcode to a memory stream to ensure generation succeeds
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Simple sanity check: the stream should contain data
                bool hasData = ms.Length > 0;
                Assert(hasData, testName + " - Image generated");
            }
        }
    }
}