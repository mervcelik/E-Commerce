import { Divider, Grid, Table, TableBody, TableCell, TableContainer, TableRow, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { useParams } from "react-router";
import { IProduct } from "../../model/IProduct";

export default function ProductDetails() {
    const { id } = useParams();
    const [product, setProduct] = useState<IProduct>();
    const [loading, setLoading] = useState(true);
    useEffect(() => {
        fetch(`http://localhost:5077/api/products/${id}`)
            .then(res => res.json())
            .then(data => setProduct(data))
            .catch(error => console.log(error))
            .finally(() => setLoading(false));
    }, [id]);

    if (loading) return <Typography variant="h5">Loading...</Typography>;
    if (!product) return <Typography variant="h5">Product not found</Typography>;
    return (
        <Grid container spacing={6}>
            <Grid size={{ xl: 3, lg: 4, md: 5, sm: 6, xs: 12 }}>
                <img src={`http://localhost:5077/images/${product.imageUrl}`} alt={product.name} style={{ maxWidth: "100%" }} />
            </Grid>
            <Grid size={{ xl: 9, lg: 8, md: 7, sm: 6, xs: 12 }}>
                <Typography variant="h3">{product.name}</Typography>
                <Divider sx={{ mb: 2 }} />
                <Typography variant="h4" color="secondary">{(product.price / 100).toFixed(2)} ₺</Typography>
                <TableContainer>
                    <Table>
                        <TableBody>
                            <TableRow>
                                <TableCell>Name</TableCell>
                                <TableCell>{product.name}</TableCell>
                            </TableRow>
                              <TableRow>
                                <TableCell>Description</TableCell>
                                <TableCell>{product.description}</TableCell>
                            </TableRow>
                              <TableRow>
                                <TableCell>Stock</TableCell>
                                <TableCell>{product.stock}</TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
                </TableContainer>
            </Grid>
        </Grid>
    )
}