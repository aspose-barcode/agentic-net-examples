---
name: special-barcode-recognition-settings
description: C# examples for Special Barcode Recognition Settings using Aspose.BarCode for .NET
language: csharp
framework: net9.0
parent: ../agents.md
---

# AGENTS - Special Barcode Recognition Settings

## Persona

You are a C# developer specializing in barcode generation and recognition using Aspose.BarCode for .NET,
working within the **Special Barcode Recognition Settings** category.
This folder contains standalone C# examples for Special Barcode Recognition Settings operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Required Namespaces

- `using System;`
- `using System.IO;`
- `using Aspose.BarCode.BarCodeRecognition;`
- `using Aspose.BarCode.Generation;`

## Files in this folder

| File | Key APIs | Description |
|------|----------|-------------|
| [adjust-net-threadpool-minimum-threads-to-2-and-maximum-threads-to-8-before-creating-barcodereader-instances.cs](./adjust-net-threadpool-minimum-threads-to-2-and-maximum-threads-to-8-before-creating-barcodereader-instances.cs) | `BarCodeReader` | Adjust .NET ThreadPool minimum threads to 2 and maximum threads to 8 before creating BarCo... |
| [configure-processorsettingsuseallcores-true-to-allocate-all-cpu-cores-automatically-for-barcode-recognition.cs](./configure-processorsettingsuseallcores-true-to-allocate-all-cpu-cores-automatically-for-barcode-recognition.cs) |  | Configure ProcessorSettings.UseAllCores true to allocate all CPU cores automatically for b... |
| [create-background-worker-that-reads-barcodes-from-video-stream-using-processorsettings-for-optimal-core-usage.cs](./create-background-worker-that-reads-barcodes-from-video-stream-using-processorsettings-for-optimal-core-usage.cs) | `BarCodeReader` | Create a background worker that reads barcodes from a video stream using ProcessorSettings... |
| [create-batch-process-that-reads-multiple-images-with-stripfnc-false-to-keep-fnc-symbols.cs](./create-batch-process-that-reads-multiple-images-with-stripfnc-false-to-keep-fnc-symbols.cs) |  | Create a batch process that reads multiple images with StripFNC false to keep FNC symbols. |
| [create-batch-process-that-reads-multiple-images-with-stripfnc-true-to-strip-fnc-symbols.cs](./create-batch-process-that-reads-multiple-images-with-stripfnc-true-to-strip-fnc-symbols.cs) |  | Create a batch process that reads multiple images with StripFNC true to strip FNC symbols. |
| [create-multithreaded-barcode-scanner-that-processes-image-files-in-parallel-using-default-processorsettings.cs](./create-multithreaded-barcode-scanner-that-processes-image-files-in-parallel-using-default-processorsettings.cs) | `BarCodeReader` | Create a multithreaded barcode scanner that processes image files in parallel using defaul... |
| [create-sample-that-reads-batch-of-png-files-applying-australiapostsettingscustomerinformationinterpretingtypectable.cs](./create-sample-that-reads-batch-of-png-files-applying-australiapostsettingscustomerinformationinterpretingtypectable.cs) | `AustralianPostShortBarSectionsType` | Create a sample that reads a batch of PNG files applying AustraliaPostSettings.CustomerInf... |
| [create-sample-that-reads-batch-of-tiff-images-applying-australiapostsettingscustomerinformationinterpretingtypentable.cs](./create-sample-that-reads-batch-of-tiff-images-applying-australiapostsettingscustomerinformationinterpretingtypentable.cs) | `AustralianPostShortBarSectionsType`, `BarCodeImageFormat` | Create a sample that reads a batch of TIFF images applying AustraliaPostSettings.CustomerI... |
| [design-ui-component-allowing-users-to-toggle-stripfnc-and-view-real-time-decoding-results.cs](./design-ui-component-allowing-users-to-toggle-stripfnc-and-view-real-time-decoding-results.cs) |  | Design a UI component allowing users to toggle StripFNC and view real‑time decoding result... |
| [develop-batch-job-that-extracts-images-from-pdf-files-and-decodes-barcodes-with-multithreading-enabled.cs](./develop-batch-job-that-extracts-images-from-pdf-files-and-decodes-barcodes-with-multithreading-enabled.cs) |  | Develop a batch job that extracts images from PDF files and decodes barcodes with multithr... |
| [develop-console-application-that-decodes-all-barcodes-in-directory-with-stripfnc-false-and-prints-results.cs](./develop-console-application-that-decodes-all-barcodes-in-directory-with-stripfnc-false-and-prints-results.cs) | `BarCodeResult` | Develop a console application that decodes all barcodes in a directory with StripFNC false... |
| [develop-service-that-reads-australia-post-barcodes-from-network-share-and-applies-ignoreendingfillingpatternsforctable.cs](./develop-service-that-reads-australia-post-barcodes-from-network-share-and-applies-ignoreendingfillingpatternsforctable.cs) | `AustralianPostShortBarSectionsType`, `BarCodeReader` | Develop a service that reads Australia Post barcodes from a network share and applies Igno... |
| [develop-utility-that-converts-decoded-australia-post-barcode-data-to-json-using-custom-decoder.cs](./develop-utility-that-converts-decoded-australia-post-barcode-data-to-json-using-custom-decoder.cs) | `AustralianPostShortBarSectionsType` | Develop a utility that converts decoded Australia Post barcode data to JSON using a custom... |
| [develop-utility-that-converts-decoded-australia-post-barcode-data-to-xml-using-selected-interpreting-type.cs](./develop-utility-that-converts-decoded-australia-post-barcode-data-to-xml-using-selected-interpreting-type.cs) | `AustralianPostShortBarSectionsType` | Develop a utility that converts decoded Australia Post barcode data to XML using the selec... |
| [disable-multithreaded-barcode-reading-by-setting-processorsettingsuseallcores-false-and-useonlythiscorescount-to-1.cs](./disable-multithreaded-barcode-reading-by-setting-processorsettingsuseallcores-false-and-useonlythiscorescount-to-1.cs) | `BarCodeReader` | Disable multithreaded barcode reading by setting ProcessorSettings.UseAllCores false and U... |
| [enable-australiapostsettingsignoreendingfillingpatternsforctable-to-suppress-filler-z-symbols-in-ctable-mode.cs](./enable-australiapostsettingsignoreendingfillingpatternsforctable-to-suppress-filler-z-symbols-in-ctable-mode.cs) | `AustralianPostShortBarSectionsType` | Enable AustraliaPostSettings.IgnoreEndingFillingPatternsForCTable to suppress filler "z" s... |
| [implement-custom-class-inheriting-customerinformationdecoder-and-assign-it-to-australiapostsettingscustomdecoder.cs](./implement-custom-class-inheriting-customerinformationdecoder-and-assign-it-to-australiapostsettingscustomdecoder.cs) | `AustralianPostShortBarSectionsType` | Implement a custom class inheriting CustomerInformationDecoder and assign it to AustraliaP... |
| [implement-diagnostic-tool-that-reports-current-threadpool-thread-counts-before-and-after-barcode-processing.cs](./implement-diagnostic-tool-that-reports-current-threadpool-thread-counts-before-and-after-barcode-processing.cs) | `BarCodeReader` | Implement a diagnostic tool that reports current ThreadPool thread counts before and after... |
| [implement-error-handling-for-unsupported-barcode-types-when-stripfnc-is-true-and-fnc-symbols-are-present.cs](./implement-error-handling-for-unsupported-barcode-types-when-stripfnc-is-true-and-fnc-symbols-are-present.cs) |  | Implement error handling for unsupported barcode types when StripFNC is true and FNC symbo... |
| [implement-fallback-decoder-that-switches-to-single-thread-mode-if-multithreaded-processing-exceeds-memory-limits.cs](./implement-fallback-decoder-that-switches-to-single-thread-mode-if-multithreaded-processing-exceeds-memory-limits.cs) |  | Implement a fallback decoder that switches to single‑thread mode if multithreaded processi... |
| [implement-fallback-mechanism-that-retries-decoding-with-stripfnc-true-if-initial-attempt-with-false-fails.cs](./implement-fallback-mechanism-that-retries-decoding-with-stripfnc-true-if-initial-attempt-with-false-fails.cs) |  | Implement a fallback mechanism that retries decoding with StripFNC true if initial attempt... |
| [implement-feature-flag-that-enables-or-disables-multithreaded-barcode-reading-at-application-startup.cs](./implement-feature-flag-that-enables-or-disables-multithreaded-barcode-reading-at-application-startup.cs) | `BarCodeReader` | Implement a feature flag that enables or disables multithreaded barcode reading at applica... |
| [implement-logging-of-each-barcode-decoding-operation-indicating-whether-fnc-symbols-were-stripped-or-retained.cs](./implement-logging-of-each-barcode-decoding-operation-indicating-whether-fnc-symbols-were-stripped-or-retained.cs) |  | Implement logging of each barcode decoding operation, indicating whether FNC symbols were ... |
| [implement-wrapper-class-that-encapsulates-processorsettings-configuration-for-easy-reuse-across-projects.cs](./implement-wrapper-class-that-encapsulates-processorsettings-configuration-for-easy-reuse-across-projects.cs) |  | Implement a wrapper class that encapsulates ProcessorSettings configuration for easy reuse... |
| [instantiate-australiapostsettings-and-assign-it-to-barcodereaderrecognitionsettings-for-custom-australia-post-decoding.cs](./instantiate-australiapostsettings-and-assign-it-to-barcodereaderrecognitionsettings-for-custom-australia-post-decoding.cs) | `AustralianPostShortBarSectionsType`, `BarCodeReader` | Instantiate AustraliaPostSettings and assign it to BarCodeReader.RecognitionSettings for c... |
| [set-australiapostsettingscustomerinformationinterpretingtype-to-ctable-for-ctable-format-decoding-of-australia-post-barc.cs](./set-australiapostsettingscustomerinformationinterpretingtype-to-ctable-for-ctable-format-decoding-of-australia-post-barc.cs) | `AustralianPostShortBarSectionsType` | Set AustraliaPostSettings.CustomerInformationInterpretingType to CTable for CTable format ... |
| [set-australiapostsettingscustomerinformationinterpretingtype-to-ntable-for-ntable-format-decoding-of-australia-post-barc.cs](./set-australiapostsettingscustomerinformationinterpretingtype-to-ntable-for-ntable-format-decoding-of-australia-post-barc.cs) | `AustralianPostShortBarSectionsType` | Set AustraliaPostSettings.CustomerInformationInterpretingType to NTable for NTable format ... |
| [set-australiapostsettingscustomerinformationinterpretingtype-to-other-for-custom-decoding-of-australia-post-barcodes.cs](./set-australiapostsettingscustomerinformationinterpretingtype-to-other-for-custom-decoding-of-australia-post-barcodes.cs) | `AustralianPostShortBarSectionsType` | Set AustraliaPostSettings.CustomerInformationInterpretingType to Other for custom decoding... |
| [set-barcodereaderstripfnc-to-false-to-remove-fnc-symbols-from-decoded-results.cs](./set-barcodereaderstripfnc-to-false-to-remove-fnc-symbols-from-decoded-results.cs) | `BarCodeReader`, `BarCodeResult` | Set BarCodeReader.StripFNC to false to remove FNC symbols from decoded results. |
| [set-barcodereaderstripfnc-to-true-to-retain-fnc-symbols-in-decoded-results.cs](./set-barcodereaderstripfnc-to-true-to-retain-fnc-symbols-in-decoded-results.cs) | `BarCodeReader`, `BarCodeResult` | Set BarCodeReader.StripFNC to true to retain FNC symbols in decoded results. |
| [set-processorsettingsmaxadditionalallowedthreads-to-2-to-cap-extra-worker-threads-for-controlled-multithreading.cs](./set-processorsettingsmaxadditionalallowedthreads-to-2-to-cap-extra-worker-threads-for-controlled-multithreading.cs) |  | Set ProcessorSettings.MaxAdditionalAllowedThreads to 2 to cap extra worker threads for con... |
| [set-processorsettingsuseonlythiscorescount-to-4-to-restrict-barcode-recognition-to-four-cpu-cores.cs](./set-processorsettingsuseonlythiscorescount-to-4-to-restrict-barcode-recognition-to-four-cpu-cores.cs) |  | Set ProcessorSettings.UseOnlyThisCoresCount to 4 to restrict barcode recognition to four C... |
| [write-benchmark-comparing-decoding-speed-when-processorsettingsuseallcores-is-true-versus-false.cs](./write-benchmark-comparing-decoding-speed-when-processorsettingsuseallcores-is-true-versus-false.cs) |  | Write a benchmark comparing decoding speed when ProcessorSettings.UseAllCores is true vers... |
| [write-benchmark-comparing-performance-when-processorsettingsmaxadditionalallowedthreads-is-zero-single-thread-versus-gre.cs](./write-benchmark-comparing-performance-when-processorsettingsmaxadditionalallowedthreads-is-zero-single-thread-versus-gre.cs) |  | Write a benchmark comparing performance when ProcessorSettings.MaxAdditionalAllowedThreads... |
| [write-code-that-records-cpu-usage-statistics-while-processorsettingsuseallcores-processes-large-image-set.cs](./write-code-that-records-cpu-usage-statistics-while-processorsettingsuseallcores-processes-large-image-set.cs) |  | Write code that records CPU usage statistics while ProcessorSettings.UseAllCores processes... |
| [write-code-that-switches-australiapostsettingscustomerinformationinterpretingtype-at-runtime-based-on-user-selection.cs](./write-code-that-switches-australiapostsettingscustomerinformationinterpretingtype-at-runtime-based-on-user-selection.cs) | `AustralianPostShortBarSectionsType` | Write code that switches AustraliaPostSettings.CustomerInformationInterpretingType at runt... |
| [write-configuration-loader-that-reads-processorsettings-values-from-json-file-at-application-startup.cs](./write-configuration-loader-that-reads-processorsettings-values-from-json-file-at-application-startup.cs) |  | Write a configuration loader that reads ProcessorSettings values from a JSON file at appli... |
| [write-helper-method-that-configures-threadpoolsetminthreads-based-on-number-of-barcode-files-to-process.cs](./write-helper-method-that-configures-threadpoolsetminthreads-based-on-number-of-barcode-files-to-process.cs) | `BarCodeReader` | Write a helper method that configures ThreadPool.SetMinThreads based on the number of barc... |
| [write-script-that-generates-synthetic-barcode-images-containing-embedded-fnc-symbols-for-testing-stripfnc-behavior.cs](./write-script-that-generates-synthetic-barcode-images-containing-embedded-fnc-symbols-for-testing-stripfnc-behavior.cs) | `BarcodeGenerator` | Write a script that generates synthetic barcode images containing embedded FNC symbols for... |
| [write-script-that-processes-list-of-image-paths-in-parallel-using-tpl-while-respecting-processorsettings-limits.cs](./write-script-that-processes-list-of-image-paths-in-parallel-using-tpl-while-respecting-processorsettings-limits.cs) |  | Write a script that processes a list of image paths in parallel using TPL while respecting... |
| [write-script-that-resets-processorsettings-to-default-values-after-completing-multithreaded-barcode-job.cs](./write-script-that-resets-processorsettings-to-default-values-after-completing-multithreaded-barcode-job.cs) | `BarCodeReader` | Write a script that resets ProcessorSettings to default values after completing a multithr... |
| [write-test-confirming-processorsettingsuseallcores-respects-system-s-hyper-threading-configuration.cs](./write-test-confirming-processorsettingsuseallcores-respects-system-s-hyper-threading-configuration.cs) |  | Write a test confirming ProcessorSettings.UseAllCores respects the system's hyper‑threadin... |
| [write-test-confirming-processorsettingsuseonlythiscorescount-does-not-exceed-physical-core-count.cs](./write-test-confirming-processorsettingsuseonlythiscorescount-does-not-exceed-physical-core-count.cs) |  | Write a test confirming ProcessorSettings.UseOnlyThisCoresCount does not exceed the physic... |
| [write-unit-test-confirming-ignoreendingfillingpatternsforctable-only-affects-decoding-when-customerinformationinterpreti.cs](./write-unit-test-confirming-ignoreendingfillingpatternsforctable-only-affects-decoding-when-customerinformationinterpreti.cs) | `Unit` | Write a unit test confirming IgnoreEndingFillingPatternsForCTable only affects decoding wh... |
| [write-unit-test-verifying-barcodereader-removes-fnc-symbols-when-stripfnc-is-false.cs](./write-unit-test-verifying-barcodereader-removes-fnc-symbols-when-stripfnc-is-false.cs) | `Unit`, `BarCodeReader` | Write a unit test verifying BarCodeReader removes FNC symbols when StripFNC is false. |
| [write-unit-test-verifying-barcodereader-retains-fnc-symbols-when-stripfnc-is-true.cs](./write-unit-test-verifying-barcodereader-retains-fnc-symbols-when-stripfnc-is-true.cs) | `Unit`, `BarCodeReader` | Write a unit test verifying BarCodeReader retains FNC symbols when StripFNC is true. |
| [write-unit-test-verifying-custom-customerinformationdecoder-receives-raw-barcode-bytes-before-interpretation.cs](./write-unit-test-verifying-custom-customerinformationdecoder-receives-raw-barcode-bytes-before-interpretation.cs) | `Unit` | Write a unit test verifying custom CustomerInformationDecoder receives raw barcode bytes b... |
| [write-validation-routine-ensuring-processorsettingsmaxadditionalallowedthreads-does-not-exceed-system-limits.cs](./write-validation-routine-ensuring-processorsettingsmaxadditionalallowedthreads-does-not-exceed-system-limits.cs) |  | Write a validation routine ensuring ProcessorSettings.MaxAdditionalAllowedThreads does not... |

## Category Statistics
- Total examples: 48
- Failed: 0
- Pass rate: 100.0%

## Key API Surface

- `BarCodeReader`
- `QualitySettings`
- `XDimensionMode`
- `DeconvolutionMode`
- `BarcodeSvmDetectorSettings`

## Failed Tasks

All tasks passed ✅

<!-- AUTOGENERATED:START -->
Updated: 2026-04-28 | Run: `20260428_043750` | Examples: 48
<!-- AUTOGENERATED:END -->
