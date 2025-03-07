Weather Tracker (Develop Azure Compute Solutions)
This is a web application that helps users track real-time weather updates for their selected cities. It also sends alerts when certain weather conditions are met, such as predicting rain.

Infrastructure Overview:
1. Azure App Service Web App – Hosts the web application, making it accessible to users.
2. Azure Container Registry – Stores Docker images of the app for easy deployment.
3. Azure Container Instance – Runs containers for development and testing purposes.
4. Azure Functions – Triggers weather alerts when specific conditions are detected.
5. Azure Container Apps – Runs the application’s containers in a production environment.

This setup ensures the app is scalable, efficient, and can send timely weather updates and alerts.
