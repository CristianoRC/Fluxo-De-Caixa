import React from 'react';
import ReactDOM from 'react-dom/client';

import './index.css';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';

// Pages
import CreateWallet from './pages/createWallet';
import Report from './pages/report';
import Transaction from './pages/transaction';
import Statement from './pages/statement';

const router = createBrowserRouter([
  {
    path: '/',
    element: <CreateWallet />,
  },
  {
    path: '/wallet',
    element: <CreateWallet />,
  },
  {
    path: '/report',
    element: <Report />,
  },
  {
    path: '/transaction',
    element: <Transaction />,
  },
  {
    path: '/statement',
    element: <Statement />,
  },
]);

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>,
);
