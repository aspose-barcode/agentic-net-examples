---
name: mailmark-two-dimensional-barcode
description: C# examples for Mailmark Two Dimensional Barcode using Aspose.BarCode for .NET
language: csharp
framework: net9.0
parent: ../agents.md
---

# AGENTS - Mailmark Two Dimensional Barcode

## Persona

You are a C# developer specializing in barcode generation and recognition using Aspose.BarCode for .NET,
working within the **Mailmark Two Dimensional Barcode** category.
This folder contains standalone C# examples for Mailmark Two Dimensional Barcode operations.
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
| [adjust-generator-settings-to-produce-barcode-image-suitable-for-high-resolution-printing-on-labels.cs](./adjust-generator-settings-to-produce-barcode-image-suitable-for-high-resolution-printing-on-labels.cs) | `BarcodeGenerator` | Adjust generator settings to produce a barcode image suitable for high‑resolution printing... |
| [apply-custom-foreground-and-background-colors-to-barcode-image-using-generator-settings.cs](./apply-custom-foreground-and-background-colors-to-barcode-image-using-generator-settings.cs) | `BarcodeColorParameters`, `BarcodeGenerator` | Apply custom foreground and background colors to the barcode image using generator setting... |
| [batch-generate-mailmark-barcodes-from-csv-file-containing-multiple-rows-of-field-data.cs](./batch-generate-mailmark-barcodes-from-csv-file-containing-multiple-rows-of-field-data.cs) | `BarcodeGenerator` | Batch generate Mailmark barcodes from a CSV file containing multiple rows of field data. |
| [batch-read-multiple-mailmark-barcode-images-from-directory-and-output-their-decoded-data-to-csv.cs](./batch-read-multiple-mailmark-barcode-images-from-directory-and-output-their-decoded-data-to-csv.cs) | `BarCodeReader` | Batch read multiple Mailmark barcode images from a directory and output their decoded data... |
| [cache-generated-barcode-images-in-memory-to-avoid-redundant-regeneration-for-identical-field-sets.cs](./cache-generated-barcode-images-in-memory-to-avoid-redundant-regeneration-for-identical-field-sets.cs) | `BarcodeGenerator` | Cache generated barcode images in memory to avoid redundant regeneration for identical fie... |
| [compare-generation-time-and-image-size-between-mailmark-type-7-and-type-29-barcodes.cs](./compare-generation-time-and-image-size-between-mailmark-type-7-and-type-29-barcodes.cs) | `BarcodeGenerator` | Compare generation time and image size between Mailmark type 7 and type 29 barcodes. |
| [configure-complexbarcodegenerator-to-produce-300-dpi-png-image-with-custom-dimensions-for-printing.cs](./configure-complexbarcodegenerator-to-produce-300-dpi-png-image-with-custom-dimensions-for-printing.cs) | `ComplexBarcodeGenerator`, `BarcodeGenerator` | Configure ComplexBarcodeGenerator to produce a 300 dpi PNG image with custom dimensions fo... |
| [configure-complexbarcodegenerator-to-use-specific-module-size-for-higher-density-barcodes.cs](./configure-complexbarcodegenerator-to-use-specific-module-size-for-higher-density-barcodes.cs) | `ComplexBarcodeGenerator`, `BarcodeGenerator` | Configure ComplexBarcodeGenerator to use a specific module size for higher density barcode... |
| [create-console-application-that-prompts-users-for-mailmark-fields-and-saves-resulting-barcode-as-png.cs](./create-console-application-that-prompts-users-for-mailmark-fields-and-saves-resulting-barcode-as-png.cs) |  | Create a console application that prompts users for Mailmark fields and saves the resultin... |
| [create-mailmark2dcodetext-instance-and-assign-routing-service-and-customer-data-values.cs](./create-mailmark2dcodetext-instance-and-assign-routing-service-and-customer-data-values.cs) | `Mailmark2DCodetext` | Create a Mailmark2DCodetext instance and assign routing, service, and customer data values... |
| [deserialize-json-back-into-mailmark2dcodetext-instance-and-generate-corresponding-barcode.cs](./deserialize-json-back-into-mailmark2dcodetext-instance-and-generate-corresponding-barcode.cs) | `Mailmark2DCodetext`, `BarcodeGenerator` | Deserialize JSON back into a Mailmark2DCodetext instance and generate the corresponding ba... |
| [develop-reusable-helper-method-that-accepts-mailmark-fields-and-returns-generated-barcode-image-stream.cs](./develop-reusable-helper-method-that-accepts-mailmark-fields-and-returns-generated-barcode-image-stream.cs) | `BarcodeGenerator` | Develop a reusable helper method that accepts Mailmark fields and returns a generated barc... |
| [encode-customer-information-with-alternative-encoding-and-assess-its-impact-on-barcode-capacity.cs](./encode-customer-information-with-alternative-encoding-and-assess-its-impact-on-barcode-capacity.cs) |  | Encode customer information with an alternative encoding and assess its impact on barcode ... |
| [extract-individual-fields-such-as-routing-and-service-code-from-decoded-mailmark2dcodetext.cs](./extract-individual-fields-such-as-routing-and-service-code-from-decoded-mailmark2dcodetext.cs) | `Mailmark2DCodetext` | Extract individual fields such as routing and service code from the decoded Mailmark2DCode... |
| [generate-barcode-with-transparent-background-for-overlaying-on-other-graphics.cs](./generate-barcode-with-transparent-background-for-overlaying-on-other-graphics.cs) | `BarcodeGenerator` | Generate a barcode with a transparent background for overlaying on other graphics. |
| [generate-mailmark-type-7-barcode-image-using-specified-routing-and-service-code-fields.cs](./generate-mailmark-type-7-barcode-image-using-specified-routing-and-service-code-fields.cs) | `BarcodeGenerator` | Generate a Mailmark type 7 barcode image using specified routing and service code fields. |
| [handle-cases-where-customer-information-uses-unsupported-characters-by-logging-warning-and-skipping-generation.cs](./handle-cases-where-customer-information-uses-unsupported-characters-by-logging-warning-and-skipping-generation.cs) | `BarcodeGenerator` | Handle cases where customer information uses unsupported characters by logging a warning a... |
| [implement-dependency-injection-to-provide-barcode-generation-service-throughout-application.cs](./implement-dependency-injection-to-provide-barcode-generation-service-throughout-application.cs) | `BarcodeGenerator` | Implement dependency injection to provide a barcode generation service throughout the appl... |
| [implement-error-handling-to-manage-cases-where-barcode-decoding-fails-or-returns-incomplete-data.cs](./implement-error-handling-to-manage-cases-where-barcode-decoding-fails-or-returns-incomplete-data.cs) |  | Implement error handling to manage cases where barcode decoding fails or returns incomplet... |
| [implement-retry-mechanism-for-barcode-generation-when-transient-errors-occur-during-image-saving.cs](./implement-retry-mechanism-for-barcode-generation-when-transient-errors-occur-during-image-saving.cs) | `BarcodeGenerator` | Implement a retry mechanism for barcode generation when transient errors occur during imag... |
| [integrate-barcode-generation-into-aspnet-mvc-controller-and-return-image-as-fileresult.cs](./integrate-barcode-generation-into-aspnet-mvc-controller-and-return-image-as-fileresult.cs) | `BarcodeGenerator` | Integrate barcode generation into an ASP.NET MVC controller and return the image as a File... |
| [log-detailed-decoding-information-including-field-names-and-values-to-assist-troubleshooting.cs](./log-detailed-decoding-information-including-field-names-and-values-to-assist-troubleshooting.cs) |  | Log detailed decoding information, including field names and values, to assist troubleshoo... |
| [parallelize-barcode-generation-for-large-datasets-using-task-parallel-library-to-improve-performance.cs](./parallelize-barcode-generation-for-large-datasets-using-task-parallel-library-to-improve-performance.cs) | `BarcodeGenerator` | Parallelize barcode generation for large datasets using Task Parallel Library to improve p... |
| [read-mailmark-2d-barcode-from-image-file-using-barcodereader-with-decodetypedatamatrix.cs](./read-mailmark-2d-barcode-from-image-file-using-barcodereader-with-decodetypedatamatrix.cs) | `Mailmark2DCodetext`, `DataMatrixParameters`, `DecodeType`, `BarCodeReader` | Read a Mailmark 2D barcode from an image file using BarCodeReader with DecodeType.DataMatr... |
| [rotate-generated-mailmark-barcode-by-90-degrees-to-satisfy-specific-layout-requirements.cs](./rotate-generated-mailmark-barcode-by-90-degrees-to-satisfy-specific-layout-requirements.cs) | `BarcodeGenerator` | Rotate the generated Mailmark barcode by 90 degrees to satisfy specific layout requirement... |
| [save-generated-mailmark-barcode-as-jpeg-file-to-specified-output-folder.cs](./save-generated-mailmark-barcode-as-jpeg-file-to-specified-output-folder.cs) | `BarcodeGenerator` | Save the generated Mailmark barcode as a JPEG file to a specified output folder. |
| [serialize-mailmark2dcodetext-object-to-json-for-storage-and-later-reconstruction-in-applications.cs](./serialize-mailmark2dcodetext-object-to-json-for-storage-and-later-reconstruction-in-applications.cs) | `Mailmark2DCodetext` | Serialize a Mailmark2DCodetext object to JSON for storage and later reconstruction in appl... |
| [set-barcodereaderdecodetype-to-decodetypedatamatrix-before-invoking-read-method-on-image.cs](./set-barcodereaderdecodetype-to-decodetypedatamatrix-before-invoking-read-method-on-image.cs) | `DataMatrixParameters`, `DecodeType`, `BarCodeReader` | Set BarCodeReader.DecodeType to DecodeType.DataMatrix before invoking the Read method on t... |
| [stream-generated-barcode-directly-to-http-response-without-writing-to-disk.cs](./stream-generated-barcode-directly-to-http-response-without-writing-to-disk.cs) | `BarcodeGenerator` | Stream the generated barcode directly to an HTTP response without writing to disk. |
| [use-complexcodetextreadertrydecodemailmark2d-to-obtain-mailmark2dcodetext-object-from-decoded-result.cs](./use-complexcodetextreadertrydecodemailmark2d-to-obtain-mailmark2dcodetext-object-from-decoded-result.cs) | `ComplexCodetextReader`, `Mailmark2DCodetext`, `BarCodeResult` | Use ComplexCodetextReader.TryDecodeMailmark2D to obtain a Mailmark2DCodetext object from t... |
| [use-memorystream-to-hold-barcode-image-for-in-memory-processing-and-transmission.cs](./use-memorystream-to-hold-barcode-image-for-in-memory-processing-and-transmission.cs) |  | Use a MemoryStream to hold the barcode image for in‑memory processing and transmission. |
| [validate-that-all-non-customer-fields-conform-to-c40-character-set-before-generation.cs](./validate-that-all-non-customer-fields-conform-to-c40-character-set-before-generation.cs) | `BarcodeGenerator` | Validate that all non‑customer fields conform to the C40 character set before generation. |
| [validate-that-customer-data-length-does-not-exceed-capacity-for-selected-mailmark-type.cs](./validate-that-customer-data-length-does-not-exceed-capacity-for-selected-mailmark-type.cs) |  | Validate that customer data length does not exceed capacity for the selected Mailmark type... |
| [write-unit-tests-that-verify-generated-barcodes-contain-exact-routing-and-service-code-values.cs](./write-unit-tests-that-verify-generated-barcodes-contain-exact-routing-and-service-code-values.cs) | `Unit`, `BarcodeGenerator` | Write unit tests that verify generated barcodes contain the exact routing and service code... |

## Category Statistics
- Total examples: 34
- Failed: 0
- Pass rate: 100.0%

## Key API Surface

- `ComplexBarcodeGenerator`
- `Mailmark2DCodetext`
- `ComplexCodetextReader`
- `BarCodeReader`
- `DecodeType`

## Failed Tasks

All tasks passed ✅

<!-- AUTOGENERATED:START -->
Updated: 2026-06-28 | Examples: 34
<!-- AUTOGENERATED:END -->
