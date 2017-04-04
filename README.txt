# CalculatorWebService

- In Visual Studio opens the two projects and runs both of them

- On CalculatorService.client, the method for testing with user parameters call to ConexionController.cs, but the option test(6) calls to TestCalculator.cs
- Class response is the same for all server responses  however the request is different in Divide method and Square
- We have two diferents Logs, one in the CalculatorService.client about the TestCalculator.cs and other for CalculatorController.cs on CalculatorService.server
- An example of "operation": Add(1+2+3+4), Subtract(1-2-3-4), Multiply(1*2*3*4). If you write a bad expression on Add method such as 1*2*3*4, the server send you that there is a bad expression for Add method and you return to the top again 
