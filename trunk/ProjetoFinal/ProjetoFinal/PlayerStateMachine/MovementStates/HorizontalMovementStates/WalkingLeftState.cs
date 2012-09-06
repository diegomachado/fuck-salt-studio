﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ProjetoFinal.Entities;
using OgmoLibrary;
using ProjetoFinal.Network.Messages;

namespace ProjetoFinal.Managers.LocalPlayerStates
{
    class WalkingLeftState : HorizontalMovementState
    {
        public override HorizontalMovementState Update(short playerId, GameTime gameTime, Player player, Layer collisionLayer, Dictionary<HorizontalStateType, HorizontalMovementState> playerStates)
        {
            double elapsedTime = gameTime.ElapsedGameTime.TotalSeconds;

            player.FacingLeft = true;
            player.Speed -= player.WalkForce; // TODO: * elapsedTime;
            player.SpeedX *= player.Friction;

            if (player.IsMovingHorizontally || handleHorizontalCollision(player, collisionLayer, elapsedTime))
                return playerStates[HorizontalStateType.Idle];
            else
                return this;
        }

        public override HorizontalMovementState StoppedMovingLeft(short playerId, Player player, Dictionary<HorizontalStateType, HorizontalMovementState> playerStates)
        {
            OnPlayerStateChanged(playerId, player, UpdatePlayerStateType.Horizontal, (short)HorizontalStateType.StoppingWalkingLeft);

            return playerStates[HorizontalStateType.StoppingWalkingLeft];
        }

        public override HorizontalMovementState MovedRight(short playerId, Player player, Dictionary<HorizontalStateType, HorizontalMovementState> playerStates)
        {
            OnPlayerStateChanged(playerId, player, UpdatePlayerStateType.Horizontal, (short)HorizontalStateType.WalkingRight);

            return playerStates[HorizontalStateType.WalkingRight];
        }
        
        public override string ToString()
        {
            return "WalkingLeft";
        }
    }
}