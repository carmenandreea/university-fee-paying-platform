import React from 'react';
import ReactDom from 'react-dom';
import './index.css';
import { TaxationForm } from './TaxationForm.js';
ReactDom.render(<TaxationForm />, document.getElementById('root'));

export function readUser() {
  const json = window.localStorage.getItem('redirect');
  return json === null ? {} : JSON.parse(json);
}
export function readOptions() {
  const json = window.localStorage.getItem('options');
  return json === null ? [] : JSON.parse(json);
}
