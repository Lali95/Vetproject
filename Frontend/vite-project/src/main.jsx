import React from 'react'
import ReactDOM from 'react-dom/client'
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import MedicalRecord from './Pages/MedicalRecord';



import './index.css'


import Layout from './Pages/Layout';
import ErrorPage from "./Pages/ErrorPage";
import Home from './Pages/Home';
import MedicalRecordForm from './Pages/MedicalRecordForm';
import SearchMedicalRecord from './Pages/SearchMedicalRecord';


const router = createBrowserRouter([
  {
       element: <Layout />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "/",
        element: <Home/>,
      },
     {
      path: "/medicalRecord",
      element: <MedicalRecord/>,
     },
     {
      path: "/create-medical-record",
      element: <MedicalRecordForm/>,
     },
     {
      path: "/search-medical-record",
      element: <SearchMedicalRecord/>,
     }
     
    ],
  },
]);
const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
 
    <RouterProvider router={router} />
  
);
    