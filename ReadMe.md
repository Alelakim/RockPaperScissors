# RockPaperScissors

## Description
A application that alows you to settle your diffrences in a game of rock, paper or scissors via api or swagger.

The application is written as .NetCoreApp v6.0 and you can run it from Visual Studio or any application that enables you the send api requests (Postman is one example)

This is the first version and it is fairly simple. When creating this the focus was to get the strucutres in place. To make it easy to improve and scale in the future.
At the moment the data is not persisted, it is only in memory. But since the structure is there you only have swap the InMemoryGameRepository to a Db-class to change that.
In the next version the programming would be changed to run asynchronous, but for now it is synchronous.
In the next version the testing would also be extended with integration test and further unit testing.

##How to run the program
Clone the project from github [Link](https://github.com/Alelakim/RockPaperScissors) to Visual Stuido OR if you have the files, open up the solution in Visual Studio 2019 (or higher)

## How to use the application
You can only be two players and you must use different names. Example of game:

1. PLayer One sends a request (/api/games), including your name in the body, to create a new game and will recive a response with the game id.
2. Player One sends the id to player Two via slack, email or prefferd choice.
3. Player Two joins the game (/api/games/{id}/join) using the game id and includes their name in the request body.
4. PLayer One makes a move (rock) (api/games/{id}/move) using the game id and includes their name and move in the body.
5. PLayer Two makes a move (paper) (api/games/{id}/move) using the game id and includes their name and move in the body.
6. Player One checks the result and can see that they won
7. Player two checks the result and can see they lost

