// Title: 4‑State Mailmark Barcode Generation with Input Validation
// Description: Demonstrates validating alphanumeric input, converting it to uppercase, and encoding it into a 4‑state Mailmark barcode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on Mailmark (4‑state) symbology. It showcases the use of MailmarkCodetext and ComplexBarcodeGenerator classes to create barcode images, a common task for developers implementing postal or logistics solutions that require Mailmark encoding.
// Prompt: Validate alphanumeric input for a 4‑state barcode generator and ensure uppercase conversion before encoding.
// Tags: barcode, alphanumeric-validation, uppercase-conversion, mailmark, 4-state, complexbarcode, aspnet, image-output

using System;
using System.Text.RegularExpressions;
using Aspose.BarCode.ComplexBarcode;

namespace BarcodeExample
{
    /// <summary>
    /// Generates a Mailmark (4‑state) barcode after validating and formatting input.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Accepts optional command‑line input, validates it, formats it, and creates a Mailmark barcode image.
        /// </summary>
        /// <param name="args">Command‑line arguments; first argument is the raw input string.</param>
        static void Main(string[] args)
        {
            // Use command‑line argument if provided, otherwise a default sample.
            string rawInput = args.Length > 0 ? args[0] : "EF61AH8T";

            // Validate that the input contains only alphanumeric characters.
            if (!Regex.IsMatch(rawInput, @"^[A-Za-z0-9]+$"))
            {
                Console.WriteLine("Invalid input: only alphanumeric characters are allowed.");
                return;
            }

            // Convert to uppercase as required by the barcode specification.
            string upper = rawInput.ToUpperInvariant();

            // Pad/truncate to 8 characters and append the mandatory trailing space.
            string destination = upper.PadRight(8).Substring(0, 8) + " ";

            // Configure the Mailmark (4‑state) codetext.
            var mailmark = new MailmarkCodetext
            {
                Format = 4,                 // 4‑state format.
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = 16563762,
                DestinationPostCodePlusDPS = destination
            };

            // Generate and save the barcode image.
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                generator.Save("mailmark.png");
            }

            Console.WriteLine("Barcode generated: mailmark.png");
        }
    }
}