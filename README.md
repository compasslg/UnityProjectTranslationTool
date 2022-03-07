# UnityProjectTranslationTool

## Introduction
This is a tool that can be used to translate Unity games.

The current version is able to scan through a c# Assembly or a folder containing source code files, and picks out all the hard coded text strings, allowing you to translate and replace them.

## Current Status
* Translate hard coded text in c# assembly.
* Translate hard coded text in source code folder.
* Load and save the translation progress in a .utp project file.
* Simple filters to ignore strings that might not need translation.

## In Progress
* Provide better filters and let user observe the context that the text is used in code.
* Provide a way to inject and replace resources, including text files, fonts, or images.
* Support for different languages and encodings.
