#!/bin/bash

# Function to make the HTTP request and display the response
make_request() {
    local base_url=localhost:3031
    local value=$1
    local allow_rounding=false
    local response=$(curl -s "http://$base_url/Converter/value?value=$value&allowRounding=$allow_rounding")
    echo ""
    echo "OUTPUT => \"$response\""
    echo ""
    echo "*** Type 'exit' to end session."
    echo ""
}

echo "*******************************************"
echo "*-- LET'S CONVERT NUMBER TO WORDS v1.0 ---*"
echo "*******************************************"
echo ""
counter=1
while true; do
    
    echo -n "INPUT $counter (Decimal): "
    read input_value

    if [ "$input_value" == "exit" ]; then
        break
    fi
      ((counter++))

    make_request $input_value 
done

echo ""
echo "Session ended."
