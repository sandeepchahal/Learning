1. Run the below command to start the consul service. Make sure you are in the root directory where the file is present
    docker compose up -d

    All the services should be running in docker
2. Try accessing http://localhost:8500
    - it should show customer and account api registered

3. Api gateway would be running on http://localhost:8080
    - Access Customer apis as below
        --> http://localhost:8080/customer/get-all --> Get all the customers
        --> http://localhost:8080/customer/get/1 --> Get the customer with specified id
        --> http://localhost:8080/customer/add -->add the customer by passing below json data
            {
                "Id": 1,
                "Name": "John Doe",
                "Email": "john.doe@example.com",
                "Phone": "123-456-7890"
            }

        --> http://localhost:8080/customer/update/1 --> pass the id in the url to update the customer along with below data
            {
                "Id": 1,
                "Name": "John Doe",
                "Email": "john.doe@example.com",
                "Phone": "123-456-7890"
            }

        --> http://localhost:8080/customer/delete/1  --> it would delete the customer

    - Access Account apis as below
        --> http://localhost:8080/account/get/1 --> Get the account and customer with specified id
        --> http://localhost:8080/customer/addMoney/customerId -->add the money by specifiying the customer id in url and passing the amount in body
        --> http://localhost:8080/customer/withdrawMoney/customerId -->withdrwal the money by specifiying the customer id in url and passing the amount in body



