using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace URGP
{
    internal class Sequencer
    {

        public static void SequenceLines(Game1 game)
        {
            if (game.filePath == @"Dialogues/Dia1.dlg")
            {
                if (game.Line == 7)
                {
                    if (Vector2.Distance(game.bottomMid, game.NewbottomMid) <= 0.1f)

                    {
                        game.Line++;
                        Progress.ProgressLines(game);
                        game.infoText2 = "";
                    }

                }
                else if (game.Line == 8)
                {
                    if (Vector2.Distance(game.bottomMid, game.NewbottomMid) <= 0.1f)
                    {
                        game.Line++;
                        Progress.ProgressLines(game);
                        game.infoText2 = "";
                    }
                }
                else if (game.Line == 9)
                {
                    if (Vector2.Distance(game.bottomLeft, game.NewbottomLeft) <= 0.1f && Vector2.Distance(game.bottomRight, game.NewbottomRight) <= 0.1f)
                    {
                        game.Line++;
                        Progress.ProgressLines(game);
                        game.infoText2 = "";
                    }
                }
            }
            }
        }
}
