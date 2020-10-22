# UnityProjectTranslationTool

## Introduction
This is a tool that scans through a folder containing code and picks out all the strings that represent hard coded texts, providing you a simple GUI to help you translate them.
At the moment, it is only good for translating unity projects since it provides some filters to ignore the strings that might not represent actual texts in game, though you can still use it for other things. In the future, more filters will added so it will support other projects.

## Current Status
* Scan a folder containing code for unity projects.
* Save the data in a .utp text file.
* Load the data from the text file.
* It currently uses GB2312 encoding, so it's best for translating text to Chinese.

## In Progress
* Replace the original text in the code with your translation.
* Optimization

## Future Goals
* Support for other projects.
* Support for other languages.
