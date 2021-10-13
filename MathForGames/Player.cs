using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;

namespace MathForGames
{
    class Player : Actor
    {
        private Vector2 _velocity;
        private float _speed;

        public float GetSpeed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector2 GetVelocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Player(char icon, float x, float y, float speed, string name = "Actor", ConsoleColor IconColor = ConsoleColor.White) :
            base(icon, x, y, name, IconColor)
        {
            _speed = speed;
        }

        /// <summary>
        /// Checks which button is pressed by the player, then moves the player object to that position
        /// Updates every frame
        /// </summary>
        public override void Update()
        {
            Vector2 Movedirection = new Vector2();
            ConsoleKey KeyPressed = Engine.GetConsoleKey();

            if (KeyPressed == ConsoleKey.A)
            {
                Movedirection = new Vector2 { X = -1};
            }
            if (KeyPressed == ConsoleKey.D)
            {
                Movedirection = new Vector2 { X = 1 };
            }
            if (KeyPressed == ConsoleKey.W)
            {
                Movedirection = new Vector2 { Y = -1 };
            }
            if (KeyPressed == ConsoleKey.S)
            {
                Movedirection = new Vector2 { Y = 1 };
            }

            GetVelocity = Movedirection * _speed;

            GetPosition += _velocity;
        }


        /// <summary>
        /// Preforms an action if the position of the player is equal to the position of another actor
        /// or the child of an actor
        /// </summary>
        /// <param name="collider"> The actor the player collided with </param>
        public override void OnCollision(Actor collider)
        {
            base.OnCollision(collider);
            if (collider is Racer)
            {
                Engine.KnockOpponentBack(collider);
            }
        }
    }
}
