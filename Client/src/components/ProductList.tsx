import { Grid } from "@mui/material";
import { IProduct } from "../model/IProduct";
import Product from "./Product";


interface Props {
  products: IProduct[],
}

export default function ProductList({ products }: Props) {

  return (
    <div>

      <Grid container spacing={2} >

        {products.map((p: IProduct) => (
          <Grid size={{ xs: 12, sm: 6, md: 4 }} >
            <Product key={p.id} product={p} />

          </Grid>

        ))}
      </Grid>

    </div >

  )
}