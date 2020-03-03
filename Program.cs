using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame
{

    enum Direction
    {
        North, South, East, West
    }

    public abstract class MapSite
    {
        public abstract void Enter();
    }

    class Room : MapSite
    {
        int roomNumber = 0;
        Dictionary<Direction, MapSite> sides;

        public Room(int roomNo)
        {
            this.roomNumber = roomNo;
            sides = new Dictionary<Direction, MapSite>(4);
        }

        public override void Enter()
        {
            Console.WriteLine("Room");
        }

        public MapSite GetSide(Direction direction)
        {
            return sides[direction];
        }
        public void SetSide(Direction direction, MapSite mapSite)
        {
            this.sides.Add(direction, mapSite);
        }

        public int RoomNumber
        {
            get { return roomNumber; }
            set { roomNumber = value; }
        }

    }


    class Wall : MapSite
    {
        public Wall()
        {

        }

        public override void Enter()
        {
            Console.WriteLine("Wall");
        }
    }

    class Door : MapSite
    {
        Room room1 = null;
        Room room2 = null;
        bool isOpen;
        
        public Door(Room room1, Room room2)
        {
            this.room1 = room1;
            this.room2 = room2;
        }

        public override void Enter()
        {
            Console.WriteLine("Door");
        }

        public Room OtherSideFrom(Room room)
        {
            if (room == room1)
                return room2;
            else
                return room1;
        }
    }

    class Maze
    {
        Dictionary<int, Room> rooms = null;

        public Maze()
        {
            this.rooms = new Dictionary<int, Room>();
        }

        public void AddRoom(Room room)
        {
            rooms.Add(room.RoomNumber, room);
        }
        public Room RoomNo(int number)
        {
            return rooms[number];
        }
    }

    //class MazeGame
    //{
    //    public Maze CreateMaze()
    //    {
    //        Maze aMaze = new Maze();
    //        Room r1 = new Room(1);
    //        Room r2 = new Room(2);
    //        Door theDoor = new Door(r1, r2);

    //        aMaze.AddRoom(r1);
    //        aMaze.AddRoom(r2);

    //        r1.SetSide(Direction.North, new Wall());            
    //        r1.SetSide(Direction.East, theDoor);
    //        r1.SetSide(Direction.South, new Wall());
    //        r1.SetSide(Direction.West, new Wall());

    //        r2.SetSide(Direction.North, new Wall());
    //        r2.SetSide(Direction.East, new Wall());
    //        r2.SetSide(Direction.South, new Wall());            
    //        r2.SetSide(Direction.West, theDoor);

    //        return aMaze;
    //    }
    //}

    //--------------------------------------------------- AbstractFactory

    class MazeFactory
    {
        public virtual Maze MakeMaze()
        {
            return new Maze();
        }

        public virtual Wall MakeWall()
        {
            return new Wall();
        }

        public virtual Room MakeRoom(int number)
        {
            return new Room(number);
        }

        public virtual Door MakeDoor(Room room1, Room room2)
        {
            return new Door(room1, room2);
        }
    }

    //class MazeGame
    //{
    //    MazeFactory factory = null;

    //    public Maze CreateMaze(MazeFactory factory)
    //    {
    //        this.factory = factory;
    //        Maze aMaze = this.factory.MakeMaze();
    //        Room r1 = this.factory.MakeRoom(1);
    //        Room r2 = this.factory.MakeRoom(2);
    //        Door aDoor = this.factory.MakeDoor(r1, r2);

    //        aMaze.AddRoom(r1);
    //        aMaze.AddRoom(r2);

    //        r1.SetSide(Direction.North, this.factory.MakeWall());
    //        r1.SetSide(Direction.East, aDoor);
    //        r1.SetSide(Direction.South, this.factory.MakeWall());
    //        r1.SetSide(Direction.West, this.factory.MakeWall());

    //        r2.SetSide(Direction.North, this.factory.MakeWall());
    //        r2.SetSide(Direction.East, this.factory.MakeWall());
    //        r2.SetSide(Direction.South, this.factory.MakeWall());
    //        r2.SetSide(Direction.West, aDoor);

    //        return aMaze;
    //    }

    //}


    //--------------------------------------------------- Builder

    abstract class MazeBuilder
    {
        public abstract void BuildMaze();
        public abstract void BuildRoom(int roomNo);
        public abstract void BuildDoor(int roomFrom, int roomTo);
        public abstract Maze GetMaze();

    }

    //class MazeGame
    //{
    //    public Maze CreateMaze(MazeBuilder builder)
    //    {
    //        builder.BuildMaze();
    //        builder.BuildRoom(1);
    //        builder.BuildRoom(2);
    //        builder.BuildDoor(1, 2);

    //        return builder.GetMaze();
    //    }

    //    public Maze CreateComplexMaze(MazeBuilder builder)
    //    {
    //        // create 1001 room
    //        for (int i = 0; i < 1001; i++)
    //        {
    //            builder.BuildRoom(i);
    //        }
    //        return builder.GetMaze();
    //    }
    //}

    class StandardMazeBuilder : MazeBuilder
    {
        Maze currentMaze = null;

        public StandardMazeBuilder()
        {
            this.currentMaze = null;
        }

        public override void BuildMaze()
        {
            this.currentMaze = new Maze();
        }

        public override void BuildRoom(int roomNo)
        {
            Room room = new Room(roomNo);
            currentMaze.AddRoom(room);

            room.SetSide(Direction.North, new Wall());
            room.SetSide(Direction.East, new Wall());
            room.SetSide(Direction.South, new Wall());
            room.SetSide(Direction.West, new Wall());
        }



        public override void BuildDoor(int roomFrom, int roomTo)
        {
            Room room1 = currentMaze.RoomNo(roomFrom);
            Room room2 = currentMaze.RoomNo(roomTo);
            Door door = new Door(room1, room2);

            room1.SetSide(CommonWall(room1, room2), door);
            room2.SetSide(CommonWall(room1, room2), door);
        }

        private Direction CommonWall(Room room1, Room room2)
        {
            if (room1.GetSide(Direction.North) is Wall &&
                room1.GetSide(Direction.South) is Wall &&
                room1.GetSide(Direction.East) is Wall &&
                room1.GetSide(Direction.West) is Wall && 
                room2.GetSide(Direction.North) is Wall &&
                room2.GetSide(Direction.South) is Wall &&
                room2.GetSide(Direction.East) is Wall &&
                room2.GetSide(Direction.West) is Wall)
            {
                return Direction.East;
            }
            else
            {
                return Direction.West;
            }
        }

        public override Maze GetMaze()
        {
            return this.currentMaze;
        }
    }

    //--------------------------------------------------- FactoryMethod

    class MazeGame
    {
        public Maze CreateMaze()
        {
            Maze aMaze = this.MakeMaze();

            Room r1 = MakeRoom(1);
            Room r2 = MakeRoom(2);
            Door theDoor = MakeDoor(r1, r2);

            aMaze.AddRoom(r1);
            aMaze.AddRoom(r2);

            r1.SetSide(Direction.North, MakeWall());
            r1.SetSide(Direction.East, theDoor);
            r1.SetSide(Direction.South, MakeWall());
            r1.SetSide(Direction.West, MakeWall());
            r2.SetSide(Direction.North, MakeWall());
            r2.SetSide(Direction.East, MakeWall());
            r2.SetSide(Direction.South, MakeWall());
            r2.SetSide(Direction.West, theDoor);

            return aMaze;

        }

        public virtual Maze MakeMaze()
        {
            return new Maze();
        }

        public virtual Room MakeRoom(int number)
        {
            return new Room(number);
        }

        public virtual Wall MakeWall()
        {
            return new Wall();
        }

        public virtual Door MakeDoor(Room room1, Room room2)
        {
            return new Door(room1, room2);
        }

    }

    class Spell
    {
        public Spell()
        {
            Console.WriteLine("Spell...");
        }
    }

    class EnchantedRoom : Room
    {
        private Spell spell = null;
        public EnchantedRoom(int roomNo) : base(roomNo)
        {

        }

        public EnchantedRoom(int roomNo, Spell spell) : base(roomNo)
        {
            this.spell = spell;
        }
    }

    class DoorNeedingSpell : Door
    {
        public DoorNeedingSpell(Room room1, Room room2) : base(room1, room2)
        {

        }
    }

    class RoomWithBomb : Room
    {
        public RoomWithBomb(int roomNo) : base(roomNo)
        {

        }
    }

    class BombedWall : Wall
    {

    }

    class EnchantedMazeGame : MazeGame
    {
        public EnchantedMazeGame()
        {

        }

        public override Room MakeRoom(int number)
        {
            return new EnchantedRoom(number, this.CastSpell());
        }
        public override Door MakeDoor(Room room1, Room room2)
        {
            return new DoorNeedingSpell(room1, room2);
        }
        protected Spell CastSpell()
        {
            return new Spell();
        }
    }    

    class BombedMazeGame : MazeGame
    {
        public BombedMazeGame()
        {

        }

        public override Wall MakeWall()
        {
            return new BombedWall();
        }
        public override Room MakeRoom(int number)
        {
            return new RoomWithBomb(number);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
        }

    }
}
