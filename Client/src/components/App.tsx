import Header from "./Header"
import { Container, CssBaseline } from "@mui/material"
import { Outlet } from "react-router"
import { ToastContainer } from "react-toastify"

function App() {
  return (
    <>
    <ToastContainer position="bottom-right" hideProgressBar theme="colored" />
      <CssBaseline />
      <Header />
      <Container>
        <Outlet/>
      </Container>

    </>
  )
}

export default App
