# Standart VAT Rates in EU
An application in C# capable of printing out three EU countries with the lowest and three EU countries with the highest standard VAT rate as of today within the EU.

This console application uses http://jsonvat.com/ to retrieve the list of all vat rates across the EU in JSON format. 

# Notes
Since the required functionality is pretty simple, we just need to display the top 3 lowest and highest rates, there was no point in implementing more complicated design techniques. Some improvements however could be introduced to ensure future-proofing and consistency:

1. Implement a "Facade" design approach where we create an interface for accessing the data while we hide the source. We could add more sources and abstract the retrieval and formatting proccesses. 
2. Implement caching (like Redis) to minimize requests to the web api and to ensure consistency. 
3. We could have used different libraries to retrieve the data, like RestSharp (https://github.com/restsharp/RestSharp), that include Serialization/Deserialization mechanisms.

