import { Product } from "../../app/models/product";
import ProductCard from "./ProductCard";
import Grid from "@mui/material/Grid";

interface Props {
    products: Product[]
}


export default function ProductList({products}: Props) {
    return (
        <Grid container spacing={4}> 
            {products.map((product) => (
                <Grid item xs={4} sm={4} md={3} lg={3}key={product.id}>
                <ProductCard  product={product} />
                </Grid>
            ))}
        </Grid>
    )
}