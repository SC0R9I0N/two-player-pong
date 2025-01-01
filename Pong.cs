using System; 
using System.Linq; 
using System.Threading;

class Pong {

    // Creating the field for the game
    const int LENGTH = 50;
    const int WIDTH = 15;
    const char TILE = '#';

    static void Main() {
        string line = string.Concat(Enumerable.Repeat(TILE, LENGTH));

        // print the borders 
        while(true) { 
            Console.SetCursorPosition(0,0);
            Console.WriteLine(line);

            Console.SetCursorPosition(0, WIDTH);
            Console.WriteLine(line);
        }

        // make racket sizes
        const int RACKET_LENGTH = WIDTH/4;
        const char RACKET_TILE = '|';

        // initialize the heights
        int leftHeight = 0;
        int rightHeight = 0;

        // make the rackets appear
        for(int i = 0; i < RACKET_LENGTH; i++) {
            Console.SetCursorPosition(0, i + 1 + leftHeight);
            Console.WriteLine(RACKET_TILE);
            Console.SetCursorPosition(LENGTH - 1, i + 1 + rightHeight);
            Console.WriteLine(RACKET_TILE);
        }

        // add the ball
        int ballX = LENGTH/2;
        int ballY = WIDTH/2;
        const char BALL_TILE = 'o';

        bool isBallDown = true;
        bool isBallRight = true;

        // handle player movement
        while(!Console.KeyAvailable) {
            Console.SetCursorPosition(ballX, ballY);
            Console.WriteLine(BALL_TILE);
            Thread.Sleep(100); // adds timer so players can react

            Console.SetCursorPosition(ballX, ballY);
            Console.WriteLine(" ");

            //update position of ball
            if(isBallDown) {
                ballY++;
            } else {
                ballY--;
            }

            if(isBallRight) {
                ballX++;
            } else {
                ballX--;
            }
        }

        // check when key is pressed
        switch(Console.ReadKey().Key) {
            case ConsoleKey.UpArrow:
            if(rightHeight > 0) {
                rightHeight --;
            }
            break;

            case ConsoleKey.DownArrow:
            if(rightHeight < WIDTH - RACKET_LENGTH - 1) {
                rightHeight ++;
            }
            break;

            case ConsoleKey.W:
            if(leftHeight > 0) {
                leftHeight--;
            }
            break;

            case ConsoleKey.S:
            if(leftHeight < WIDTH - RACKET_LENGTH - 1) {
                leftHeight++;
            }
            break;
        }

        // clear previous position
        for(int i = 0; i < WIDTH; i++) {
            Console.SetCursorPosition(0,i);
            Console.WriteLine(" ");
            Console.SetCursorPosition(LENGTH - 1, i);
            Console.WriteLine(" ");
        }

        int leftPoints = 0;
        int rightPoints = 0;

        int scoreboardX = LENGTH / 2 - 2;
        int scoreboardY = WIDTH + 1;

        if(ballY == 1 || ballY == WIDTH -1) {
            isBallDown = !isBallDown; //change direction
        }

        if(ballX == 1) {
            //left racket hits ball and it bounces
            if(ballY >= leftHeight + 1 && ballY <= leftHeight + RACKET_LENGTH) {
                isBallRight = !isBallRight;
            } else {
                rightPoints++;
                ballY = WIDTH/2;
                ballX = LENGTH/2;
                Console.SetCursorPosition(scoreboardX, scoreboardY);
                Console.WriteLine($"{leftPoints} | {rightPoints}");
                if(rightPoints == 10) {
                    goto outer;
                }
            }
        }

        if(ballX == LENGTH - 2) {
            //right racket hits ball
            if(ballY >= rightHeight + 1 && ballY <= rightHeight + RACKET_LENGTH) {
                isBallRight = !isBallRight;
            }else {
                leftPoints++;
                ballY = WIDTH/2;
                ballX = LENGTH/2;
                Console.SetCursorPosition(scoreboardX, scoreboardY);
                Console.WriteLine($"{leftPoints} | {rightPoints}");
                if(leftPoints == 10) {
                    goto outer;
                }
            }
        }

        outer:;
                Console.Clear();
                Console.SetCursorPosition(0,0);

                if(rightPoints == 10) {
                    Console.WriteLine("Right player won!");
                }
                if(leftPoints == 10) {
                    Console.WriteLine("Left player won!");
                }
    }
}