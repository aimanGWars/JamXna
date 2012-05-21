using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Barebones.Framework; // for entity

namespace _2dgame
{
    class AbstractFactory
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // Abstract interfaces
        public interface EntityFactory
        {
            Entity CreateEntity(Engine Owner);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // Factory Products (entities)
        public class DefaultEntity : Entity
        {
            public DefaultEntity()
            {

            }
        }

        public class StudentEntity : Entity
        {
            public StudentEntity()
            {

            }
        }

        public class CameraEntity : Entity
        {
            public CameraEntity()
            {

            }            
        }

        public class QueenEntity : Entity
        {
            public QueenEntity()
            {

            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // Factories (entities' creators)
        public class DefaultFactory : EntityFactory
        {
            Entity EntityFactory.CreateEntity(Engine Owner)
            {
                return Owner.CreateEntity();
            }
        }

        public class StudentFactory : EntityFactory
        {
            Entity EntityFactory.CreateEntity(Engine Owner)
            {
                return Owner.CreateEntity();
            }
        }

        public class CameraFactory : EntityFactory
        {
            Entity EntityFactory.CreateEntity(Engine Owner)
            {
                return Owner.CreateEntity();
            }
        }

        public class QueenFactory : EntityFactory
        {
            Entity EntityFactory.CreateEntity(Engine Owner)
            {
                return Owner.CreateEntity();
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // Factory Creator
        public static EntityFactory CreateSpecificFactory(String FactoryType)
        {
            if (FactoryType == "Student")
            {
                return new StudentFactory();
            }
            else if (FactoryType == "Camera")
            {
                return new CameraFactory();
            }
            else if (FactoryType == "Queen")
            {
                return new QueenFactory();
            }
            else
            {
                return new DefaultFactory();
            }
        }
    }
}
