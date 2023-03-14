import { createBrowserRouter, Navigate } from "react-router-dom";
import Catalog from "../../features/Catalog/Catalog";
import ProductDetails from "../../features/Catalog/ProductDetails";
import AboutPage from "../../features/about/AboutPage";
import Register from "../../features/account/Register";
import BasketPage from "../../features/basket/BasketPage";
import HomePage from "../../features/home/HomePage";
import NotFound from "../api/errors/NotFound";
import ServerError from "../api/errors/ServerError";
import App from "../layout/App";
import ContactPage from "../../features/contact/ContactPage";
import Login from "../../features/account/Login";
import RequireAuth from "./RequireAuth";
import CheckoutPage from "../../features/checkout/CheckoutPage";
import Orders from "../../features/orders/Orders";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            {element: <RequireAuth/>, children:[
                {path: 'checkout', element: <CheckoutPage />},
                {path: 'orders', element: <Orders />}
            ]},
            {path: '', element: <HomePage />},
            {path: 'catalog', element: <Catalog />},
            {path: 'catalog/:id', element: <ProductDetails />},
            {path: 'about', element: <AboutPage />},
            {path: 'contact', element: <ContactPage />},
            {path: 'server-error', element: <ServerError />},
            {path: 'not-found', element: <NotFound />},
            {path: 'basket', element: <BasketPage />},
            {path: 'login', element: <Login />},
            {path: 'register', element: <Register />},
            {path: '*', element: <Navigate replace to='/not-found' />}
        ]
    }
])