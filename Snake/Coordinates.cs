using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    /// <summary>
    /// This class is a combination of 2 integer numbers: X and Y coordinates - that define position of an object on the grid
    /// </summary>
    public class Coordinates
    {
        private int x, y;

        /// <summary>
        /// Basic constructor without parameters
        /// </summary>
        public Coordinates()
        {
            x = 0;
            y = 0;
        }

        /// <summary>
        /// Constructor with 2 parameters
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Coordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Method to get X coordinate
        /// </summary>
        /// <returns>int - X coordinate</returns>
        public int getX()
        {
            return x;
        }

        /// <summary>
        /// Method to get Y coordinate
        /// </summary>
        /// <returns>int - Y coordinate</returns>
        public int getY()
        {
            return y;
        }

        /// <summary>
        /// Method to set both coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public void setCoord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Method to set X coordinate
        /// </summary>
        /// <param name="x">X coordinate</param>
        public void setX(int x)
        {
            this.x = x;
        }

        /// <summary>
        /// Method to setY coordinate
        /// </summary>
        /// <param name="y">Y coordinate</param>
        public void setY(int y)
        {
            this.y = y;
        }

        /// <summary>
        /// Method to check if two objects of Coordinates class point to the same position, i.e. has the same coordinates
        /// </summary>
        /// <param name="c">Coordinates object for comparison</param>
        /// <returns>true if objects points to the same coordinates</returns>
        public Boolean equal(Coordinates c)
        {
            return (c.getX() == x && c.getY() == y);
        }

        /// <summary>
        /// Methoc that checks if Coordinates stays in the ranges. 0 <= X < 15, 0 <= Y < 10
        /// </summary>
        /// <returns>true if both coordinates stay in ranges</returns>
        public Boolean inRange()
        {
            return (x >= 0 && x < 15 && y >= 0 && y < 10);
        }
    }
}
