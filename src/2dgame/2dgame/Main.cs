using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Barebones.Components;
using Meat.Resources;
using Barebones.Dependencies;
using Microsoft.Xna.Framework;
using Barebones.Xna;
using Meat.Rendering;
using Meat.Resources.Factory;
using Barebones.Framework;
using _2dgame.Components;
using Microsoft.Xna.Framework.Graphics;
using _2dgame.EngineComponents;
using Meat.Input;
using Microsoft.Xna.Framework.Audio;

namespace _2dgame
{
    class Main : EngineComponent
    {
        static readonly Vector2 DESIRED_SCREEN_SIZE = new Vector2(1440, 900);
        const float IMAGE_SCALE = 0.01f;

        Random m_Rand = new Random();
        EZBake m_EZBakeOven;
        Physics m_Physics;

        public override IEnumerable<Barebones.Dependencies.IDependency> GetDependencies()
        {
            yield break;
        }

        /// <summary>
        /// Pour mieux commencer veuillez commencer par regarder la methode OnInitialise un peu plus bas pour voir
        /// comment créer ce qu'il y a l'écran.
        /// </summary>
        protected override void OnOwnerSet()
        {
            Owner.AddComponent(new ResourceLoader());

            m_EZBakeOven = new EZBake();
            Owner.AddComponent(m_EZBakeOven);

            //create renderer
            //create the projection matrix
            Matrix projection = Matrix.CreateOrthographic(IMAGE_SCALE * DESIRED_SCREEN_SIZE.X, IMAGE_SCALE * DESIRED_SCREEN_SIZE.Y, -1, 1);
            Owner.AddComponent( new RawRenderer(projection, Color.Black) );

            Vector2 minWorld = -0.5f * IMAGE_SCALE * DESIRED_SCREEN_SIZE;
            Vector2 maxWorld = 0.5f * IMAGE_SCALE * DESIRED_SCREEN_SIZE;

            m_Physics = new Physics(Vector2.Zero, minWorld, maxWorld)
                {
                    DebugView = false
                };
            Owner.AddComponent(m_Physics);

            Owner.AddComponent(new CameraMan());

            Owner.AddComponent(new TouchReader());
            Owner.AddComponent(new KeyboardReader());

            Owner.Forum.RegisterListener<InitializeMessage>(OnInitialise);
            Owner.Forum.RegisterListener<CreatedMessage>(OnCreated);

            base.OnOwnerSet();
        }

        void OnCreated(CreatedMessage msg)
        {
            msg.Manager.PreferredBackBufferWidth = (int)DESIRED_SCREEN_SIZE.X;
            msg.Manager.PreferredBackBufferHeight = (int)DESIRED_SCREEN_SIZE.Y;

            msg.Manager.SupportedOrientations = DisplayOrientation.LandscapeLeft |
                                                DisplayOrientation.LandscapeRight;
        }

        //this is like the loading phase, it will let us use these items as handles on entities later on
        void RegisterResources()
        {
            ResourceLoader loader = Owner.GetComponent<ResourceLoader>();
            loader.AddResource(new ContentResource<SoundEffect>("laugh"));
            loader.AddResource(new ContentResource<SoundEffect>("fanfare"));
        }

        void OnInitialise(InitializeMessage msg)
        {
            RegisterResources();
            
            //add the camera to view the scene
            Entity camera = Owner.CreateEntity();
            camera.AddComponent(new Camera());

            camera.Transform = Matrix.CreateWorld(new Vector3(0, -1, 1), -Vector3.UnitZ, Vector3.UnitY);

			Entity background = Owner.CreateEntity();
			Vector2 backsize = IMAGE_SCALE * DESIRED_SCREEN_SIZE;
			m_EZBakeOven.MakeParallaxSprite(background, backsize, "Background", 0.9f);

			//create carré rouge
			Entity eleve = Owner.CreateEntity();
			camera.AddComponent(new FollowEntity(eleve, 0.5f * Vector3.UnitY, false, false));
			eleve.AddComponent(new GamepadComponent());
			Vector2 bodySize = IMAGE_SCALE * new Vector2(50, 50);
			m_EZBakeOven.MakeSprite(eleve, bodySize, "Eleve", 4, 10);
			eleve.AddComponent(m_Physics.CreateRectangle(0.5f * bodySize, 1.0f, FarseerPhysics.Dynamics.BodyType.Dynamic));
			eleve.AddComponent(new Eleve(1f));


            ResourceLoader loader = Owner.GetComponent<ResourceLoader>();
            loader.ForceLoadAll();

            // enter: The Queen!
            loader.GetResource("fanfare").Get<SoundEffect>().Play();
        }
    }
}
