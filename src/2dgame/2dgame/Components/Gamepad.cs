using System;
using System.Collections.Generic;
using System.Text;

using Barebones.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _2dgame.Components
{
	class GamepadComponent : EntityComponent
	{
		private PlayerIndex m_Player = PlayerIndex.One;

		public bool isConnected()
		{
			GamePadState state = GamePad.GetState(m_Player);
			return state.IsConnected;
		}

		public Vector2 getLeftThumbStick()
		{
			GamePadState state = GamePad.GetState( m_Player );
			return state.ThumbSticks.Left;
		}

		public Vector2 getRightThumbStick()
		{
			GamePadState state = GamePad.GetState(m_Player);
			return state.ThumbSticks.Right;
		}

		public override IEnumerable<Barebones.Dependencies.IDependency> GetDependencies()
		{
			yield break;
		}
	}
}
