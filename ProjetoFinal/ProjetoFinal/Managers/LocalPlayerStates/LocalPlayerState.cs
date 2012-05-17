﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using ProjetoFinal.Entities;
using OgmoLibrary;

namespace ProjetoFinal.Managers.LocalPlayerStates
{
    abstract class LocalPlayerState
    {
        static Dictionary<Enum, LocalPlayerState> localPlayerStates = new Dictionary<Enum, LocalPlayerState>();

        public abstract LocalPlayerState Update(GameTime gameTime, Player localPlayer, Layer collisionLayer);

        #region Public Messages

        public virtual LocalPlayerState Jumped(Player localPlayer)
        {
            return this;
        }

        public virtual LocalPlayerState MovingLeft(Player localPlayer)
        {
            localPlayer.Speed -= localPlayer.walkForce;
            localPlayer.FacingLeft = true;

            return this;
        }

        public virtual LocalPlayerState MovingRight(Player localPlayer)
        {
            localPlayer.Speed += localPlayer.walkForce;
            localPlayer.FacingLeft = false;

            return this;
        }

        #endregion

        #region Protected Methods

        protected bool checkVerticalCollision(Rectangle collisionBox, Vector2 speed, Layer collisionLayer)
        {
            Point corner1, corner2;

            if (speed.Y < 0)
            {
                corner1 = new Point(collisionBox.Left, collisionBox.Top);
                corner2 = new Point(collisionBox.Right, collisionBox.Top);
            }
            else
            {
                corner1 = new Point(collisionBox.Left, collisionBox.Bottom);
                corner2 = new Point(collisionBox.Right, collisionBox.Bottom);
            }

            if (collisionLayer.GetTileValueByPixelPosition(corner1) || collisionLayer.GetTileValueByPixelPosition(corner2))
                return true;

            return false;
        }

        protected bool checkHorizontalCollision(Rectangle collisionBox, Vector2 speed, Layer collisionLayer)
        {
            Point corner1, corner2;

            if (speed.X < 0)
            {
                corner1 = new Point(collisionBox.Left, collisionBox.Top);
                corner2 = new Point(collisionBox.Left, collisionBox.Bottom);
            }
            else
            {
                corner1 = new Point(collisionBox.Right, collisionBox.Top);
                corner2 = new Point(collisionBox.Right, collisionBox.Bottom);
            }

            if (collisionLayer.GetTileValueByPixelPosition(corner1) || collisionLayer.GetTileValueByPixelPosition(corner2))
                return true;

            return false;
        }

        protected bool handleHorizontalCollision(Player localPlayer, Layer collisionLayer)
        {
            Rectangle collisionBoxOffset = localPlayer.CollisionBox;

            for (int i = 0; i < Math.Abs(localPlayer.Speed.X); ++i)
            {
                collisionBoxOffset.Offset(Math.Sign(localPlayer.Speed.X), 0);
                if (!checkHorizontalCollision(collisionBoxOffset, localPlayer.Speed, collisionLayer))
                {
                    localPlayer.Position += new Vector2(Math.Sign(localPlayer.Speed.X), 0);
                }
                else
                {
                    localPlayer.SpeedX = 0;
                    return true;
                }
            }

            return false;
        }

        protected bool handleVerticalCollision(Player localPlayer, Layer collisionLayer)
        {
            Rectangle collisionBoxOffset = localPlayer.CollisionBox;

            for (int i = 0; i < Math.Abs(localPlayer.Speed.Y); ++i)
            {
                collisionBoxOffset.Offset(0, Math.Sign(localPlayer.Speed.Y));

                if (!checkVerticalCollision(collisionBoxOffset, localPlayer.Speed, collisionLayer))
                {
                    localPlayer.Position += new Vector2(0, Math.Sign(localPlayer.Speed.Y));
                }
                else
                {
                    localPlayer.SpeedY = 0;
                    return true;
                }
            }

            return false;
        }

        /*
        protected LocalPlayerState clampHorizontalSpeed(Vector2 speed, LocalPlayerState localPlayerState)
        {
            // So player doesn't slide forever
            if (Math.Abs(speed.X) < 0.2)
            {
                speed.X = 0;

                return localPlayerState;
            }
        }*/
        #endregion
    }
}