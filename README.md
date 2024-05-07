# XtractFlow Demonstrator on Blazor

## About XtractFlow

XtractFlow is an intelligent document processing (IDP) engine developed by PSPDFKit. It uses generative AI to automate the classification, processing, and extraction of data from a wide range of document formats with human-level accuracy. This includes PDFs, JPEGs, Office files, and CAD documents, among others. The engine simplifies document processing setups, reduces deployment time, and supports a no-code, natural-language processing approach.

This project is an open-source demonstrator of XtractFlow built with Blazor, designed to showcase the capabilities of the XtractFlow engine in a web environment.

## Features

- **Document Upload**: Users can upload various document formats to see how XtractFlow classifies and extracts data.
- **Template configurator**: Users are able easily customize existing templates with custom fields or build new templates from scratch.
- **Data Extraction Display**: Extracted data is displayed in a structured format, allowing users to understand how XtractFlow interprets uploaded documents.

## Quickstart Guide

### Prerequisites

Before you begin, ensure you have the following installed:
- [.NET 7.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- An IDE that supports Blazor (e.g., [Visual Studio](https://visualstudio.microsoft.com/vs/), [Visual Studio Code](https://code.visualstudio.com/))

### Setup

1. **Clone the repository:**
    ```bash
    git clone https://github.com/Orpalis/XtractFlow.WebDemo.git
    cd XtractFlow.WebDemo
    ```

2. **Fill API keys**
    Open the appsettings.json file and fill either your Azure OpenAI informations (`ApiKey`, `ResourceName` & your `DeploymentId`) or your OpenAI API key.

2. **Run the application:**
    ```bash
    dotnet run
    ```
4. **Navigate to the application:** Open your web browser and go to https://localhost:5001 to start using the XtractFlow demonstrator.

## Using the Demonstrator

- **Upload a Document:** Use the upload interface to select and submit one or multiples documents.
- **Configure templates:** Based on your needs, you've to setup your classification and / or templates that XtractFlow will rely on to run the process, you can either build templates from scratch or use the ones already predefined in XtractFlow (e.g. Invoice, license driver, etc...)
- **View Extracted Data:** After processing, the extracted data will be displayed. You can review the accuracy and details of the classification or the extraction or both depending of the initial configuration.

## Contributing

We welcome contributions from the community! If you'd like to contribute to the XtractFlow Demonstrator, please fork the repository and submit a pull request.

- **Issues:** Feel free to submit issues and enhancement requests.
- **Pull Requests:** Before submitting a pull request, please ensure your changes adhere to the existing codebase style and all tests pass.
