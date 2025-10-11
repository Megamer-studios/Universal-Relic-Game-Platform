using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
namespace URGP
{
    internal class Progress
    {
        public static void ProgressLines(Game1 game)
        {
            if (game.filePath == @"Dialogues/Dia1.dlg")
            {
                if (game.Line == 1)
                {
                    game.Line1();
                }
                else if (game.Line == 2)
                {
                    game.isQuestion = false;
                    game.infoText = "";
                    game.bgImg = false;
                    game.backgroundColor = Color.Black;
                    game.background = game.Content.Load<Texture2D>("bg2");
                    game.NewbottomMid.Y += 900;
                    game.NewbottomLeft.Y += 900;
                    game.NewbottomRight.Y += 900;
                    game.Cem1 = game.Content.Load<Texture2D>("Empty");
                    game.Cem2 = game.Content.Load<Texture2D>("Empty");
                    game.Cem3 = game.Content.Load<Texture2D>("Empty");
                    game.isSoundPlaying = false;
                    game.soundEffect = game.Content.Load<SoundEffect>("Scream");
                    game.soundEffect.Play();
                    game.Portrait = game.Content.Load<Texture2D>("Empty");
                    game.BMG = game.Content.Load<SoundEffect>("AMachine");
                }
                else if (game.Line == 3)
                {
                    game.isQuestion = false;
                    game.infoText = "";
                    game.bgImg = true;
                    game.backgroundColor = Color.Black;
                    game.background = game.Content.Load<Texture2D>("bg2");
                    game.NewbottomMid.Y -= 900;

                    game.Cem1 = game.Content.Load<Texture2D>("Sprite2");
                    game.Cem2 = game.Content.Load<Texture2D>("Sprite1");
                    game.Cem3 = game.Content.Load<Texture2D>("Sprite5");
                    game.isSoundPlaying = true;
                    game.Portrait = game.Content.Load<Texture2D>("Portrait2");
                    game.BMG = game.Content.Load<SoundEffect>("AMachine");
                }
                else if (game.Line == 4)
                {
                    game.isQuestion = false;
                    game.infoText = "";
                    game.bgImg = true;
                    game.backgroundColor = Color.Black;
                    game.background = game.Content.Load<Texture2D>("bg2");
                    game.ResetPositions();
                    game.Cem1 = game.Content.Load<Texture2D>("Sprite2");
                    game.Cem2 = game.Content.Load<Texture2D>("Sprite1");
                    game.Cem3 = game.Content.Load<Texture2D>("Sprite5");
                    game.isSoundPlaying = true;
                    game.Portrait = game.Content.Load<Texture2D>("Portrait1");
                    game.BMG = game.Content.Load<SoundEffect>("AMachine");
                }
                else if (game.Line == 5) {
                    game.isQuestion = false;
                    game.infoText = "";
                    game.bgImg = false;
                    game.backgroundColor = Color.Black;
                    game.NewbottomLeft.X -= 900;
                    game.NewbottomRight.X += 900;
                    game.background = game.Content.Load<Texture2D>("bg1");
                   
                    game.Cem1 = game.Content.Load<Texture2D>("Sprite2");
                    game.Cem2 = game.Content.Load<Texture2D>("Sprite1");
                    game.Cem3 = game.Content.Load<Texture2D>("Sprite5");
                    game.isSoundPlaying = true;
                    game.Portrait = game.Content.Load<Texture2D>("Portrait3");
                    game.BMG = game.Content.Load<SoundEffect>("Dead");
                }
                else if (game.Line == 6)
                {
                    game.isQuestion = false;
                    game.infoText = "";
                    game.bgImg = true;
                    game.backgroundColor = Color.Black;
                    game.ResetPositions();
                    game.background = game.Content.Load<Texture2D>("bg2");

                    game.Cem1 = game.Content.Load<Texture2D>("Sprite2");
                    game.Cem2 = game.Content.Load<Texture2D>("Sprite1");
                    game.Cem3 = game.Content.Load<Texture2D>("Sprite5");
                    game.isSoundPlaying = true;
                    game.Portrait = game.Content.Load<Texture2D>("Portrait1");
                    game.BMG = game.Content.Load<SoundEffect>("AMachine");
                }      
                else if (game.Line == 7)
                {
                    game.isQuestion = true;
                    game.infoText = "";
                    game.bgImg = true;
                    game.backgroundColor = Color.Black;
                    game.NewbottomMid.Y -= 200;
                    game.background = game.Content.Load<Texture2D>("bg2");

                    game.Cem1 = game.Content.Load<Texture2D>("Sprite2");
                    game.Cem2 = game.Content.Load<Texture2D>("Sprite1");
                    game.Cem3 = game.Content.Load<Texture2D>("Sprite5");
                    
                    game.isSoundPlaying = true;
                    game.Portrait = game.Content.Load<Texture2D>("Portrait2");
                    game.BMG = game.Content.Load<SoundEffect>("AMachine");
                }
                else if (game.Line == 8)
                {
                    game.isQuestion = true;
                    game.infoText = "";
                    game.bgImg = true;
                    game.backgroundColor = Color.Black;
                    game.NewbottomMid.Y += 900;
                    game.NewbottomMid.X -= 900;
                    game.background = game.Content.Load<Texture2D>("bg2");

                    game.Cem1 = game.Content.Load<Texture2D>("Sprite2");
                    game.Cem2 = game.Content.Load<Texture2D>("Sprite1");
                    game.Cem3 = game.Content.Load<Texture2D>("Sprite5");

                    game.isSoundPlaying = true;
                    game.Portrait = game.Content.Load<Texture2D>("Portrait1");
                    game.BMG = game.Content.Load<SoundEffect>("AMachine");
                }
                else if (game.Line == 9)
                {
                    game.isQuestion = false;
                    game.infoText = "";
                    game.bgImg = true;
                    game.backgroundColor = Color.Black;
                    game.NewbottomRight.X += 600;
                    game.NewbottomLeft.X -= 600;
                    game.background = game.Content.Load<Texture2D>("bg2");

                    game.Cem1 = game.Content.Load<Texture2D>("Sprite2");
                    game.Cem2 = game.Content.Load<Texture2D>("Sprite1");
                    game.Cem3 = game.Content.Load<Texture2D>("Sprite5");

                    game.isSoundPlaying = true;
                    game.Portrait = game.Content.Load<Texture2D>("Empty");
                    game.BMG = game.Content.Load<SoundEffect>("AMachine");
                }
            }
        }
    }
}
