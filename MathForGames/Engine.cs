using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MathLibrary;

namespace MathForGames
{
    class Engine
    {
        private static bool _shouldApplicationclose = false;
        private static int _currentSceneIndex;
        private static Scene[] _scenes = new Scene[0];
        private static Icon[,] _buffer;
        private static string _winnerName = "";

        /// <summary>
        /// Called to begin the application
        /// </summary>
        public void Run()
        {
            //Call start for the entire application
            Start();

            //Loop until application is told to close
            while (!_shouldApplicationclose)
            {
                Update();
                Draw();
                Thread.Sleep(150);
            }

            //Calll at the end of the entire application.
            End();
        }

        /// <summary>
        /// Called when the application starts
        /// </summary>
        private void Start()
        {
            Scene RaceScene = new Scene();

            // Finish lines 
            Actor FinishLine1 = new Actor('|', new Vector2 { X = 115, Y = 0 }, "FinishLine", ConsoleColor.Green);
            Actor FinishLine2 = new Actor('|', new Vector2 { X = 115, Y = 1 }, "FinishLine", ConsoleColor.Green);
            Actor FinishLine3 = new Actor('|', new Vector2 { X = 115, Y = 2 }, "FinishLine", ConsoleColor.Green);
            Actor FinishLine4 = new Actor('|', new Vector2 { X = 115, Y = 3 }, "FinishLine", ConsoleColor.Green);

            //Racers
            Racer Racer1 = new Racer('S', 0, 0, 3, "Selby", ConsoleColor.Yellow);
            Racer Racer2 = new Racer('A', 0, 1, 4, "Adrien", ConsoleColor.Red);
            Racer Racer3 = new Racer('D', 0, 2, 2, "Dianne", ConsoleColor.Blue);

            //Player
            Player RacerPlayer = new Player('P', 0, 3, 2, "Player", ConsoleColor.Cyan);

            //Adds all actors to the race scene
            RaceScene.AddActor(FinishLine1);
            RaceScene.AddActor(FinishLine2);
            RaceScene.AddActor(FinishLine3);
            RaceScene.AddActor(FinishLine4);
            RaceScene.AddActor(Racer1);
            RaceScene.AddActor(Racer2);
            RaceScene.AddActor(Racer3);
            RaceScene.AddActor(RacerPlayer);

            //Sets up the scene array
            _scenes = new Scene[] { RaceScene };

            //Starts the current scene
            _scenes[_currentSceneIndex].Start();

            //Sets cursor to be invisible
            Console.CursorVisible = false;
        }

        /// <summary>
        /// Called everytime the game loops
        /// </summary>
        private void Update()
        {
            _scenes[_currentSceneIndex].Update();

            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }

        /// <summary>
        /// Called At the end of the application
        /// </summary>
        private void End()
        {
            _scenes[_currentSceneIndex].End();
            Console.WriteLine(_winnerName + "Is the winner of the race");
        }

        /// <summary>
        /// Called everytime the game loops to update visuals
        /// </summary>
        private void Draw()
        {
            //Clear the stuff that was on the screen in the last frame
            _buffer = new Icon[Console.WindowWidth, Console.WindowHeight-1];

            //Reset the cursor position
            Console.SetCursorPosition(0, 0);

            //Adds all actor icons to buffer
            _scenes[_currentSceneIndex].Draw();

            //Iterate through buffer
            for (int y = 0; y < _buffer.GetLength(1); y++)
            {
                for (int x = 0; x < _buffer.GetLength(0); x++)
                {
                    if (_buffer[x, y].Symbol == '\0')
                    {
                        _buffer[x, y].Symbol = ' ';
                    }
                    //Set console color
                    Console.ForegroundColor = _buffer[x, y].color;
                    //Print the symbol of the item in the buffer
                    Console.Write(_buffer[x, y].Symbol);
                }
                //Skip a line once row is complete
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Adds a new scene to the engines scene array
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public int AddScene(Scene scene)
        {
            //Create Temporary Array
            Scene[] TempArray = new Scene[_scenes.Length + 1];

            //Copy all values into temporary array
            for (int i = 0; i < _scenes.Length; i++)
            {
                TempArray[i] = _scenes[i];
            }

            //Set the last index to be the new scene
            TempArray[_scenes.Length] = scene;

            //set the old array to the new array
            _scenes = TempArray;

            //Return the last index
            return _scenes.Length - 1;
        }


        /// <summary>
        /// Gets the next key in the input stream
        /// </summary>
        /// <returns>The key that was pressed</returns>
        public static ConsoleKey GetConsoleKey()
        {
            //If there is No Key being pressed...
            if (!Console.KeyAvailable)
            {
                //...Return
                return 0;
            }

            //Return the current key being pressed
            return Console.ReadKey(true).Key;
        }

        /// <summary>
        /// Addds the icon to the buffer to print to the screen in the next draw call.
        /// prints the icon at the given position in the buffer.
        /// </summary>
        /// <param name="icon"> The icon to draw</param>
        /// <param name="position"> the position of the icon in the buffer</param>
        /// <returns>False if the position is outside the bounds of the buffer</returns>
        public static bool Render(Icon icon, Vector2 position)
        {
            //If the position is out of bounds...
            if (position.X < 0 || position.X >= _buffer.GetLength(0) || position.Y < 0 || position.Y >= _buffer.GetLength(1))
            {
                //...Returns false
                return false;
            }

            //set the buffer at the position index to the icon.
            _buffer[(int)position.X, (int)position.Y] = icon;
            return true;
        }

        //Closes the Application
        public static void CloseApplication()
        {
            _shouldApplicationclose = true;
        }

        public static void ChangeWinnerName(string newName)
        {
            _winnerName = newName;
        }

        /// <summary>
        /// Knocks an opposing actor back a space if the player collides with it
        /// </summary>
        /// <param name="opponent"></param>
        public static void KnockOpponentBack(Actor opponent)
        {
            //How much actor is knocked back
            Vector2 KnockbackValue = new Vector2 { X = 1, Y = 0 };

            //Subtract the current position by that knockback value
            opponent.GetPosition -= KnockbackValue;
        }
    }
}
