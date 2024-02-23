import React from 'react'
import ReactDOM from 'react-dom/client'
import { createBrowserRouter, RouterProvider } from "react-router-dom";

import './index.css'


import Layout from './Pages/Layout';
import ErrorPage from "./Pages/ErrorPage";
import Home from './Pages/Home';


const router = createBrowserRouter([
  {
       element: <Layout />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "/",
        element: <Home/>,
      },
     
     
    ],
  },
]);
const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
 
    <RouterProvider router={router} />
  
);
    