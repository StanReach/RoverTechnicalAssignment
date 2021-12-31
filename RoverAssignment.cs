using System.Drawing;

namespace RoverAssignment
{
    public class RoverAssignment
    {
        public const int NORTH = 1;
        public const int SOUTH = -1;
        /*
         * Environment class describes the grid enviroment as defined by the input parameters
         * variables must be private and only accessable through Get() functions as the parameters of the environment shouldn't be set after init
         */
        public class Environment
        {
            Dictionary<Point, Component> map;
            int componentCount;
            int gridSize;
            public Environment(string[] inputParams)
            {
                gridSize = Int32.Parse(inputParams[0]);
                componentCount = Int32.Parse(inputParams[1]);
                map = new Dictionary<Point, Component>();

                for (int i = 1; i <= componentCount; i++)
                {
                    map.Add(ParsePointFromString(inputParams[i + 1]), new Component(i));
                }
            }
            public int GetGridSize()
            {
                return gridSize;
            }
            public int GetComponentCount()
            {
                return componentCount;
            }
            public Component GetMapValue(Point key)
            {
                Component objectAtGridPos;

                if (map.TryGetValue(key, out objectAtGridPos))
                {
                    return objectAtGridPos;
                }

                return null;
            }
        }

        public class Vehicle
        {
            public string pathLog;
            public Environment env;
            public Point position;
            public int yDirection;

            public void MoveNorth()
            {
                position.Y += 1;
                pathLog += "N";
            }
            public void MoveEast()
            {
                position.X += 1;
                pathLog += "E";
            }
            public void MoveSouth()
            {
                position.Y -= 1;
                pathLog += "S";
            }
            public void MoveWest()
            {
                position.X -= 1;
                pathLog += "W";
            }

            //Moves the vehicle to any position on the grid
            public void MoveToTarget(Point target)
            {
                while (position.Y > target.Y)
                {
                    MoveSouth();
                }
                while (position.X > target.X)
                {
                    MoveWest();
                }
                while (position.Y < target.Y)
                {
                    MoveNorth();
                }
                while (position.X < target.X)
                {
                    MoveEast();
                }
            }
        }

        /*Rover class defines all the possible action the rover can take*/
        public class Rover : Vehicle
        {
            public int nextComponentToCollect;
            public Dictionary<int, Point> componentsMap;
            public int numOfSeenComp;

            public Rover(Point startingPosition, Environment initialEnv)
            {
                position = startingPosition;
                yDirection = 1;
                env = initialEnv;
                nextComponentToCollect = 1;
                componentsMap = new Dictionary<int, Point>();
                numOfSeenComp = 0;
            }

            public void Start()
            {
                MoveToTarget(new Point(0, 0)); //The rover first moves to the origin to make scanning the environment more simple.
                CheckForComp();
                do
                {
                    if (numOfSeenComp == env.GetComponentCount())
                    {
                        FindPrevSeenComponents();
                    }
                    Move();
                    CheckForComp();
                } while (true);
            }

            /*
            Rover checks if it's current coordinates contain a component if they do. If it is the next one it can collect it does so. Else it
            Stores it's location so it can collect it once it has all preceeding components.
            */
            void CheckForComp()
            {
                Component objectAtGridPos = env.GetMapValue(position);

                if (objectAtGridPos != null)
                {
                    if (objectAtGridPos.GetNumber() == nextComponentToCollect)
                    {
                        CollectComponent();
                    }
                    else
                    {
                        componentsMap.Add(objectAtGridPos.GetNumber(), position);
                    }
                    numOfSeenComp++;
                }
            }
            /*
            Recursive calling of this procedure allows multiple previously seen components to be collected in the correct order consecutively
            */
            void FindPrevSeenComponents()
            {
                Point target;

                if (componentsMap.TryGetValue(nextComponentToCollect, out target))
                {
                    MoveToTarget(target);
                    CollectComponent();
                    FindPrevSeenComponents();
                }
            }

            void CollectComponent()
            {
                pathLog += "P";
                nextComponentToCollect++;

                if (nextComponentToCollect > env.GetComponentCount()) //once all components are collected the exit condition is satisfied and the rover terminates
                {
                    Console.WriteLine(pathLog);
                    System.Environment.Exit(0);
                }
            }

            /*The Rover moves in a zig-zag over the grid so as not to check multiple positions twice*/
            void Move()
            {
                if (((position.Y + yDirection) >= env.GetGridSize()) || ((position.Y + yDirection) < 0))
                {
                    if (position.X + 1 <= env.GetGridSize())
                    {
                        MoveEast();
                        yDirection = -yDirection;
                    }
                }
                else
                {
                    MoveInYDirection(yDirection);
                }
            }

            void MoveInYDirection(int yDirection)
            {
                if (yDirection == SOUTH)
                {
                    MoveSouth();
                }
                if (yDirection == NORTH)
                {
                    MoveNorth();
                }
            }
        }
        public class Component
        {
            int componentNumber;
            public Component(int num)
            {
                componentNumber = num;
            }
            public int GetNumber()
            {
                return componentNumber;
            }
        }
        public static void Main(string[] args)
        {
            SanitizeInput(args);
            Environment env = new Environment(args);
            Rover rover = new Rover(ParsePointFromString(args.Last()), env);
            rover.Start();
        }
        public static Point ParsePointFromString(string arg)
        {
            string[] splitArgs = arg.Split(",");
            if (splitArgs.Length != 2)
            {
                Console.WriteLine("Error: input is not a Coordinate");
                System.Environment.Exit(1);
            }

            return new Point(int.Parse(splitArgs[0]), int.Parse(splitArgs[1]));
        }
        public static void SanitizeInput(string[] args)
        {
            if (args.Length < 4 || args.Length != (Int32.Parse(args[1]) + 3))
            {
                Console.WriteLine("incorrect number of arguments, Terminating program");
                System.Environment.Exit(1);
            }
            if (Int32.Parse(args[0]) <= 0)
            {
                Console.WriteLine("gridSize cannot be 0, Terminating program");
                System.Environment.Exit(1);
            }
        }
    }
}