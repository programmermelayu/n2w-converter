// import React, { useState } from 'react';
// import axios from 'axios';
// import 'bootstrap/dist/css/bootstrap.min.css';

// const FormComponent = () => {
//   const [inputValue, setInputValue] = useState('');
//   const [responseText, setResponseText] = useState('');
//   const [error, setError] = useState('');

//   const handleSubmit = async (e) => {
//     e.preventDefault();
//     const baseURL = process.env.REACT_APP_BASE_URL;

//     try {
//       const response = await axios.get(`${baseURL}/Converter/value`, {
//         params: {
//           value: inputValue,
//           allowRounding: true
//         }
//       });
//       setResponseText(response.data);
//       setError('');
//     } catch (error) {
//       console.error("There was an error making the request:", error);
//       setResponseText('Error occurred. Please try again.');
//     }
//   };

//   const handleInputChange = (e) => {
//     const value = e.target.value;
//     if (isNaN(value)) {
//       setError('Please enter a valid decimal number');
//     } else {
//       setError('');
//     }
//     setInputValue(value);
//   };

//   return (
//     <div className="container mt-5 text-center">
//       <h1>Number to Words Converter</h1>
//       <h4>TEST PAGE</h4>
//       <div className="row justify-content-center">
//         <div className="col-md-6">
//           <form onSubmit={handleSubmit}>
//             <div className="mb-3">
//               <label htmlFor="decimalInput" className="form-label">
//                 Please input any decimal number:
//               </label>
//               <input
//                 type="text"
//                 step="0.01"
//                 className={`form-control ${error ? 'is-invalid' : ''}`}
//                 id="decimalInput"
//                 value={inputValue}
//                 onChange={handleInputChange}
//                 required
//                 style={{ width: '80%', margin: '0 auto' }}
//               />
//               {error && <div className="invalid-tooltip d-block">{error}</div>}
//             </div>
//             <button type="submit" className="btn btn-success">
//               Convert
//             </button>
//           </form>
//           {responseText && (
//             <div className="mt-3">
//               <h5>Result:</h5>
//               <p><strong>{responseText}</strong></p>
//             </div>
//           )}
//         </div>
//       </div>
//     </div>
//   );
// };

// export default FormComponent;

import React, { useState, useEffect } from "react";
import axios from "axios";
import "bootstrap/dist/css/bootstrap.min.css";

const FormComponent = () => {
  const [inputValue, setInputValue] = useState("");
  const [allowRounding, setAllowRounding] = useState(false);
  const [responseText, setResponseText] = useState("");
  const [error, setError] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    const baseURL = process.env.REACT_APP_BASE_URL;

    try {
      const response = await axios.get(`${baseURL}/Converter/value`, {
        params: {
          value: inputValue,
          allowRounding: allowRounding,
        },
      });
      setResponseText(response.data);
      setError("");
    } catch (error) {
      console.error("There was an error making the request:", error);
      setResponseText("Error occurred. Please try again.");
    }
  };

  useEffect(() => {
    const fetchData = async () => {
      if (inputValue && !error) {
        const baseURL = process.env.REACT_APP_BASE_URL;

        try {
          const response = await axios.get(`${baseURL}/Converter/value`, {
            params: {
              value: inputValue,
              allowRounding: allowRounding,
            },
          });
          setResponseText(response.data);
        } catch (error) {
          console.error("There was an error making the request:", error);
          setResponseText("Error occurred. Please try again.");
        }
      } else {
        setResponseText("");
      }
    };

    fetchData();
  }, [inputValue, allowRounding, error]); // Trigger the effect when inputValue, allowRounding, or error changes


  const handleInputChange = (e) => {
    const value = e.target.value;
    let errorMsg = "";
    if (isNaN(value)) {
      errorMsg = "Please enter a valid decimal number";
      setResponseText("");
    } 

    // Check if the value contains a decimal point and handle rounding
    if (allowRounding === false) {
      if (value.includes(".")) {
        const decimalPart = value.split(".")[1]; // Get the part after the decimal point
        if (decimalPart && decimalPart.length > 2) {
          errorMsg = "Please check 'Allow Rounding' to handle more decimal points";   
          setResponseText("");  
        } 
    }
  }

    setError(errorMsg);

    setInputValue(value);
  };

  const handleCheckboxChange = (e) => {
    setAllowRounding(e.target.checked);
  };

  return (
    <div className="container mt-5 text-center">
      <h1>Number to Words Converter</h1>
      <h4>
        <span class="badge text-bg-warning">TEST PAGE</span>
      </h4>
      <hr className="content-hr" />
      <div className="row justify-content-center">
        <div className="col-md-6">
          <form onSubmit={handleSubmit}>
            <div className="mb-3 d-flex align-items-center">
              <input
                type="text"
                step="0.01"
                className={`form-control ${error ? "is-invalid" : ""}`}
                id="decimalInput"
                value={inputValue}
                onChange={handleInputChange}
                required
                placeholder="Please input any decimal number and see the magic"
                style={{ flex: "1", marginRight: "10px" }}
              />
              <div className="form-check">
                <input
                  type="checkbox"
                  className="form-check-input"
                  id="allowRounding"
                  checked={allowRounding}
                  onChange={handleCheckboxChange}
                />
                <label className="form-check-label" htmlFor="allowRounding">
                  Allow Rounding
                </label>
              </div>
              {error && <div className="invalid-tooltip d-block">{error}</div>}
            </div>
            <button hidden disabled={!!error} type="submit" className="btn btn-success">
              Convert
            </button>
          </form>
          {responseText && (
              <div className="alert alert-success response-text" role="alert">
                <strong>{responseText}</strong>
              </div>
            )}
             {error && (
              <div className="alert alert-danger response-text" role="alert">
                {error}
              </div>
            )}
          
        </div>
      </div>
    </div>
  );
};

export default FormComponent;
