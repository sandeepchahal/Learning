public class RateLimiterMiddleware()
{
    //customer, orders, orderitems, product, category
    // brand - 
    // category - 
    // user/customer
    // product - master, categoryid, name
    // productitem - price information 
    
    
    // order - customerid, productId
    // order item - orderId - productitemid, price, 
    // shipping - orderId - cost - 
    
    
    
    // want average of sales in given month for every product
    
    // context.Order.AsNoTracking().GroupBy(col=>col.Name, col.Orderitems.Avg(av=>av.price)).ToListAsync();
    
    //  select c.name, avg(oi.price) as average from Order o join orderitem oi on oi.orderId  = o.id
    // join product p on p.id = o.ProductId
    // join category c on c.id o = p.categoryid
    // group by c.name
//     

    // ##VCV# - VC

    public void Output()
    {
        string text = "cvv###";

        string output = ""; // cvv

        foreach (var item in text)
        {
            if (item == '#' && !string.IsNullOrEmpty(output))
            {
                // remove 1 from the last 
                output = output.Length == 1 ? "" : output.Substring(0, output.Length - 1);
            }
            else 
            {
                output.Append(item);
            }
        }
    }

}
