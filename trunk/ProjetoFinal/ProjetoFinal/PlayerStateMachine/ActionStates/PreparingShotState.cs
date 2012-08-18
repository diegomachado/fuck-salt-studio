﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ProjetoFinal.Entities;
using OgmoLibrary;
using ProjetoFinal.Network.Messages;
using ProjetoFinal.PlayerStateMachine;

namespace ProjetoFinal.Managers.LocalPlayerStates
{
    class PreparingShotState : ActionState
    {
        public override ActionState Update(short playerId, GameTime gameTime, Player player, Dictionary<ActionStateType, ActionState> playerStates)
        {
            // TODO: Sincronizar animação de atirar com criação e disparo da flecha
            return this;
        }

        public override ActionState ShotReleased(short playerId, Player player, Dictionary<ActionStateType, ActionState> playerStates)
        {
            return playerStates[ActionStateType.Shooting];
        }

        public override string ToString()
        {
            return "Action Shooting";
        }
    }
}