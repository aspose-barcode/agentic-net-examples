---
name: maxicode-barcode
description: C# examples for MaxiCode Barcode using Aspose.BarCode for .NET
language: csharp
framework: net9.0
parent: ../agents.md
---

# AGENTS - MaxiCode Barcode

## Persona

You are a C# developer specializing in barcode generation and recognition using Aspose.BarCode for .NET,
working within the **MaxiCode Barcode** category.
This folder contains standalone C# examples for MaxiCode Barcode operations.
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
| [apply-custom-background-color-to-maxicode-barcode-and-verify-that-decoding-remains-successful.cs](./apply-custom-background-color-to-maxicode-barcode-and-verify-that-decoding-remains-successful.cs) | `BarcodeColorParameters` | Apply a custom background color to a MaxiCode barcode and verify that decoding remains suc... |
| [apply-custom-foreground-color-to-maxicode-mode-2-barcode-using-generator-s-forecolor-property.cs](./apply-custom-foreground-color-to-maxicode-mode-2-barcode-using-generator-s-forecolor-property.cs) | `MaxiCodeCodetextMode2`, `BarcodeColorParameters`, `BarcodeGenerator` | Apply a custom foreground color to a MaxiCode Mode 2 barcode using the generator's ForeCol... |
| [batch-decode-all-maxicode-png-files-in-directory-and-export-results-to-csv-report.cs](./batch-decode-all-maxicode-png-files-in-directory-and-export-results-to-csv-report.cs) | `BarCodeResult` | Batch decode all MaxiCode PNG files in a directory and export the results to a CSV report. |
| [configure-barcodereader-to-decode-maxicode-images-from-byte-array-and-retrieve-both-primary-and-secondary-messages.cs](./configure-barcodereader-to-decode-maxicode-images-from-byte-array-and-retrieve-both-primary-and-secondary-messages.cs) | `BarCodeReader` | Configure BarcodeReader to decode MaxiCode images from a byte array and retrieve both prim... |
| [configure-barcodereader-to-ignore-checksum-errors-while-decoding-maxicode-barcodes-in-high-noise-environment.cs](./configure-barcodereader-to-ignore-checksum-errors-while-decoding-maxicode-barcodes-in-high-noise-environment.cs) | `BarCodeReader` | Configure BarcodeReader to ignore checksum errors while decoding MaxiCode barcodes in a hi... |
| [create-aspnet-mvc-action-that-returns-generated-maxicode-barcode-image-based-on-query-string-parameters.cs](./create-aspnet-mvc-action-that-returns-generated-maxicode-barcode-image-based-on-query-string-parameters.cs) | `BarcodeGenerator` | Create an ASP.NET MVC action that returns a generated MaxiCode barcode image based on quer... |
| [create-console-utility-that-reads-list-of-codetext-strings-and-outputs-corresponding-maxicode-png-files.cs](./create-console-utility-that-reads-list-of-codetext-strings-and-outputs-corresponding-maxicode-png-files.cs) |  | Create a console utility that reads a list of codetext strings and outputs corresponding M... |
| [create-helper-method-that-builds-maxicode-structured-secondary-messages-from-address-components.cs](./create-helper-method-that-builds-maxicode-structured-secondary-messages-from-address-components.cs) | `MaxiCodeStructuredCodetext` | Create a helper method that builds MaxiCode structured secondary messages from address com... |
| [create-maxicode-mode-3-barcode-using-structured-secondary-message-and-export-image-as-jpeg.cs](./create-maxicode-mode-3-barcode-using-structured-secondary-message-and-export-image-as-jpeg.cs) | `MaxiCodeCodetextMode3`, `MaxiCodeStructuredCodetext` | Create a MaxiCode Mode 3 barcode using a structured secondary message and export the image... |
| [create-maxicode-mode-6-barcode-apply-transparent-background-and-write-file-to-memory-stream.cs](./create-maxicode-mode-6-barcode-apply-transparent-background-and-write-file-to-memory-stream.cs) |  | Create a MaxiCode Mode 6 barcode, apply a transparent background, and write the file to a ... |
| [create-unit-test-that-verifies-generated-maxicode-mode-2-codetext-matches-expected-formatted-string.cs](./create-unit-test-that-verifies-generated-maxicode-mode-2-codetext-matches-expected-formatted-string.cs) | `MaxiCodeCodetextMode2`, `Unit`, `BarcodeGenerator` | Create a unit test that verifies the generated MaxiCode Mode 2 codetext matches the expect... |
| [develop-web-api-endpoint-that-accepts-json-builds-maxicode-mode-3-codetext-and-returns-png-data.cs](./develop-web-api-endpoint-that-accepts-json-builds-maxicode-mode-3-codetext-and-returns-png-data.cs) | `MaxiCodeCodetextMode3` | Develop a Web API endpoint that accepts JSON, builds a MaxiCode Mode 3 codetext, and retur... |
| [encode-numeric-postal-code-in-primary-message-of-maxicode-mode-2-and-verify-decoding-accuracy.cs](./encode-numeric-postal-code-in-primary-message-of-maxicode-mode-2-and-verify-decoding-accuracy.cs) | `MaxiCodeCodetextMode2` | Encode a numeric postal code in the primary message of a MaxiCode Mode 2 and verify decodi... |
| [export-generated-maxicode-barcode-as-base64-string-for-embedding-in-json-api-responses.cs](./export-generated-maxicode-barcode-as-base64-string-for-embedding-in-json-api-responses.cs) | `BarcodeGenerator` | Export a generated MaxiCode barcode as a base64 string for embedding in JSON API responses... |
| [generate-maxicode-barcode-with-custom-margin-of-10-pixels-on-all-sides-for-better-visual-separation.cs](./generate-maxicode-barcode-with-custom-margin-of-10-pixels-on-all-sides-for-better-visual-separation.cs) | `BarcodeGenerator` | Generate a MaxiCode barcode with a custom margin of 10 pixels on all sides for better visu... |
| [generate-maxicode-barcode-with-custom-quiet-zone-size-to-meet-specific-scanning-requirements.cs](./generate-maxicode-barcode-with-custom-quiet-zone-size-to-meet-specific-scanning-requirements.cs) | `BarcodeGenerator` | Generate a MaxiCode barcode with a custom quiet zone size to meet specific scanning requir... |
| [generate-maxicode-barcode-with-structured-secondary-message-containing-recipient-name-street-and-city-fields.cs](./generate-maxicode-barcode-with-structured-secondary-message-containing-recipient-name-street-and-city-fields.cs) | `MaxiCodeStructuredCodetext`, `BarcodeGenerator` | Generate a MaxiCode barcode with a structured secondary message containing recipient name,... |
| [generate-maxicode-mode-2-barcode-with-unstructured-secondary-message-and-save-it-as-png.cs](./generate-maxicode-mode-2-barcode-with-unstructured-secondary-message-and-save-it-as-png.cs) | `MaxiCodeCodetextMode2`, `MaxiCodeStructuredCodetext`, `BarcodeGenerator` | Generate a MaxiCode Mode 2 barcode with an unstructured secondary message and save it as P... |
| [generate-maxicode-mode-4-barcode-with-default-primary-data-and-store-result-in-bmp-format.cs](./generate-maxicode-mode-4-barcode-with-default-primary-data-and-store-result-in-bmp-format.cs) | `BarcodeGenerator` | Generate a MaxiCode Mode 4 barcode with default primary data and store the result in BMP f... |
| [implement-batch-generation-of-maxicode-mode-2-barcodes-from-csv-file-containing-primary-and-secondary-data-rows.cs](./implement-batch-generation-of-maxicode-mode-2-barcodes-from-csv-file-containing-primary-and-secondary-data-rows.cs) | `MaxiCodeCodetextMode2`, `BarcodeGenerator` | Implement batch generation of MaxiCode Mode 2 barcodes from a CSV file containing primary ... |
| [implement-error-handling-that-catches-barcodeexception-when-decoding-unreadable-maxicode-image.cs](./implement-error-handling-that-catches-barcodeexception-when-decoding-unreadable-maxicode-image.cs) |  | Implement error handling that catches BarcodeException when decoding an unreadable MaxiCod... |
| [implement-retry-mechanism-that-attempts-to-decode-maxicode-barcode-up-to-three-times-on-failure.cs](./implement-retry-mechanism-that-attempts-to-decode-maxicode-barcode-up-to-three-times-on-failure.cs) |  | Implement a retry mechanism that attempts to decode a MaxiCode barcode up to three times o... |
| [include-iso-country-identifier-in-secondary-structured-message-of-maxicode-mode-3-barcode.cs](./include-iso-country-identifier-in-secondary-structured-message-of-maxicode-mode-3-barcode.cs) | `MaxiCodeCodetextMode3`, `MaxiCodeStructuredCodetext` | Include an ISO country identifier in the secondary structured message of a MaxiCode Mode 3... |
| [produce-maxicode-mode-5-barcode-set-custom-image-width-and-height-and-save-it-as-tiff.cs](./produce-maxicode-mode-5-barcode-set-custom-image-width-and-height-and-save-it-as-tiff.cs) | `BarCodeImageParameters` | Produce a MaxiCode Mode 5 barcode, set custom image width and height, and save it as TIFF. |
| [rotate-generated-maxicode-barcode-by-90-degrees-and-save-rotated-image-as-gif.cs](./rotate-generated-maxicode-barcode-by-90-degrees-and-save-rotated-image-as-gif.cs) | `BarcodeGenerator` | Rotate a generated MaxiCode barcode by 90 degrees and save the rotated image as GIF. |
| [set-barcode-image-dpi-to-300-when-generating-maxicode-to-improve-print-quality.cs](./set-barcode-image-dpi-to-300-when-generating-maxicode-to-improve-print-quality.cs) | `BarcodeGenerator` | Set the barcode image DPI to 300 when generating a MaxiCode to improve print quality. |
| [set-generator-s-imageformat-property-to-gif-and-produce-series-of-maxicode-images.cs](./set-generator-s-imageformat-property-to-gif-and-produce-series-of-maxicode-images.cs) | `BarCodeImageFormat`, `BarcodeGenerator` | Set the generator's ImageFormat property to GIF and produce a series of MaxiCode images. |
| [set-generator-s-modulesize-property-to-2-to-produce-denser-maxicode-image-for-compact-labels.cs](./set-generator-s-modulesize-property-to-2-to-produce-denser-maxicode-image-for-compact-labels.cs) | `BarcodeGenerator` | Set the generator's ModuleSize property to 2 to produce a denser MaxiCode image for compac... |
| [upload-generated-maxicode-png-file-to-azure-blob-storage-using-azure-sdk-after-successful-creation.cs](./upload-generated-maxicode-png-file-to-azure-blob-storage-using-azure-sdk-after-successful-creation.cs) | `BarcodeGenerator` | Upload a generated MaxiCode PNG file to Azure Blob storage using the Azure SDK after succe... |
| [use-async-methods-to-generate-maxicode-image-and-write-it-to-file-without-blocking-ui.cs](./use-async-methods-to-generate-maxicode-image-and-write-it-to-file-without-blocking-ui.cs) | `BarcodeGenerator` | Use async methods to generate a MaxiCode image and write it to a file without blocking the... |
| [use-barcodereader-to-decode-maxicode-image-streamed-from-network-socket-without-saving-to-disk.cs](./use-barcodereader-to-decode-maxicode-image-streamed-from-network-socket-without-saving-to-disk.cs) | `BarCodeReader` | Use the BarcodeReader to decode a MaxiCode image streamed from a network socket without sa... |
| [use-maxicodecodetext-helper-to-concatenate-multiple-secondary-messages-into-single-unstructured-field.cs](./use-maxicodecodetext-helper-to-concatenate-multiple-secondary-messages-into-single-unstructured-field.cs) | `MaxiCodeStructuredCodetext` | Use the MaxiCodeCodetext helper to concatenate multiple secondary messages into a single u... |
| [use-maxicodecodetextmode2-helper-to-build-complex-primary-data-and-generate-barcode-image.cs](./use-maxicodecodetextmode2-helper-to-build-complex-primary-data-and-generate-barcode-image.cs) | `MaxiCodeCodetextMode2`, `BarcodeGenerator` | Use the MaxiCodeCodetextMode2 helper to build complex primary data and generate the barcod... |
| [validate-input-fields-for-maxicode-mode-3-using-provided-structured-codetext-classes-before-generation.cs](./validate-input-fields-for-maxicode-mode-3-using-provided-structured-codetext-classes-before-generation.cs) | `MaxiCodeCodetextMode3`, `MaxiCodeStructuredCodetext`, `BarcodeGenerator` | Validate input fields for MaxiCode Mode 3 using the provided structured codetext classes b... |

## Category Statistics
- Total examples: 34
- Failed: 0
- Pass rate: 100.0%

## Key API Surface

- `ComplexBarcodeGenerator`
- `MaxiCodeCodetextMode2`
- `MaxiCodeCodetextMode3`
- `MaxiCodeStructuredCodetext`
- `ComplexCodetextReader`

## Failed Tasks

All tasks passed ✅

<!-- AUTOGENERATED:START -->
Updated: 2026-04-28 | Run: `20260428_211204` | Examples: 34
<!-- AUTOGENERATED:END -->
