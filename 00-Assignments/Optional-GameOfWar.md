# Card Game War Simulation in VB.NET

## Objective
The purpose of this assignment is to develop a VB.NET program that simulates the classic card game "War" between two players. Through this project, you will learn about object-oriented programming, user interface design, and game logic implementation using classes and modules. By the end of this assignment, you will have a functioning card game that showcases your understanding of these concepts.

## Project Structure
The project consists of multiple classes and modules to separate the user interface from the game logic. Below are the key components of the project:

### 1. User Interface Class (WarGUIForm.vb)
- Handles user interactions and displays game information.
- Contains buttons for starting and ending the game.
- Displays the number of cards each player has, the number of captured cards for each player, the winner of each round, and the overall game winner (by captured cards).

### 2. Game Logic Class (WarGameLogic.vb)
- Manages the game mechanics, including shuffling the deck, dealing cards, and tracking rounds and wars.
- Contains methods to start the game, initialize the deck, shuffle cards, and play rounds.

### 3. Supporting Classes
- Card Class: Represents a single card with a rank and suit.
- WarPlayer Class: Represents a player, managing their hand and captured cards, and allowing them to play cards during the game.


## Requirements
To successfully complete this assignment, you will need to implement the following components and follow the updated game rules:


### Game Rules

- The game ends immediately when a player cannot continue because they have no cards left to play.
- If both players run out of cards at the same time (such as during a war), the game ends. The player with the most captured cards wins; if both have the same number, the game is a tie.
- Captured cards from each round are not replayed; they are kept in a separate pile for each player.
- At the end of the game, the player with the most captured cards is declared the winner. If both players have the same number of captured cards, the game is a tie.



### How to Handle "War" Situations

- A "war" occurs when both players play cards of equal rank.
- In a war, each player places three cards face down (if possible) and then one card face up.
- The face-up cards are compared:
  - The player with the higher face-up card wins all cards played in that war, including both the original tied cards and all face-down cards.
  - If the face-up cards are tied again, another war occurs immediately (“back-to-back war”). Repeat the process: each player places three more cards face down (if possible) and one face up.
  - Continue this process until one player wins the war or a player cannot continue (does not have enough cards).
- If a player has fewer than four cards, they place all but their last card face down and play the last card face up. If a player runs out of cards during a war, the game ends immediately. If both players run out of cards at the same time during a war, the game ends and the winner is determined by the number of captured cards, or declared a tie if both have the same number.
- The winner of the war collects all cards played in that war, including both face-down and face-up cards.


### Displaying Cards in Play

- All cards that are in play during a round or war (including face-up and face-down cards) must be displayed on the game interface.
- Use the provided card images in the resources folder to show each card. For face-down cards, use the image file named "back.jpg"; for face-up cards, use the appropriate card face image.
- Do not display the cards in each player’s hand or captured pile; only show the cards currently in play for the round or war.

### Implementation Guidelines

- Track each player’s hand (cards available to play) and their captured pile (cards won in rounds).
- When a player wins a round (or a war), add the captured cards to their captured pile, not their hand.
- Do not shuffle or replay captured cards.
- The game should end as soon as either player’s hand is empty.
- Display the final result: the number of captured cards for each player and announce the winner.

- GUI Class
  - Create a Windows Forms application with controls for:
    - Start new game button
    - End game button
    - Labels to display:
      - Number of cards each player has
      - Number of captured cards for each player
      - Winner of each round
      - Total rounds played
      - Total wars played
    - Exit button
    - Splash screen for initial display

- Game Logic Class
  - Initialize and shuffle a standard 52-card deck.
  - Deal cards evenly between two players.
  - Play rounds until one player runs out of cards.
  - Handle "wars" when cards of the same rank are played.
  - Track captured cards separately for each player.
  - At the end, compare the number of captured cards and declare the winner.
  - Implement methods to play cards, capture cards, and get the count of remaining and captured cards from players.

- Card Class
  - Implement properties for rank and suit.
  - Include a constructor to initialize card attributes.

- WarPlayer Class
  - Manage a queue of cards for each player (hand).
  - Manage a separate collection for captured cards.

## Validation
Ensure that the following validations are implemented:

- Prevent starting a new game if one is already in progress.
- Handle cases where players have no cards left appropriately.
- Display appropriate messages for each game state, including end game conditions.


## Functionality
Your program should include the following functionalities:

- When the "Start New Game" button is clicked:
  - Initialize the game and shuffle the deck.
  - Deal cards to the two players.
  - Begin playing rounds until one player runs out of cards, or both run out of cards at the same time (especially during a war).

- In each round:
  - Each player plays the top card of their deck.
  - Determine the round winner based on card rank.
  - Handle cases of "wars" (when both players play cards of equal rank), including the possibility that both players run out of cards at the same time.

- When the game ends:
  - Display the overall winner (the player with the most captured cards) and the final results, including the number of captured cards for each player.
  - If both players have the same number of captured cards, display that the game ended in a tie.


## User Interface
  - Labels for displaying game state information, including the number of captured cards for each player and the final winner by captured cards. Show only the number of captured cards for each player; do not display the actual cards in the captured pile.

- Use Windows Forms to create the GUI.
+- Verify that only cards currently in play for the round or war are displayed, and that players' hands and captured piles are not shown visually (only the count of captured cards is displayed). The count of captured cards should be clearly shown in the user interface.
  - Buttons for starting and ending the game.
  - Labels for displaying game state information, including the number of captured cards for each player and the final winner by captured cards.
  - Use a splash screen for the initial loading.
  - Display all cards currently in play during a round or war, showing face-up and face-down cards using the provided images from the resources folder.
  - You do NOT need to display the contents of each player's hand or their captured pile—only the cards currently in play for the round or war.


## Testing
Test your program with the following scenarios:

- Start a game and ensure both players are dealt 26 cards.
- Play multiple rounds, ensuring the correct winner is announced for each round.
- Test the "war" functionality by playing rounds where both players have equal ranked cards.
- Test back-to-back wars (multiple consecutive wars) and verify that all cards in play are displayed correctly (face up or face down) using the provided images.
- Verify that only cards currently in play for the round or war are displayed, and that players' hands and captured piles are not shown.
- End the game and verify that the correct winner (by captured cards) is displayed, along with the number of captured cards for each player.
- Test the case where both players run out of cards at the same time (especially during a war) and confirm the game ends as a tie or with the correct winner by captured cards.


### Edge Cases
- Attempt to start a new game while one is in progress.
- Ensure the program handles cases when one player runs out of cards correctly.
- Ensure the program handles cases when both players run out of cards at the same time, especially during a war. In this case, the game ends and the winner is determined by the number of captured cards, or declared a tie if both have the same number.

## Submission Instructions
- Submit your github repository link containing the complete project. 
- Ensure all files/Resources are included and the project runs without errors.
- Ensure the following naming conventions are used:
  - Main form: WarGUIForm.vb
  - Game logic: WarGameLogic.vb
  - Card class: Card.vb
  - Player class: WarPlayer.vb
  - Splash screen: SplashScreenForm.vb
  - About Form: AboutForm.vb
