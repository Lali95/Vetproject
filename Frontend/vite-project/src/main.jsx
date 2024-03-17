import React from 'react';
import { createRoot } from "react-dom/client";
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import LandingPage from './Pages/LandingPage';
import ErrorPage from './Pages/ErrorPage';
import Home from './Pages/Home';
import MedicalRecord from './Pages/MedicalRecord';
import DisplayMedicalRecord from './Pages/DisplayMedicalRecord';
import MedicalRecordForm from './Pages/MedicalRecordForm';
import SearchMedicalRecord from './Pages/SearchMedicalRecord';
import './index.css';
import UpdateMedicalRecord from './Components/UpdateMedicalRecord';

const router = createBrowserRouter([
  {
    element: <LandingPage />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: '/',
        element: <Home />,
      },
      {
        path: '/medicalRecord',
        element: <MedicalRecord />,
      },
      {
        path: '/create-medical-record',
        element: <MedicalRecordForm />,
      },
      {
        path: '/search-medical-record',
        element: <SearchMedicalRecord />,
      },
      {
        path: '/medicalRecord/:id',
        element: <DisplayMedicalRecord />,
      },
      {
        path: "/medicalRecord/:id/update",
        element: <UpdateMedicalRecord/>
      }
    ],
  },
]);

const root = createRoot(document.getElementById('root'));

root.render(
  <RouterProvider router={router} />
);
