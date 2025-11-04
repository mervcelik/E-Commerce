import { useEffect, useState } from "react"
import { IProduct } from "../model/IProduct"
import Header from "./Header"
import ProductList from "./ProductList"
import { Container, CssBaseline } from "@mui/material"

function App() {

  const [products, setProducts] = useState<IProduct[]>([])
  useEffect(() => {
    fetch("http://localhost:5077/api/products")
      .then(response => response.json())
      .then(data => setProducts(data))

  }, [])//[] component ilk yüklendiğinde çalışması için eklenir.

  // function addProduct() {
  //   setProducts([...products, { id: Date.now(), name: "Product 4", price: 400, isActive: true }])
  // }

  return (
    <>
      <CssBaseline />
      <Header />
      <Container>
        <ProductList products={products}/>
      </Container>

    </>
  )
}

export default App
