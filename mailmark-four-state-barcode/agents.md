---
name: mailmark-four-state-barcode
description: C# examples for Mailmark Four State Barcode using Aspose.BarCode for .NET
language: csharp
framework: net9.0
parent: ../agents.md
---

# AGENTS - Mailmark Four State Barcode

## Persona

You are a C# developer specializing in barcode generation and recognition using Aspose.BarCode for .NET,
working within the **Mailmark Four State Barcode** category.
This folder contains standalone C# examples for Mailmark Four State Barcode operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Required Namespaces

- `using System;`
- `using System.IO;`
- `using Aspose.BarCode.ComplexBarcode;`
- `using Aspose.BarCode.Generation;`
- `using Aspose.BarCode.BarCodeRecognition;`

## Files in this folder

| File | Key APIs | Description |
|------|----------|-------------|
| [adjust-barcode-module-size-to-produce-compact-mailmark-barcode-suitable-for-small-labels.cs](./adjust-barcode-module-size-to-produce-compact-mailmark-barcode-suitable-for-small-labels.cs) |  | Adjust barcode module size to produce a compact Mailmark barcode suitable for small labels... |
| [batch-generate-mailmark-barcodes-from-customer-records-saving-each-as-separate-bmp-files.cs](./batch-generate-mailmark-barcodes-from-customer-records-saving-each-as-separate-bmp-files.cs) | `BarcodeGenerator` | Batch generate Mailmark barcodes from customer records, saving each as separate BMP files. |
| [compare-performance-of-generating-mailmark-barcodes-as-png-versus-jpeg-by-measuring-file-size-and-generation-time.cs](./compare-performance-of-generating-mailmark-barcodes-as-png-versus-jpeg-by-measuring-file-size-and-generation-time.cs) | `BarcodeGenerator` | Compare performance of generating Mailmark barcodes as PNG versus JPEG by measuring file s... |
| [configure-barcodereader-for-multi-threaded-processing-to-accelerate-decoding-of-large-mailmark-image-batches.cs](./configure-barcodereader-for-multi-threaded-processing-to-accelerate-decoding-of-large-mailmark-image-batches.cs) | `BarCodeReader` | Configure BarCodeReader for multi‑threaded processing to accelerate decoding of large Mail... |
| [configure-barcodereader-to-ignore-quiet-zones-while-decoding-mailmark-barcodes-in-densely-packed-images.cs](./configure-barcodereader-to-ignore-quiet-zones-while-decoding-mailmark-barcodes-in-densely-packed-images.cs) | `BarCodeReader` | Configure BarCodeReader to ignore quiet zones while decoding Mailmark barcodes in densely ... |
| [configure-complexbarcodegenerator-for-high-resolution-output-at-300-dpi-with-transparent-background.cs](./configure-complexbarcodegenerator-for-high-resolution-output-at-300-dpi-with-transparent-background.cs) | `ComplexBarcodeGenerator`, `BarcodeGenerator` | Configure ComplexBarcodeGenerator for high‑resolution output at 300 DPI with transparent b... |
| [create-function-accepting-raw-barcode-image-bytes-and-returning-populated-mailmarkcodetext-object.cs](./create-function-accepting-raw-barcode-image-bytes-and-returning-populated-mailmarkcodetext-object.cs) |  | Create a function accepting raw barcode image bytes and returning a populated MailmarkCode... |
| [create-helper-class-abstracting-mailmark-barcode-generation-exposing-methods-to-set-individual-data-fields.cs](./create-helper-class-abstracting-mailmark-barcode-generation-exposing-methods-to-set-individual-data-fields.cs) | `BarcodeGenerator` | Create a helper class abstracting Mailmark barcode generation, exposing methods to set ind... |
| [create-powershell-script-invoking-net-library-to-batch-convert-csv-rows-into-individual-mailmark-barcode-images.cs](./create-powershell-script-invoking-net-library-to-batch-convert-csv-rows-into-individual-mailmark-barcode-images.cs) |  | Create a PowerShell script invoking the .NET library to batch convert CSV rows into indivi... |
| [develop-console-app-that-reads-multiple-tiff-files-extracts-mailmark-barcodes-and-logs-decoded-fields.cs](./develop-console-app-that-reads-multiple-tiff-files-extracts-mailmark-barcodes-and-logs-decoded-fields.cs) | `BarCodeReader` | Develop a console app that reads multiple TIFF files, extracts Mailmark barcodes, and logs... |
| [develop-rest-endpoint-receiving-json-mailmark-fields-and-returning-generated-barcode-as-base64-string.cs](./develop-rest-endpoint-receiving-json-mailmark-fields-and-returning-generated-barcode-as-base64-string.cs) | `BarcodeGenerator` | Develop a REST endpoint receiving JSON Mailmark fields and returning the generated barcode... |
| [enable-barcode-rotation-by-setting-complexbarcodegenerator-rotationangle-property-before-generating-image.cs](./enable-barcode-rotation-by-setting-complexbarcodegenerator-rotationangle-property-before-generating-image.cs) | `ComplexBarcodeGenerator`, `BarcodeGenerator` | Enable barcode rotation by setting ComplexBarcodeGenerator RotationAngle property before g... |
| [generate-mailmark-4-state-barcode-with-complexbarcodegenerator-and-save-as-png.cs](./generate-mailmark-4-state-barcode-with-complexbarcodegenerator-and-save-as-png.cs) | `ComplexBarcodeGenerator`, `MailmarkCodetext`, `BarcodeGenerator` | Generate a Mailmark 4‑state barcode with ComplexBarcodeGenerator and save as PNG. |
| [generate-mailmark-barcode-into-memorystream-and-return-image-bytes-to-web-api.cs](./generate-mailmark-barcode-into-memorystream-and-return-image-bytes-to-web-api.cs) | `BarcodeGenerator` | Generate a Mailmark barcode into a MemoryStream and return image bytes to a web API. |
| [generate-mailmark-barcode-with-custom-margins-to-ensure-sufficient-white-space-around-symbol.cs](./generate-mailmark-barcode-with-custom-margins-to-ensure-sufficient-white-space-around-symbol.cs) | `BarcodeGenerator` | Generate a Mailmark barcode with custom margins to ensure sufficient white space around th... |
| [implement-asynchronous-barcode-generation-with-taskrun-to-prevent-ui-thread-blocking-during-complex-mailmark-creation.cs](./implement-asynchronous-barcode-generation-with-taskrun-to-prevent-ui-thread-blocking-during-complex-mailmark-creation.cs) | `BarcodeGenerator` | Implement asynchronous barcode generation with Task.Run to prevent UI thread blocking duri... |
| [implement-error-handling-to-catch-exceptions-when-barcode-generation-fails-due-to-invalid-codetext-characters.cs](./implement-error-handling-to-catch-exceptions-when-barcode-generation-fails-due-to-invalid-codetext-characters.cs) | `BarcodeGenerator` | Implement error handling to catch exceptions when barcode generation fails due to invalid ... |
| [implement-logging-of-decoding-failures-capturing-raw-image-path-and-exception-details-for-mailmark-troubleshooting.cs](./implement-logging-of-decoding-failures-capturing-raw-image-path-and-exception-details-for-mailmark-troubleshooting.cs) |  | Implement logging of decoding failures, capturing raw image path and exception details for... |
| [instantiate-mailmarkcodetext-and-set-service-type-routing-code-and-customer-reference.cs](./instantiate-mailmarkcodetext-and-set-service-type-routing-code-and-customer-reference.cs) |  | Instantiate a MailmarkCodetext and set service type, routing code, and customer reference. |
| [integrate-mailmark-barcode-generation-into-aspnet-mvc-view-rendering-image-directly-via-data-uri.cs](./integrate-mailmark-barcode-generation-into-aspnet-mvc-view-rendering-image-directly-via-data-uri.cs) | `BarcodeGenerator` | Integrate Mailmark barcode generation into an ASP.NET MVC view, rendering the image direct... |
| [load-barcode-image-from-network-stream-decode-it-and-verify-service-type-matches-expected-value.cs](./load-barcode-image-from-network-stream-decode-it-and-verify-service-type-matches-expected-value.cs) |  | Load a barcode image from a network stream, decode it, and verify service type matches exp... |
| [parse-raw-codetext-from-barcodereader-using-complexcodetextreadertrydecodemailmark-to-retrieve-fields.cs](./parse-raw-codetext-from-barcodereader-using-complexcodetextreadertrydecodemailmark-to-retrieve-fields.cs) | `ComplexCodetextReader`, `BarCodeReader` | Parse raw CodeText from BarCodeReader using ComplexCodetextReader.TryDecodeMailmark to ret... |
| [read-mailmark-barcode-from-jpeg-stream-using-barcodereader-with-decodetypemailmark.cs](./read-mailmark-barcode-from-jpeg-stream-using-barcodereader-with-decodetypemailmark.cs) | `DecodeType`, `BarCodeReader` | Read a Mailmark barcode from a JPEG stream using BarCodeReader with DecodeType.Mailmark. |
| [set-barcode-foreground-to-blue-and-background-to-white-before-generating-image.cs](./set-barcode-foreground-to-blue-and-background-to-white-before-generating-image.cs) | `BarcodeGenerator` | Set barcode foreground to blue and background to white before generating the image. |
| [set-complexbarcodegenerator-errorcorrectionlevel-to-maximum-to-boost-reed-solomon-redundancy-for-low-quality-prints.cs](./set-complexbarcodegenerator-errorcorrectionlevel-to-maximum-to-boost-reed-solomon-redundancy-for-low-quality-prints.cs) | `ComplexBarcodeGenerator`, `BarcodeGenerator` | Set ComplexBarcodeGenerator ErrorCorrectionLevel to maximum to boost Reed‑Solomon redundan... |
| [test-barcode-readability-after-applying-gaussian-blur-to-generated-image-to-simulate-printing-defects.cs](./test-barcode-readability-after-applying-gaussian-blur-to-generated-image-to-simulate-printing-defects.cs) | `BarcodeGenerator` | Test barcode readability after applying Gaussian blur to the generated image to simulate p... |
| [use-complexbarcodegenerator-to-embed-mailmark-barcode-onto-existing-pdf-page-as-image-overlay.cs](./use-complexbarcodegenerator-to-embed-mailmark-barcode-onto-existing-pdf-page-as-image-overlay.cs) | `ComplexBarcodeGenerator`, `BarcodeGenerator` | Use ComplexBarcodeGenerator to embed a Mailmark barcode onto an existing PDF page as an im... |
| [validate-generated-mailmark-barcode-includes-automatically-calculated-checksum-by-inspecting-codetext-property.cs](./validate-generated-mailmark-barcode-includes-automatically-calculated-checksum-by-inspecting-codetext-property.cs) | `BarcodeGenerator` | Validate generated Mailmark barcode includes automatically calculated checksum by inspecti... |
| [write-unit-tests-verifying-successful-decoding-of-mailmark-barcodes-with-intentional-reed-solomon-error-patterns.cs](./write-unit-tests-verifying-successful-decoding-of-mailmark-barcodes-with-intentional-reed-solomon-error-patterns.cs) | `Unit` | Write unit tests verifying successful decoding of Mailmark barcodes with intentional Reed‑... |

## Category Statistics
- Total examples: 29
- Failed: 0
- Pass rate: 100.0%

## Key API Surface

- `ComplexBarcodeGenerator`
- `MailmarkCodetext`
- `ComplexCodetextReader`
- `BarCodeReader`
- `DecodeType`

## Failed Tasks

All tasks passed ✅

<!-- AUTOGENERATED:START -->
Updated: 2026-04-28 | Run: `20260428_044126` | Examples: 29
<!-- AUTOGENERATED:END -->
