namespace Game
{
    using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
   public class Engine
    {
        private const long ElapsedTicks = 10000;
        private IConsoleRenderer renderer; // Console printer
        private IUserInterface userInterface; //event handler
        private GameUnitGenerator unitGenerator;
        private bool IN_LOOP = true;

        private List<MovingUnit> movingObjects; // add moving objects
        private List<GameUnit> staticObjects; //static
        private List<GameUnit> allObjects; //list with all objects on the screne

        private Player player; //player

        private Stopwatch stopWatch;

        public Engine(IConsoleRenderer renderer, IUserInterface userInterface, GameUnitGenerator unitGenerator)
        {
            this.renderer = renderer;
            this.userInterface = userInterface;
            this.unitGenerator = unitGenerator;
            this.staticObjects = new List<GameUnit>();
            this.movingObjects = new List<MovingUnit>();
            this.allObjects = new List<GameUnit>();
            this.stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        private void AddStaticObject(GameUnit obj)
        {
            this.staticObjects.Add(obj);
            this.allObjects.Add(obj);
        }

        private void AddMovingObject(MovingUnit obj)
        {
            this.movingObjects.Add(obj);
            this.allObjects.Add(obj);
        }
        public void AddPlayer(GameUnit obj)
        {
            //first remove old player from the list
            this.player = obj as Player;
            GameUnit foundUnit = allObjects.Find(x => x.GetTopLeftCoords().Equals(player.GetTopLeftCoords()));
            allObjects.Remove(foundUnit);

            this.player = obj as Player; //add the new player
            this.AddStaticObject(obj);
        }

        public virtual void MovePlayerLeft()
        {
            this.player.MoveLeft(); // move player left
        }

        public virtual void MovePlayerRight()
        {
            this.player.MoveRight(); // move player right
        }

        public virtual void MovePlayerDown()
        {
            this.player.MoveDown();

        }

        public virtual void MovePlayerUp()
        {
            this.player.MoveUp();
        }

        public virtual void PlayerShoot()
        {
            MovingUnit weapon = this.player.Shoot();// get produced weapon
            this.movingObjects.Add(weapon); // add it in the list with moving objects
            this.allObjects.Add(weapon);
        }

        public virtual void Break()
        {
            IN_LOOP = false;
        }

        public virtual void AddObject(GameUnit obj)
        {
            if (obj is MovingUnit)
            {
                this.AddMovingObject(obj as MovingUnit);
            }
            else
            {
                if (obj is Player)
                {
                    AddPlayer(obj);

                }
                else
                {
                    this.AddStaticObject(obj);
                }
            }
        }

        public virtual void GenerateUnit()
        {
            List<GameUnit> randomUnit = this.unitGenerator.GenerateStaticUnit(allObjects);
            allObjects.AddRange(randomUnit);
        }
        public virtual void Run()
        {
            Sounds.SFX(Sounds.SoundEffects.Move);
            while (IN_LOOP)
            {
                this.userInterface.ProcessInput(); // process event from the console
                if (stopWatch.ElapsedMilliseconds > ElapsedTicks)
                {
                    this.GenerateUnit(); //Generate random unit;
                    this.stopWatch.Restart();
                }
                
                //Print health points of the player
                this.renderer.WriteOnPosition("HP: " + (player.HealthPoints / 10)+"%", new Point(1, 0), 8);
                this.renderer.WriteOnPosition(new string('\u2588', player.HealthPoints / 10), 
                    new Point(1, 8), 10, ConsoleColor.Red, ConsoleColor.White);

                //Print all game units and move them if necessary
                foreach (var obj in this.allObjects)
                {
                    this.renderer.ReDraw(obj, true);
                    obj.Move();
                    this.renderer.ReDraw(obj, false);
                }

                CollisionDispatcher.HandleCollisions(this.movingObjects, this.staticObjects); // handle all collisions

                //collect all destroyed objects
                List<GameUnit> destroyedObjects = allObjects.FindAll(obj => obj.IsDestroyed);

                this.allObjects.RemoveAll(obj => obj.IsDestroyed);
                this.movingObjects.RemoveAll(obj => obj.IsDestroyed);
                this.staticObjects.RemoveAll(obj => obj.IsDestroyed);

                this.renderer.ClearDestroyedObjects(destroyedObjects); // clear all destroyed objects

                Thread.Sleep(300);
            }
        }
    }
}
