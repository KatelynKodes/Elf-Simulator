using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Racer: Actor
    {
        //Private variables
        private float _RunSpeed;
        private Vector2 _velocity;

        // Constructor
        public Racer(char RacerIcon, float x, float y, float RacerSpeed, Color Racercolor, string RacerName) 
            : base(RacerIcon, x, y, Racercolor, RacerName)
        {
            _RunSpeed = RacerSpeed;
        }

        /// <summary>
        /// Moves the racer to the right at a constant velocity depending on their runspeed
        /// Then adds that velocity to the players position to change the racers position on screen
        /// </summary>
        public override void Update(float deltaTime)
        {
            //Movement direction is always forward
            Vector2 MovementDirection = new Vector2 { X = 1 };

            //Sets the velocity to the movement direction times the runspeed
            _velocity = MovementDirection * _RunSpeed;

            //adds the velocity to the base position
            base.GetPosition += _velocity;
        }

        public override void OnCollision(Actor actor)
        {
            base.OnCollision(actor);
        }
    }
}
