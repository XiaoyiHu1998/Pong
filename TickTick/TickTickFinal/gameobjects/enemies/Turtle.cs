﻿using Microsoft.Xna.Framework;

class Turtle : Enemy
{
    protected float sneezeTime;
    protected float idleTime;

    public Turtle()
    {
        LoadAnimation("Sprites/Turtle/spr_sneeze@9", "sneeze", false);
        LoadAnimation("Sprites/Turtle/spr_idle", "idle", true);
        PlayAnimation("idle");
        Reset();
    }

    public override void Reset()
    {
        sneezeTime = 0.0f;
        idleTime = 5.0f;
    }

    public override void Update(GameTime gameTime)
    {
        if (alive)
        {
            base.Update(gameTime);
            if (sneezeTime > 0)
            {
                PlayAnimation("sneeze");
                sneezeTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (sneezeTime <= 0.0f)
                {
                    idleTime = 5.0f;
                    sneezeTime = 0.0f;
                }
            }
            else if (idleTime > 0)
            {
                PlayAnimation("idle");
                idleTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (idleTime <= 0.0f)
                {
                    idleTime = 0.0f;
                    sneezeTime = 5.0f;
                }
            }
            CheckPlayerCollision();
        }
        
    }

    public void CheckPlayerCollision()
    {
        Player player = GameWorld.Find("player") as Player;
        if (!CollidesWith(player))
        {
            return;
        }
        if (sneezeTime > 0)
        {
            player.Die(false);
        }
        else if (idleTime > 0 && player.Velocity.Y > 0)
        {
            player.Jump(1500);
        }
    }
}
