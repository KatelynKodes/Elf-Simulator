using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;

namespace MathForGames
{
    class UIText : Actor
    {
        public string Text;
        public int Width;
        public int Height;

        public UIText(float x, float y, string name, ConsoleColor color, int width, int height, string text = "")
            : base('\0', x, y, name, color)
        {
            Text = text;
            Width = width;
            Height = height;
        }

        public override void Draw()
        {
            int CursorPosX = (int)GetPosition.X;
            int CursorPosY = (int)GetPosition.Y;

            Icon currentLetter = new Icon {color = GetIcon.color};

            char[] textChars = Text.ToCharArray();

            for (int i = 0; i < textChars.Length; i++)
            {
                currentLetter.Symbol = textChars[i];

                if (currentLetter.Symbol == '\n')
                {
                    CursorPosX = (int)GetPosition.X;
                    CursorPosY++;
                    continue;
                }

                Engine.Render(currentLetter, new Vector2 { X = CursorPosX, Y = CursorPosY });

                CursorPosX++;

                if (CursorPosX - (int)GetPosition.X > Width)
                {
                    CursorPosX = (int)GetPosition.X;
                    CursorPosY++;
                }

                if (CursorPosY - (int)GetPosition.Y > Height)
                {
                    break;
                }
            }
        }
    }
}
