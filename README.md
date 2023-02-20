# The project has following dependencies:
    TargetFramework = net6.0
    "Microsoft.NET.Test.Sdk" Version="17.1.0" 
    "MSTest.TestAdapter" Version="2.2.8" 
    "MSTest.TestFramework" Version="2.2.8"  
    "RestSharp" Version="108.0.3"

The easiest way to read the code and run tests is to open sln file in Visual Studio (2022 version is preferable)
Tests are written on MStest framework which is VS native

For the non functional testing I'd suggest 
1. Perfromance testing
2. Load testing. 

For both load and performance testing good tool is Jmeter. It's powerfull, open source, is well documented. Also a lot of load testing services support runnig Jmeter

3. Security testing can be done with Owasp or Acunetix
