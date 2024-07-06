# Number to Words Converter

## How to Run the API

### Prerequisites
Ensure Docker is installed on your machine to proceed with either method.

### Method 1: Using Public Docker Image

1. **Run the Docker Container**

    ```sh
    docker run -p 3004:8080 -d --rm --name n2w-hub nasminzain/n2w-converter-api
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

    If the Docker container is running successfully, you will be able to connect to the API and start sending requests.

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

    If the Docker container is running successfully, you will be able to connect to the API and start sending requests.

---

By following these instructions, you can set up and run the Number to Words Converter API quickly and efficiently. If you encounter any issues, please check the Docker and GitHub documentation for further assistance.