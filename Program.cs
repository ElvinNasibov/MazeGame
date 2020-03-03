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

    class MazeGame
    {
        public Maze CreateMaze()
        {
            Maze aMaze = new Maze();
            Room r1 = new Room(1);
            Room r2 = new Room(2);
            Door theDoor = new Door(r1, r2);

            aMaze.AddRoom(r1);
            aMaze.AddRoom(r2);

            r1.SetSide(Direction.North, new Wall());            
            r1.SetSide(Direction.East, theDoor);
            r1.SetSide(Direction.South, new Wall());
            r1.SetSide(Direction.West, new Wall());

            r1.SetSide(Direction.North, new Wall());
            r1.SetSide(Direction.East, new Wall());
            r1.SetSide(Direction.South, new Wall());            
            r1.SetSide(Direction.West, theDoor);

            return aMaze;
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
