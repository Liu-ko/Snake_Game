using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    /// <summary>
    /// Holds the main object of the game. It has 2 Joins: tail and head for iteration
    /// </summary>
    class Snake
    {
        private Join head;
        private Join tail;
        private int length;

        /// <summary>
        /// Basic constructor without variables
        /// </summary>
        public Snake()
        {
            head = null;
            tail = null;
            length = 0;
        }

        /// <summary>
        /// Constructor that receives coordinates for the first bone. Set head and tail to the same bone
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">y coordinate</param>
        public Snake(int x, int y)
        {
            Join j = new Join(new Bone(6, x, y));
            head = j;
            tail = j;
            length = 1;
        }

        /// <summary>
        /// Method to get Snake's head
        /// </summary>
        /// <returns>Join - head of the snake</returns>
        public Join getHead()
        {
            return head;
        }

        /// <summary>
        /// Method to get Snake's tail
        /// </summary>
        /// <returns>Join - tail of the snake</returns>
        public Join getTail()
        {
            return tail;
        }

        /// <summary>
        /// Method to get snake's length
        /// </summary>
        /// <returns>int - snake's length</returns>
        public int getLength()
        {
            return length;
        }

        /// <summary>
        /// Method to set snake's head
        /// </summary>
        /// <param name="h">The first, head Join</param>
        public void setHead(Join h)
        {
            head = h;
        }

        /// <summary>
        /// Method to set snake's tail
        /// </summary>
        /// <param name="h">The last, tail Join</param>
        public void setTail(Join t)
        {
            tail = t;
        }

        /// <summary>
        /// Method that adds new Join to snake's tail
        /// </summary>
        /// <param name="j">Join that should be added</param>
        public void addJoinToTail(Join j)
        {
            if (j != null)
            {
                tail.setNext(j);
                j.setPrev(tail);
                tail = j;
                length++;
            }
        }

        /// <summary>
        /// Method that adds new Bone to snake's tail
        /// </summary>
        /// <param name="b">Bone that should be added</param>
        public void addBoneToTail(Bone b)
        {
            if (b != null)
            {
                Join j = new Join(b, tail, null);
                tail.setNext(j);
                tail = j;
                length++;
            }
        }

        /// <summary>
        /// Method that moves snake's head by adding new Join to it. Snake is following received direction
        /// </summary>
        /// <param name="direction">direction in whitch snake is moving</param>
        /// <returns></returns>
        public Coordinates moveHead(int direction)
        {
            //creates next bone and connects it before snake's head. That imitates that head was moved
            Bone newBone = new Bone(direction, head.getBone().getCoord().getX(), head.getBone().getCoord().getY());
            Join j = new Join(newBone, null, head);
            head.setPrev(j);
            //direction of ex-head should be changed. 8-up, 2-down, 4-left, 6-right. 1,7,3,9 - corners. 
            //use numeric keypad to visualise direction better
            int z = head.getBone().getDirection();
            if (direction != z)
            {//if direction of nex head and ex-head are different, then change diretion of ex-head to hold corner
                if (direction == 8)
                {
                    head.getBone().setDirection(z == 4 ? 1 : 3);
                    j.getBone().getCoord().setY(newBone.getCoord().getY() - 1);
                }
                else if (direction == 6)
                {
                    head.getBone().setDirection(z == 8 ? 7 : 1);
                    j.getBone().getCoord().setX(newBone.getCoord().getX() + 1);
                }
                else if (direction == 2)
                {
                    head.getBone().setDirection(z == 4 ? 7 : 9);
                    j.getBone().getCoord().setY(newBone.getCoord().getY() + 1);
                }
                else if (direction == 4)
                {
                    head.getBone().setDirection(z == 8 ? 9 : 3);
                    j.getBone().getCoord().setX(newBone.getCoord().getX() - 1);
                }
            }
            else switch (direction)
                {// correction of coordinates of a new head
                    case 2: j.getBone().getCoord().setY(newBone.getCoord().getY() + 1); break;
                    case 4: j.getBone().getCoord().setX(newBone.getCoord().getX() - 1); break;
                    case 6: j.getBone().getCoord().setX(newBone.getCoord().getX() + 1); break;
                    case 8: j.getBone().getCoord().setY(newBone.getCoord().getY() - 1); break;
                }
            setHead(j);
            length++;
            return head.getBone().getCoord();
        }

        /// <summary>
        /// Method that moves snake's tail by deleting the last bone
        /// </summary>
        /// <returns>Coordinates of the last bone that was deleted</returns>
        public Coordinates moveTail()
        {
            int z = tail.getBone().getDirection(); // direction last bone follow
            Coordinates last = tail.getBone().getCoord();
            setTail(tail.getPrev());
            tail.setNext(null);
            int d = tail.getBone().getDirection(); // direction snake moves
            if (d != z)
            {//if directioin of ex-tail and new tail are different, direction of new tail should be changed, as it used to hold corner
                if (d == 9)
                    tail.getBone().setDirection(z == 6 ? 2 : 4);
                else if (d == 7)
                    tail.getBone().setDirection(z == 8 ? 6 : 2);
                else if (d == 3)
                    tail.getBone().setDirection(z == 2 ? 4 : 8);
                else if (d == 1)
                    tail.getBone().setDirection(z == 4 ? 8 : 6);
            }
            length--;
            return last;
        }
    }
}
