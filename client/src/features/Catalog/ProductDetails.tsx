import { CardContent, CardMedia, Grid, Table, TableBody, TableCell, TableRow, Typography } from "@mui/material";
import axios from "axios";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Product } from "../../app/models/product";
import agent from "../../app/api/agent";
import NotFound from "../../app/api/errors/NotFound";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { useStoreContext } from "../../app/context/StoreContext";


export default function ProductDetails() {
  const {basket} = useStoreContext();
  const { id } = useParams <{ id: string }> ();
  const [product, setProduct] = useState <Product | null> (null);
  const[loading, setLoading] = useState (true);
  const [quantity, setQuantity] = useState(0);
  const [submitting, setSubmitting] = useState(false);
  const item = basket?.items.find(x => x.productId === product?.id);

  

  useEffect(() => {
    agent.Catalog.details(parseInt(id))
    .then(response => setProduct(response))
    .catch(error => console.log(error))
    .finally(() => setLoading(false))
  }, [id])


  if (loading) return <LoadingComponent message='Loading product...' />

  if (!product) return <NotFound />

  return (
    <Grid container spacing={6} >
      <Grid item xs={12} sm={6}>
        <img src={product.pictureUrl} alt={product.name} style={{width: '100%'}} />
      </Grid>
      <Grid item xs={12} sm={6}>
        <CardContent sx={{ flex: "1 0 auto" }}>
          <Typography component="h3" variant="h3">
            {product.name}
          </Typography>
          <Typography variant="h4" color="secondary">
            €{(product.price / 100).toFixed(2)}
          </Typography>
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
                <TableCell>Type</TableCell>
                <TableCell>{product.type}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Brand</TableCell>
                <TableCell>{product.brand}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Quantity In Stock</TableCell>
                <TableCell>{product.quantityInStock}</TableCell>
              </TableRow>
            </TableBody>
          </Table>
        </CardContent>
      </Grid>

    </Grid>
  );
}