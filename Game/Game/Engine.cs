namespace Game
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using Game.Levels;
    using Game.Interfaces;
    using Game.Data;
    using Game.Tools;
    
    public class Engine
    {
        private const long ElapsedTicks = 10000;
        private IConsoleRenderer renderer; // Console printer
        private IUserInterface userInterface; //event handler
        private GameUnitGenerator unitGenerator; // bonus generator
        public bool inLoop = true;

        private List<MovingUnit> movingObjects; // add moving objects
        private List<GameUnit> staticObjects; //static
        private List<GameUnit> allObjects; //list with all objects on the screne
        private List<Enemy> enemies;
        private Player player; //player
        private Stopwatch stopWatch;

        //constructors
        public Engine(IConsoleRenderer renderer, IUserInterface userInterface, 
            GameUnitGenerator unitGenerator)
        {
            this.renderer = renderer;
            this.userInterface = userInterface;
            this.unitGenerator = unitGenerator;
            this.staticObjects = new List<GameUnit>();
            this.movingObjects = new List<MovingUnit>();
            this.allObjects = new List<GameUnit>();
            this.enemies = new List<Enemy>();
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
            this.player.MaxMovePoint = new Point(renderer.RenderFieldMatrixRows, renderer.RenderFieldMatrixCols);
            GameUnit foundUnit = allObjects.Find(x => x.GetTopLeftCoords().Equals(player.GetTopLeftCoords()));
            allObjects.Remove(foundUnit);
            
            //add the new player
            this.player = obj as Player; 
            this.AddStaticObject(obj);
        }

        //methods
        public virtual void MovePlayerLeft()
        {
            this.player.MoveLeft(); 
        }

        public virtual void MovePlayerRight()
        {
            this.player.MoveRight(); 
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
            IList<MovingUnit> weapon = this.player.Shoot();// get produced weapon
            this.movingObjects.AddRange(weapon); // add it in the list with moving objects
            this.allObjects.AddRange(weapon);
        }

        public virtual void EnemyShoot()
        {
            Random generator = this.unitGenerator.RandomGenerator;
            int randomEnemyIndex = generator.Next(0, this.movingObjects.Count);
            Enemy currEnemy = this.movingObjects[randomEnemyIndex] as Enemy;
            
            while (currEnemy == null)
            {
                randomEnemyIndex = generator.Next(0, this.movingObjects.Count);
                currEnemy = this.movingObjects[randomEnemyIndex] as Enemy;
            }
            MovingUnit enemyShot = currEnemy.Shoot();
            movingObjects.Add(enemyShot);
            allObjects.Add(enemyShot);
        }

        public virtual void Break()
        {
            inLoop = false;
        }

        public virtual void AddObject(GameUnit obj)
        {
            if (obj is Enemy)
            {
                enemies.Add(obj as Enemy);
            }

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
            List<MovingUnit> randomUnit = this.unitGenerator.GenerateStaticUnit(allObjects);
            movingObjects.AddRange(randomUnit);
            allObjects.AddRange(randomUnit);
        }

        public virtual void Run()
        {
            Sounds.SFX(Sounds.SoundEffects.Move);           
            while (inLoop)
            {
                
                this.userInterface.ProcessInput(); // process event from the console
                if (stopWatch.ElapsedMilliseconds > ElapsedTicks)
                {
                    this.GenerateUnit(); //Generate random unit;
                    this.EnemyShoot();
                    this.stopWatch.Restart();
                }
                
                //Print player status
                PrintCurrentStatus();

                //Print all game units and move them if necessary
                foreach (var obj in this.allObjects)
                {
                    this.renderer.ReDraw(obj, true);
                    obj.Move();
                    this.renderer.ReDraw(obj, false);
                }
                try
                {
                    CollisionDispatcher.HandleCollisions(this.movingObjects, this.staticObjects); // handle all collisions  

                    //collect all destroyed objects
                    List<GameUnit> destroyedObjects = allObjects.FindAll(obj => obj.IsDestroyed);
                    var destroyedEnemies = destroyedObjects.FindAll(obj => obj.GetType().Name == "Enemy");
                    this.player.Score += 100 * destroyedEnemies.Count;

                    this.allObjects.RemoveAll(obj => obj.IsDestroyed);
                    this.movingObjects.RemoveAll(obj => obj.IsDestroyed);
                    this.staticObjects.RemoveAll(obj => obj.IsDestroyed);

                    this.renderer.ClearDestroyedObjects(destroyedObjects); // clear all destroyed objects
                }
                catch
                {
                    this.renderer.ClearScreen(); //clearing the screen
                    
                    this.renderer.DrawTextBoxTopLeft("You are dead.",  //printing "You're dead." in a nice box
                        Console.WindowWidth/2 - 6, 
                        Console.WindowHeight/2 - 2, ConsoleColor.Red);
                    
                    Thread.Sleep(1000); //giving the user a second to read that he's dead like he doesn't already know it.
                    
                    this.renderer.ClearScreen(); // clearing the screen

                    this.renderer.DrawTextBoxTopLeft("Try again!", //Print a "Try again!" message
                        Console.WindowWidth / 2 - 5,
                        Console.WindowHeight / 2 - 2, ConsoleColor.Red);
                    
                    Thread.Sleep(1000);
                    
                    this.Break(); //And jump to the main menu
                }

                if (enemies.Count == 0)
                {
                    inLoop = false;
                    renderer.DrawTextBoxTopLeft("You won", 20, 29);
                }

                Thread.Sleep(300);
            }
        }

        public void Initialize(Level level)
        {
            AddPlayer(level.Player);
            List<Enemy> enemies = level.Enemies;

            foreach (var enemy in enemies)
            {
                AddObject(enemy);
            }
        }

        private void PrintCurrentStatus()
        {
            this.renderer.WriteOnPosition("HP: " + (player.HealthPoints) + "%", new Point(1, 1), 5 + player.HealthPoints);
            this.renderer.WriteOnPosition(new string('\u2588', player.HealthPoints / 10),
                new Point(1, 10), 10, ConsoleColor.Red, ConsoleColor.White);
            this.renderer.WriteOnPosition("Inventory: " + this.player.Weapon.Name, new Point(1, 21), 20, ConsoleColor.White);

            this.renderer.WriteOnPosition("Score: " + this.player.Score, new Point(1, 47), 20, ConsoleColor.Yellow);
        }

        public void Reset()
        { 
            movingObjects.Clear();
            staticObjects.Clear();
            allObjects.Clear();            
        }

        public void SetPlayer(Player initionalPlayer)
        {
            AddPlayer(initionalPlayer);
        }
    }
}
