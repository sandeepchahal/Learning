Overview

This project is built using .NET 9.0 and is designed to read a CSV file, load its data into a SQL database, and expose APIs for processing and querying the data.

Setup Instructions

SQL Server Setup
The application expects SQL Server to be running in Docker. You can either run it using the Docker command below or update the connection string in the Lumel.ApiService project if you're using an existing database.
CSV File Location
The application expects a data.csv file to be available at a specific path. Please provide the full path, including the file name, in the appsettings.json file.
Running the Application
Set Lumel.ApiService as the startup project.
Run the project. On startup, the application will automatically apply any pending database migrations and start the service.
Background Job
A background job will execute on startup, read the specified CSV file, and load its contents into the database. This job runs at a 60-minute interval by default (configurable in appsettings.json).
Swagger UI
Once the service is running, you can access the Swagger UI at:
http://localhost:5033/swagger/index.html
API Endpoints

Route | Method | Request Body | Sample Response | Description
/api/csv/process | POST | None | File is processed successfully | Processes the CSV file and loads its contents into the database, similar to the background job.
/api/order/revenue | GET | None (query params: startDate, endDate [optional]) | {
"totalRevenue": 68255.3839,
"totalRevenueByProduct": {
"P110": 1287.39,
"P121": 1432.6004,
"P137": 209.2794,
"P139": 348.894,
"P141": 96.0498,
"P143": 2198.924,
"P147": 1130.16,
"P157": 430.1184,
"P183": 130.2048,
"P184": 80.8696,
"P187": 524.6944,
"P200": 719.926,
"P221": 761.5944,
"P228": 124.978,
"P236": 901.4892,
"P238": 570.84,
"P244": 1468.586,
"P247": 493.4604,
"P252": 1200.591,
"P253": 517.4325,
"P259": 838.95,
"P268": 2330.6085,
"P273": 628.2792,
"P282": 2194.237,
"P290": 1689.158,
"P291": 83.16,
"P300": 180.5997,
"P310": 1226.3968,
"P323": 1108.9152,
"P324": 746.253,
"P329": 166.866,
"P348": 528.9138,
"P349": 243.1792,
"P356": 1452.97,
"P384": 1957.52,
"P400": 287.9424,
"P401": 468.0186,
"P410": 263.9728,
"P411": 462.75,
"P421": 1153.6427,
"P424": 1158.885,
"P436": 124.2368,
"P437": 1158.5616,
"P438": 127.1809,
"P454": 187.3773,
"P455": 373.2215,
"P461": 239.1606,
"P469": 1154.696,
"P482": 1304.0544,
"P484": 346.1424,
"P494": 232.1307,
"P517": 1408.7304,
"P533": 109.04,
"P545": 673.3898,
"P557": 1365.7248,
"P560": 1143.3708,
"P566": 246.9582,
"P576": 358.68,
"P580": 129.195,
"P584": 1429.4576,
"P588": 399.5144,
"P589": 100.301,
"P590": 204.6675,
"P591": 1023.6408,
"P601": 701.6592,
"P647": 1103.7168,
"P658": 267.2608,
"P664": 426.0762,
"P680": 424.6704,
"P731": 79.8028,
"P732": 1594.1756,
"P768": 444.808,
"P772": 67.2012,
"P781": 967.1535,
"P786": 1763.01,
"P789": 104.063,
"P800": 2044.574,
"P818": 345.8511,
"P819": 138.533,
"P856": 25.043,
"P859": 578.442,
"P860": 37.096,
"P864": 578.88,
"P870": 179.5306,
"P885": 1454.112,
"P889": 652.2912,
"P894": 147.1673,
"P908": 570.24,
"P910": 662.3655,
"P942": 329.2176,
"P943": 819.2416,
"P948": 164.6678,
"P956": 2049.74,
"P964": 1142.1265,
"P985": 296.24,
"P993": 457.6945
},
"totalRevenueByCategory": {
"Accessories": 17769.9306,
"Books": 11160.3921,
"Clothing": 8108.025,
"Electronics": 16858.343,
"Shoes": 14358.6932
},
"totalRevenueByRegion": {
"Asia": 19613.765,
"Australia": 9782.9741,
"Europe": 19409.8133,
"North America": 19448.8315
}
}| Calculates the revenue between the specified startDate and optional endDate.
