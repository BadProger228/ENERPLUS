# EnergyPlus File Builder

## Description
**EnergyPlus File Builder** is a tool for generating configuration files for the EnergyPlus program. It allows users to define zones, surfaces, and materials (not predefined ones, but with user-specified characteristics). The program supports saving and opening files, but only those created within this application.

## Features
- Create configuration files for EnergyPlus.
- Add zones, surfaces (with 3 or 4 points), and materials with custom characteristics.
- Save and load files (only those created in this program).
- Support for JSON format.

## Technologies Used
- **Programming Language:** C#
- **Framework:** WinForms
- **Data Storage Format:** JSON

## Libraries Used
- **BenchmarkDotNet** – for performance benchmarking.
- **Microsoft.VisualStudio.UnitTesting** – for unit testing.
- **Newtonsoft.Json** – for JSON processing.
- **NUnit** – for testing.

## How to Run
1. Install .NET Framework (if required).
2. Open the project in **Visual Studio**.
3. Ensure all dependencies are installed (via NuGet packages).
4. Build and run the application.

