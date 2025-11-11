import { useState, useEffect } from "react"
import { IProduct } from "../../model/IProduct"
import ProductList from "./ProductList";
import { Typography } from "@mui/material";

export default function CatalogPage() {
    const [products, setProducts] = useState<IProduct[]>([]);
  const [loading, setLoading] = useState(true);
    useEffect(() => {
        fetch("http://localhost:5077/api/products")
            .then(response => response.json())
            .then(data => setProducts(data))
             .finally(() => setLoading(false));
    },[]);
    if (loading) return <Typography variant="h5">Loading...</Typography>;
    return(<ProductList products={products}/>);
}