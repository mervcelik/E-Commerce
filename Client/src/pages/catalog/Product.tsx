import { Button, Card, CardActions, CardContent, CardMedia, Typography } from "@mui/material"
import { AddShoppingCart } from "@mui/icons-material"
import SearchIcon from '@mui/icons-material/Search';
import { IProduct } from "../../model/IProduct";
import { Link } from "react-router";
import { useState } from "react";
import requests from "../../api/request";
import { LoadingButton } from "@mui/lab";
import { useCartContext } from "../../context/CartContext";
import { toast } from "react-toastify";
import { currencyTRY } from "../../utils/formatCurrency";

interface Props {
  product: IProduct
}

export default function Product({ product }: Props) {

  const [loading, setloading] = useState(false);
  const { setCart } = useCartContext();

  function handleAddItem(productId: number) {
    setloading(true);
    requests.Cart.addItem(productId)
      .then(cart => {
        setCart(cart);
        toast.success("Ürün Sepetinize Eklendi");
      })
      .catch((error) => console.log(error))
      .finally(() => setloading(false));
  }
  return (
    <Card>
      <CardMedia sx={{ height: 160, backgroundSize: "Contain" }} image={`http://localhost:5077/images/${product.imageUrl}`} />
      <CardContent>
        <Typography gutterBottom variant="h6" component="h2" color="text-secondary">{product.name}</Typography>
        <Typography variant="body2" color="secondary">{currencyTRY.format(product.price)} ₺</Typography>
      </CardContent>
      <CardActions>
        {/* <Button variant="outlined" size="small" startIcon={<AddShoppingCart />} color="success" onClick={() => handleAddItem(product.id)}>Add to cart</Button> */}
        <LoadingButton
          loading={loading}
          onClick={() => handleAddItem(product.id)}
          variant="outlined"
          size="small"
          startIcon={<AddShoppingCart />}
          color="success">
          Add to cart
        </LoadingButton>
        <Button component={Link} to={`/catalog/${product.id}`} variant="outlined" size="small" startIcon={<SearchIcon />} color="primary">View</Button>
      </CardActions>
    </Card>
  )
}