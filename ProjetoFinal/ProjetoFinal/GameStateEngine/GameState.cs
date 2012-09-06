﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using ProjetoFinal.Managers;

namespace ProjetoFinal.GameStateEngine
{
    public enum GameStateState
    {
        TransitionOn,
        Active,
        TransitionOff,
        Hidden,
    }
    
    abstract class GameState
    {
        GameStateState state = GameStateState.TransitionOn;

        protected InputManager inputManager = InputManager.Instance;
        protected GraphicsManager graphicsManager = GraphicsManager.Instance;
        protected TextureManager textureManager = TextureManager.Instance;
        protected EventManager eventManager = EventManager.Instance;
        
        public GameStatesManager GameStatesManager { protected get; set; }
        public GameStateState State
        {
            get { return state; }
            set { state = value; }
        }

        public bool IsPopup { get; protected set; }

        // TODO: Adicionar Método Initialize

        public virtual void LoadContent(ContentManager content) { }

        public virtual void Update(GameTime gameTime/*, bool otherScreenHasFocus, bool coveredByOtherScreen*/) { }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            float frameRate;
            frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
            spriteBatch.DrawString(spriteFont, "FPS: " + Math.Round(frameRate), new Vector2(graphicsManager.ScreenSize.X - 70, 5), Color.White);
        }

        public virtual void UnloadContent() { }
    }
}