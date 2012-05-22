using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Barebones.Components;
using Meat.Input;
using Barebones.Dependencies;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace _2dgame.Components
{
    class Eleve : EntityComponent, Barebones.Framework.IUpdateable
    {
		GamepadComponent m_GamePad;
        KeyboardReader m_Keyboard;
        PhysicsComponent m_Physics;

        readonly float m_Speed;

        public Eleve(float speed )
        {
            m_Speed = speed;
        }

        public override IEnumerable<Barebones.Dependencies.IDependency> GetDependencies()
        {
            yield return new Dependency<KeyboardReader>( item => m_Keyboard = item );
            yield return new Dependency<PhysicsComponent>(item => m_Physics = item);
			yield return new Dependency<GamepadComponent>(item => m_GamePad = item);
        }

        protected override void OnOwnerSet()
        {
            Owner.Forum.RegisterListener<CollisionMsg>(OnCollision);
            base.OnOwnerSet();
        }

        public void Update(float dt)
        {
			Vector2 vel = Vector2.Zero;

			if (m_GamePad.isConnected())
			{
				vel = m_GamePad.getLeftThumbStick();
			}
			else
			{
				if (m_Keyboard.IsKeyDown(Keys.D) || m_Keyboard.IsKeyDown(Keys.Right))
					vel.X += m_Speed;

				if (m_Keyboard.IsKeyDown(Keys.A) || m_Keyboard.IsKeyDown(Keys.Left))
					vel.X -= m_Speed;

				if (m_Keyboard.IsKeyDown(Keys.W) || m_Keyboard.IsKeyDown(Keys.Up))
					vel.Y += m_Speed;

				if (m_Keyboard.IsKeyDown(Keys.S) || m_Keyboard.IsKeyDown(Keys.Down))
					vel.Y -= m_Speed;
			}

			m_Physics.LinearVelocity = vel;
            
        }

        void OnCollision(CollisionMsg msg)
        {
            if (msg.First.Name == "Eleve" || msg.Second.Name == "Eleve")
            {
                Game2D.gameHud.lives--;
                if (Game2D.gameHud.lives <= 0)
                {
                    Game2D.gameHud.lives = 0;
                }
            }
            
            //Game2D.gameHud.score++;
        }
    }
}
