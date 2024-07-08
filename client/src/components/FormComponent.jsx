import React, { useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';

const FormComponent = () => {
  const [inputValue, setInputValue] = useState('');
  const [responseText, setResponseText] = useState('');
  const [error, setError] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    const baseURL = process.env.REACT_APP_BASE_URL;

    try {
      const response = await axios.get(`${baseURL}/Converter/value`, {
        params: {
          value: inputValue,
          allowRounding: true
        }
      });
      setResponseText(response.data);
      setError('');
    } catch (error) {
      console.error("There was an error making the request:", error);
      setResponseText('Error occurred. Please try again.');
    }
  };

  const handleInputChange = (e) => {
    const value = e.target.value;
    if (isNaN(value)) {
      setError('Please enter a valid decimal number');
    } else {
      setError('');
    }
    setInputValue(value);
  };

  return (
    <div className="container mt-5 text-center">
      <h1>Number to Words Converter</h1>
      <h4>TEST PAGE</h4>
      <div className="row justify-content-center">
        <div className="col-md-6">
          <form onSubmit={handleSubmit}>
            <div className="mb-3">
              <label htmlFor="decimalInput" className="form-label">
                Please input any decimal number:
              </label>
              <input
                type="text"
                step="0.01"
                className={`form-control ${error ? 'is-invalid' : ''}`}
                id="decimalInput"
                value={inputValue}
                onChange={handleInputChange}
                required
                style={{ width: '80%', margin: '0 auto' }}
              />
              {error && <div className="invalid-tooltip d-block">{error}</div>}
            </div>
            <button type="submit" className="btn btn-success">
              Convert
            </button>
          </form>
          {responseText && (
            <div className="mt-3">
              <h5>Result:</h5>
              <p><strong>{responseText}</strong></p>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default FormComponent;
