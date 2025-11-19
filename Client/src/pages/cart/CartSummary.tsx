import { TableCell, TableRow } from "@mui/material"
import { useCartContext } from "../../context/CartContext"
import { currencyTRY } from "../../utils/formatCurrency";

export default function CartSummary() {
    const { cart } = useCartContext();
    const subtotal = cart?.cartItems.reduce((sum, item) => sum + item.price * item.quantity, 0) ?? 0;
    const tax = subtotal * 0.2;
    const total = subtotal + tax;
    return (
        <>
            <TableRow>
                <TableCell align="right" colSpan={5} >Ara Toplam</TableCell>
                <TableCell align="right"   >{currencyTRY.format(subtotal)}</TableCell>
            </TableRow>
            <TableRow>
                <TableCell align="right" colSpan={5} >Vergi(%20)</TableCell>
                <TableCell align="right"   >{currencyTRY.format(tax)}</TableCell>
            </TableRow>
            <TableRow>
                <TableCell align="right" colSpan={5} >Toplam</TableCell>
                <TableCell align="right"   >{currencyTRY.format(total)}</TableCell>
            </TableRow>
        </>
    )
}