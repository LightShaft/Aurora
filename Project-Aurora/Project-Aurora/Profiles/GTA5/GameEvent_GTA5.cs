﻿using System;
using System.Collections.Generic;
using Aurora.EffectsEngine;
using Aurora.Profiles.GTA5.GSI;
using System.Drawing;
using Aurora.Settings;
using System.Linq;

namespace Aurora.Profiles.GTA5
{
    public class GameEvent_GTA5 : LightEvent
    {
        public GameEvent_GTA5() : base()
        {
            
        }

        public override void UpdateLights(EffectFrame frame)
        {
            Queue<EffectLayer> layers = new Queue<EffectLayer>();

            GTA5Profile settings = (GTA5Profile)this.Application.Profile;

            foreach (var layer in Application.Profile.Layers.Reverse().ToArray())
            {
                if (layer.Enabled && layer.LogicPass)
                    layers.Enqueue(layer.Render(_game_state));
            }

            //Scripts
            this.Application.UpdateEffectScripts(layers, _game_state);

            frame.AddLayers(layers.ToArray());
        }

        public override void SetGameState(IGameState new_game_state)
        {
            if (new_game_state is GameState_GTA5)
            {
                _game_state = new_game_state;
            }
        }

        public override void ResetGameState()
        {
            _game_state = new GameState_GTA5();
        }
    }
}
