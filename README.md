# Number to Words Converter

- [Number to Words Converter](#number-to-words-converter)
  - [Components Description](#components-description)
  - [Let's Talk About Algorithm](#lets-talk-about-algorithm)
    - [Step by Step How it Works](#step-by-step-how-it-works)
      - [1. Understand the Problem](#1-understand-the-problem)
      - [2. Break Down the Number into Smaller Components](#2-break-down-the-number-into-smaller-components)
      - [3. Create Objects for Each Component](#3-create-objects-for-each-component)
      - [4. Convert Each Component into Words](#4-convert-each-component-into-words)
      - [5. Construct the Final Word Representation](#5-construct-the-final-word-representation)
      - [6. Handle Special Cases](#6-handle-special-cases)
      - [7. Implement the Algorithm](#7-implement-the-algorithm)
      - [8. Test and Refine](#8-test-and-refine)
  - [How to Run the API](#how-to-run-the-api)
    - [Prerequisites](#prerequisites)
    - [Method 1: Using Public Docker Image](#method-1-using-public-docker-image)
      - [Steps Summary](#steps-summary)
    - [Method 2: Using Local Docker Image](#method-2-using-local-docker-image)
      - [Steps Summary](#steps-summary-1)
    - [Method 3: Running the API from VS Code](#method-3-running-the-api-from-vs-code)
    - [Prerequisites](#prerequisites-1)
      - [Steps Summary](#steps-summary-2)
  - [How to Run Unit Tests](#how-to-run-unit-tests)
    - [Prerequisites](#prerequisites-2)
    - [Steps to Run Unit Tests](#steps-to-run-unit-tests)
      - [Steps Summary](#steps-summary-3)

---

## Components Description

N2W Converter is a simple .NET Web API that can convert dollar and cents in decimal values into words. There are 3 main impotant components in this project:

| **Component** | **Description** |
|:-------------|:----------------|
| **API**       | Web API project that contains endpoints that accept decimal values, convert them to words, and respond with the results. You can test this API using either `SwaggerUI` or the `demo.sh` script. |
| **API.Tests** | Unit tests for the `Convert` function to ensure all scenarios are handled. While not guaranteed to be bug-free, it aims to be nearly so. |
| **demo.sh**   | Interactive shell script to test the API. You can edit the base URL and port number here, and set `AllowRounding` to `True` or `False` (default is `False`). |

---

---

## Let's Talk About Algorithm

Algorithm used here might not be concise and sexier with brilliant mathematical formula you can find from internet or suggested by your favourite Generative AI. The thought pattern right here is to implement the solution as "everything objects". Having the decimal numbers, no matter how large it is, you can clearly see what matter most is the three properties, "the ones", "the tens" or "the teens" or "the hundreds". Hence I every decimal number can be divided into objects containing these 3 properties. How to extract it? Easy, just use string index manipulation to divide it into smaller object with these 3 properties and assign them with correct words. Finally loop the objects to construct one full words and you're done.


### Step by Step How it Works

The algorithm for converting a number to its word representation is based on breaking down a decimal number into its basic components and converting those components into words. Here's a step-by-step guide to the thought process and implementation:

#### 1. Understand the Problem

- **Goal:** Convert a decimal number into its word representation.
- **Key Elements:** To convert numbers into words, focus on three main properties of a number:
  - **Ones:** The single digits.
  - **Tens:** The two-digit numbers.
  - **Hundreds:** The three-digit numbers.

#### 2. Break Down the Number into Smaller Components

- **Concept:** Every decimal number can be divided into smaller parts based on "ones", "tens", and "hundreds."
- **Approach:** Use string index manipulation to split the number into these parts.
  - **String Manipulation:** Convert the number to a string and extract parts from it using indices.
  - **Example:** For the number 123:
    - **Ones:** 3
    - **Tens:** 2
    - **Hundreds:** 1

#### 3. Create Objects for Each Component

- **Concept:** Define objects or data structures to hold the components and their word representations.
- **Implementation:** Create classes or structures to represent "ones," "tens," and "hundreds."
  - **Example Objects:**
    - **OneInWord:** Stores the word for the single digit (e.g., "three").
    - **TenInWord:** Stores the word for the tens digit (e.g., "twenty").
    - **HundredInWord:** Stores the word for the hundreds digit (e.g., "one hundred").

#### 4. Convert Each Component into Words

- **Concept:** Map each numeric component to its word equivalent.
- **Implementation:** Use predefined arrays or dictionaries for number-to-word mapping.
  - **Example:**
    - **Ones Array:** `["zero", "one", "two", ... , "nine"]`
    - **Tens Array:** `["", "", "twenty", "thirty", ... , "ninety"]`
    - **Teens Array:** `["ten", "eleven", "twelve", ... , "nineteen"]`

#### 5. Construct the Final Word Representation

- **Concept:** Combine the words for "ones," "tens," and "hundreds" into a complete word representation of the number.
- **Implementation:** Loop through the components and concatenate their word representations.
  - **Example:** For 123, the final word would be "one hundred twenty-three."

#### 6. Handle Special Cases

- **Concept:** Address special cases such as numbers from 10 to 19 (teens) and ensure correct formatting.
- **Implementation:** Check for these cases during the conversion and apply the correct word mappings.

#### 7. Implement the Algorithm

- **Concept:** Put all the steps into code.
- **Implementation:** Write functions or methods that perform the above tasks.

#### 8. Test and Refine

- **Concept:** Test the implementation with various numbers to ensure correctness.
- **Implementation:** Write test cases for different numbers and refine the algorithm as needed.

---

## How to Run the API

### Prerequisites
1. Ensure Docker is installed on your machine to proceed with either method.
2. Ensure you have the [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.

### Method 1: Using Public Docker Image

1. **Run the Docker Container**

    ```sh
    docker run -p 3004:8080 -d --rm --name n2w-converter-api nasminzain/n2w-converter-api
    ```

2. **Clone the Repository**

    ```sh
    git clone https://github.com/programmermelayu/n2w-converter.git
    ```

3. **Edit the Port Number**

    Update the port number in `n2w-converter/demo.sh` to match the port number specified in Step 1.

    ```sh
    # Line 5: local base_url=localhost:3004
    ```

4. **Run the Demo Script**

    ```sh
    ./demo.sh
    ```

#### Steps Summary

- **Run the Docker Container:** `docker run -p 3004:8080 -d --rm --name n2w-converter-api nasminzain/n2w-converter-api`
- **Clone the Repository:** `git clone https://github.com/programmermelayu/n2w-converter.git`
- **Edit the Port Number:** Update in `n2w-converter/demo.sh`
    ```sh
    # Line 5: local base_url=localhost:3004
    ```
- **Run the Demo Script:** `./demo.sh`


---

### Method 2: Using Local Docker Image

1. **Clone the Repository**

    ```sh
    git clone https://github.com/programmermelayu/n2w-converter.git
    ```

2. **Build the Docker Image**

    Navigate to the `API` directory and run the following command:

    ```sh
    cd n2w-converter/API
    docker build -t n2w-converter-api .
    ```

3. **Run the Docker Container**

    ```sh
    docker run -p 3004:8080 -d --rm --name n2w-converter-api n2w-converter-api
    ```

4. **Edit the Port Number**

    Update the port number in `n2w-converter/demo.sh` to match the port number specified in Step 3.

    ```sh
    # Line 5: local base_url=localhost:3004
    ```

5. **Run the Demo Script**

    ```sh
    ./demo.sh
    ```

#### Steps Summary

- **Clone the Repository:** `git clone https://github.com/programmermelayu/n2w-converter.git`
- **Build the Docker Image:** `cd n2w-converter/API` and `docker build -t n2w-converter-api .`
- **Run the Docker Container:** `docker run -p 3004:8080 -d --rm --name n2w-converter-api n2w-converter-api`
- **Edit the Port Number:** Update in `n2w-converter/demo.sh`
    ```sh
    # Line 5: local base_url=localhost:3004
    ```
- **Run the Demo Script:** `./demo.sh`

    If the Docker container is running successfully, you will be able to connect to the API and start sending requests.

---

### Method 3: Running the API from VS Code

### Prerequisites
- Ensure you have the [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.

1. **Clone the Repository**

    Clone the repository to your local machine using the following command:

    ```sh
    git clone https://github.com/programmermelayu/n2w-converter.git
    ```

2. **Open the Project in VS Code**

    Navigate to the project directory and open it in VS Code:

    ```sh
    cd n2w-converter
    code .
    ```

3. **Open the API Project**

    In VS Code, open the `API` directory where the `.csproj` file is located. You can do this by either using the VS Code file explorer or the following command in the integrated terminal:

    ```sh
    cd API
    ```

4. **Restore Dependencies**

    Restore the .NET dependencies required for the project:

    ```sh
    dotnet restore
    ```

5. **Build the Project**

    Build the .NET project to ensure everything is set up correctly:

    ```sh
    dotnet build
    ```

6. **Run the API**

    Start the .NET Web API using the following command:

    ```sh
    dotnet run
    ```

    By default, the API will run on `http://localhost:5xxx`. You can verify the exact URL in the output of the `dotnet run` command.

7. **Test the API with Swagger**

    Open your web browser and navigate to `http://localhost:5xxx/swagger` (or the respective URL based on the `dotnet run` output) to access the Swagger UI for API testing.

8. **Edit the Port Number**

    Update the port number in `n2w-converter/demo.sh` to match the port number specified in Step 6.

    ```sh
    # Line 5: local base_url=http://localhost:5052
    ```

9.  **Run the Demo Script**

    Open a new terminal window or tab and execute the demo script to run the API demo:

    ```sh
    ./demo.sh
    ```

#### Steps Summary

- **Clone the Repository:** `git clone https://github.com/programmermelayu/n2w-converter.git`
- **Open the Project in VS Code:** `cd n2w-converter` and `code .`
- **Open the API Project:** `cd API`
- **Restore Dependencies:** `dotnet restore`
- **Build the Project:** `dotnet build`
- **Run the API:** `dotnet run`
- **Test the API with Swagger:** Navigate to `http://localhost:5052/swagger`
- **Edit the Port Number:** Update in `demo.sh`
    ```sh
    # Line 5: local base_url=http://localhost:5052
    ```
- **Run the Demo Script:** `./demo.sh`


---

## How to Run Unit Tests 

### Prerequisites

- Ensure you have the [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.
- Make sure `API.Tests` exist in the solution directory

### Steps to Run Unit Tests

1. **Open a Terminal or Command Prompt**

   - On **Windows**, you can use Command Prompt, PowerShell, or the terminal integrated into Visual Studio Code.
   - On **macOS** or **Linux**, open the Terminal application.

2. **Navigate to `API.Tests` Directory**

   Change the directory to the root where the `.sln` file is located, or directly to the directory of the test project.

   ```sh
   cd n2w-converter
   ```

3. **Restore Dependencies**

   Ensure that all necessary packages are installed by running the `dotnet restore` command.

   ```sh
   dotnet restore
   ```

   *Restores the NuGet packages required for the project.*

4. **Build the Solution**

   Compile the code to make sure there are no build errors.

   ```sh
   dotnet build
   ```

   *Builds the project and prepares it for testing.*

5. **Run the Tests**

   Execute the `dotnet test` command to run all the unit tests in the solution or test project.

   ```sh
   dotnet test
   ```

   *Runs all the unit tests and displays the test results in the terminal.*

6. **View the Test Results**

   After running `dotnet test`, you will see the results in the terminal, including which tests passed, failed, or were skipped.

   - **Green**: Tests passed.
   - **Red**: Tests failed.
   - **Yellow**: Tests were skipped.

   *Example output:*
   ```
   Test Run Successful.
   Total tests: 15
   Passed: 15
   Failed: 0
   Skipped: 0
   ```
8. **Generate Test Reports**

   You can generate test reports using the `--logger` option.

   ```sh
   dotnet test --logger "trx;LogFileName=test_results.trx"
   ```

   *Generates a `.trx` file with test results that can be viewed in Visual Studio or other tools.*

#### Steps Summary

- **Open a Terminal or Command Prompt:** Use Command Prompt, PowerShell, or Terminal depending on your OS.
- **Navigate to Your Project Directory:** Change the directory to where the `.sln` file or test project is located.
    ```sh
    cd path/to/your/project
    ```
- **Restore Dependencies:** Install necessary packages.
    ```sh
    dotnet restore
    ```
- **Build the Solution:** Compile the project.
    ```sh
    dotnet build
    ```
- **Run the Tests:** Execute all unit tests.
    ```sh
    dotnet test
    ```
- **View the Test Results:** Check the terminal for test outcomes.
- **Generate Test Reports (Optional):** Create a `.trx` file with test results.
    ```sh
    dotnet test --logger "trx;LogFileName=test_results.trx"
    ```
---
By following these instructions, you can set up and run the Number to Words Converter API quickly and efficiently. If you encounter any issues, please check the Docker and GitHub documentation for further assistance.