1. Run the below command to start the consul service. Make sure you are in the root directory where the file is present
   docker compose up -d

   All the services should be running in docker

2. Try accessing http://localhost:8500

   - it should all the services registered

3. Api gateway would be running on http://localhost:8080

   Get the token by logging into user url

   # url - http://localhost:5010/user/login

   Admin -
   {
   "username": "admin",
   "password": "admin123"
   }
   User -
   {
   "username": "user",
   "password": "user123"
   }

Admin Can add the product and product details at below urls.
Before making the calls, add the auth token into the header

# Add Product - http://localhost:5010/admin/product/add

                  {
                     "ProductId": 1,
                     "Name": "Sample Product",
                     "Description": "This is a sample product description.",
                     "Quantity": 100
                  }

# Add Product Detail - http://localhost:5010/admin/product/detail/add/{productid}

{
"ProductDetailId": 1,
"ProductId": 101,
"Size": "Medium",
"Price": 29.99,
"Design": "Striped",
"Quantity": 50
}

Admin can remove the product by replace add with remove in url and passing the ID.

Product Info and product details can be fetched with any authorization.

# Product - http://localhost:5010/product/get-all

# Product Detail - localhost:5010/product-detail/get-all-by-product-id/1

User access is required to add the items into the cart and checkout, therefore login as user and add the recieved token into the authorization header.

# Adding into cart - http://localhost:5010/cart/add

               {
               "productId": 2,
               "productDetailId": 4,
               "quantity": 10
               }

--> When adding into the cart, no quantity is being reserved. Instead we would be checking the quantity while processing the checkout.
--> Automatically cart items would be deleted after 5 mintues. This is the expiry time of cart items.

# check cart items at - http://localhost:5010/cart/get-all

# Processing the checkout - http://localhost:5010/checkout/process

                        {
                        "CartItems": [
                           {
                              "productId": 2,
                              "productDetailId": 4,
                              "quantity": 10
                           }
                        ]
                        }

--> User can checkout multiple items at a time.
--> Once checkout process is done, notification would be sent to notification api which can be seen in docker notification api container
--> for any error during checkout, notification would be sent as well. Also we would be undo the changes we have already done by executing compensate transactions.
