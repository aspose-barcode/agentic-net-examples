// Title: Read Barcodes from Image Files via Command Line
// Description: The console app iterates over image file paths supplied as command‑line arguments, detects any barcodes using Aspose.BarCode, and prints their type and value.
// Prompt: Develop a console application that reads barcodes from a list of file paths supplied via command line.
// Tags: barcode, read, console, aspose.barcode, decode, file-io

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to read barcodes from image files whose paths are provided via command‑line arguments.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Processes each supplied file path, attempts to read any barcodes,
    /// and writes the results to the console.
    /// </summary>
    /// <param name="args">Array of file paths passed as command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Build a list of file paths: use command‑line arguments if present, otherwise fall back to sample names.
        var filePaths = new List<string>();
        if (args != null && args.Length > 0)
        {
            filePaths.AddRange(args);
        }
        else
        {
            // Sample file names – the program will report if they are missing.
            filePaths.Add("sample1.png");
            filePaths.Add("sample2.png");
        }

        // Process each file path individually.
        foreach (var path in filePaths)
        {
            // Verify that the file exists before attempting to read it.
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            try
            {
                // Create a BarCodeReader that scans for all supported barcode types in the image.
                using (var reader = new BarCodeReader(path, DecodeType.AllSupportedTypes))
                {
                    // Read all barcodes found in the image.
                    var results = reader.ReadBarCodes();

                    // If no barcodes were detected, inform the user.
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcodes detected in file: {path}");
                    }
                    else
                    {
                        // Output each detected barcode's type and decoded text.
                        foreach (var result in results)
                        {
                            Console.WriteLine($"File: {path} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Report any unexpected errors that occur during processing.
                Console.WriteLine($"Error processing file '{path}': {ex.Message}");
            }
        }
    }
}