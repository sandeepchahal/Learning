1. Run the below command to start the consul service. Make sure you are in the root directory where the file is present
    docker-compose -f consul.yml up -d
2. Run the api services and they should appear on the consul localhost url: - http://localhost:8500
3. Run the API gateway. Below are the API urls that can be called.
    API Gateway URL: http://localhost:5079
    Customer API - http://localhost:5079/customer/**
    Account API - http://localhost:5079/account/**
Note: -  ** Should be replaced by actual endpoints

